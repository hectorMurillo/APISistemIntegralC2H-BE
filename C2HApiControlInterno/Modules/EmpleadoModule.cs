using DA.C2H;
using Models.Comisiones;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using WarmPack.Classes;
using Model = Models.Empleados;
namespace C2HApiControlInterno.Modules
{
    public class EmpleadoModule: NancyModule
    {
        private readonly DA.C2H.DAEmpleado _DAempleado = null;
        private readonly DAComisiones _DAComisiones = null;
        public EmpleadoModule() : base("/empleados")
        {
            this.RequiresAuthentication();
            _DAempleado = new DA.C2H.DAEmpleado();
            _DAComisiones = new DAComisiones();
            Get("/todos", _ => GetTodos());
            Get("/tipos", _ => GetTipos());
            Post("/SubTipos", _ => GetSubTipos());
            Get("/tiposUtilizados", _ => GetTiposUtilizados());
            Post("/guardar", _ => PostEmpleado());
                   
            Post("/personalCargaDiesel/guardar", _ => PostPersonalCargaDiesel());
            Get("/personalCargaDiesel/{codPersonal}", x => GetPersonalCargaDiesel(x));
            Get("/tiposEmpleado/{codTipoEmpleado}", x => GetTiposEmpleado(x));
            Post("guardar/tipoEmpleado", _ => PostGuardarTipoEmpleado());
            //COMENTARIO DE PRUEBA 
            Get("/{codEmpleado}", x => ObtenerEmpleado(x));
            Get("/empleadosNoUsuario", _ => ObtenerEmpleadoNoUsuario());
            Get("/comision", _ => GetTodos());

            Post("/guardar-archivo", _ => guardarArchivo());
            Get("/documentacion/{codEmpleado}", x => GetDocumentacionPorEmpleado(x));
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
            Result result = new Result();
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
            var p = this.BindUsuario();
            try
            {
                var empleado = this.Bind<Model.Empleado>();
                result = _DAempleado.GuardarEmpleado(empleado, p.IdUsuario);
            }
            catch(Exception ex)
            {
                result = new Result
                {
                    Message = ex.Message,
                    Data = null,
                    Value = false
                };
            }
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

        private dynamic guardarArchivo()
        {
            Result result = new Result();
            try
            {
                

                int codigo = (int)this.Request.Form.codigo;
                int codigoEmpleado = (int)this.Request.Form.codigoEmpleado;
                int codigoTipoDocumento = (int)this.Request.Form.codigoTipoDocumento;
                var archivo = this.Request.Files;
                byte[] buffer = new byte[0];
                string extension = (string)this.Request.Form.extension;
                bool vieneImagen = false;

                if(archivo.Count() > 0)
                {
                    var ms = new MemoryStream();
                    string filePath = Path.Combine(new DefaultRootPathProvider().GetRootPath(), "/" + archivo.ElementAt(0).Name);
                    archivo.ElementAt(0).Value.CopyTo(ms);
                    buffer = ms.ToArray();
                    vieneImagen = true;
                }

                result = _DAempleado.guardarArchivo(codigo, codigoEmpleado, codigoTipoDocumento, buffer, extension, vieneImagen);

               
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        // obtener usuario por cod
        private object ObtenerEmpleado(dynamic x)
        {
            Result result = new Result();
            try
            {
                //Si no se ha logeado marcará error aqui
                int codEmpleado = x.codEmpleado == null ? 0 : x.codEmpleado;
                var codUsuario = this.BindUsuario().IdUsuario;
                
                    var r = _DAempleado.ObtenerEmpleado(codEmpleado,codUsuario);
                
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


        private object ObtenerEmpleadoNoUsuario()
        {
            Result<List<Model.Empleado>> result = new Result<List<Model.Empleado>>();
            try
            {
                result = _DAempleado.ConsultaEmpleadosNoUsuario();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}