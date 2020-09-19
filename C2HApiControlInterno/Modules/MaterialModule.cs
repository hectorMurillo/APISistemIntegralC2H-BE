using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Model = Models.Materiales;
namespace C2HApiControlInterno.Modules
{
    public class MaterialModule:NancyModule
    {
        private readonly DA.C2H.DAMateriales _DAMateriales = null;
        public MaterialModule() : base("/materiales")
        {
            this.RequiresAuthentication();

            _DAMateriales = new DA.C2H.DAMateriales();
            //Aqui me quede 
            Get("/concretos/todos", _ => GetTiposConcretos());
            Post("/concretos/guardar", _ => PostTipoConcreto());
            Get("/resistencias/todos", _ => GetTiposResistencias());
            Post("/resistencias/guardar", _ => PostTipoResistencia());
            Get("/tma/todos", _ => GetTMA());
            Post("/tma/guardar", _ => PostTMA());
            Get("/especificaciones/todos", _ => GetEspecificacionesConcreto());
            Get("/tipoObra/todos", _ => GetTipoObras());
        }

        private object GetTipoObras()
        {
            Result<List<Model.TipoObra>> result = new Result<List<Model.TipoObra>>();
            try
            {
                result = _DAMateriales.consultaTipoObras();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostTMA()
        {
            Result result = new Result();
            try
            {
                var tma = this.Bind<Model.TMA>();
                result = _DAMateriales.GuardarTMA(tma);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTMA()
        {
            Result<List<Model.TMA>> result = new Result<List<Model.TMA>>();
            try
            {
                //Si no se ha logeado marcará error aqui  
                int codTMA = 0;
                result = _DAMateriales.consultaTMA(codTMA);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetEspecificacionesConcreto()
        {
            Result<List<Model.EspecificacionesConcreto>> result = new Result<List<Model.EspecificacionesConcreto>>();
            try
            {
                //Si no se ha logeado marcará error aqui  
                int codEspecificacion = 0;
                result = _DAMateriales.consultaEspecificaciones(codEspecificacion);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostTipoResistencia()
        {
            Result result = new Result();
            try
            {
                var tipoResistencia = this.Bind<Model.TipoResistencia>();
                result = _DAMateriales.GuardarTipoResistencia(tipoResistencia);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostTipoConcreto()
        {
            Result result = new Result();
            try
            {
                var tipoConcreto = this.Bind<Model.TipoConcreto>();
                result = _DAMateriales.GuardarTipoConcreto(tipoConcreto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTiposResistencias()
        {
            Result<List<Model.TipoResistencia>> result = new Result<List<Model.TipoResistencia>>();
            try
            {
                //Si no se ha logeado marcará error aqui  
                int codTipoResistencia = 0;
                result = _DAMateriales.consultaTipoResistencia(codTipoResistencia);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTiposConcretos()
        {
            Result<List<Model.TipoConcreto>> result = new Result<List<Model.TipoConcreto>>();
            try
            {
                //Si no se ha logeado marcará error aqui  
                int codTipoConcreto = 0;
                result = _DAMateriales.consultaTipoConcreto(codTipoConcreto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}