using Models;
using Models.Cobranza;
using Models.Dosificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DACobranza
    {

        private readonly Conexion _conexion = null;

        public DACobranza()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<DatosNotaRemision>> ObtenerNotasRemision()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcCobranzaNotasRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DatosNotaRemision>> ObtenerDatosNota(int folioNota)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotaRemision", ConexionDbType.Int, folioNota);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcNotaRemisionDatosCon", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }


        public Result<List<DatosNotaRemision>> ObtenerNotasRemision(int folioNota)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioNota);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcPedidoNotasRemisionCon", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }
       


        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionSurtiendo()
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("+", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionEntradasSalidas(bool entrada)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEntrada", ConexionDbType.Bit, entrada);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcCobranzaNotasRemisionEntradasSalidasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<NotaRemisionCobranza>> ObtenerNotasRemisionCobranza(DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<NotaRemisionCobranza>> result = new Result<List<NotaRemisionCobranza>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<NotaRemisionCobranza>("ProcCobranzaNotasDeRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarPagoNotaRemision(int idNotasRemisionEnc, decimal importeAbonar, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, idNotasRemisionEnc);
                parametros.Add("@pImportePagado", ConexionDbType.Decimal, importeAbonar);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCobranzaNotaRemisionPagoGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<NotaRemisionCobranza>> ObtenerNotaRemisionCobranza(int idNotasRemisionEnc)
        {
            Result<List<NotaRemisionCobranza>> result = new Result<List<NotaRemisionCobranza>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, idNotasRemisionEnc);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<NotaRemisionCobranza>("ProcCobranzaNotaDeRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DetalleAbonosNotaRemision>> ObtenerDetalleAbonosNotaRemision(int idNotasRemisionEnc)
        {
            Result<List<DetalleAbonosNotaRemision>> result = new Result<List<DetalleAbonosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotasRemisionEnc", ConexionDbType.Int, idNotasRemisionEnc);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DetalleAbonosNotaRemision>("ProcCobranzaDetalleAbonosNotaDeRemisionCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
