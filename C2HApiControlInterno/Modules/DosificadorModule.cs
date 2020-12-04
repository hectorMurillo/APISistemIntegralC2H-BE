using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Dosificador;
using Models.Equipos;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class DosificadorModule:NancyModule
    {

        DADosificador _DADosificador = new DADosificador();
        public DosificadorModule() : base("/dosificador")
        {
            Get("/formulas", _ => GetFormulas());
            Get("/ultimo-folio-ginco/", _ => GetUltimoFolioGinco());
            Get("/obras-clientes/{codCliente}", parametros => GetObrasCliente(parametros));
            Get("/operadores/{bombeable}", parametros => GetOperadores(parametros));
            Get("/equipo-operador/{codOperador}/{esBombeable}", parametros => GetEquipoOperador(parametros));

            Post("notaRemision/guardar", _ => PostGuardarNotaRemision());
            Post("formula/guardar", _ => PostGuardarFormulaProducto());
        }

        private object GetUltimoFolioGinco()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                result = _DADosificador.ObtenerUltimoFolioGinco();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object PostGuardarFormulaProducto()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var formula = this.Bind<FormulaModel>();
                result = _DADosificador.GuardarFormula(formula);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object PostGuardarNotaRemision()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var notaRemision = this.Bind<NotaRemisionEncModel>();
                //result = _DADosificador.GuardarNotaRemision(notaRemision, codUsuario);
                //if (result.Value)
                //{


                //}
                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\prueba.pdf";
                string pdfBase64 = "";
                Byte[] bytes;

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\rptNota.rpt");
                reporte.SetParameterValue("@Folio", 10000);
                reporte.SetParameterValue("@Cliente", notaRemision.CodCliente);
                reporte.SetParameterValue("@Obra", notaRemision.CodObra);
                reporte.SetParameterValue("@Producto", notaRemision.Producto);
                reporte.SetParameterValue("@Cantidad", notaRemision.Cantidad);
                reporte.SetParameterValue("@Operador", notaRemision.CodOperador_1);
                reporte.SetParameterValue("@Equipo", notaRemision.CodEquipo_BB);
                reporte.SetParameterValue("@Vendedor", notaRemision.codVendedor);

                //reporte.SetDataSource();
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                bytes = File.ReadAllBytes(rutaPdf);
                pdfBase64 = Convert.ToBase64String(bytes);
                //Result.Data = pdfBase64;
                //File.Delete(rutaPdf);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object GetFormulas()
        {
            Result<List<FormulaModel>> result = new Result<List<FormulaModel>>();
            try
            {
                result = _DADosificador.ObtenerFormulas();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetObrasCliente(dynamic parametros)
        {
            Result<List<ObrasModel>> result = new Result<List<ObrasModel>>();
            try
            {
                int codCliente = parametros.codCliente;
                result = _DADosificador.ObtenerObrasClientes(codCliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object GetOperadores(dynamic parametros)
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                bool esBombeable = parametros.bombeable;
                result = _DADosificador.ObtenerOperadores(esBombeable);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetEquipoOperador(dynamic parametros)
        {
            Result<List<EquipoModel>> result = new Result<List<EquipoModel>>();
            try
            {
                bool bombeable = parametros.esBombeable;
                int codOperador = parametros.codOperador;
                result = _DADosificador.ObtenerEquipoOperador(bombeable,codOperador);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}