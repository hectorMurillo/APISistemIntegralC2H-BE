using API.Models;
using Microsoft.IdentityModel.Tokens;
using Models.Authentication;
using Models.Usuario;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WarmPack.App;
using WarmPack.Utilities;

namespace Models
{
    public class Globales
    {
        private static AppConfiguration _Configuracion;
        private static bool produccion = false;
        public static string CorreoAutomatico = "";
        public static string CorreoAutomaticoPassword = "";
        public static string Host = "";
        public static int Port = 0;
        public static string URLEncuesta = "";
        public static string URLWeb = "";
        public static string URLFacebook = "";
        public static string FolderPDF = "";
        public static string PathDB = "";
        public static string PathTemp = "";
        //var pathdirectorio = "c:\\pruebaprueba\\";
        //var pathdirectorio = "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
        //using (var db = new LiteDatabase("h:\\root\\home\\hector14-001\\www\\api\\temp\\refresh.db"))              


        public static void ObtenerInformacionGlobal()
        {
            FolderPDF = !produccion ? "c:\\pruebaprueba\\" : "h:\\root\\home\\hector14-001\\www\\api\\PRUEBAPRUEBA";
            PathDB = !produccion ? "c:\\temp\\refresh.db" : "h:\\root\\home\\hector14-001\\www\\api\\temp\\refresh.db";
            PathTemp = !produccion ? "c:\\temp\\" : "h:\\root\\home\\hector14-001\\www\\api\\temp\\";

            if (!(Directory.Exists(PathTemp)))
            {
                Directory.CreateDirectory(PathTemp);
            }
        }

        // opciones de configuracion        
        public static AppConfiguration Configuracion
        {
            get
            {
                if (_Configuracion == null)
                {
                    // moficar la semilla con precaución, solo al inicio o no se va a poder leer la configuración al menos que se modifique
                    _Configuracion = new AppConfiguration(new Encrypter("Ch4ng0s!!"));
                }

                return _Configuracion;
            }
            set
            {
                _Configuracion = value;
            }
        }

        // modificar la clave de seguridad del token        
        public static string Key => "C0NCR3T0S2H!!";


        // modificar el emisor del token si es necesario        
        public static string ValidIssuer = "http://www.concretos2h.com";


        // modificar las audencias del token
        public static List<String> ValidAudiences = new List<String> { "Sistema Agentes", "Pagina Web" };

        // modificar la cadena de conexion - fija solo para pruebas
        //public static string ConexionPrincipal => "data source = 172.19.1.22; initial catalog = sid; user id = sa; password = Difarmer01";
        //public static string ConexionPrincipal => @"data source=DESKTOP-PL5JBRK\SQLEXPRESS;initial catalog=C2HControlInterno;persist security info=True;user id=sa;password=1234;";
        //public static string ConexionPrincipal => @"data source=sql5052.site4now.net;initial catalog=DB_A55757_c2h;persist security info=True;user id=DB_A55757_c2h_admin;password=hector141093;";
        //public static string ConexionPrincipal => @"data source=DESKTOP-FDVVO85;initial catalog=DB_A55757_c2h;persist security info=True;user id=sa;password=123;";


        //BD PRUEBAS
<<<<<<< HEAD
        public static string ConexionPrincipal => @"Data Source=np:\\.\pipe\LOCALDB#A8F6612A\tsql\query;Initial Catalog=DB_A55757_prueba;Integrated Security=True
";
=======
        //public static string ConexionPrincipal => @"data source=SQL5081.site4now.net; initial catalog=DB_A55757_prueba; persist security info=True;user id=DB_A55757_prueba_admin; password=_C0NCR3T05D0SH;";
        
        public static string ConexionPrincipal => @"data source=SQL5061.site4now.net; initial catalog=db_a55757_v2; persist security info=True;user id=db_a55757_v2_admin; password=_C0NCR3T05D0SH;";
>>>>>>> MasterProduccion

        //BD LOCAL
        //public static string ConexionPrincipal => @"data source=DESKTOP-PL5JBRK\SQLEXPRESS;initial catalog=DB_A55757_prueba;persist security info=True;user id=sa;password=1234;";
        //public static string ConexionPrincipal => @"data source=HECTOR-MURILLO\TEW_SQLEXPRESS;initial catalog=DB_A55757_prueba;persist security info=True;user id=sa;password=1234;";


        public static string WebBannersPath => @"c:\inetpub\wwwroot\imagenes\banners\";


        // lo de aqui abajo es el manejo de los tokens, manejar con precaución ...

        public static byte[] KeyByteArray = Encoding.ASCII.GetBytes(Convert.ToBase64String(Encoding.ASCII.GetBytes(Globales.Key)));
        public static SymmetricSecurityKey SigningKey = new SymmetricSecurityKey(KeyByteArray);

        public static List<RefreshTokenItemModel> RefreshTokens = new List<RefreshTokenItemModel>();

        public static TokenResponseModel GetJwt(UsuarioModel usuario)
        {
            var now = DateTime.Now;
            const int expireMinutes = 518400;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Usuario),
                new Claim("idUsuario", usuario.IdUsuario.ToString()),
                new Claim("codEmpleado", usuario.CodEmpleado.ToString()),
                new Claim("usuario", usuario.Usuario),
                new Claim("nombre", usuario.Nombre),
                new Claim("codEmpleado",usuario.CodEmpleado.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
                issuer: Globales.ValidIssuer,
                audience: Globales.ValidAudiences?.FirstOrDefault(),
                claims: claims,
                notBefore: now,
                expires: now.Add(TimeSpan.FromMinutes(expireMinutes)),
                signingCredentials: new SigningCredentials(Globales.SigningKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new TokenResponseModel
            {
                AccessToken = encodedJwt,
                ExpiresIn = (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds,
                RefreshToken = Guid.NewGuid().ToString(),
                NombreAutenticado = usuario.Nombre
            };


            RefreshTokenRepository.Guardar(new RefreshTokenItemModel() { Usuario = usuario, Uid = response.RefreshToken });
            //Globales.RefreshTokens.RemoveAll(p => p.Usuario?.IdUsuario == usuario.IdUsuario);
            //Globales.RefreshTokens.Add(new RefreshTokenItemModel() { Usuario = usuario, Uid = response.RefreshToken });

            return response;
        }
    }
}