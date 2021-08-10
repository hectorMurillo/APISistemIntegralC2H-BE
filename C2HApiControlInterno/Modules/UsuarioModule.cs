using DA.C2H;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WarmPack.Classes;
using Model = Models.Usuario;
namespace C2HApiControlInterno.Modules

{
    public class UsuarioModule : NancyModule
    {
        private readonly DAUsuario _DAUsuario = null;
        public UsuarioModule() : base("/usuario")
        {
            this.RequiresAuthentication();

            _DAUsuario = new DAUsuario();

            //Post("/menu-principal", _ => postMenuPrincipal());
            Get("/obtenerMenu", _ => getMenuPrincipal());
            Get("/todos", _ => GetTodos());
            Get("/actualizar-estatus/{idUsuario}/{activar}", x => ActualizarEstatusUsuario(x));
            Post("/guardar", _ => PostGuardarUsuario());
        }

        private object PostGuardarUsuario()
        {
            Result result = new Result();
            try
            {
                var usuario = this.Bind<Model.UsuarioModel>();
                result = _DAUsuario.GuardarUsuario(usuario);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTodos()
        {
            Result<List<Model.UsuarioModel>> result = new Result<List<Model.UsuarioModel>>();
            try
            {
                //Si no se ha logeado marcará error aqui
                var codCliente = this.BindUsuario().IdUsuario;
                result = _DAUsuario.ConsultaUsuarios();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object getMenuPrincipal()
        {
            Result<List<Model.Menu>> result = new Result<List<Model.Menu>>();
            Result<List<Model.EstructuraModulo>> resultModificado = new Result<List<Model.EstructuraModulo>>();
            resultModificado.Data = new List<Model.EstructuraModulo>();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                result = _DAUsuario.ConsultaMenuPermisos(codUsuario);

                var estructuraMenu = new List<Model.EstructuraModulo>();
                var estructuraModulo = new Model.EstructuraModulo();
                var funcionModulo = new Model.EstructuraFuncion();

                for (int i = 0; i < result.Data.Count(); i++)
                {
                    Model.Menu menuActual = new Model.Menu();
                    menuActual = result.Data[i];
                    if (estructuraMenu.Find(x => x.Modulo.Equals(menuActual.Modulo)) == null)
                    {
                        if (estructuraModulo.Modulo != null)
                        {
                            resultModificado.Data.Add(estructuraModulo);
                            estructuraModulo = new Model.EstructuraModulo();
                            estructuraModulo.Modulo = menuActual.Modulo;
                            estructuraMenu.Add(estructuraModulo);
                        }
                        funcionModulo = new Model.EstructuraFuncion();
                        estructuraModulo.Modulo = menuActual.Modulo;
                        estructuraModulo.Funciones = new List<Model.EstructuraFuncion>();
                        funcionModulo.Escritura = menuActual.Escritura;
                        funcionModulo.Funcion = menuActual.Funcion;
                        funcionModulo.Lectura = menuActual.Lectura;
                        funcionModulo.URL = menuActual.URL;

                        estructuraModulo.Funciones.Add(funcionModulo);
                        estructuraMenu.Add(estructuraModulo);
                    }
                    else
                    {
                        estructuraModulo = estructuraMenu.Find(x => x.Modulo.Equals(menuActual.Modulo));
                        funcionModulo = new Model.EstructuraFuncion();
                        funcionModulo.Escritura = menuActual.Escritura;
                        funcionModulo.Funcion = menuActual.Funcion;
                        funcionModulo.Lectura = menuActual.Lectura;
                        funcionModulo.URL = menuActual.URL;

                        estructuraModulo.Funciones.Add(funcionModulo);
                    }
                }
                resultModificado.Data.Add(estructuraModulo);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            #region
            //try
            //{              
            //    var codUsuario = this.BindUsuario().IdUsuario;
            //    result = _DAUsuario.ConsultaMenuPermisos(codUsuario);
            //    var estructuraMenu = new List<Model.EstructuraModulo>();
            //    int i = 0;
            //    if (result.Value)
            //    {
            //        Model.EstructuraModulo estructuraNueva = new Model.EstructuraModulo();

            //        result.Data.ForEach(element =>
            //        {                                                
            //            if (estructuraMenu.Exists(x=> x.Modulo.Equals(element.Modulo)))
            //            {
            //                //El modulo ya está agregado en la lista                            
            //                estructuraNueva = estructuraMenu.Find(x => x.Modulo.Equals(element.Modulo));

            //                Model.EstructuraFuncion funcionNueva = new Model.EstructuraFuncion();                            
            //                funcionNueva.Escritura = element.Escritura;
            //                funcionNueva.Lectura = element.Lectura;
            //                funcionNueva.URL = element.URL;
            //                funcionNueva.Funcion = element.Funcion;

            //                estructuraNueva.Funciones.Add(funcionNueva);                            
            //            }
            //            else
            //            {
            //                //El módulo no está agregadado aún
            //                estructuraNueva.Modulo = element.Modulo;
            //                Model.EstructuraFuncion funcionNueva = new Model.EstructuraFuncion();
            //                estructuraNueva.Funciones = new List<Model.EstructuraFuncion>();
            //                funcionNueva.Escritura = element.Escritura;
            //                funcionNueva.Lectura = element.Lectura;
            //                funcionNueva.URL = element.URL;
            //                funcionNueva.Funcion = element.Funcion;
            //                estructuraNueva.Funciones.Add(funcionNueva);
            //            }                                                
            //            estructuraMenu.Add(estructuraNueva);
            //        });
            //        resultModificado.Value = result.Value;
            //        resultModificado.Message = result.Message;
            //        resultModificado.Data = estructuraMenu;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    result.Message = ex.Message;
            //}
            #endregion
            return Response.AsJson(resultModificado);
        }

        private object ActualizarEstatusUsuario(dynamic x)
        {

            Result result = new Result();

            try
            {
                int idUsuario = x.idUsuario;
                bool activar = x.activar;

                result = _DAUsuario.ActualizarEstatusUsuario(idUsuario, activar);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }
    }
}