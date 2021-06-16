using Models.Herramientas;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class PlantaModule : NancyModule
    {
        private readonly DA.C2H.DAPlanta _DAPlanta = null;

        public PlantaModule() : base("/planta")
        {
            this.RequiresAuthentication();

            _DAPlanta = new DA.C2H.DAPlanta();

            Get("/ubicacion", _ => GetUbicacion());
        }

        private object GetUbicacion()
        {
            Result<ParametrosModel> result = new Result<ParametrosModel>();

            try
            {
                result = _DAPlanta.consultaCoordenadas();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}