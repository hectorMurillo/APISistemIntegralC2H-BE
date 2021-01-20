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
            Post("guardar", _ => GuardarPedido());
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

    }
}