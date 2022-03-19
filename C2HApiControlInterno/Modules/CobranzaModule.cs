using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Dosificador;
using Models.Clientes;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;
using Models;
using Models.Cobranza;
using Ionic.Zip;
using System.Net;
using System.Text;
using Model = Models.Cobranza;

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
            Get("/CalcularDistancia/{origen}/{destino}", x => CalcularDistancia(x));
            Get("/obtenerAnticiposPorObra/{codObra}", x => getAnticipoPorObra(x));

            Get("/getListasPreciosDet/{codigo}", x => getListasPreciosGet(x));
            Get("/getListasPrecios", _ => getListasPrecios());
            Get("/getListaPreciosEnc/{codigo}", x => getListaPrecios(x));
            Get("/obtener-obras/{codigo}", x => ObtenerObrasCliente(x));
            Get("/obtener-anticipos/{codigo}", x => ObtenerAnticipos(x));
            Get("/obtener-obra/{codigo}", x => ObtenerObra(x));
            Post("/guardar-lista-precios", _ => PostListaPrecios()); 
            Post("/guardar-anticipo", _ => PostAnticipo());
            Post("/postPrecioProducto", _ => PostPrecioProducto());
            
        }

        private object getAnticipoPorObra(dynamic x)
        {
            Result<List<Anticipo>> result = new Result<List<Anticipo>>();
            try
            {
                result = _DACobranza.ObtenerAnticiposPorObra(x.codObra);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerAnticipos(dynamic x)
        {
            Result<List<Anticipo>> result = new Result<List<Anticipo>>();
            try
            {
                result = _DACobranza.ObtenerAnticipos(x.codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        
        private object ObtenerObra(dynamic x)
        {
            Result<List<Obra>> result = new Result<List<Obra>>();
            try
            {
                result = _DACobranza.ObtenerObra(x.codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object getListaPrecios(dynamic x)
        {
            Result<List<ListaPrecios>> result = new Result<List<ListaPrecios>>();
            try
            {
                result = _DACobranza.ObtenerListaPrecios(x.codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object getListasPreciosGet(dynamic x)
        {
            Result<List<ListaPreciosDet>> result = new Result<List<ListaPreciosDet>>();
            try
            { 
                result = _DACobranza.ObtenerListasPreciosDet(x.codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        
         private object PostPrecioProducto()
        {
            Result result = new Result();
            var p = this.BindUsuario();
            try
            {
                var producto = this.Bind<Model.ListaPreciosDet>();
                result = _DACobranza.GuardarProductoPrecio(producto, p.IdUsuario);
            }
            catch (Exception ex)
            {
                result = new Result
                {
                    Message = ex.Message,
                    Data = null,
                    Value = false
                };
            }
            return Response.AsJson(result);
        }

        private object PostAnticipo()
        {
            Result result = new Result();
            var p = this.BindUsuario();
            try
            {
                var anticipo = this.Bind<Model.Anticipo>();
                result = _DACobranza.GuardarAnticipo(anticipo, p.IdUsuario);
            }catch(Exception ex)
            {
                result = new Result
                {
                    Message = ex.Message,
                    Data = null,
                    Value = false
                };
            }
            return Response.AsJson(result);
        }
        

        private object EditarListaPrecios()
        {
            Result result = new Result();
            var p = this.BindUsuario();
            try
            {
                var lista = this.Bind<Model.ListaPrecios>();
                result = _DACobranza.GuardarListaPrecios(lista, p.IdUsuario);
            }
            catch (Exception ex)
            {
                result = new Result
                {
                    Message = ex.Message,
                    Data = null,
                    Value = false
                };
            }
            return Response.AsJson(result);
        }

        private object PostListaPrecios()
        {
            Result result = new Result();
            var p = this.BindUsuario();
            try
            {
                var lista = this.Bind<Model.ListaPrecios>();
                result = _DACobranza.GuardarListaPrecios(lista, p.IdUsuario);
            }catch(Exception ex)
            {
                result = new Result
                {
                    Message = ex.Message,
                    Data = null,
                    Value = false
                };
            }
            return Response.AsJson(result);
        }

        private object getListasPrecios()
        {
            Result<List<ListaPrecios>> result = new Result<List<ListaPrecios>>();
            try
            {
                result = _DACobranza.getListasPrecios();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerObrasCliente(dynamic x)
        {
            Result<List<DireccionesXClientesModel>> result = new Result<List<DireccionesXClientesModel>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DACobranza.ObtenerObras(x.codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object CalcularDistancia(dynamic x)
        {
            Result result = new Result();
            try
            {
                string origen = x.origen;
                string destino = x.destino;
                // string url = "https://pokeapi.co/api/v2/pokemon/1";
                string url = "https://maps.googleapis.com/maps/api/distancematrix/json?origins=" + origen + "&destinations=" + destino + "&mode=driving&units=metric&language=en&avoid=&key=AIzaSyAbG_eyVgmdfq7xuPTvidPbj36Vf-Tfjnk";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                //HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                //WebResponse myResp = request.GetResponse();
                //string contenido = File.ReadAllText("https://pokeapi.co/api/v2/pokemon/1");
                return readStream.ReadToEnd();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
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
                    string rutaPdf = Globales.FolderPDF + string.Format(@"\NotaRemision_{000}.pdf", nota.Folio);


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
                    reporte.SetParameterValue("@bombaequipo", nota.EquipoBomba);
                    reporte.SetParameterValue("@cancelado", 0);
                    reporte.SetParameterValue("@fecha", nota.Fecha);

                    reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                    //bytes = File.ReadAllBytes(rutaPdf);

                }
                using (ZipFile zip = new ZipFile())
                {
                    for (int i = 0; i < datos.Data.Count; i++)
                     {
                   
                        string rutaPdf = Globales.FolderPDF + string.Format(@"\NotaRemision_{000}.pdf", datos.Data[i].Folio);
                        if (System.IO.File.Exists(rutaPdf))
                        {
                            zip.AddFile(rutaPdf, "");
                        }

                    }

                    string archivoFinal = Globales.CarpetaZIPtemporal();

                    if (!System.IO.Directory.Exists(Globales.FolderPDF + @"\ZIP"))
                    {
                        System.IO.Directory.CreateDirectory(Globales.FolderPDF + @"\ZIP");
                    }

                    zip.Save(archivoFinal);
                    buffer = System.IO.File.ReadAllBytes(archivoFinal);

                    regresa = Convert.ToBase64String(buffer);

                    if (System.IO.File.Exists(archivoFinal))
                    {
                        System.IO.File.Delete(archivoFinal);
                    }

                    for (int i = 0; i < datos.Data.Count; i++)
                    {

                        string rutaPdf = Globales.FolderPDF + string.Format(@"\NotaRemision_{000}.pdf", datos.Data[i].Folio);
                        if (System.IO.File.Exists(rutaPdf))
                        {
                            File.Delete(rutaPdf);
                        }

                    }

                    result.Data = regresa;
                    result.Value = true;
                }
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