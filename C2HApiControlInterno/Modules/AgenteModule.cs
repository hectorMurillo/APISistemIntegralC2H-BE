using Models.AgenteVentas;
using Models.Clientes;
using Models.Dosificador;
using Models.Pedidos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Model = Models.AgenteVentas;
namespace C2HApiControlInterno.Modules
{
    public class AgenteModule:NancyModule
    {
        private readonly DA.C2H.DAAgenteVentas _DAAgentesVentas = null;
        public AgenteModule() : base("/agentes")
        {
            //this.RequiresAuthentication();

            _DAAgentesVentas = new DA.C2H.DAAgenteVentas();
            Get("/todosCombo", _ => GetTodos());
            Get("/notas-remision/{codAgente}", parametros => NotasRemisionAgente(parametros));
            Get("/pedidos", _ => PedidosAgente());
            Get("/notas-remision-detalle/{idNotaRemision}", parametros => DetalleNotasRemisionAgente(parametros));

            Get("/clientes/{codAgente}", parametro => ClientesPorAgente(parametro));
            Get("/productos-cliente/{codCliente}/{codProducto}", parametros => ProductosXCliente(parametros));
            Post("/guardar-precio-productoXCliente", _ => GuardarPrecioProductoXCliente());
        }

        
        private object PedidosAgente()
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                int codAgente = this.BindUsuario().CodEmpleado;
                result = _DAAgentesVentas.ObtenerPedidos(codAgente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarPrecioProductoXCliente()
        {
            Result result = new Result();
            try
            {
                var producto = this.Bind<PrecioProductoModel>();
                int codAgente = this.BindUsuario().CodEmpleado;
                result = _DAAgentesVentas.GuardarPrecioProductoXCliente(producto, codAgente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message; 
            }
            return Response.AsJson(result);
        }
        private object ProductosXCliente(dynamic parametros)
        {
            Result<List<ProductosClienteModel>> result = new Result<List<ProductosClienteModel>>();
            try
            {
                int codAgente = this.BindUsuario().CodEmpleado;
                int codProducto = parametros.codProducto == null || parametros.codProducto == "undefined" ? 0 : parametros.codProducto;
                int codCliente = parametros.codCliente;
                result = _DAAgentesVentas.ObtenerProductosPorCliente(codAgente, codCliente,codProducto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ClientesPorAgente(dynamic parametros)
        {
            Result<List<ClientesModel>> result = new Result<List<ClientesModel>>();
            try
            {
                //int codAgente = this.BindUsuario().CodEmpleado;
                int codAgente = parametros.codAgente;
                result = _DAAgentesVentas.ObtenerClientesPorAgente(codAgente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object NotasRemisionAgente(dynamic parametros)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                //int codAgente = parametros.codAgente;
                int codAgente = this.BindUsuario().CodEmpleado;
                result = _DAAgentesVentas.ObtenerNotasRemisionAgente(codAgente);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object DetalleNotasRemisionAgente(dynamic parametros)
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                int idNotaRemision = parametros.idNotaRemision;
                result = _DAAgentesVentas.ObtenerDetalleClientesPorAgente(idNotaRemision);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
        

        private object GetTodos()
        {
            Result<List<Model.AgenteVentasCombo>> result = new Result<List<Model.AgenteVentasCombo>>();
            try
            {
                result = _DAAgentesVentas.ConsultaAgenteVentasCombo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}