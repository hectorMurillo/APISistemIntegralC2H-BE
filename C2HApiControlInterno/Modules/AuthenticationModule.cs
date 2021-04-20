using API.Models;
using DA.Authentication;
using DA.C2H;
using Models;
using Models.Authentication;
using Nancy;
using Nancy.ModelBinding;
//using C2H.Web.API.DA.Authentication;
//using C2H.Web.API.Models;
//using C2H.Web.API.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using WarmPack.Classes;

namespace C2HApiControlInterno.Modules
{
    public class AuthenticationModule : NancyModule
    {
        private readonly DAAuthentication _DAAuthentication = null;

        public AuthenticationModule() : base("/seguridad")
        {
            _DAAuthentication = new DAAuthentication();
          
            // bloque de seguridad aqui
            Post("/login", _ => PostLogin());

            Post("/logout", _ => PostLogout());

            Post("/renovar-token", _ => PostRenovarToken());            
            Post("/verificaPermisoURL/{url}/{codUsuario}", x => postVerificaPermisoURL(x));
            Post("/verificar-nombre-usuario", _ => postVerificarNombreUsuario());
            //Post("/recuperar-password-correo", _ => postRecuperarPasswordEnviaCorreo());
            //Post("/recuperar-password-verificar", _ => postRecuperarPasswordEnviaVerifica());
            //Post("/recuperar-password-restablecer", _ => postRecuperarPasswordEnviaRestablecer());
        }


        //private object postRecuperarPasswordEnviaRestablecer()
        //{
        //    var usuario = this.BindModel();
        //    string referencia = usuario.referencia;
        //    string password = usuario.password;

        //    var r = _DAAuthentication.postRecuperarPasswordEnviaRestablecer(referencia, password);

        //    return Response.AsJson(r);
        //}

        //private object postRecuperarPasswordEnviaVerifica()
        //{
        //    var usuario = this.BindModel();
        //    string referencia = usuario.referencia;

        //    var r = _DAAuthentication.postRecuperarPasswordEnviaVerificar(referencia);

        //    return Response.AsJson(r);
        //}

        //private object postRecuperarPasswordEnviaCorreo()
        //{
        //    var usuario = this.BindModel();
        //    string idUsuario = usuario.IdUsuario;

        //    var r = _DAAuthentication.postRecuperarPasswordEnviaCorreo(idUsuario);

        //    return Response.AsJson(r);
        //}
        private object postVerificaPermisoURL(dynamic x)
        {
            //var p = this.BindModel();
            //var codUsuario = this.BindUsuario().IdUsuario;
            //string url = p.uRL;

            string url = x.url;
            int codUsuario = x.codUsuario;

            var r = _DAAuthentication.verificaPermisoURL(codUsuario, url);
            return Response.AsJson(r);
            //return Response.AsJson(new Result(r.Value, r.Message));
        }
        //private object PostLogin()
        //{
        //    var credenciales = this.Bind<CredencialesModel>();

        //    // validar el usuario aqui, modificar el store que se manda llamar dentro del método Login
        //    var r = _DAAuthentication.Login(credenciales);

        //    // insertar el accessToken aqui
        //    if (r.Value)
        //    {
        //        var accessToken = Globales.GetJwt(r.Data);

        //        return Response.AsJson(new Result(r.Value, r.Message, accessToken));
        //    }

        //    return Response.AsJson(r);

        //}

        private object PostLogin()
        {
            try
            {
                var credenciales = this.Bind<CredencialesModel>();
                //EnviarCorreo env = new EnviarCorreo();
                //env.SendMail("Este es un mensaje de prueba","a");
                // validar el usuario aqui, modificar el store que se manda llamar dentro del método Login
                var r = _DAAuthentication.Login2(credenciales);

                if (r.Value)
                {
                    return Response.AsJson(new Result(r.Value, r.Message, r.Data));
                }

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {

                return Response.AsJson(ex);
            }
        }

        private object PostLogout()
        {
            try
            {
                var p = this.BindModel();

                string refreshToken = p.refresh_token;

                // borrar el refreshToken para que no se puedan solicitar mas tokens
                Globales.RefreshTokens.RemoveAll(t => t.Uid == refreshToken);

                return Response.AsJson(new Result(true, "Se ha completado el comando correctamente"));
            }
            catch (Exception ex)
            {

                return Response.AsJson(ex);
            }
          
        }

        private object PostRenovarToken()
        {
            try
            {
                var p = this.BindModel();

                string refreshToken = p.refresh_token;

                //var token = Globales.RefreshTokens.SingleOrDefault(x => x.Uid == refreshToken);
                var token = RefreshTokenRepository.Encontrar(refreshToken);
                if (token != null)
                {
                    var tokenNew = Globales.GetJwt(token.Usuario);

                    return Response.AsJson(tokenNew);
                }
                else
                {
                    return Response.AsJson("Token inválido", HttpStatusCode.Unauthorized);
                }
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex);
            }
           
        }

        private object postVerificarNombreUsuario()
        {
            try
            {
                var usuario = this.BindModel();
                string idUsuario = usuario.IdUsuario;

                var r = _DAAuthentication.postVerificarNombreUsuario(idUsuario);

                return Response.AsJson(r);
            }
            catch (Exception ex)
            {
                return Response.AsJson(ex);
            }
          
        }
    }
}