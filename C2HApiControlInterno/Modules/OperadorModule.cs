﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DA.C2H;
using Models.Operador;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class OperadorModule : NancyModule
    {
        private readonly DAOperador _DAOperador = null;

        public OperadorModule() : base("/operadores")
        {
            this.RequiresAuthentication();

            _DAOperador = new DAOperador();
            Get("/todos", _ => ObtenerOperadores());
        }

        private object ObtenerOperadores()
        {
            Result<List<Operador>> result = new Result<List<Operador>>();
            try
            {
                //var codCliente = this.BindUsuario().IdUsuario;
                result = _DAOperador.ObtenerOperadores();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}