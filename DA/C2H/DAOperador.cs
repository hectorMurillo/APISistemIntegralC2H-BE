using Models;
using Models.Operador;
using Models.Operadores;
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

        public Result<List<Operador>> ObtenerOperadores(int codOperador)
        {
            Result<List<Operador>> result = new Result<List<Operador>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodOperador", ConexionDbType.Int, codOperador);
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

        public Result<List<Operador>> ObtenerOperadoresEntradasSalidas(bool entrada)
        {
            Result<List<Operador>> result = new Result<List<Operador>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEntrada", ConexionDbType.Bit, entrada);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Operador>("ProcCatOperadoresEntradasSalidasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<OperadorTipo>> ObtenerTiposOperadores()
        {
            Result<List<OperadorTipo>> result = new Result<List<OperadorTipo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<OperadorTipo>("ProcCatOperadoresTiposCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarOperador(OperadorModel operador, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, operador.codigo);
                parametros.Add("@pNombre", ConexionDbType.VarChar, operador.nombre);
                parametros.Add("@pApellidoP", ConexionDbType.VarChar, operador.apellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, operador.apellidoM);
                parametros.Add("@pRFC", ConexionDbType.VarChar, operador.rFC);
                parametros.Add("@pCodigoTipoEmpleado", ConexionDbType.Int, operador.codigoTipoEmpleado);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, operador.correo);
                parametros.Add("@pCelular", ConexionDbType.VarChar, operador.celular);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, operador.estatus);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcOperadoresGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
