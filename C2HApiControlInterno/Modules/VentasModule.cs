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

        }

        private object ReporteMensualMetrosCubicos(dynamic x)
        {
            Result result = new Result();

            var reporteMensualMetros = this.Bind<RptMensualMetrosModel>();
            DateTime fechaDesde = x.fechaDesde;
            DateTime fechaHasta = x.fechaHasta;

            var r = new Result<List<RptMensualMetros>>();
            r = _DAVentas.ReporteMensualMetrosCubicos(reporteMensualMetros, fechaDesde, fechaHasta);

            if (r.Value)
            {
                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\RptMensualMetrosTEST.pdf";
                string pdfBase64 = "";
                Byte[] bytes;

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\RptMensualMetros.rpt");
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

    }
}