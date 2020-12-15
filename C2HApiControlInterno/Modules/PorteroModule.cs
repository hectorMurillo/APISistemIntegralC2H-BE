using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA.C2H;
//using Models.Operador;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class PorteroModule : NancyModule
    {
        private readonly DAPortero _DAPortero = null;

        public PorteroModule() : base("/porteros")
        {
            this.RequiresAuthentication();

            _DAPortero = new DAPortero();
            Get("/guardar-entradas-salidas/{codEquipo}/{codOperador}/{kilometraje}/{horometraje}", x => GuardarEntradasSalidas(x));
            Get("/guardar-suministros/{codEquipo}/{codOperador}/{diesel}/{anticongelante}/{aceite}", x => GuardarSuministros(x));
        }

        private object GuardarEntradasSalidas(dynamic x)
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;

                int codEquipo = x.codEquipo;
                int codOperador = x.codOperador;
                decimal kilometraje = x.kilometraje;
                decimal horometraje = x.horometraje;

                result = _DAPortero.GuardarEntradasSalidas(codEquipo, codOperador, kilometraje, horometraje, codUsuario);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return Response.AsJson(result);
        }

        private object GuardarSuministros(dynamic x)
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;

                int codEquipo = x.codEquipo;
                int codOperador = x.codOperador;
                decimal diesel = x.diesel;
                decimal anticongelante = x.anticongelante;
                decimal aceite = x.aceite;

                result = _DAPortero.GuardarSuministros(codEquipo, codOperador, diesel, anticongelante, aceite, codUsuario);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return Response.AsJson(result);
        }


    }
}