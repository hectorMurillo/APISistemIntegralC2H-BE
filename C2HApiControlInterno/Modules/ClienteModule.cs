﻿using Models.Clientes;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using WarmPack.Classes;
using Model = Models.Clientes;


namespace C2HApiControlInterno.Modules
{
    public class ClienteModule : NancyModule
    {
        private readonly DA.C2H.DAClientes _DAClientes = null;

        public ClienteModule() : base("/clientes")
        {
            //this.RequiresAuthentication();

            _DAClientes = new DA.C2H.DAClientes();
            Get("/todos", _ => GetTodos());
            Get("/clientes-combo", _ => GetClientesCombo());
            Get("/{codCliente}", x => GetCliente(x));
            Get("/sugerenciaUsuario/{codCliente}", x => GetSugerenciaUsuario(x));
            Get("/obtenerUsuarioCliente/{codCliente}", x => GetUsuario(x));
            Get("/direccion/{codDireccion}", x => GetDireccion(x));
            Post("/guardarUsuarioCliente", _ => postUsuarioCliente());
            Post("/guardar", _ => PostCliente());
            Post("/direcciones/guardar", _ => PostClienteDireccion());
            Post("/contactos/guardar", _ => PostClienteContacto());
            Post("/cliente-forzar/guardar", _ => PostGuardarNuevoCliente());
            Get("/clientes-agente/{codAgente}", x => ObtenerClientesAgente(x));
            Get("/clientes-detenidos", _ => GetClientesDetenidos());
            Get("/cobranza", _ => ObtenerClientesCobranza());
            Get("/actualizar-estatus/{codigo}/{activar}", x => ActualizarEstatusCliente(x));


            //
            Get("/tipos-cliente", _ => ObtenerTiposCliente());
            Get("/segmentos", _ => ObtenerSegmentos());
            Get("/tipos-cliente-credito", _ => ObtenerTiposClienteCredito());
            Get("/tipos-lista-precios", _ => ObtenerTiposListaPrecios());

            Get("/historial-cliente/{codCliente}/{fechaDesde}/{fechaHasta}", x => ObtenerHistorialCliente(x));



            Get("/obtener-clientes/{codCliente}", x => ObtenerClientes(x));


        }



        private object GetDireccion(dynamic x)
        {
            int codDireccion = x.codDireccion;

            Result<List<DireccionesXClientesModel>> result = new Result<List<DireccionesXClientesModel>>();

            try
            {
                //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
                result = _DAClientes.consultaDireccion(codDireccion);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

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
                //var cliente = this.Bind<Model.ClientesModel>();

                var parametro = this.BindModel();
                var lstFormulas = new ClientesModel();
                var cliente = parametro.cliente.toString();
                var documentos = parametro.documentos;
                JavaScriptSerializer js = new JavaScriptSerializer();
                ClientesModel blogObject = js.Deserialize<ClientesModel>(cliente);
        
                foreach (var element in cliente)
                {
                    lstFormulas.Codigo = element.codigo;
                    lstFormulas.Nombre = element.nombre;
                    lstFormulas.NombreCompleto = element.nombreCompleto;
                    lstFormulas.ApellidoP = element.apellidoP;
                    lstFormulas.ApellidoM = element.apellidoM;
                    lstFormulas.RFC = element.rFC;
                    lstFormulas.regimenFiscal = element.regimenFiscal;
                    lstFormulas.Alias = element.alias;
                    lstFormulas.Celular = element.celular;
                    lstFormulas.Correo = element.correo;
                    lstFormulas.NombreComercial = element.nombreComercial;
                    lstFormulas.RazonSocial = element.razonSocial;
                    lstFormulas.codVendedor = element.codEmpleadoVendedor;
                    lstFormulas.CodTipoCliente = element.codTipoCliente;
                    lstFormulas.CodSegmento = element.codSegmento;
                    lstFormulas.CodTipoClienteCredito = element.codTipoClienteCredito;
                    lstFormulas.CodTipoListaPrecio = element.codTipoListaPrecio;
                    lstFormulas.DiaRevision = element.diaRevision;
                    lstFormulas.FacturarPublicoGeneral = element.facturarPublicoGeneral;
                }

                foreach (var item in documentos)
                {
                    var test = "";
                }

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

        private object ObtenerClientesCobranza()
        {
            Result result = new Result();
            try
            {
                result = _DAClientes.ObtenerClientesCobranza();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ActualizarEstatusCliente(dynamic x)
        {

            Result result = new Result();

            try
            {
                int codigo = x.codigo;
                bool activar = x.activar;

                result = _DAClientes.ActualizarEstatusCliente(codigo, activar);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }

        private object ObtenerTiposCliente()
        {
            Result<List<TipoCliente>> result = new Result<List<TipoCliente>>();
            try
            {
                result = _DAClientes.ObtenerTiposCliente();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerSegmentos()
        {
            Result<List<Segmento>> result = new Result<List<Segmento>>();
            try
            {
                result = _DAClientes.ObtenerSegmentos();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposClienteCredito()
        {
            Result<List<TipoClienteCredito>> result = new Result<List<TipoClienteCredito>>();
            try
            {
                result = _DAClientes.ObtenerTiposClienteCredito();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposListaPrecios()
        {
            Result<List<TipoListaPrecio>> result = new Result<List<TipoListaPrecio>>();
            try
            {
                result = _DAClientes.ObtenerTiposListaPrecios();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerHistorialCliente(dynamic x)
        {
            Result result = new Result();
            try
            {
                DateTime fechaDesde = x.fechaDesde;
                DateTime fechaHasta = x.fechaHasta;
                int codCliente = x.codCliente;

                result = _DAClientes.ObtenerHistorialCliente(codCliente, fechaDesde, fechaHasta);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }


        private object ObtenerClientes(dynamic x)
        {
            Result<List<ClienteModel2>> result = new Result<List<ClienteModel2>>();
            int codCliente = x.codCliente;

            try
            {
                result = _DAClientes.ObtenerClientes(codCliente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}