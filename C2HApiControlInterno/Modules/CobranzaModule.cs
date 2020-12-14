using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA.C2H;
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
        }

        private object ObtenerNotasRemision()
        {
            Result<List<DACobranza>> result = new Result<List<DACobranza>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                //result = _DACobranza.ObtenerNotasRemision();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}