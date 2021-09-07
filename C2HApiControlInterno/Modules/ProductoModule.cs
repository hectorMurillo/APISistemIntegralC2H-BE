using DA.C2H;
using Models.Productos;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class ProductoModule : NancyModule
    {
        private readonly DAProductos _DAProductos = null;

        public ProductoModule() : base("/producto")
        {
            this.RequiresAuthentication();

            _DAProductos = new DAProductos();
            Get("/tipos-unidad-medida/{codigo}", parametros => ObtenerTiposUnidadMedida(parametros));
            Get("/tipos-insumos/{codigo}", parametros => ObtenerTiposInsumos(parametros));
            Get("/insumos/{codigo}", parametros => ObtenerInsumos(parametros));
            Get("/tipo-producto/{codigo}", parametros => ObtenerTiposProducto(parametros));
            Get("/tipo-resistencia-concreto/{codigo}", parametros => ObtenerTiposResistenciaContreto(parametros));

            Post("/producto", _ => GuardarProducto());
            Post("/insumo", _ => GuardarInsumo());
            Post("/tipos-insumo", _ => GuardarTipoInsumo());
            Post("/tipos-unidad-medida",_ => GuardarTipoUnidadMedida());
        }

        private object GuardarProducto()
        {
            Result result = new Result();
            try
            {

                var producto = this.Bind<ProductoModel>();
                result = _DAProductos.GuardarProducto(producto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposResistenciaContreto(dynamic parametros)
        {
            Result<List<TipoResistenciaCModel>> result = new Result<List<TipoResistenciaCModel>>();
            try
            {
                int codigo = parametros.codigo;
                result = _DAProductos.ObtenerTiposResistenciaContreto(codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposProducto(dynamic parametros)
        {
            Result<List<TipoProductoModel>> result = new Result<List<TipoProductoModel>>();
            try
            {
                int codigo = parametros.codigo;
                result = _DAProductos.ObtenerTiposProducto(codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarInsumo()
        {
            Result result = new Result();
            try
            {

                var insumo = this.Bind<InsumoModel>();
                result = _DAProductos.GuardarInsumo(insumo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerInsumos(dynamic parametros)
        {
            Result<List<InsumoModel>> result = new Result<List<InsumoModel>>();
            try
            {
                int codigo = parametros.codigo;
                result = _DAProductos.ObtenerInsumos(codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarTipoInsumo()
        {
            Result result = new Result();
            try
            {

                var tipoInsumo = this.Bind<TipoInsumoModel>();
                result = _DAProductos.GuardarTipoInsumo(tipoInsumo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposInsumos(dynamic parametros)
        {
            Result<List<TipoInsumoModel>> result = new Result<List<TipoInsumoModel>>();
            try
            {
                int codigo = parametros.codigo;
                result = _DAProductos.ObtenerTipoInsumos(codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarTipoUnidadMedida()
        {
            Result result = new Result();
            try
            {
           
                var tipoUnidadMedida = this.Bind<TipoUnidadMedidaModel>();
                result = _DAProductos.GuardarUnidadMedida(tipoUnidadMedida);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object ObtenerTiposUnidadMedida(dynamic parametros)
        {
            Result<List<TipoUnidadMedidaModel>> result = new Result<List<TipoUnidadMedidaModel>>();
            try
            {
                int codigo = parametros.codigo;
                result = _DAProductos.ObtenerTipoUnidadesMedida(codigo);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}