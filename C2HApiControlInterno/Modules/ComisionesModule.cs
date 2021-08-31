using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models;
using Models.Comisiones;
using Models.Empleados;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class ComisionesModule : NancyModule
    {
        private readonly DAComisiones _DAComisiones = null;
        public ComisionesModule() : base("/comisiones")
        {
            //this.RequiresAuthentication();
            _DAComisiones = new DAComisiones();
            Get("/obtenerEmpleadosComisiones", _ => ObtenerEmpleadosConComisiones());
            Get("/tiposEmpleado", _ => ObtenerTiposDeEmpleados());
            Post("/rpt-comisiones", _ => ObtenerPdfNotaRemision());
            Post("/obtenerComisiones", _ => ObtenerComisiones());
            Post("/asignar-comisiones", _ => AsignarComisiones());
            Post("/obtenerComisionesPorEmpleado", _ => ObtenerComisionesPorEmpleado());
            Post("/guardar-comisiones", _ => GuardarComisionesEmpleado());
        }

        
        private object ObtenerTiposDeEmpleados()
        {
            var r = new Result<List<TipoEmpleado>>();
            try
            {
              
                r = _DAComisiones.ObtenerTipoEmpleado();
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
                return Response.AsJson(r);
            }
            return Response.AsJson(r);
        }
        private object AsignarComisiones()
        {
            var r = new Result();
            try
            {
                List<ComisionesXEmpleadoModel> lstComisiones = new List<ComisionesXEmpleadoModel>();
                var parametro = this.BindModel();

                var comisiones = parametro.data.comisiones;

                foreach (var element in comisiones)
                {
                    ComisionesXEmpleadoModel comision = new ComisionesXEmpleadoModel();
                    comision.Codigo = element.codigo;
                    comision.CodTipoComision = element.codTipoComision;
                    comision.Monto = element.monto;
                    comision.Seleccionado = element.seleccionado;
                    lstComisiones.Add(comision);
                }
                int codEmpleado = parametro.data.codEmpleado;
                DateTime fechaComision = parametro.data.fechaComision;
                r = _DAComisiones.AsignarComisionEmpleado(lstComisiones, codEmpleado, fechaComision);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
                return Response.AsJson(r);
            }
            return Response.AsJson(r);
        }

        private object ObtenerPdfNotaRemision()
        {
            Result result = new Result();
            var parametro = this.BindModel();

            DateTime fechaIni = parametro.fechaInicial;
            DateTime fechaFin = parametro.fechaFinal;
            int tipoEmpleado = parametro.tipoEmpleado;
            //var reporteEntradasSalidas = this.Bind<List<ReporteComisionesModel>>();
            var r = _DAComisiones.ObtenerDatosComisiones(fechaIni, fechaFin, tipoEmpleado);

            var reporteEntradasSalidas = r.Data;

            var totalGral = reporteEntradasSalidas.Sum(s => s.Subtotal);

            var pathdirectorio = Globales.FolderPDF;
            //var pathdirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
            if (!Directory.Exists(pathdirectorio))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathdirectorio);
            }

            var path = HttpRuntime.AppDomainAppPath;
            //string rutapdf = "c:\\pruebaprueba\\prueba.pdf";
            //string rutapdf = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA\\prueba.pdf";
            string rutaPdf = $"{ Globales.FolderPDF}\\prueba.pdf";
            string pdfbase64 = "";
            byte[] bytes;
            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\reportes\\RptComisiones.rpt");

            reporte.SetDataSource(reporteEntradasSalidas);
            reporte.SetParameterValue("fechaDesde", fechaIni);
            reporte.SetParameterValue("fechaHasta", fechaFin);
            reporte.SetParameterValue("tipoEmpleado", tipoEmpleado);
            reporte.SetParameterValue("totalGral", totalGral);
            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

            bytes = File.ReadAllBytes(rutaPdf);
            pdfbase64 = Convert.ToBase64String(bytes);
            result.Data = pdfbase64;
            result.Value = true;
            File.Delete(rutaPdf);


            return Response.AsJson(result);
        }

        private object GuardarComisionesEmpleado()
        {
            var r = new Result();
            try
            {
                List<ComisionesXEmpleadoModel> lstComisiones = new List<ComisionesXEmpleadoModel>();
                var parametro = this.BindModel();
                
                var comisiones = parametro.data.comisiones;

                foreach (var element in comisiones)
                {
                    ComisionesXEmpleadoModel comision = new ComisionesXEmpleadoModel();
                    comision.Codigo = element.codigo;
                    comision.CodTipoComision = element.codTipoComision;
                    comision.Monto = element.monto;
                    lstComisiones.Add(comision);
                }
                int codEmpleado = parametro.data.codEmpleado;
                DateTime fechaComision = parametro.data.fechaComision;
                r =  _DAComisiones.GuardarComisionesEmpleado(lstComisiones, codEmpleado, fechaComision);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
                return Response.AsJson(r);
            }
            return Response.AsJson(r);
        }

        private object ObtenerComisiones()
        {
            Result<List<ComisionModel>> result = new Result<List<ComisionModel>>();
            try
            {
                var parametro = this.BindModel();
                int codTipoEmpleado = parametro.codTipoEmpleado;
                result = _DAComisiones.ObtenerComisiones(codTipoEmpleado);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerEmpleadosConComisiones()
        {
            Result<List<EmpleadosComisionModel>> result = new Result<List<EmpleadosComisionModel>>();
            try
            {
                result = _DAComisiones.ObtenerEmpleadosConComisiones();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerComisionesPorEmpleado()
        {
            Result<List<ComisionesXEmpleadoModel>> result = new Result<List<ComisionesXEmpleadoModel>>();
            try
            {
                var parametro = this.BindModel();
                int codEmpleado = parametro.codEmpleado;
                DateTime diaComision = parametro.diaComision;
                result = _DAComisiones.ObtenerComisionesPorEmpleado(codEmpleado, diaComision);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}