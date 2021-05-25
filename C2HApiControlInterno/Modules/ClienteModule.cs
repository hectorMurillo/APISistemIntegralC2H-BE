using Models.Clientes;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Model = Models.Clientes;


namespace C2HApiControlInterno.Modules
{
    public class ClienteModule : NancyModule
    {
        private readonly DA.C2H.DAClientes _DAClientes = null;

        public ClienteModule() : base("/clientes")
        {
            this.RequiresAuthentication();

            _DAClientes = new DA.C2H.DAClientes();
            Get("/todos", _ => GetTodos());
            Get("/clientes-combo", _ => GetClientesCombo());
            Get("/{codCliente}", x => GetCliente(x));
            Get("/sugerenciaUsuario/{codCliente}", x => GetSugerenciaUsuario(x));
            Get("/obtenerUsuarioCliente/{codCliente}", x => GetUsuario(x));
            Post("/guardarUsuarioCliente", _ => postUsuarioCliente());
            Post("/guardar", _ => PostCliente());
            Post("/direcciones/guardar", _ => PostClienteDireccion());
            Post("/contactos/guardar", _ => PostClienteContacto());
            Post("/cliente-forzar/guardar", _ => PostGuardarNuevoCliente());
            Get("/clientes-agente/{codAgente}", x => ObtenerClientesAgente(x));
            Get("/clientes-detenidos", _ => GetClientesDetenidos());            

        }        

        private object PostClienteContacto()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var contacto = this.Bind<Model.ContactoXClienteModel>();
                result = _DAClientes.ContactoGuardar(contacto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        private object GetClientesCombo()
        {
            Result<List<ClientesModel>> result = new Result<List<ClientesModel>>();
            try
            {
                result = _DAClientes.ClientesCombo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetCliente(dynamic x)
        {
            Result result = new Result();
            try
            {
                //Si no se ha logeado marcará error aqui
                int codCliente = x.codCliente == null ? 0 : x.codCliente;
                var codUsuario = this.BindUsuario().IdUsuario;
                if (codCliente > 0)
                {
                    var r = _DAClientes.ConsultaClientes(codUsuario, codCliente);
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

        private object GetUsuario(dynamic x)
        {
            Result<List<Model.UsuarioClienteModel>> result = new Result<List<Model.UsuarioClienteModel>>();
            try
            {
                int codCliente = x.codCliente;
                result = _DAClientes.consultarUsuarioCliente(codCliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object postUsuarioCliente()
        {
            Result result = new Result();
            try
            {
                var usuarioCte = this.Bind<Model.UsuarioClienteModel>();
                result = _DAClientes.UsuarioClienteGuardar(usuarioCte);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostGuardarNuevoCliente()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var cliente = this.Bind<Model.ClientesModel>();
                result = _DAClientes.ClienteGuardarForzar(cliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetSugerenciaUsuario(dynamic x)
        {
            Result<List<Model.UsuarioClienteModel>> result = new Result<List<Model.UsuarioClienteModel>>();
            try
            {
                int codCliente = x.codCliente;
                result = _DAClientes.consultaUsuarioSugerido(codCliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostCliente()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var cliente = this.Bind<Model.ClientesModel>();
                result = _DAClientes.ClienteGuardar(cliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostClienteDireccion()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var direccion = this.Bind<Model.DireccionesXClientesModel>();
                result = _DAClientes.DireccionesGuardar(direccion);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetTodos()
        {
            Result result = new Result();
            try
            {
                //Si no se ha logeado marcará error aqui
                var codUsuario = this.BindUsuario().IdUsuario;

                result = _DAClientes.ConsultaClientes(codUsuario, 0);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        //
        private object ObtenerClientesAgente(dynamic x)
        {
            Result<List<ClientesModel>> result = new Result<List<ClientesModel>>();
            int codAgente = x.codAgente;

            try
            {
                result = _DAClientes.ObtenerClientesAgente(codAgente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetClientesDetenidos()
        {
            Result<List<ClientesModel>> result = new Result<List<ClientesModel>>();
            try
            {
                result = _DAClientes.ClientesDetenidos();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}