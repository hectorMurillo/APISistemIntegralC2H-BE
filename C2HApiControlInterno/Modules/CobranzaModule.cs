using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Dosificador;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;
using Models;
using Models.Cobranza;
using Ionic.Zip;

namespace C2HApiControlInterno.Modules
{
    public class CobranzaModule : NancyModule
    {
        private readonly DACobranza _DACobranza = null;

        public CobranzaModule() : base("/cobranza")
        {
            this.RequiresAuthentication();

            _DACobranza = new DACobranza();
            Get("/obtenerNotasRemision", _ => ObtenerNotasRemision());
            Get("/obtenerNotasRemision/{folio}", parametros => ObtenerDatosReporte(parametros));
            Get("/obtener-notas-surtiendo", _ => ObtenerNotasRemisionSurtiendo());
            Get("/obtener-notas-remision/{entrada}", x => ObtenerNotasRemisionEntradasSalidas(x));
            Get("/obtener-notas-remision-cobranza/{fechaDesde}/{fechaHasta}", x => ObtenerNotasRemisionCobranza(x));
            Post("/notas-remision-guardar-pago/{idNotasRemisionEnc}/{importeAbonar}", x => GuardarPagoNotaRemision(x));
            Get("/obtener-nota-remision-cobranza/{idNotasRemisionEnc}", x => ObtenerNotaRemisionCobranza(x));
            Get("/obtener-detalle-abonos-nota-remision-cobranza/{idNotasRemisionEnc}", x => ObtenerDetalleAbonosNotaRemision(x));


        }

        private object ObtenerNotasRemision()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DACobranza.ObtenerNotasRemision();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        public object ObtenerDatosReporte(dynamic parametros)
        {
            int folioNota = parametros.folio;
            var usuario = this.BindUsuario().Usuario;
            //string regresa = string.Empty;

            Result result = new Result();

            var nota = new DatosNotaRemision();
            var datos = new Result<List<DatosNotaRemision>>();
            byte[] buffer;
            string regresa;
            datos = _DACobranza.ObtenerNotasRemision(folioNota);
            string archivoFinal = "";
            result.Message = datos.Message;
            result.Value = datos.Value;

