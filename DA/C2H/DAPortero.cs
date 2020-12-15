using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAPortero
    {
        private readonly Conexion _conexion = null;

        public DAPortero()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result GuardarEntradasSalidas(int codEquipo, int codOperador, decimal kilometraje, decimal horometraje, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEquipo", ConexionDbType.Int, codEquipo);
                parametros.Add("@pCodOperador", ConexionDbType.Int, codOperador);
                parametros.Add("@pKilometraje", ConexionDbType.Decimal, kilometraje);
                parametros.Add("@pHorometraje", ConexionDbType.Decimal, horometraje);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcEquiposEntradasSalidasGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return result;
        }

        public Result GuardarSuministros(int codEquipo, int codOperador, decimal diesel, decimal anticongelante, decimal aceite, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEquipo", ConexionDbType.Int, codEquipo);
                parametros.Add("@pCodOperador", ConexionDbType.Int, codOperador);
                parametros.Add("@pDiesel", ConexionDbType.Decimal, diesel);
                parametros.Add("@pAnticongelante", ConexionDbType.Decimal, anticongelante);
                parametros.Add("@pAceite", ConexionDbType.Decimal, aceite);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcEquiposSuministrosGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return result;
        }
    }
}
