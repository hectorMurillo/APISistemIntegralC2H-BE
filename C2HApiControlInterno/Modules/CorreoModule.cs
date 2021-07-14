using DA.C2H;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class CorreoModule : NancyModule
    {
        private readonly DACorreo _DACorreo = null;

        public CorreoModule() : base("correos")
        {
            _DACorreo = new DACorreo();
            Post("/guardarValoracion", _ => GuardarValoracionServicio());
            Post("/verificarEncuesta", _ => VerificarEncuesta());
        }


        private object GuardarValoracionServicio()
        {
            Result r = new Result();
            try
            {
                var p = this.BindModel();

                int codPedido = p.datos.codPedido;
                int general = p.datos.general;
                int personal = p.datos.personal;
                int producto = p.datos.producto;
                string comentarios = p.datos.comentarios;
                r = _DACorreo.GuardarValoracionServicio(codPedido, general, personal, producto, comentarios);

            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.ToString();
            }

            return Response.AsJson(r);
        }

        private object VerificarEncuesta()
        {
            Result r = new Result();
            try
            {
                var p = this.BindModel();

                int codPedido = p.codPedido;

                r = _DACorreo.VerificarEncuesta(codPedido);

            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.ToString();
            }

            return Response.AsJson(r);
        }
    }
}