using Models;
using Models.Configuracion;
using Models.Usuario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

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



        public Result<List<ModuloPermisoModel>> ObtenerModulos()
        {
            Result<List<ModuloPermisoModel>> result = new Result<List<ModuloPermisoModel>>();
            try
            {
                var parametros = new ConexionParameters();
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

        public Result<List<FuncionModel>> ObtenerFuncionesUsuario(int codModulo, int codUsuario)
        {
            Result<List<FuncionModel>> result = new Result<List<FuncionModel>>();
            try
            {
                //List<FuncionModel> Funciones = new List<FuncionModel>();
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pCodModulo", ConexionDbType.Int, codModulo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                //r = _conexion.RecordsetsExecute<FuncionModel>("ProcCatFuncionesCon", parametros);
                result = _conexion.ExecuteWithResults<FuncionModel>("ProcCatFuncionesCon", parametros);
            }
            catch (Exception ex)
            {
                result.Value = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public Result GuardarFuncionesUsuario(List<FuncionModel> datos, int codUsuario, int codSucursal)
        {
            string funciones = datos.ToXml("Funciones");
            var r = new Result();
            try
            {
                ConexionParameters parametros = new ConexionParameters();
                parametros.Add("@pFunciones", ConexionDbType.Xml, funciones);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pCodModulo", ConexionDbType.Int, codSucursal);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                r = _conexion.Execute("ProcFuncionesPermisosGuardar", parametros);
                return r;
            }
            catch (Exception e)
            {
                r.Value = false;
                r.Message = e.Message;
                return r;
            }
        }

    }
}