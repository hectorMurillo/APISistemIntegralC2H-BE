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

            Result result = new Result();

            var nota = new DatosNotaRemision();
            var datos = new Result<List<DatosNotaRemision>>();
            datos = _DACobranza.ObtenerDatosNota(folioNota);
            if (datos.Value)
            {
                nota = datos.Data[0];

                //var pathDirectorio = "C:\\PRUEBAPRUEBA\\";
                var pathDirectorio = Globales.FolderPDF;
                //var pathDirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
                if (!Directory.Exists(pathDirectorio))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathDirectorio);
                }

                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = Globales.FolderPDF;
                //string rutaPdf = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA\\prueba.pdf";
                string pdfBase64 = "";
                Byte[] bytes;

                ReportDocument reporte = new ReportDocument();
                reporte.Load(path + "\\Reportes\\rptNota.rpt");
                reporte.SetParameterValue("@Folio", nota.Folio);
                reporte.SetParameterValue("@FolioGinco", nota.FolioGinco);
                reporte.SetParameterValue("@Cliente", nota.Cliente);
                reporte.SetParameterValue("@Obra", nota.Obra);
                reporte.SetParameterValue("@Producto", nota.Producto);
                reporte.SetParameterValue("@Cantidad", nota.Cantidad);
                reporte.SetParameterValue("@Operador", nota.Operador);
                reporte.SetParameterValue("@Equipo", nota.Equipo);
                reporte.SetParameterValue("@Vendedor", nota.Vendedor);
                reporte.SetParameterValue("@Usuario", usuario);
                reporte.SetParameterValue("@Bombeable", nota.Bombeable);
                reporte.SetParameterValue("@Imper", nota.Imper);
                reporte.SetParameterValue("@Fibra", nota.Fibra);
                reporte.SetParameterValue("@BombaEquipo", nota.BombaEquipo);

                reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutaPdf);

                bytes = File.ReadAllBytes(rutaPdf);
                pdfBase64 = Convert.ToBase64String(bytes);
                result.Data = pdfBase64;
                result.Value = datos.Value;
                File.Delete(rutaPdf);
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
    }
}