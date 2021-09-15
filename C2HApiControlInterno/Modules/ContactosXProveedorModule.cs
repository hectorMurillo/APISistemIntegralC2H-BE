using Models.Proveedores;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WarmPack.Classes;
using Model = Models.Proveedores;

namespace C2HApiControlInterno.Modules
{
    public class ContactosXProveedorModule : NancyModule
    {
        private readonly DA.C2H.DAContactosXProveedor _DACxP = null;

        public ContactosXProveedorModule() : base("/ContactosProv")
        {
            this.RequiresAuthentication();

            _DACxP = new DA.C2H.DAContactosXProveedor();

            Get("/{IdProveedor}", x => GetContactos(x));
            Get("/contacto/{IdContacto}", x => GetContacto(x));
            Post("/guardar", x => PostContacto());
            Post("/desactivar", _ => DesactivarContacto());
        }

        private object GetContactos(dynamic x)
        {
            Result result = new Result();

            try
            {
                int idProveedor = x.idProveedor == null ? 0 : x.idProveedor;
                result = _DACxP.consultaContactos(idProveedor);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object PostContacto()
        {
            Result result = new Result();
            try
            {
                var Contacto = this.Bind<Model.ContactosXProveedorModel>();
                result = _DACxP.GuardarContacto(Contacto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object DesactivarContacto()
        {
            Result result = new Result();
            try
            {
                var Contacto = this.Bind<Model.ContactosXProveedorModel>();
                result = _DACxP.ContactoDesactivar(Contacto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private object GetContacto(dynamic x)
        {

            Result result = new Result();

            try
            {
                int idContacto = x.idContacto == null ? 0 : x.idContacto;
                //Mando llamar al DA que manda llamar al stored y el resultado lo guardo en result
                result = _DACxP.consultaContacto(idContacto);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);

        }
    }
}