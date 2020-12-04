using Models;
using Models.Dosificador;
using Models.Equipos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DADosificador
    {
        private readonly Conexion _conexion = null;

        public DADosificador()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<int>> ObtenerUltimoFolioGinco()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcObtenerFolioGinco", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<int>> ObtenerUltimoFolioNR()
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcObtenerFolioNotaRemision", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        
        public Result<List<FormulaModel>> ObtenerFormulas()
        {
            Result<List<FormulaModel>> result = new Result<List<FormulaModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<FormulaModel>("ProcCatFormulasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<ObrasModel>> ObtenerObrasClientes(int codCliente)
        {
            Result<List<ObrasModel>> result = new Result<List<ObrasModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ObrasModel>("ProcCatObrasClientesCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<OperadorModel>> ObtenerOperadores(bool bombeable)
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, bombeable);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<OperadorModel>("ProcOperadoresCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<EquipoModel>> ObtenerEquipoOperador(bool bombeable,int codOperador)
        {
            Result<List<EquipoModel>> result = new Result<List<EquipoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, bombeable);
                parametros.Add("@pCodOperador", ConexionDbType.Int, codOperador);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<EquipoModel>("ProcEquipoOperadorCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarNotaRemision(NotaRemisionEncModel notaRemision, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioGinco", ConexionDbType.Int, notaRemision.FolioGinco);
                parametros.Add("@pFolioNotaRemision", ConexionDbType.Int, notaRemision.FolioNotaRemision);
                parametros.Add("@pHoraSalida", ConexionDbType.VarChar, notaRemision.HoraSalida);
                parametros.Add("@pCodCliente", ConexionDbType.Int, notaRemision.CodCliente);
                parametros.Add("@pCodObra", ConexionDbType.Int, notaRemision.CodObra);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, notaRemision.codVendedor);
                parametros.Add("@pCodFormula", ConexionDbType.Int, notaRemision.CodFormula);
                parametros.Add("@pCodOperador1", ConexionDbType.Int, notaRemision.CodOperador_1);
                parametros.Add("@pCodOperador2", ConexionDbType.Int, notaRemision.CodOperador_2);
                parametros.Add("@pCodEquipo_CR", ConexionDbType.Int, notaRemision.CodEquipo_CR);
                parametros.Add("@pCodEquipo_BB", ConexionDbType.Int, notaRemision.CodEquipo_BB);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, notaRemision.Cantidad);
                parametros.Add("@pEsBombeable", ConexionDbType.Bit, notaRemision.ChKBombeable);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pFibra", ConexionDbType.Bit, notaRemision.ChKFibra);
                parametros.Add("@pImper", ConexionDbType.Bit, notaRemision.ChKImper);
                parametros.Add("@pProducto", ConexionDbType.VarChar, notaRemision.Producto);

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcNotaRemisionGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<int>> GuardarFormula(FormulaModel formula)
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pNomenclatura", ConexionDbType.VarChar, formula.Nomenclatura );
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, formula.Descripcion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcCatFormulasGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
