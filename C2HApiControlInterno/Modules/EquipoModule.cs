using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;
using Model = Models.Equipos;
namespace C2HApiControlInterno.Modules
{
    public class EquipoModule:NancyModule
    {
        private readonly DA.C2H.DAEquipo _DAEquipo = null;
        public EquipoModule() : base("/equipos")
        {
          // this.RequiresAuthentication();

            _DAEquipo = new DA.C2H.DAEquipo();
            Get("/todos", _ => GetTodos());
            Get("/{codEquipo}", x => GetEquipo(x));
            Get("/modelos/{codModelo}", x=> GetModelo(x));
            Get("/marcas/{codMarca}", x => GetMarca(x));
            Get("/tanques/{codTanque}", x => GetTanque(x));
            Get("/modelosCombo", _ => GetTodosModelosCombo());  
            Get("/marcasCombo", _ => GetTodosMarcasCombo());
            Get("/tipoEquipoCombo", _ => GetTipoEquipoCombo());
            Get("/consumiblesCombo", _ => GetConsumibleCombo());
            Get("/tiposEquipo/{codTipoEquipo}", x => GetTiposEquipo(x));
            Post("/guardar", _ => PostGuardarEquipo());
            Post("/guardar/marca", _ => PostGuardarMarca());
            Post("/guardar/modelo", _ => PostGuardarModelo());
            Post("guardar/tanque", _ => PostGuardarTanque());
            Post("guardar/tipoEquipo", _ => PostGuardarTipoEquipo());
        }


        private object GetConsumibleCombo()
        {
            Result<List<Model.ConsumbibleCombo>> result = new Result<List<Model.ConsumbibleCombo>>();
            try
            {
                result = _DAEquipo.ConsultaConsumibleCombo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTanque(dynamic x)
        {
            Result result = new Result();
            try
            {
                int codTanque = x.codTanque == null ? 0 : x.codTanque;
                var r = _DAEquipo.consultaTanque(codTanque);

                result.Data = r.Data;
                result.Message = r.Message;
                result.Value = r.Value;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object PostGuardarTanque()
        {
            Result result = new Result();
            var tanque = this.Bind<Model.TanquesCombustibleModel>();
            result = _DAEquipo.GuardarTanque(tanque);
            return Response.AsJson(result);
        }

        private object PostGuardarModelo()
        {
            Result result = new Result();
            var modelo = this.Bind<Model.ModeloEquipoModel>();
            result = _DAEquipo.GuardarModelo(modelo);
            return Response.AsJson(result);
        }

        private object PostGuardarMarca()
        {
            Result result = new Result();
            var marca = this.Bind<Model.MarcaEquipoModel>();
            result = _DAEquipo.GuardarMarca(marca);
            return Response.AsJson(result);
        }

        private object GetMarca(dynamic x)
        {
            Result result = new Result();
            try
            {
                int codMarca = x.codMarca== null ? 0 : x.codMarca;
                var r = _DAEquipo.consultaMarca(codMarca);

                result.Data = r.Data;
                result.Message = r.Message;
                result.Value = r.Value;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object GetModelo(dynamic x)
        {
            Result result = new Result();
            try
            {
                int codModelo = x.codModelo == null ? 0 : x.codModelo;
                var r = _DAEquipo.consultaModelo(codModelo);

                result.Data = r.Data;
                result.Message = r.Message;
                result.Value = r.Value;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetEquipo(dynamic x)
        {
            Result result = new Result();
            try
            {
                int codEquipo = x.codEquipo == null ? 0 : x.codEquipo;
                if(codEquipo > 0)
                {
                    var r = _DAEquipo.ConsultaEquipos(codEquipo);

                    result.Data = r.Data;
                    result.Message = r.Message;
                    result.Value = r.Value;

                }
                else
                {
                    this.GetTodos();
                    return 0;
                }
            }catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }

        private object PostGuardarEquipo()
        {
            Result result = new Result();
            var equipo = this.Bind<Model.EquipoModel>();
            result = _DAEquipo.GuardarEquipo(equipo);
            return Response.AsJson(result);
        }

        private object GetTipoEquipoCombo()
        {
            Result<List<Model.TipoEquipoCombo>> result = new Result<List<Model.TipoEquipoCombo>>();
            try
            {
                result = _DAEquipo.CosultarTipoEquipo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTodosMarcasCombo()
        {
            Result<List<Model.MarcaEquipoCombo>> result = new Result<List<Model.MarcaEquipoCombo>>();
            try
            {
                result = _DAEquipo.ConsultaMarcaCombo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTodosModelosCombo()
        {
            Result<List<Model.ModeloEquipoCombo>> result = new Result<List<Model.ModeloEquipoCombo>>();
            try
            {
                result = _DAEquipo.ConsultaModelosCombo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }

        //private object GetTodasMarcas()
        //{
        //    Result < List < Model.Equipos
        //}

        private object GetTodos()
        {
            Result<List<Model.EquipoModel>> result = new Result<List<Model.EquipoModel>>();
            try
            {
                var codCliente = this.BindUsuario().IdUsuario;
                result = _DAEquipo.ConsultaEquipos(0);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTiposEquipo(dynamic x)
        {
            Result result = new Result();
            try
            {
                int codTipoEquipo = x.codTipoEquipo == null ? 0 : x.codTipoEquipo;

                var r = _DAEquipo.ConsultaTiposEquipo(codTipoEquipo);

                result.Data = r.Data;
                result.Message = r.Message;
                result.Value = r.Value;

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostGuardarTipoEquipo()
        {
            Result result = new Result();
            try
            {

                var tipoEquipo = this.Bind<Model.TipoEquipoModel>();
                result = _DAEquipo.GuardarTipoEquipo(tipoEquipo);
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}