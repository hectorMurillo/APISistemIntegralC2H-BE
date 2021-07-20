using DA.C2H;
using Models.Comisiones;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class ComisionesModule : NancyModule
    {
        private readonly DAComisiones _DAComisiones = null;
        public ComisionesModule() : base("/comisiones")
        {
            this.RequiresAuthentication();
            _DAComisiones = new DAComisiones();
            Get("/obtenerComisiones", _ => ObtenerComisiones());
        }

        private object ObtenerComisiones()
        {
            Result<List<ComisionModel>> result = new Result<List<ComisionModel>>();
            try
            {
                result = _DAComisiones.ObtenerNotasRemision();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}