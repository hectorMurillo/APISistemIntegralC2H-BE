using Models.Usuario;
using System;
using System.Security.Claims;
using Nancy;

namespace C2HApiControlInterno
{
    public static class NancyExtensions
    {
        public static UsuarioModel BindUsuario(this NancyModule nancy)
        {
            var usuario = new UsuarioModel()
            {
                IdUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirstValue("idUsuario")),
                Usuario = nancy.Context.CurrentUser.FindFirstValue("usuario"),
                Nombre = nancy.Context.CurrentUser.FindFirstValue("nombre"),
                IdSubUsuario = Convert.ToInt32(nancy.Context.CurrentUser.FindFirstValue("idSubUsuario"))
            };

            return usuario;
        }
    }
}