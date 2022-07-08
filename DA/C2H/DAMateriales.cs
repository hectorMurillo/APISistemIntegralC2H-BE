using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Materiales;
namespace DA.C2H
{
    public class DAMateriales
    {
        private readonly Conexion _conexion = null;
        public DAMateriales()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Model.TipoResistencia>> consultaTipoResistencia(int codTipoResistencia)
        {
            Result<List<Model.TipoResistencia>> result = new Result<List<Model.TipoResistencia>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigoTipo", ConexionDbType.VarChar, codTipoResistencia);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TipoResistencia>("ProcCatTipoResistenciaCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.TipoObra>> consultaTipoObras()
        {
            Result<List<Model.TipoObra>> result = new Result<List<Model.TipoObra>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TipoObra>("ProcTiposObrasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.TMA>> consultaTMA(int codTMA)
        {
            Result<List<Model.TMA>> result = new Result<List<Model.TMA>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigoTMA", ConexionDbType.VarChar, codTMA);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TMA>("ProcCatTMA", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.EspecificacionesConcreto>> consultaEspecificaciones(int codEspecificacion)
        {
            Result<List<Model.EspecificacionesConcreto>> result = new Result<List<Model.EspecificacionesConcreto>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigoEspecificacion", ConexionDbType.VarChar, codEspecificacion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.EspecificacionesConcreto>("ProcEspecificacionesConcretoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarTipoResistencia(Model.TipoResistencia tipoResistencia)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodTipoResistencia", ConexionDbType.Int, tipoResistencia.Codigo);
                parametros.Add("@pDescripcionCorta", ConexionDbType.VarChar, tipoResistencia.DescripcionCorta);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, tipoResistencia.Descripcion);
                parametros.Add("@pCodTipoConcreto", ConexionDbType.Int, tipoResistencia.CodTipoConcreto);
                parametros.Add("@pEstatus", ConexionDbType.Bit, tipoResistencia.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcTipoResistenciaGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarTipoConcreto(Model.TipoConcreto tipoConcreto)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodTipoConcreto", ConexionDbType.Int, tipoConcreto.Codigo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, tipoConcreto.Descripcion);
                parametros.Add("@pDescripcionCorta", ConexionDbType.VarChar, tipoConcreto.DescripcionCorta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcTipoConcretoGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        

        public Result GuardarProductosAgregados(int codigo, decimal cantidad,  int indicador)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, cantidad);
                parametros.Add("@pIndicador", ConexionDbType.Int, indicador); 

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcGuardarProductosAgregados", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarTMA(Model.TMA tma)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodTMA", ConexionDbType.Int, tma.Codigo);
                parametros.Add("@pDescripcionCorta", ConexionDbType.VarChar, tma.DescripcionCorta);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, tma.Descripcion);
                parametros.Add("@pEstatus", ConexionDbType.Bit, tma.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcTMAGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.ProductosAgregadosModel>> ObtenerProductosAgregados()
        {
            Result<List<Model.ProductosAgregadosModel>> result = new Result<List<Model.ProductosAgregadosModel>>();
            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output,300);

                result = _conexion.ExecuteWithResults<Model.ProductosAgregadosModel>("ProcObtenerProductosAgregados", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }


        public Result<List<Model.TipoConcreto>> consultaTipoConcreto(int codTipoConcreto)
        {
            Result<List<Model.TipoConcreto>> result = new Result<List<Model.TipoConcreto>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigoTipoConcreto", ConexionDbType.VarChar, codTipoConcreto);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.TipoConcreto>("ProcCatTipoConcretoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