            if (datos.Value)
            {
                for (int i = 0; i < datos.Data.Count; i++)
                {
                    nota = datos.Data[i];

                    //var pathDirectorio = "C:\\PRUEBAPRUEBA\\";
                    Globales.ObtenerInformacionGlobal();
                    var pathDirectorio = Globales.FolderPDF;
                    //var pathDirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
                    if (!Directory.Exists(pathDirectorio))
                    {
                        DirectoryInfo di = Directory.CreateDirectory(pathDirectorio);
                    }

                    var path = HttpRuntime.AppDomainAppPath;
                    //string rutaPdf = Globales.FolderPDF + "\\notaRemision_{0i}.pdf";
                    string rutaPdf = Globales.FolderPDF + string.Format(@"\NotaRemision_{000}.xml", i + 1);


                    //string rutaPdf = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA\\prueba.pdf";
                    //string pdfBase64 = "";
                    Byte[] bytes;

                    ReportDocument reporte = new ReportDocument();
                    reporte.Load(path + "\\Reportes\\rptNota.rpt");
                    reporte.SetParameterValue("@folio", nota.Folio);
                    reporte.SetParameterValue("@folioginco", nota.FolioGinco);
                    reporte.SetParameterValue("@cliente", nota.Cliente);
                    reporte.SetParameterValue("@obra", nota.Obra);
                    reporte.SetParameterValue("@producto", nota.Producto);
                    reporte.SetParameterValue("@cantidad", nota.Cantidad);
                    reporte.SetParameterValue("@operador", nota.Operador);
                    reporte.SetParameterValue("@nomenclatura", nota.Nomenclatura);
                    reporte.SetParameterValue("@equipo", nota.Equipo);
                    reporte.SetParameterValue("@vendedor", nota.Vendedor);
                    reporte.SetParameterValue("@usuario", usuario);
                    reporte.SetParameterValue("@bombeable", nota.Bombeable);
                    reporte.SetParameterValue("@imper", nota.Imper);
                    reporte.SetParameterValue("@fibra", nota.Fibra);
                    reporte.SetParameterValue("@bombaequipo", nota.BombaEquipo);
                    reporte.SetParameterValue("@cancelado", 0);
                    reporte.SetParameterValue("@fecha", nota.Fecha);

                    reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                    //bytes = File.ReadAllBytes(rutaPdf);

                    using (ZipFile zip = new ZipFile())
                    {

                        if (System.IO.File.Exists(rutaPdf))
                        {
                            zip.AddFile(rutaPdf, "");
                        }

                        //rutaPdf = Globales.FolderPDF + string.Format(@"\PDF\asdasd.pdf");

                        //if (System.IO.File.Exists(rutaPdf))
                        //{
                        //    zip.AddFile(rutaPdf, "");
                        //}

                        File.Delete(rutaPdf);


                        archivoFinal = Globales.CarpetaZIPtemporal();

                        if (!System.IO.Directory.Exists(Globales.FolderPDF + @"\ZIP"))
                        {
                            System.IO.Directory.CreateDirectory(Globales.FolderPDF + @"\ZIP");
                        }

                        zip.Save(archivoFinal);

                      

                        //Borrar datos generados ...


                        //if (System.IO.File.Exists(archivoFinal))
                        //{
                        //    System.IO.File.Delete(archivoFinal);
                        //}

                        //rutaPdf = Globales.FolderPDF + string.Format(@"\PDF\Fasd.pdf");
                        //File.Delete(rutaPdf);

                    }

                    buffer = System.IO.File.ReadAllBytes(archivoFinal);

                    regresa = Convert.ToBase64String(buffer);

                    result.Data = regresa;
                    result.Value = true;

                }


               
                //pdfBase64 = Convert.ToBase64String(bytes);
                //result.Data = pdfBase64;
                //result.Value = datos.Value;
                //File.Delete(rutaPdf);

              

            }
            return result;
        }

        private object ObtenerNotasRemisionSurtiendo()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DACobranza.ObtenerNotasRemisionSurtiendo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerNotasRemisionEntradasSalidas(dynamic parametros)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                bool entrada = parametros.entrada;
                result = _DACobranza.ObtenerNotasRemisionEntradasSalidas(entrada);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerNotasRemisionCobranza(dynamic x)
        {
            Result<List<NotaRemisionCobranza>> result = new Result<List<NotaRemisionCobranza>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;

                result = _DACobranza.ObtenerNotasRemisionCobranza(fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarPagoNotaRemision(dynamic x)
        {
            Result result = new Result();
            try
            {
                var codCliente = this.BindUsuario().IdUsuario;
                int idNotasRemisionEnc = x.idNotasRemisionEnc;
                decimal importeAbonar = x.importeAbonar;

                result = _DACobranza.GuardarPagoNotaRemision(idNotasRemisionEnc, importeAbonar, codCliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object ObtenerNotaRemisionCobranza(dynamic x)
        {
            Result<List<NotaRemisionCobranza>> result = new Result<List<NotaRemisionCobranza>>();
            try
            {
                int idNotasRemisionEnc = x.idNotasRemisionEnc;

                result = _DACobranza.ObtenerNotaRemisionCobranza(idNotasRemisionEnc);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerDetalleAbonosNotaRemision(dynamic x)
        {
            Result<List<DetalleAbonosNotaRemision>> result = new Result<List<DetalleAbonosNotaRemision>>();
            try
            {
                int idNotasRemisionEnc = x.idNotasRemisionEnc;

                result = _DACobranza.ObtenerDetalleAbonosNotaRemision(idNotasRemisionEnc);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}