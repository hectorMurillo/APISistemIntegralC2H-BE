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

            Get("/viajes-operadores/toExcel/{operador}/{bombeable}/{fechaDesde}/{fechaHasta}", parametros => ObtenerViajesOperadoresExcel(parametros));

            Get("/todos/auxiliar/", _ => ObtenerOperadoresAUXILIAR());
            Post("/obtener-viajes/{fechaDesde}/{fechaHasta}/{operador}/{bombeable}", x => ObtenerViajes(x));

            Post("/obtener-viajes-operador/{fechaDesde}/{fechaHasta}/{operador}/{bombeable}", x => ObtenerViajesOperador(x));
            Post("/cambiarTipoOperador/{codEmpleado}/{motivo}", x => CambiarTipoOperador(x));
        }
            


        private object CambiarTipoOperador(dynamic x)
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var codEmpleado = x.codEmpleado == null ? 0 : x.codEmpleado;
                var motivo = x.motivo == null ? "" : x.motivo;
                result = _DAOperador.GuardarCambioTipoOperador(codUsuario,codEmpleado, motivo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
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
        
         private object ObtenerViajesOperadoresExcel(dynamic paremeters)
        {
            Result<List<ViajesOperadoresExcel>> result = new Result<List<ViajesOperadoresExcel>>();
            try
            {
                string FechaDesde = paremeters.fechaDesde;
                string FechaHasta = paremeters.fechaHasta;

                string operador = paremeters.operador == "-" ? "" : paremeters.operador;
                int bombeable = paremeters.bombeable == "-" ? "" : paremeters.bombeable;
                //public Result<List<NotaRemisionAuxiliarExcel>> ObtenerDatosNotaRemisionAExcel(int codVendedor, string cliente, string obra, DateTime fechaDesde, DateTime fechaHasta)

                result = _DAOperador.ObtenerViajesOperadoresExcel(operador, bombeable, FechaDesde, FechaHasta);
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

        private object ObtenerOperadoresAUXILIAR()
        {
            Result<List<OperadorAuxiliar>> result = new Result<List<OperadorAuxiliar>>();
            try
            {
                result = _DAOperador.ObtenerOperadoresAUXILIAR();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerViajes(dynamic x)
        {
            Result result = new Result();
            try
            {
                string operador = x.operador;
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                int bombeable = x.bombeable;
                var r = new Result<List<Viajes>>();
                r = _DAOperador.ObtenerViajes(operador, fechaDesde, fechaHasta, bombeable);
                result.Data = r.Data;
                result.Value = r.Value;
                result.Message = r.Message;

                return Response.AsJson(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return Response.AsJson(result);
            }

        }

        private object ObtenerViajesOperador(dynamic x)
        {
            Result result = new Result();
            try
            {
                string operador = x.operador;
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                int bombeable = x.bombeable;

                var r = new Result<List<ViajesDetalle>>();
                r = _DAOperador.ObtenerViajesOperador(operador, fechaDesde, fechaHasta, bombeable);
                result.Data = r.Data;
                result.Value = r.Value;
                result.Message = r.Message;

                return Response.AsJson(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                return Response.AsJson(result);
            }
        }

    }
}