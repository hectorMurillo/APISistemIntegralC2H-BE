using Models;
using Models.Porteros;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2HApiControlInterno.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get("/", _ => GetRoot());
            Get("/Correo", _ => PruebaCorreo());
        }

        private object GetRoot()
        {
            Globales.ObtenerInformacionGlobal();
            return Response.AsJson("API C2H FUNCIONANDO! 👍");
        }

        private object PruebaCorreo()
        {
            EnviarCorreo correo = new EnviarCorreo();
            PedidoCorreoModel data = new PedidoCorreoModel();
            data.Correo = "cris_ales@live.com.mx";
            data.NombreComercial = "test";
            data.FolioPedido = 1;
            correo.SendMail(data);
            return Response.AsJson("Correo");
        }
    }
}