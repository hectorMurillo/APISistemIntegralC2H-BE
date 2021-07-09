using DA.C2H;
using Nancy;
using Nancy.ModelBinding;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using WarmPack.Classes;
using Models.Noticias;

namespace C2HApiControlInterno.Modules
{
    public class NoticiasModule : NancyModule
    {
        private readonly DANoticias _DANoticias = null;

        public NoticiasModule() : base("/noticias")
        {
            _DANoticias = new DANoticias();
            Get("/todas", _ => ObtenerNoticias());
            Post("/registrarNoticia", _ => registrarNoticia());
        }

        private object ObtenerNoticias()
        {
            Result<List<Noticias>> result = new Result<List<Noticias>>();

            try
            {
                result = _DANoticias.ObtenerNoticias();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Response.AsJson(result);
        }

        private dynamic registrarNoticia()
        {
            try
            {
                Result result = new Result();

                var codUsuario = this.BindUsuario().IdUsuario;
                int idNoticias = (int)this.Request.Form.idNoticias;
                string titulo = (string)this.Request.Form.titulo;
                string descripcion = (string)this.Request.Form.descripcion;
                var Files = this.Request.Files;
                byte[] buffer = new byte[0];
                bool vieneImagen = false;

                if(Files.Count() > 0)
                {
                    var ms = new MemoryStream();
                    string filePath = Path.Combine(new DefaultRootPathProvider().GetRootPath(), " / " + Files.ElementAt(0).Name);
                    Files.ElementAt(0).Value.CopyTo(ms);
                    buffer = ms.ToArray();
                    vieneImagen = true;
                }

                result = _DANoticias.RegistrarNoticia(idNoticias, titulo, descripcion, buffer, codUsuario, vieneImagen);

                return Response.AsJson(result);
            }
            catch(Exception ex)
            {
                return Response.AsJson(new Result());
            }
        }



    }
}