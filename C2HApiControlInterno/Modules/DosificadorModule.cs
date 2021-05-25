﻿using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using DA.C2H;
using Models.Dosificador;
using Models.Equipos;
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
    public class DosificadorModule:NancyModule
    {

        DADosificador _DADosificador = new DADosificador();
        public DosificadorModule() : base("/dosificador")
        {
            //this.RequiresAuthentication();

            Get("/ultimo-folio-ginco/", _ => UltimoFolioGinco());
            Get("/notasRemision-canceladas/", _ => NotasRemisionCanceladas());
            Get("/formulas/{codigo}", parametros => Formulas(parametros));
            Get("/ultimo-folio-notaRemision/", _ => UltimoFolioNotaRemision());
            Get("/obras-clientes/{codCliente}", parametros => ObrasCliente(parametros));
            Get("/operadores/{bombeable}", parametros => Operadores(parametros));
            Get("/operadores-camion-revolvedor", _ => ObtenerOperadoresCamionRevolvedor());
            Get("/operadores-camion-bombeable", _ => ObtenerOperadoresCamionBombeable());
            Get("/equipo-operador/{codOperador}/{esBombeable}", parametros => EquipoOperador(parametros));
            Get("/folio-pedido/{folioPedido}", parametros => FolioPedido(parametros));
            Get("/verificar-notasRemision-pedido/{folioPedido}", parametros => VerificarNotasRemisionPedido(parametros));
            Get("/notasRemision-especial/{codigo}/{folioGinco}", parametros => ObtenerNotasRemisionEspecial(parametros));
            Get("/notaRemision/pdf/{folio}", parametros => ObtenerPdfNotaRemision(parametros));
            
            Post("notaRemision/cancelar", _ => CancelarNotaRemision());
            Post("notaRemision/guardar", _ => GuardarNotaRemision());
            Post("notaRemision/agregar-nota", _ => AgregarNotaRemisionEspecial());
            Post("formula/guardar", _ => GuardarFormulaProducto());
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


        private object VerificarNotasRemisionPedido(dynamic parametros)
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

        private object UltimoFolioGinco()
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
        private object FolioPedido(dynamic parametros)
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

        private object NotasRemisionCanceladas()
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
        

        private object UltimoFolioNotaRemision()
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
        
        private object GuardarFormulaProducto()
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
        private object GuardarNotaRemision()
        {

            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionEncModel>();
                result = _DADosificador.GuardarNotaRemision(notaRemision, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerPdfNotaRemision(dynamic parametros)
        {
            Result result = new Result();
            var usuario = this.BindUsuario().Nombre;
            var folio = parametros.folio;
            var nota = new DatosNotaRemision();
            var datos = new Result<List<DatosNotaRemision>>();
            datos = _DADosificador.ObtenerDatosNota(folio);

            nota = datos.Data[0];

            var pathdirectorio = "c:\\pruebaprueba\\";
            //var pathdirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
            if (!Directory.Exists(pathdirectorio))
            {
                DirectoryInfo di = Directory.CreateDirectory(pathdirectorio);
            }

            var path = HttpRuntime.AppDomainAppPath;
            string rutapdf = "c:\\pruebaprueba\\prueba.pdf";
            //string rutapdf = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA\\prueba.pdf";
            string pdfbase64 = "";
            byte[] bytes;

            ReportDocument reporte = new ReportDocument();
            reporte.Load(path + "\\reportes\\rptnota.rpt");
            reporte.SetParameterValue("@folio", nota.Folio);
            reporte.SetParameterValue("@folioginco", nota.FolioGinco);
            reporte.SetParameterValue("@cliente", nota.Cliente);
            reporte.SetParameterValue("@obra", nota.Obra);
            reporte.SetParameterValue("@producto", nota.Producto);
            reporte.SetParameterValue("@cantidad", nota.Cantidad);
            reporte.SetParameterValue("@operador",nota.Operador);
            reporte.SetParameterValue("@nomenclatura", nota.Nomenclatura);
            reporte.SetParameterValue("@equipo", nota.Equipo);
            reporte.SetParameterValue("@vendedor", nota.Vendedor);
            reporte.SetParameterValue("@usuario", usuario);
            reporte.SetParameterValue("@bombeable", nota.Bombeable);
            reporte.SetParameterValue("@imper", nota.Imper);
            reporte.SetParameterValue("@fibra", nota.Fibra);
            reporte.SetParameterValue("@bombaequipo", nota.BombaEquipo);

            //reporte.setparametervalue("@sello", usuario);

            //reporte.setdatasource();
            reporte.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf);

            bytes = File.ReadAllBytes(rutapdf);
            pdfbase64 = Convert.ToBase64String(bytes);
            result.Data = pdfbase64;
            File.Delete(rutapdf);
            result.Value = true;

            return Response.AsJson(result); ;
        
        
    }

        private object AgregarNotaRemisionEspecial()
        {

            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var notaRemision = this.Bind<NotaRemisionEncModel>();
                result = _DADosificador.AgregarNotaRemisionEspecial(notaRemision, codUsuario);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        

        private object Formulas(dynamic parametros)
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

        private object ObtenerNotasRemisionEspecial(dynamic parametros)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                int codigo = parametros.codigo;
                int folioGinco = parametros.folioGinco;
                result = _DADosificador.ObtenerNotasRemisionEspecial(codigo, folioGinco);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        
        private object ObrasCliente(dynamic parametros)
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
        private object Operadores(dynamic parametros)
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

        private object ObtenerOperadoresCamionRevolvedor()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                result = _DADosificador.ObtenerOperadoresCamionRevolvedor();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerOperadoresCamionBombeable()
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                result = _DADosificador.ObtenerOperadoresCamionBombeable();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object EquipoOperador(dynamic parametros)
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

        private object CancelarNotaRemision()
        {
            Result result = new Result();
            try
            {
                var notaRemision = this.Bind<DatosNotaRemision>();
                result = _DADosificador.CancelarNotaRemision(notaRemision);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}