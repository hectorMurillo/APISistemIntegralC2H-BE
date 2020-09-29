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
using Models.Configuracion;

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
            Get("/modulos", _ => ObtenerModulos());
            Get("/funciones/{codModulo}/{codUsuario}", parametro => ObtenerFuncionesUsuario(parametro));
            Post("/funciones/guardar/{codModulo}/{codUsuario}", parametro => GuardarFuncionesUsuario(parametro));



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


        private object ObtenerModulos()
        {
            //int codigo = p.codigo;

            var r = _DAConfiguracion.ObtenerModulos();

            return Response.AsJson(r);
        }

        private dynamic ObtenerFuncionesUsuario(dynamic arg)
        {
            int codModulo = arg.codModulo;
            int codUsuario = arg.codUsuario;

            Result<List<FuncionModel>> result = new Result<List<FuncionModel>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DAConfiguracion.ObtenerFuncionesUsuario(codModulo, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);


        }



        private dynamic GuardarFuncionesUsuario(dynamic arg)
        {
            Result result = new Result();
            try
            {
                int codUsuario = arg.codUsuario;
                int codModulo = arg.codModulo;
                var datos = this.Bind<List<FuncionModel>>();
                result = _DAConfiguracion.GuardarFuncionesUsuario(datos, codUsuario, codModulo);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Response.AsJson(result);

        }





    }
}