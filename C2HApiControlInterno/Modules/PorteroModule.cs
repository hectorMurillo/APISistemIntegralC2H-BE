﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA.C2H;
using Models.Porteros;
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
            Post("/guardar-entradas-salidas", _ => GuardarEntradasSalidas());
            Post("/guardar-suministros", _ => GuardarSuministros());
            Get("/obtener-entradas-salidas/{fechaDesde}/{fechaHasta}", x => ObtenerEntradasSalidas(x));
            Get("/obtener-suministros/{fechaDesde}/{fechaHasta}", x => ObtenerSuministros(x));
        }

        private object GuardarEntradasSalidas()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var entradaSalida = this.Bind<EntradaSalidaModel>();
                result = _DAPortero.GuardarEntradasSalidas(entradaSalida, codUsuario);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return Response.AsJson(result);
        }

        private object GuardarSuministros()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var suministros = this.Bind<SuministroModel>();
                result = _DAPortero.GuardarSuministros(suministros, codUsuario);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return Response.AsJson(result);
        }

        private object ObtenerEntradasSalidas(dynamic x)
        {
            Result<List<EntradaSalida>> result = new Result<List<EntradaSalida>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;

                result = _DAPortero.ObtenerEntradasSalidas(fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object ObtenerSuministros(dynamic x)
        {
            Result<List<Suministro>> result = new Result<List<Suministro>>();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;

                result = _DAPortero.ObtenerSuministros(fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


    }
}