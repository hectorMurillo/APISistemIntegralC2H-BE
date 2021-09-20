using Models.Proveedores;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WarmPack.Classes;
using Model = Models.Proveedores;


namespace C2HApiControlInterno.Modules
{
    public class ProveedorModule : NancyModule
    {
        private readonly DA.C2H.DAProveedor _DAProveedores = null;

        public ProveedorModule() : base("/proveedores")
        {
            this.RequiresAuthentication();

            _DAProveedores = new DA.C2H.DAProveedor();
            Get("/todos", x => GetTodos());
            Get("/{CodProveedor}", x => GetProveedor(x));
            Post("/guardar", _ => PostProveedor());
            Post("/desactivar", _ => DesactivarProveedor());
        }

            private object GetTodos()
            {

                Result result = new Result();

                try
                {

                    var codUsuario = this.BindUsuario().IdUsuario;
                    //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
                    result = _DAProveedores.ConsultaProveedores(codUsuario, 0);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return Response.AsJson(result);

            }

            private object GetProveedor(dynamic x)
            {
                Result result = new Result();
                try
                {
                    //Si no se ha logeado marcará error aqui
                    int codProveedor = x.codProveedor == null ? 0 : x.codProveedor;
                    var codUsuario = this.BindUsuario().IdUsuario;
                    if (codProveedor > 0)
                    {
                        var r = _DAProveedores.ConsultaProveedores(codUsuario, codProveedor);
                        //result.Data = r.Data.ElementAtOrDefault(0);
                        result.Data = r.Data;
                        result.Message = r.Message;
                        result.Value = r.Value;
                    }
                    else
                    {
                        this.GetTodos();
                        return 0;
                    }

                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return Response.AsJson(result);
            }



        private object PostProveedor()
            {
                Result<List<int>> result = new Result<List<int>>();
                try
                {
                    var Proveedor = this.Bind<Model.ProveedorModel>();
                    result = _DAProveedores.ProveedorGuardar(Proveedor);
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
                return Response.AsJson(result);
            }

        private object DesactivarProveedor()
        {
            Result result = new Result();
            try
            {
                var Proveedor = this.Bind<Model.ProveedorModel>();
                result = _DAProveedores.ProveedorDesactivar(Proveedor);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
    }

 