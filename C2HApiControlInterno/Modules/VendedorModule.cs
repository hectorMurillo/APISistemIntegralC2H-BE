using DA.C2H;
using Models.Vendedores;
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
    public class VendedorModule : NancyModule
    {
        private readonly DAVendedor _DAVendedor = null;

        public VendedorModule() : base("/vendedores")
        {
            this.RequiresAuthentication();

            _DAVendedor = new DAVendedor();
            Get("/todos/{codVendedor}", x => ObtenerVendedores(x));
            Post("guardar", _ => GuardarVendedor());

        }

        private object ObtenerVendedores(dynamic x)
        {
            Result<List<Vendedor>> result = new Result<List<Vendedor>>();
            try
            {
                int codVendedor = x.codVendedor == null ? 0 : x.codVendedor;

                result = _DAVendedor.ObtenerVendedores(codVendedor);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarVendedor()
        {
            Result result = new Result();
            try
            {
                var codUsuario = this.BindUsuario().IdUsuario;
                var vendedor = this.Bind<VendedorModel>();
                result = _DAVendedor.GuardarVendedor(vendedor, codUsuario);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}