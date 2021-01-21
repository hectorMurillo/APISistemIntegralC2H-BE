using DA.C2H;
using Models.Vendedores;
using Nancy;
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
            Get("/todos", _ => ObtenerVendedores());
        }

        private object ObtenerVendedores()
        {
            Result<List<Vendedor>> result = new Result<List<Vendedor>>();
            try
            {
                result = _DAVendedor.ObtenerVendedores();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

    }
}