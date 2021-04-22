using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Ventas;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class VentasModule : NancyModule
    {
        private readonly DAVentas _DAVentas = null;

        public VentasModule() : base("/reportes")
        {
            this.RequiresAuthentication();
            _DAVentas = new DAVentas();

            Post("/reporte-mensual-metros/{fechaDesde}/{fechaHasta}", x => ReporteMensualMetrosCubicos(x));
            Get("/reporte-mensual-clientes/{fechaDesde}/{fechaHasta}/{agente}", x => ReporteMensualClientes(x));
            Post("/reporte-volumen-obra/{fechaDesde}/{fechaHasta}", x => ReporteMensualVolumenXObras(x));
            Post("/reporte-mensual-productos/{fechaDesde}/{fechaHasta}", x => ReporteMensualProductos(x));
            Get("/obtener-demanda-articulos/", _ => ObtenerDemandaArticulo());


        }

        private object ReporteMensualMetrosCubicos(dynamic x)
        {
            Result result = new Result();

            var reporteMensualMetros = this.Bind<RptMensualMetrosModel>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            try
            {
                var r = new Result<List<RptMensualMetros>>();
                r = _DAVentas.ReporteMensualMetrosCubicos(reporteMensualMetros, fechaDesde, fechaHasta);

                if (r.Value)
                {
                    var totalMetrosVendidos = r.Data.Sum(s => s.Cantidad);
                    var path = HttpRuntime.AppDomainAppPath;
                    string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualMetrosTEST.pdf";
                    string pdfBase64 = "";
                    Byte[] bytes;

                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(path + "\\Reportes\\RptMensualMetros.rpt");
                    reporte.SetDataSource(r.Data);
                    reporte.SetParameterValue("fechaDesde", fechaDesde);
                    reporte.SetParameterValue("fechaHasta", fechaHasta);
                    reporte.SetParameterValue("totalMetrosVendidos", totalMetrosVendidos);
                    reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                    bytes = File.ReadAllBytes(rutaPdf);
                    pdfBase64 = Convert.ToBase64String(bytes);
                    result.Data = pdfBase64;
                    result.Value = r.Value;
                    File.Delete(rutaPdf);
                }
                else
                {
                    result.Value = false;
                    result.Message = r.Message;
                }
            }
            catch (Exception ex)
            {
                result.Value = false;
                result.Message = ex.Message;
            }

            return Response.AsJson(result);
            
        }

        private object ReporteMensualClientes(dynamic x)
        {
            Result result = new Result();

            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;
            int agente = x.agente;

            var r = new Result<List<RptMensualClientes>>();
            r = _DAVentas.ReporteMensualClientes(fechaDesde, fechaHasta, agente);

            if (r.Value)
            {
                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualClientesTEST.pdf";
                string pdfBase64 = "";
                Byte[] bytes;
                //var totalClientes = r.Data.GroupBy(a => a.CodCliente).Count;
                int total = r.Data.GroupBy(i => i.CodCliente).Select(group => group.First()).Count();

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\RptMensualClientes.rpt");
                reporte.SetDataSource(r.Data);
                reporte.SetParameterValue("fechaDesde", fechaDesde);
                reporte.SetParameterValue("fechaHasta", fechaHasta);
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                bytes = File.ReadAllBytes(rutaPdf);
                pdfBase64 = Convert.ToBase64String(bytes);
                result.Data = pdfBase64;
                result.Value = r.Value;
                File.Delete(rutaPdf);
            }
            else {
                result.Value = false;
                result.Message = r.Message;
            }

            return Response.AsJson(result);

        }

        private object ReporteMensualVolumenXObras(dynamic x)
        {
            Result result = new Result();

            var reporteVolumenXObras = this.Bind<RptReporteVolumenObras>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptMensualMetros>>();
            r = _DAVentas.ReporteVolumenXObras(reporteVolumenXObras, fechaDesde, fechaHasta);

            if (r.Value)
            {
                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\RptVolumenXObraTEST.pdf";
                string pdfBase64 = "";
                Byte[] bytes;

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\RptVolumenXObra.rpt");
                reporte.SetDataSource(r.Data);
                reporte.SetParameterValue("fechaDesde", fechaDesde);
                reporte.SetParameterValue("fechaHasta", fechaHasta);
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                bytes = File.ReadAllBytes(rutaPdf);
                pdfBase64 = Convert.ToBase64String(bytes);
                result.Data = pdfBase64;
                result.Value = r.Value;
                File.Delete(rutaPdf);
            }
            else
            {
                result.Value = false;
                result.Message = r.Message;
            }

            return Response.AsJson(result);

        }


        private object ReporteMensualProductos(dynamic x)
        {
            Result result = new Result();

            var reporteProductos = this.Bind<RptMensualProductos>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            try
            {
                var r = new Result<List<RptMensualProductos>>();
                r = _DAVentas.ReporteMensualProductos(reporteProductos, fechaDesde, fechaHasta);
                if (r.Value)
                {
                    var path = HttpRuntime.AppDomainAppPath;
                    string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualProductosTEST.pdf";
                    string pdfBase64 = "";
                    Byte[] bytes;

                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(path + "\\Reportes\\RptMensualProductos.rpt");
                    reporte.SetDataSource(r.Data);
                    reporte.SetParameterValue("fechaDesde", fechaDesde);
                    reporte.SetParameterValue("fechaHasta", fechaHasta);
                    reporte.SetParameterValue("agente", r.Data[0].agente);

                    reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                    bytes = File.ReadAllBytes(rutaPdf);
                    pdfBase64 = Convert.ToBase64String(bytes);
                    result.Data = pdfBase64;
                    result.Value = r.Value;
                    File.Delete(rutaPdf);
                }
                else
                {
                    result.Value = false;
                    result.Message = r.Message;
                }
            }
            catch (Exception ex)
            {
                result.Value = false;
                result.Message = ex.Message;
            }

            return Response.AsJson(result);

        }

        private object ObtenerDemandaArticulo()
        {
            //Result result = new Result();

            var r = new Result<List<DemandaArticulo>>();
            r = _DAVentas.ObtenerDemandaArticulo();

            return Response.AsJson(r);


        }
    }
}