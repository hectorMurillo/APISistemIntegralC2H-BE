using Models;
using Models.Authentication;
using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.Authentication

{
    public class DAAuthentication
    {
        private readonly Conexion _conexion = null;

        public DAAuthentication()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<UsuarioModel> Login(CredencialesModel credenciales)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pNombreUsuario", ConexionDbType.VarChar, credenciales.Usuario);
            //parametros.Add("@pCodSubUsuario", ConexionDbType.VarChar, credenciales.IdSubUsuario);
            parametros.Add("@pPassword", ConexionDbType.VarChar, credenciales.Password);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = new Result<UsuarioModel>();

            _conexion.ExecuteWithResults("ProcIdentificaUsuario", parametros, row =>
            {
                r.Data = new UsuarioModel();

                r.Data.Usuario = row["Usuario"].ToString();
                r.Data.CodEmpleado = row["CodEmpleado"].ToInt32();
                //r.Data.IdSubUsuario = row["SubUsuario"].ToInt32();
                r.Data.IdUsuario = row["Codigo"].ToInt32();
                r.Data.Nombre = row["Nombre"].ToString();
            });

            r.Value = parametros.Value("@pResultado").ToBoolean();
            r.Message = parametros.Value("@pMsg").ToString();

            return r;
        }

        public Result<List<UsuarioModel>> postVerificarNombreUsuario(string idUsuario)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pIdUsuario", ConexionDbType.VarChar, idUsuario);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteWithResults<UsuarioModel>("procWebVerificarNombreUsuario", parametros);

            return r;
        }

        public Result<string> postRecuperarPasswordEnviaCorreo(string idUsuario)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pUsuario", ConexionDbType.VarChar, idUsuario);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteScalar<string>("procWebRecuperarPasswordCorreo", parametros);

            return r;
        }

        public Result<bool> verificaPermiso(int codUsuario, string modulo)
        {

            try
            {
                string query = $"SELECT dbo.FnVerificaPermiso({codUsuario},'{modulo}')";
                var r = _conexion.ExecuteScalar<bool>(query);
                return r;
            }
            catch (Exception ex)
            {
                var r = new Result<bool>();
                r.Value = false;
                r.Message = ex.Message;
                return r;
            }
        }

        public Result<string> postRecuperarPasswordEnviaVerificar(string referencia)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pReferencia", ConexionDbType.VarChar, referencia);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteScalar<string>("procWebRecuperarPasswordVerificar", parametros);

            return r;
        }

        public Result postRecuperarPasswordEnviaRestablecer(string referencia, string password)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pReferencia", ConexionDbType.VarChar, referencia);
            parametros.Add("@pPassword", ConexionDbType.VarChar, password);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.Execute("procWebRecuperarPasswordRestablecer", parametros);

            return r;
        }

        public Result<List<TipoPermiso>> verificaPermisoURL(int idUsuario, string url)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pCodUsuario", ConexionDbType.VarChar, idUsuario);
            parametros.Add("@pURL", ConexionDbType.VarChar, url);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteWithResults<TipoPermiso>("ProcVerificaPermisoURL", parametros);
            return r;
        }


        public Result Login2(CredencialesModel credenciales)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pNombreUsuario", ConexionDbType.VarChar, credenciales.Usuario);
            //parametros.Add("@pCodSubUsuario", ConexionDbType.VarChar, credenciales.IdSubUsuario);
            parametros.Add("@pPassword", ConexionDbType.VarChar, credenciales.Password);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.RecordsetsExecute("ProcIdentificaUsuario", parametros);

            if (r)
            {
                var datosUsuario = _conexion.RecordsetsResults<UsuarioModel>()?.FirstOrDefault();

                if (datosUsuario != null)
                {
                    var accessToken = Globales.GetJwt(datosUsuario);


                    var permisosUsuario = _conexion.RecordsetsResults<UsuarioPermiso>();


                    return new Result(
                        parametros.Value("@pResultado").ToBoolean(),
                        parametros.Value("@pMsg").ToString(),
                        new
                        {
                            user = datosUsuario,
                            accessToken = accessToken,
                            permisos = permisosUsuario
                        });
                }


            }
            return new Result(false, parametros.Value("@pMsg").ToString());
        }
    }
}