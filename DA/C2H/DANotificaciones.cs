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
    public class DANotificaciones
    {
        private readonly Conexion _conexion = null;
        public DANotificaciones()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }
        public Result<List<NotificacionModel>> UltimasNotificaciones(int codUsuario)
        {
            var parametros = new ConexionParameters();
            parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            _conexion.ConexionTimeOut = 120;

            var r = _conexion.ExecuteWithResults<NotificacionModel>("ProcNotificacionesCon", parametros);

            return r;
        }

        public Result NotificacionesRedireccionadas(int codUsuario, int codNotificacion, bool todasLeidas)
        {
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCliente", ConexionDbType.Int, codUsuario);
                parametros.Add("@pTodasLeidas", ConexionDbType.Bit, todasLeidas);
                parametros.Add("@pCodNotificacion", ConexionDbType.Int, codNotificacion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.ConexionTimeOut = 120;
                var r = _conexion.Execute("procNotificacionMarcarRedireccionado", parametros);

                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }

        public Result NotificacionesLeidas(int codUsuario)
        {
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCliente", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.ConexionTimeOut = 120;
                var r = _conexion.Execute("procNotificacionMarcarVisto", parametros);

                return r;
            }
            catch (Exception ex)
            {
                return new Result(ex);
            }
        }
    }
}
