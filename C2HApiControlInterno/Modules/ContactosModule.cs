using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class ContactosModule:NancyModule
    {
        private readonly DA.C2H.DAContactos _DAContactos = null;
        public ContactosModule():base("/contactos")
        {
            
            _DAContactos = new DA.C2H.DAContactos();

            Get("/puestos-codigo/{codPuesto}", parametro => GetPuestosContacto(parametro));
            Get("/puestos-tipo/{tipo}", parametro => GetPuestoPorTipo(parametro));
            Post("/puestos/guardar", _ => GuardarPuestosContacto());
        }

        private object GetPuestoPorTipo(dynamic parametro)
        {
            Result<List<Models.Contactos.PuestosContactosModel>> result = new Result<List<Models.Contactos.PuestosContactosModel>>();
            try
            {
                string tipoPuesto = parametro.tipo;
                result = _DAContactos.consultaPuesto(0,tipoPuesto.Substring(0));
            }catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GuardarPuestosContacto()
        {
            Result result = new Result();
            try
            {
                var puestoContacto = this.Bind<Models.Contactos.PuestosContactosModel>();
                result = _DAContactos.GuardarPuestoContactos(puestoContacto);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        private object GetPuestosContacto(dynamic parametro)
        {
            Result<List<Models.Contactos.PuestosContactosModel>> result = new Result<List<Models.Contactos.PuestosContactosModel>>();

            try
            {
                int codPuesto = parametro.codPuesto;
                result = _DAContactos.consultaPuesto(codPuesto);
            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }
    }
}