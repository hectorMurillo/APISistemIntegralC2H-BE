using Models;
using Models.Porteros;
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

        public Result GuardarEntradasSalidas(EntradaSalidaModel entradaSalida , int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEquipo", ConexionDbType.Int, entradaSalida.codEquipo);
                parametros.Add("@pCodOperador", ConexionDbType.Int, entradaSalida.codOperador);
                parametros.Add("@pKilometraje", ConexionDbType.Decimal, entradaSalida.kilometraje);
                parametros.Add("@pHorometraje", ConexionDbType.Decimal, entradaSalida.horometraje);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pEntrada", ConexionDbType.Bit, entradaSalida.entrada);
                parametros.Add("@pNotaRemision", ConexionDbType.Int, entradaSalida.notaRemision);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, entradaSalida.observacion);
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

        public Result GuardarSuministros(SuministroModel suministros, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEquipo", ConexionDbType.Int, suministros.codEquipo);
                parametros.Add("@pCodOperador", ConexionDbType.Int, suministros.codOperador);
                parametros.Add("@pDiesel", ConexionDbType.Decimal, suministros.diesel);
                parametros.Add("@pAnticongelante", ConexionDbType.Decimal, suministros.anticongelante);
                parametros.Add("@pAceite", ConexionDbType.Decimal, suministros.aceite);
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

        public Result<List<EntradaSalida>> ObtenerEntradasSalidas(DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<EntradaSalida>> result = new Result<List<EntradaSalida>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<EntradaSalida>("ProcEquiposEntradasSalidasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Suministro>> ObtenerSuministros(DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<Suministro>> result = new Result<List<Suministro>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.ExecuteWithResults<Suministro>("ProcEquiposSuministrosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
