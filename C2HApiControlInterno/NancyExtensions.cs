using Models.Usuario;
using System;
using System.Security.Claims;
using Nancy;
using Newtonsoft.Json;
using System.IO;

namespace C2HApiControlInterno
{
    public static class NancyExtensions
    {
        //public static UsuarioModel BindUsuario(this NancyModule nancy)
        //{
        //    var usuario = new UsuarioModel()
        //    {
        //        IdUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirstValue("idUsuario")),
        //        Usuario = nancy.Context.CurrentUser.FindFirstValue("usuario"),
        //        Nombre = nancy.Context.CurrentUser.FindFirstValue("nombre"),
        //        //IdSubUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirstValue("idSubUsuario"))
        //    };

        //    return usuario;
        //}

        //public static dynamic BindModel(this NancyModule context)
        //{
        //    var serializer = new JsonSerializer();

        //    using (var sr = new StreamReader(context.Request.Body))
        //    {
        //        using (var jsonTextReader = new JsonTextReader(sr))
        //        {
        //            return serializer.Deserialize(jsonTextReader);
        //        }
        //    }
        //}

 

        public static UsuarioModel BindUsuario(this NancyModule nancy)
        {
            if (nancy.Context.CurrentUser == null) return new UsuarioModel() { Usuario = "Usuario desconocido", IdUsuario = 0, Nombre = "No hay ningun usuario logeado" };


            var usuario = new UsuarioModel()
            {
                IdUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirst("idUsuario")?.Value),
                CodEmpleado = Convert.ToInt32(nancy.Context.CurrentUser.FindFirst("codEmpleado")?.Value),
                Usuario = nancy.Context.CurrentUser.FindFirst("usuario")?.Value,
                Nombre = nancy.Context.CurrentUser.FindFirst("nombre")?.Value,
                //IdSubUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirst("idSubUsuario").Value)
            };

            return usuario;
        }
    }
}


//public static class NancyExtensions
//{
//    public static dynamic BindModel(this NancyModule context)
//    {
//        var serializer = new JsonSerializer();

//        using (var sr = new StreamReader(context.Request.Body))
//        {
//            using (var jsonTextReader = new JsonTextReader(sr))
//            {
//                return serializer.Deserialize(jsonTextReader);
//            }
//        }
//    }

//    public static UsuarioModel BindUsuario(this NancyModule nancy)
//    {
//        if (nancy.Context.CurrentUser == null) return new UsuarioModel() { IdSubUsuario = 0, Usuario = "Usuario desconocido", IdUsuario = 0, Nombre = "No hay ningun usuario logeado" };

//        var usuario = new UsuarioModel()
//        {
//            IdUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirst("idUsuario")?.Value),
//            Usuario = nancy.Context.CurrentUser.FindFirst("usuario")?.Value,
//            Nombre = nancy.Context.CurrentUser.FindFirst("nombre")?.Value,
//            IdSubUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirst("idSubUsuario").Value)
//        };

//        return usuario;
//    }
//}