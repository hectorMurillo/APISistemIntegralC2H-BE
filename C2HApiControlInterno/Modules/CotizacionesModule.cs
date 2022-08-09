using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models;
using Models.Cotizaciones;
using Models.Pedidos;
using Models.Reportes;
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
    public class CotizacionesModule : NancyModule
    {
        private readonly DACotizaciones _DACotizaciones = null;

        public CotizacionesModule() : base("/cotizaciones")
        {
            this.RequiresAuthentication();

            _DACotizaciones = new DACotizaciones();
            Get("/obtener-cotizaciones/{cotizacion}/{fechaDesde}/{fechaHasta}", x => ObtenerCotizaciones(x));
            Get("/obtener-cotizaciones-detallado/{cotizacion}",x => ObtenerCotizaciones(x));
            Post("/guardar", x => GuardarCotizacion(x));
            Get("/obtener-cotizacion/pdf/{folioCotizacion}", parametros => ImprimirCotizacion(parametros));
            Get("/obtener-productos/{folioCotizacion}", parametros => ObtenerProductosCotizacion(parametros));

        }


        private object ObtenerCotizaciones(dynamic x)
        {
            Result<List<Cotizacion>> result = new Result<List<Cotizacion>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                int cotizacion = x.cotizacion;

                //USUARIO PUEDE SURTIR SE REQUIERE MANDAR EL USUARIO 
                var codUsuario = this.BindUsuario().IdUsuario;
                result = _DACotizaciones.ObtenerCotizaciones(cotizacion, fechaDesde, fechaHasta, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarCotizacion(dynamic x)
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var cotizaciones = this.Bind<CotizacionModelEnc>();
                result = _DACotizaciones.GuardarCotizacion(cotizaciones, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ImprimirCotizacion(dynamic parametros)
        {
            try
            {
                int folioCotizacion = parametros.folioCotizacion;

                var productosCotizacion = new Result<List<RptCotizaciones>>();
                productosCotizacion = _DACotizaciones.ObtenerDatosCotizacion(folioCotizacion);

                if(productosCotizacion.Value)
                {
                    Result result = new Result();

                    //var usuario = this.BindUsuario().Nombre;
                    var cliente = productosCotizacion.Data[0].Cliente.ToUpper();
                    var fechaCotizacion = productosCotizacion.Data[0].FechaCotizacion;
                    var totalCotizacion = productosCotizacion.Data[0].TotalCotizacion;
                    var vendedor = productosCotizacion.Data[0].Vendedor;
                    var telefono = productosCotizacion.Data[0].Celular;
                    var correo = productosCotizacion.Data[0].Correo;

                    var pathdirectorio = Globales.FolderPDF;

                    if (!Directory.Exists(pathdirectorio))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(pathdirectorio);
                    }

                    var path = HttpRuntime.AppDomainAppPath;
                    string rutapdf = $"{ Globales.FolderPDF}\\prueba-cotizacion.pdf";
                    string pdfbase64 = "";
                    byte[] bytes;
                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(path + "\\reportes\\RptCotizacion.rpt");
                    reporte.SetDataSource(productosCotizacion.Data);
                    reporte.SetParameterValue("@pFolio", folioCotizacion);
                    reporte.SetParameterValue("@pCliente", cliente);
                    reporte.SetParameterValue("@pFecha", fechaCotizacion);
                    reporte.SetParameterValue("@pTotalCotizacion", totalCotizacion);
                    reporte.SetParameterValue("@pVendedor", vendedor);
                    reporte.SetParameterValue("@pTelefono", telefono);
                    reporte.SetParameterValue("@pCorreo", correo);


                    //reporte.setdatasource();
                    reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf);

                    bytes = File.ReadAllBytes(rutapdf);
                    pdfbase64 = Convert.ToBase64String(bytes);
                    result.Data = pdfbase64;
                    File.Delete(rutapdf);
                    result.Value = true;

                    return Response.AsJson(result);
                } else
                {
                    return new Result()
                    {
                        Value = false,
                        Message = "No se encontró la cotizacion",
                        Data = null
                    };
                }
                
            }
            catch(Exception ex)
            {
                return new Result()
                {
                    Value = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }

        private object ObtenerProductosCotizacion(dynamic x)
        {
            Result<List<RptCotizaciones>> result = new Result<List<RptCotizaciones>>();
            try
            {
                int folioCotizacion = x.folioCotizacion;
                result = _DACotizaciones.ObtenerDatosCotizacion(folioCotizacion);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}