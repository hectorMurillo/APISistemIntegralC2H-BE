using DA.C2H;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class GeneralModule : NancyModule
    {
        private readonly DA.C2H.DAGenerales _DA = null;
        private readonly DAHerramientas _DAHerramientas = null;
        public GeneralModule() : base("generales")
        {
            this.RequiresAuthentication();
            _DAHerramientas = new DAHerramientas();
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

            var r = _DAHerramientas.ObtenerParametro(nombre);

            return Response.AsJson(r);
        }
    }
}