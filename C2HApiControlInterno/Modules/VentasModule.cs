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
//using Model = Models.Equipos;

namespace C2HApiControlInterno.Modules
{
    public class VentasModule : NancyModule
    {
        private readonly DAVentas _DAVentas = null;

        public VentasModule() : base("/ventas")
        {
            //this.RequiresAuthentication();
            _DAVentas = new DAVentas();

            Post("/reporte-mensual", _ => ReporteMensualMetrosCubicos());
        }

        private object ReporteMensualMetrosCubicos()
        {
            //var p = this.BindModel();

            //int usuario = (int)p.usuario;
            //int registros = (int)p.registros;
            //int codProveedor = (int)p.codProveedor;

            Result result = new Result();


            var r = new Result<List<RptMensualMetros>>();
            r = _DAVentas.ReporteMensualMetrosCuadrados();

            if (r.Value)
            {
                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\reporteTEST.pdf";
                string pdfBase64 = "";
                Byte[] bytes;

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\RptMensualMetros.rpt");
                reporte.SetDataSource(r.Data);
                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                bytes = File.ReadAllBytes(rutaPdf);
                pdfBase64 = Convert.ToBase64String(bytes);
                result.Data = pdfBase64;
                result.Value = r.Value;
                //File.Delete(rutaPdf);
            }

            return Response.AsJson(result);
            
        }



        //private dynamic ReporteMensualMetrosCubicos(dynamic arg)
        //{
        //    var p = this.BindModel();

        //    int usuario = (int)p.usuario;
        //    int registros = (int)p.registros;
        //    int codProveedor = (int)p.codProveedor;
        //    var Result = _DAMercadotecnia.articulosSinFotografiaImprimir(registros, codProveedor, usuario);

        //    if (Result.Value)
        //    {
        //        var path = HttpRuntime.AppDomainAppPath;
        //        string rutaPdf = "C:\\SidWeb\\articulos.pdf";
        //        string pdfBase64 = "";
        //        Byte[] bytes;

        //        ReportDocument reporte = new ReportDocument();
        //        reporte.Load(path + "\\Reportes\\rptImagenUbicacionProductos.rpt");
        //        reporte.SetDataSource(Result.Data);
        //        reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

        //        bytes = File.ReadAllBytes(rutaPdf);
        //        pdfBase64 = Convert.ToBase64String(bytes);
        //        Result.Data = pdfBase64;
        //        File.Delete(rutaPdf);
        //    }

        //    return Response.AsJson(Result);
        //}




    }
}