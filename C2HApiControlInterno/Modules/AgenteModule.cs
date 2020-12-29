using Models.Clientes;
using Models.Dosificador;
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
            //this.RequiresAuthentication();

            _DAAgentesVentas = new DA.C2H.DAAgenteVentas();
            Get("/todosCombo", _ => GetTodos());
            Get("/notas-remision/{codAgente}", parametros => NotasRemisionAgente(parametros));
            Get("/notas-remision-detalle/{idNotaRemision}", parametros => DetalleNotasRemisionAgente(parametros));

        }

        private object NotasRemisionAgente(dynamic parametros)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                int codAgente = parametros.codAgente;
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