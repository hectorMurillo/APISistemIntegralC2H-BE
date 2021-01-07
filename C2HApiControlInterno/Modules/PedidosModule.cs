using DA.C2H;
using Models.Pedidos;
using Nancy;
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
            Get("/obtener-pedidos/{fechaDesde}/{fechaHasta}", x => ObtenerPedidos(x));
        }


        private object ObtenerPedidos(dynamic x)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;

                result = _DAPedidos.ObtenerPedidos(fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}