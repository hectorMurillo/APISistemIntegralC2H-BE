using DA.C2H;
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
    public class PedidosModule : NancyModule
    {
        private readonly DAPedidos _DAPedidos = null;

        public PedidosModule() : base("/pedidos")
        {
            this.RequiresAuthentication();

            _DAPedidos = new DAPedidos();
            Get("/obtener-pedidos/{pedido}/{fechaDesde}/{fechaHasta}", x => ObtenerPedidos(x));
            Get("/consultar-pedidos/", _ => Pedidos());
            Post("guardar", _ => GuardarPedido());
            Get("/obtener-cierres/{folioPedido}", x => ObtenerCierres(x));
            Get("/guardar-cierres/{folioPedido}/{cantidadCierreNuevo}", x => GuardarCierres(x));
            Get("/pedidos-detenidos/{folioPedido}", x => PedidosDetenidos(x));
            Get("/autorizar-pedido-detenido/{folioPedido}/{autorizado}/{observacion}", x => AutorizarPedidoDetenido(x));
        }


        private object ObtenerPedidos(dynamic x)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                int pedido = x.pedido;

                result = _DAPedidos.ObtenerPedidos(pedido, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object Pedidos()
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {

                result = _DAPedidos.Pedidos();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarPedido()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var usuario = this.BindUsuario().Nombre;
                var pedido = this.Bind<PedidoModel>();
                result = _DAPedidos.GuardarPedido(pedido, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerCierres(dynamic x)
        {
            Result<List<PedidoCierre>> result = new Result<List<PedidoCierre>>();
            try
            {
                int folioPedido = x.folioPedido;
                result = _DAPedidos.ObtenerPedidosCierres(folioPedido);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarCierres(dynamic x)
        {
            Result<List<PedidoCierre>> result = new Result<List<PedidoCierre>>();
            try
            {
                int folioPedido = x.folioPedido;
                decimal cantidadCierreNuevo = x.cantidadCierreNuevo;

                result = _DAPedidos.GuardarCierre(folioPedido, cantidadCierreNuevo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PedidosDetenidos(dynamic x)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            int folioPedido = x.folioPedido;

            try
            {
                result = _DAPedidos.ObtenerPedidosDetenidos(folioPedido);
            }
            catch (Exception ex)
            {
                result.Value = false;
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object AutorizarPedidoDetenido(dynamic parametros)
        {
            Result result = new Result();

            try
            {
                int folioPedido = parametros.folioPedido;
                bool autorizado = parametros.autorizado;
                string observacion = parametros.observacion;

                result = _DAPedidos.AutorizarPedidoDetenido(folioPedido, autorizado, observacion);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}