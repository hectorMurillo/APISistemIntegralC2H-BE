using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Model = Models.Empresas;
namespace C2HApiControlInterno.Modules
{
    public class EmpresaModule:NancyModule
    {
        private readonly DA.C2H.DAEmpresas _DAEmpresa = null;
        public EmpresaModule():base("/empresas")
        {
            this.RequiresAuthentication();

            _DAEmpresa = new DA.C2H.DAEmpresas();

            Get("/combo", _ => GetTodosCombo());
            
        }

        private object GetTodosCombo()
        {
            Result<List<Model.EmpresaCombo>> result = new Result<List<Model.EmpresaCombo>>();
            try
            {
                result = _DAEmpresa.consultarEmpresasCombo();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}