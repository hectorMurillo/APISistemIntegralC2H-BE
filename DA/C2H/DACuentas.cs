using Models;
using Models.Cuentas;
using Models.Bancos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Cuentas;
using ModelB = Models.Bancos;

namespace DA.C2H
{
   public class DACuentas
    {
        private readonly Conexion _conexion = null;
        public DACuentas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result consultaCuentas(int IdProveedor)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdProveedor", ConexionDbType.Int, IdProveedor);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcCatCuentasCon", parametros);

                var cuenta = _conexion.RecordsetsResults<Models.Cuentas.BancoXCuenta>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { cuenta }
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
                result.Data = null;
            }
            return result;
        }

        public Result consultaCuenta(int IdCuenta)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdCuenta", ConexionDbType.Int, IdCuenta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcCatCuentasCon", parametros);

                var cuenta = _conexion.RecordsetsResults<Models.Cuentas.BancoXCuenta>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { cuenta }
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
                result.Data = null;
            }
            return result;
        }

        public Result consultaBancosNombres()
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcCatNombresBancosCon", parametros);
                //result = _conexion.ExecuteWithResults<ModelB.BancoModel>("ProcCatNombresBancosCon", parametros);

                var bancos = _conexion.RecordsetsResults<Models.Bancos.BancoModel>();
                var bancosARegresar = new List<String>();
                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { bancos/*, direccionesXCliente, contactosXCliente */}
                };
            }


            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
                result.Data = null;          
            }
            return result;
        }
        public Result CuentaDesactivar(Model.CuentaModel Cuenta)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pIdCuenta", ConexionDbType.Int, Cuenta.IdCuenta);
                parametros.Add("@pActivado", ConexionDbType.Bit, Cuenta.Activado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatCuentasGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result CuentaGuardar(Model.CuentaModel Cuenta)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                
                Cuenta.FechaRegistro = DateTime.Now;
                parametros.Add("@pIdCuenta", ConexionDbType.Int, Cuenta.IdCuenta);
                parametros.Add("@pIdProveedor", ConexionDbType.Int, Cuenta.IdProveedor);
                parametros.Add("@pIdBanco", ConexionDbType.Int, Cuenta.IdBanco);
                parametros.Add("@pNumeroCuenta", ConexionDbType.VarChar, Cuenta.NumeroCuenta);
                parametros.Add("@pCLABE", ConexionDbType.VarChar, Cuenta.CLABE);
                parametros.Add("@pCBPrincipal", ConexionDbType.Bit, Cuenta.CBPrincipal);
                parametros.Add("@pFechaRegistro", ConexionDbType.DateTime, Cuenta.FechaRegistro);
              
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatCuentasGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
