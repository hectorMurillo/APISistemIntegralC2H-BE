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
            Get("/ultimo-folio-ginco/", _ => GetUltimoFolioGinco());
            Get("/notasRemision-canceladas/", _ => GetNotasRemisionCanceladas());
            Get("/formulas/{codigo}", parametros => GetFormulas(parametros));
            Get("/ultimo-folio-notaRemision/", _ => GetUltimoFolioNotaRemision());
            Get("/obras-clientes/{codCliente}", parametros => GetObrasCliente(parametros));
            Get("/operadores/{bombeable}", parametros => GetOperadores(parametros));
            Get("/equipo-operador/{codOperador}/{esBombeable}", parametros => GetEquipoOperador(parametros));
            Get("/folio-pedido/{folioPedido}", parametros => GetFolioPedido(parametros));
            Get("/verificar-notasRemision-pedido/{folioPedido}", parametros => GetVerificarNotasRemisionPedido(parametros));
            Post("notaRemision/cancelar", _ => PostCancelarNotaRemision());
            Post("notaRemision/guardar", _ => PostGuardarNotaRemision());
            Post("formula/guardar", _ => PostGuardarFormulaProducto());
            Post("productos-formula/guardar", _ => GuardarProductoFormula());
        }

        
         private object GuardarProductoFormula()
        {
            Result result = new Result();
            try
            {
                var formula = this.Bind<List<FormulaModel>>();
                result = _DADosificador.GuardarProductosFormula(formula);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object GetVerificarNotasRemisionPedido(dynamic parametros)
        {
            Result<DatoModel> result = new Result<DatoModel>();
            try
            {
                int folioPedido = parametros.folioPedido;
                result = _DADosificador.VerificarNotaRemisionPedido(folioPedido);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
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
        private object GetFolioPedido(dynamic parametros)
        {
            Result<List<PedidoModel>> result = new Result<List<PedidoModel>>();
            try
            {
                int folioPedido = parametros.folioPedido;
                result = _DADosificador.ObtenerPedido(folioPedido);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetNotasRemisionCanceladas()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                result = _DADosificador.ObtenerNotasRemisionCanceladas();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        

        private object GetUltimoFolioNotaRemision()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                result = _DADosificador.ObtenerUltimoFolioNR();
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
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionEncModel>();
                result = _DADosificador.GuardarNotaRemision(notaRemision, codUsuario);
                if (result.Value)
                {
                    var nota = new DatosNotaRemision();
                    var datos = new Result<List<DatosNotaRemision>>();
                    datos = _DADosificador.ObtenerDatosNota(notaRemision);
                    if (result.Value)
                    {
                        nota = datos.Data[0];

                        var pathdirectorio = "c:\\pruebaprueba\\";
                        if (!Directory.Exists(pathdirectorio))
                        {
                            DirectoryInfo di = Directory.CreateDirectory(pathdirectorio);
                        }
                        
                        var path = HttpRuntime.AppDomainAppPath;
                        string rutapdf = "c:\\pruebaprueba\\prueba.pdf";
                        string pdfbase64 = "";
                        byte[] bytes;

                        ReportDocument reporte = new ReportDocument();
                        reporte.Load(path + "\\reportes\\rptnota.rpt");
                        reporte.SetParameterValue("@folio", notaRemision.Folio);
                        reporte.SetParameterValue("@folioginco", notaRemision.FolioGinco);
                        reporte.SetParameterValue("@cliente", nota.Cliente);
                        reporte.SetParameterValue("@obra", nota.Obra);
                        reporte.SetParameterValue("@producto", nota.Producto);
                        reporte.SetParameterValue("@cantidad", notaRemision.Cantidad);
                        reporte.SetParameterValue("@operador", nota.Operador);
                        reporte.SetParameterValue("@equipo", nota.Equipo);
                        reporte.SetParameterValue("@vendedor", nota.Vendedor);
                        reporte.SetParameterValue("@usuario", usuario);
                        reporte.SetParameterValue("@bombeable", notaRemision.ChKBombeable);
                        reporte.SetParameterValue("@imper", notaRemision.ChKImper);
                        reporte.SetParameterValue("@fibra", notaRemision.ChKFibra);
                        reporte.SetParameterValue("@bombaequipo", nota.BombaEquipo);

                        //reporte.setparametervalue("@sello", usuario);

                        //reporte.setdatasource();
                        reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf);

                        bytes = File.ReadAllBytes(rutapdf);
                        pdfbase64 = Convert.ToBase64String(bytes);
                        result.Data = pdfbase64;
                        File.Delete(rutapdf);
                    }

                }

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object GetFormulas(dynamic parametros)
        {
            Result<List<FormulaModel>> result = new Result<List<FormulaModel>>();
            try
            {
                int codigo = parametros.codigo;
                result = _DADosificador.ObtenerFormulas(codigo);
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

        private object PostCancelarNotaRemision()
        {
            Result result = new Result();
            try
            {
                var notaRemision = this.Bind<DatosNotaRemision>();
                result = _DADosificador.CancelarNotaRemision(notaRemision.Folio, notaRemision.FolioGinco);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}