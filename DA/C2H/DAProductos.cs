using Models;
using Models.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAProductos
    {
        private readonly Conexion _conexion = null;
        public DAProductos()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        

        public Result<List<InsumoModel>> ObtenerInsumos(int codigo)
        {
            var r = new Result<List<InsumoModel>>();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.ExecuteWithResults<InsumoModel>("ProcInsumoCon", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result<List<TipoInsumoModel>> ObtenerTipoInsumos(int codigo)
        {
            var r = new Result<List<TipoInsumoModel>>();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.ExecuteWithResults<TipoInsumoModel>("ProcTipoInsumoCon", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result<List<TipoUnidadMedidaModel>> ObtenerTipoUnidadesMedida(int codigo)
        {
            var r = new Result<List<TipoUnidadMedidaModel>>();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.ExecuteWithResults<TipoUnidadMedidaModel>("ProcTipoUnidadMedidaCon", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        
        public Result GuardarInsumo(InsumoModel insumo)
        {
            var r = new Result();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, insumo.IdInsumo);
                parametros.Add("@pIdTipoInsumo", ConexionDbType.Int, insumo.IdTipoInsumo);
                parametros.Add("@pIdProveedor", ConexionDbType.Int, insumo.IdProveedor);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, insumo.Descripcion);
                parametros.Add("@pCostoUnidad", ConexionDbType.Decimal, insumo.CostoXUnidadMedida);
                parametros.Add("@pValorMinimo", ConexionDbType.Decimal, insumo.ValorMinimo);
                parametros.Add("@pValorMaximo", ConexionDbType.Decimal, insumo.ValorMaximo);
                parametros.Add("@pValorIdeal", ConexionDbType.Decimal, insumo.ValorIdeal);
                parametros.Add("@pObservaciones", ConexionDbType.VarChar, insumo.Observaciones);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, insumo.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.Execute("ProcInsumoGuardar", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result GuardarTipoInsumo(TipoInsumoModel tipoInsumo)
        {
            var r = new Result();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, tipoInsumo.Codigo);
                parametros.Add("@pIdTipoUnidadMedida", ConexionDbType.Int, tipoInsumo.CodigoUnidadMedida);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, tipoInsumo.Descripcion);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, tipoInsumo.Observacion);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, tipoInsumo.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.Execute("ProcTipoInsumoGuardar", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }


        public Result GuardarUnidadMedida(TipoUnidadMedidaModel tipoUnidadMedida)
        {
            var r = new Result();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, tipoUnidadMedida.Codigo);
                parametros.Add("@pDescripion", ConexionDbType.VarChar, tipoUnidadMedida.Descripcion);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, tipoUnidadMedida.Observacion);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, tipoUnidadMedida.Estatus);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.Execute("ProcTipoUnidadMedidaGuardar", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }
    }
}
