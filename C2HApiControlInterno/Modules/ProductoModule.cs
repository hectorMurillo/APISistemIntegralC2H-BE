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
            //this.RequiresAuthentication();

            _DAProductos = new DAProductos();
            Get("/tipos-unidad-medida/{codigo}", parametros => ObtenerTiposUnidadMedida(parametros));
            Post("/tipos-unidad-medida",_ => GuardarTipoUnidadMedida());

        }

        private object GuardarTipoUnidadMedida()
        {
            Result result = new Result();
            try
            {
           
                var tipoUnidadMedida = this.Bind<TipoUnidadMedidaModel>();
                result = _DAProductos.Guardar(tipoUnidadMedida);
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