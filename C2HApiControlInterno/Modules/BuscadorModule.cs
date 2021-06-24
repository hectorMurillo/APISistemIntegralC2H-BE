using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2HApiControlInterno.Modules
{
    public class BuscadorModule : NancyModule
    {
        private readonly DA.C2H.DABuscador _DABuscador = null;
        public BuscadorModule() : base("buscador")
        {
            //this.RequiresAuthentication();
            _DABuscador = new DA.C2H.DABuscador();
            Get("/pedido/{buscar}", parametros => ObtenerPedidos(parametros));
        }

        private object ObtenerPedidos(dynamic p)
        {
            try
            {
                string buscar = p.buscar;

                var r = _DABuscador.ObtenerPedidos(buscar);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex.Message);
            }

        }
    }

}