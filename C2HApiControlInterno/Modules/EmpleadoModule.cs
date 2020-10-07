using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Model = Models.Empleados;
namespace C2HApiControlInterno.Modules
{
    public class EmpleadoModule: NancyModule
    {
        private readonly DA.C2H.DAEmpleado _DAempleado = null;
        public EmpleadoModule() : base("/empleados")
        {
            //this.RequiresAuthentication();
            _DAempleado = new DA.C2H.DAEmpleado();
            Get("/todos", _ => GetTodos());
            Get("/tipos", _ => GetTipos());
            Post("/SubTipos", _ => GetSubTipos());
            Get("/tiposUtilizados", _ => GetTiposUtilizados());
            Post("/guardar", _ => PostEmpleado());
            Get("/documentacion/{codEmpleado}", x => GetDocumentacionPorEmpleado(x));            
            Post("/personalCargaDiesel/guardar", _ => PostPersonalCargaDiesel());
            Get("/personalCargaDiesel/{codPersonal}", x => GetPersonalCargaDiesel(x));
            Get("/tiposEmpleado/{codTipoEmpleado}", x => GetTiposEmpleado(x));
            Post("guardar/tipoEmpleado", _ => PostGuardarTipoEmpleado());
            //COMENTARIO DE PRUEBA 
        }

        private object GetPersonalCargaDiesel(dynamic x)
        {
            Result<List<Model.PersonalCargaDiesel>> result = new Result<List<Model.PersonalCargaDiesel>>();
            try
            {
                int codPersonal = x.codPersonal;
                result = _DAempleado.consultaPersonalCargaDiesel(codPersonal);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostPersonalCargaDiesel()
        {
            Result result = new Result();
            var personalCargaDiesel = this.Bind<Model.PersonalCargaDiesel>();
            result = _DAempleado.GuardarPersonalCargaDiesel(personalCargaDiesel);
            return Response.AsJson(result);
        }

        private object GetDocumentacionPorEmpleado(dynamic x)
        {
            Result<List<Model.DocumentosEmpleado>> result = new Result<List<Model.DocumentosEmpleado>>();
            try
            {
                int codEmpleado = x.codEmpleado;
                result = _DAempleado.consultaDocumentos(codEmpleado);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetSubTipos()
        {
            Result<List<Model.SubTiposEmpleado>> result = new Result<List<Model.SubTiposEmpleado>>();
            try
            {
                var p = this.BindModel();

                int codTipo = p.codTipo;
                result = _DAempleado.consultaSubTipos(codTipo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostEmpleado()
        {
            Result result = new Result();
            var empleado = this.Bind<Model.Empleado>();
            result = _DAempleado.GuardarEmpleado(empleado);
            return Response.AsJson(result);
        }

        private object GetTiposUtilizados()
        {
            Result<List<Model.TipoEmpleado>> result = new Result<List<Model.TipoEmpleado>>();
            try
            {
                result = _DAempleado.consultaTiposUtilizados();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTipos()
        {
            Result<List<Model.TipoEmpleado>> result = new Result<List<Model.TipoEmpleado>>();
            try
            {
                result = _DAempleado.consultaTipos();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTodos()
        {
            Result<List<Model.Empleado>> result = new Result<List<Model.Empleado>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DAempleado.ConsultaEmpleados();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTiposEmpleado(dynamic x)
        {
            Result result = new Result();
            try
            {
                int codTipoEmpleado = x.codTipoEmpleado == null ? 0 : x.codTipoEmpleado;

                var r = _DAempleado.ConsultaTiposEmpleado(codTipoEmpleado);

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

        private object PostGuardarTipoEmpleado()
        {
            Result result = new Result();
            try
            {

                var tipoEmpleado = this.Bind<Model.TipoEmpleado>();
                result = _DAempleado.GuardarTipoEmpleado(tipoEmpleado);
            }
            catch (Exception ex)
            {

                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}