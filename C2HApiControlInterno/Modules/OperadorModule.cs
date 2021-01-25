using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA.C2H;
using Models.Operador;
using Models.Operadores;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class OperadorModule : NancyModule
    {
        private readonly DAOperador _DAOperador = null;

        public OperadorModule() : base("/operadores")
        {
            this.RequiresAuthentication();

            _DAOperador = new DAOperador();
            Get("/todos/{codOperador}", x => ObtenerOperadores(x));
            Get("/entradasSalidas/{entrada}", x => ObtenerOperadoresEntradasSalidas(x));
            Get("/tipos", _ => ObtenerTiposOperadores());

            Post("guardar", _ => GuardarOperador());

        }

        private object ObtenerOperadores(dynamic x)
        {
            Result<List<Operador>> result = new Result<List<Operador>>();
            try
            {
                int codOperador = x.codOperador == null ? 0 : x.codOperador;
                result = _DAOperador.ObtenerOperadores(codOperador);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerOperadoresEntradasSalidas(dynamic x)
        {
            Result<List<Operador>> result = new Result<List<Operador>>();
            try
            {
                bool entrada = x.entrada == null ? 0 : x.entrada;
                result = _DAOperador.ObtenerOperadoresEntradasSalidas(entrada);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposOperadores()
        {
            Result<List<OperadorTipo>> result = new Result<List<OperadorTipo>>();
            try
            {
                result = _DAOperador.ObtenerTiposOperadores();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarOperador()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var operador = this.Bind<OperadorModel>();
                result = _DAOperador.GuardarOperador(operador, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}