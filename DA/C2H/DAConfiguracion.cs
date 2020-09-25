using Models;
using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAConfiguracion
    {
        private readonly Conexion _conexion = null;

        public DAConfiguracion()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

       
        public Result<List<UsuarioPermisoModel>> ConsultaUsuarios()
        {
            Result<List<UsuarioPermisoModel>> result = new Result<List<UsuarioPermisoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<UsuarioPermisoModel>("ProcCatUsuariosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }



        public Result<List<ModuloPermisoModel>> ObtenerModulosUsuario(int codigo)
        {
            Result<List<ModuloPermisoModel>> result = new Result<List<ModuloPermisoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.VarChar, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ModuloPermisoModel>("ProcCatModulosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
