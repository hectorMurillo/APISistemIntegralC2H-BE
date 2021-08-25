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
            Post("/tipos-insumo", _ => GuardarTipoInsumo());
            Post("/tipos-unidad-medida",_ => GuardarTipoUnidadMedida());

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