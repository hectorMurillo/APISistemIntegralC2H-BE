using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Usuario;
namespace DA.C2H
{
    public class DAUsuario
    {
        private readonly Conexion _conexion = null;

        public DAUsuario()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        //public Result<List<UsuarioMenuPrincipalModel>> MenuPrincipal(int idUsuario)
        //{
        //    var parametros = new ConexionParameters();
        //    parametros.Add("@pIdUsuario", ConexionDbType.VarChar, idUsuario);            
        //    parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
        //    parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

        //    var r = _conexion.ExecuteWithResults<UsuarioMenuPrincipalModel>("procMobileMenuPermisos", parametros);

        //    return r;
        //}
        public Result<List<Model.Menu>> ConsultaMenuPermisos(int CodUsuario)
        {
            Result<List<Model.Menu>> result = new Result<List<Model.Menu>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.Int, CodUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.Menu>("ProcObtenerMenuPermiso", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<Model.UsuarioModel>> ConsultaUsuarios()
        {
            Result<List<Model.UsuarioModel>> result = new Result<List<Model.UsuarioModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.UsuarioModel>("ProcCatUsuariosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
