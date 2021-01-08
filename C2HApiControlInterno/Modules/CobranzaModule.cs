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

                var pathDirectorio = "C:\\PRUEBAPRUEBA\\";
                if (!Directory.Exists(pathDirectorio))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathDirectorio);
                }

                var path = HttpRuntime.AppDomainAppPath;
                string rutaPdf = "C:\\PRUEBAPRUEBA\\prueba.pdf";
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



    }
}