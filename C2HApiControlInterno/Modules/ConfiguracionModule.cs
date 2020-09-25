using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Models;
using Models.Usuario;
using DA.C2H;

namespace C2HApiControlInterno.Modules
{
    public class ConfiguracionModule : NancyModule
    {
        private readonly DAConfiguracion _DAConfiguracion = null;

        public ConfiguracionModule() : base("/configuracion")
        {
            this.RequiresAuthentication();
            _DAConfiguracion = new DAConfiguracion();
            Get("/usuarios", _ => ObtenerUsuarios());
            Get("/usuarios/modulos/{codigo}", parametros => ObtenerModulosUsuario(parametros));

        }


        private object ObtenerUsuarios()
        {
            Result<List<UsuarioPermisoModel>> result = new Result<List<UsuarioPermisoModel>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DAConfiguracion.ConsultaUsuarios();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object ObtenerModulosUsuario(dynamic p)
        {
            int codigo = p.codigo;

            var r = _DAConfiguracion.ObtenerModulosUsuario(codigo);

            return Response.AsJson(r);
        }



    }
}