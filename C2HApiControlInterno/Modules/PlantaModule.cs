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

            Get("/ubicacion/{codPlanta}", x => GetUbicacion(x));
            Get("/{codPlanta}", x => GetPlanta(x));
        }

        private object GetPlanta(dynamic x)
        {
            Result result = new Result();

            try
            {
                int codPlanta = x.codPlanta == null ? 0 : x.codPlanta;

                var r = _DAPlanta.consultaPlantas(codPlanta);
                result.Data = r.Data;
                result.Message = r.Message;
                result.Value = r.Value;
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetUbicacion(dynamic x)
        {
            Result<ParametrosModel> result = new Result<ParametrosModel>();

            try
            {
                int codPlanta = x.codPlanta == null ? 0 : x.codPlanta;
                result = _DAPlanta.consultaCoordenadas(codPlanta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}