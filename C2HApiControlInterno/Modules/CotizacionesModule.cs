using DA.C2H;
using Models.Cotizaciones;
using Models.Pedidos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
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
            //Get("/obtener-descuento-cliente/{codAgente}/{codCliente}", x => ObtenerUltimoDescuento(x));
            Get("/obtener-cotizaciones/{cotizacion}/{fechaDesde}/{fechaHasta}", x => ObtenerCotizaciones(x));
            //Post("guardar", _ => GuardarPedido());
            //Get("/obtener-cierres/{folioPedido}", x => ObtenerCierres(x));
            //Get("/guardar-cierres/{folioPedido}/{cantidadCierreNuevo}", x => GuardarCierres(x));
            //Get("/cancelar-cierres/{folioPedido}/{idCatPedidosCierres}", x => CancelarCierres(x));
            //Get("/pedidos-detenidos/{folioPedido}", x => PedidosDetenidos(x));
            //Get("/autorizar-pedido-detenido/{folioPedido}/{autorizado}/{observacion}", x => AutorizarPedidoDetenido(x));
            //Get("/cambiar-estatus/{folioPedido}/{confirmado}/{motivo}", x => CambiarEstatusPedido(x));
            //Post("/reagendar-pedido/", _ => ReagendarPedido());

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

        //private object GuardarPedido()
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        var codUsuario = this.BindUsuario().IdUsuario;
        //        var usuario = this.BindUsuario().Nombre;
        //        var pedido = this.Bind<PedidoModel>();
        //        result = _DAPedidos.GuardarPedido(pedido, codUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object ObtenerCierres(dynamic x)
        //{
        //    Result<List<PedidoCierre>> result = new Result<List<PedidoCierre>>();
        //    try
        //    {
        //        int folioPedido = x.folioPedido;
        //        result = _DAPedidos.ObtenerPedidosCierres(folioPedido);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object GuardarCierres(dynamic x)
        //{
        //    Result<List<PedidoCierre>> result = new Result<List<PedidoCierre>>();
        //    try
        //    {
        //        int folioPedido = x.folioPedido;
        //        decimal cantidadCierreNuevo = x.cantidadCierreNuevo;

        //        result = _DAPedidos.GuardarCierre(folioPedido, cantidadCierreNuevo);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object CancelarCierres(dynamic x)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        int folioPedido = x.folioPedido;
        //        int idCatPedidosCierres = x.idCatPedidosCierres;

        //        result = _DAPedidos.CancelarCierre(folioPedido, idCatPedidosCierres);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object PedidosDetenidos(dynamic x)
        //{
        //    Result<List<Pedido>> result = new Result<List<Pedido>>();
        //    int folioPedido = x.folioPedido;

        //    var codUsuario = this.BindUsuario().IdUsuario;

        //    try
        //    {
        //        result = _DAPedidos.ObtenerPedidosDetenidos(folioPedido, codUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Value = false;
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object AutorizarPedidoDetenido(dynamic parametros)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        int folioPedido = parametros.folioPedido;
        //        bool autorizado = parametros.autorizado;
        //        string observacion = parametros.observacion;

        //        result = _DAPedidos.AutorizarPedidoDetenido(folioPedido, autorizado, observacion);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object CambiarEstatusPedido(dynamic parametros)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        int folioPedido = parametros.folioPedido;
        //        bool confirmado = parametros.confirmado;
        //        string motivo = parametros.motivo;
        //        var codUsuario = this.BindUsuario().IdUsuario;

        //        result = _DAPedidos.CambiarEstatusPedido(folioPedido, confirmado, motivo, codUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object ReagendarPedido()
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        var codUsuario = this.BindUsuario().IdUsuario;
        //        var pedido = this.Bind<PedidoReagendarModel>();
        //        result = _DAPedidos.ReagendarPedido(pedido, codUsuario);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}

        //private object ObtenerUltimoDescuento(dynamic x)
        //{
        //    Result<List<DescuentoXClienteModel>> result = new Result<List<DescuentoXClienteModel>>();
        //    try
        //    {
        //        int codAgente = x.codAgente;
        //        int codCliente = x.codCliente;

        //        result = _DAPedidos.ObtenerUltimoDescuento(codAgente, codCliente);
        //    }
        //    catch (Exception ex)
        //    {
        //        result.Message = ex.Message;
        //    }
        //    return Response.AsJson(result);
        //}
    }
}