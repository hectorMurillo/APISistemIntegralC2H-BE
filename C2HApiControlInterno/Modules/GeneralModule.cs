using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2HApiControlInterno.Modules
{
    public class GeneralModule : NancyModule
    {
        private readonly DA.C2H.DAGenerales _DA = null;
        public GeneralModule() : base("generales")
        {
            this.RequiresAuthentication();

            _DA = new DA.C2H.DAGenerales();
            Get("/codigoPostal/{codigoPostal}", parametros => GetDatosCP(parametros));
            Get("/parametro/{nombre}", parametros => GetParametro(parametros));
        }

        private object GetDatosCP(dynamic p)
        {
            string codigoPostal = p.codigoPostal;

            var r = _DA.DetallesCodigoPostal(codigoPostal);

            return Response.AsJson(r);
        }

        private object GetParametro(dynamic p)
        {
            string nombre = p.nombre;

            var r = _DA.ObtenerParametro(nombre);

            return Response.AsJson(r);
        }
    }
}