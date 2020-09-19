using Nancy;
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
            this.RequiresAuthentication();

            _DAAgentesVentas = new DA.C2H.DAAgenteVentas();
            Get("/todosCombo", _ => GetTodos());

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