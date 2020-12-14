using Models;
using Models.Operador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAOperador
    {
        private readonly Conexion _conexion = null;
        public DAOperador()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }
        public Result<List<Operador>> ObtenerOperadores()
        {
            Result<List<Operador>> result = new Result<List<Operador>>();
            try
            {
                var parametros = new ConexionParameters();
                //parametros.Add("@pCodEquipo", ConexionDbType.Int, codEquipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Operador>("ProcCatOperadoresCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
