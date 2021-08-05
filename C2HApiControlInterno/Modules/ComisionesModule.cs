﻿using DA.C2H;
using Models.Comisiones;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class ComisionesModule : NancyModule
    {
        private readonly DAComisiones _DAComisiones = null;
        public ComisionesModule() : base("/comisiones")
        {
            //this.RequiresAuthentication();
            _DAComisiones = new DAComisiones();
            Get("/obtenerComisiones", _ => ObtenerComisiones());
            Get("/obtenerEmpleadosComisiones", _ => ObtenerEmpleadosConComisiones());

            Post("/obtenerComisionesPorEmpleado", _ => ObtenerComisionesPorEmpleado());
            Post("/guardar-comisiones", _ => GuardarComisionesEmpleado());
        }

        private object GuardarComisionesEmpleado()
        {
            Result<List<ComisionesXEmpleadoModel>> result = new Result<List<ComisionesXEmpleadoModel>>();
            try
            {
                List<ComisionesXEmpleadoModel> lstComisiones = new List<ComisionesXEmpleadoModel>();
                var parametro = this.BindModel();
                
                var comisiones = parametro.data.comisiones;

                foreach (var element in comisiones)
                {
                    ComisionesXEmpleadoModel comision = new ComisionesXEmpleadoModel();
                    comision.Codigo = element.codigo;
                    comision.Monto = element.monto;
                    lstComisiones.Add(comision);
                }
                int codEmpleado = parametro.data.codEmpleado;

                _DAComisiones.GuardarComisionesEmpleado(lstComisiones, codEmpleado);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerComisiones()
        {
            Result<List<ComisionModel>> result = new Result<List<ComisionModel>>();
            try
            {
                result = _DAComisiones.ObtenerNotasRemision();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerEmpleadosConComisiones()
        {
            Result<List<EmpleadosComisionModel>> result = new Result<List<EmpleadosComisionModel>>();
            try
            {
                result = _DAComisiones.ObtenerEmpleadosConComisiones();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerComisionesPorEmpleado()
        {
            Result<List<ComisionesXEmpleadoModel>> result = new Result<List<ComisionesXEmpleadoModel>>();
            try
            {
                var parametro = this.BindModel();
                int codEmpleado = parametro.codEmpleado;
                result = _DAComisiones.ObtenerComisionesPorEmpleado(codEmpleado);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}