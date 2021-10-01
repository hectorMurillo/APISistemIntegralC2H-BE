using Models;
using Models.Cotizaciones;
using Models.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DACotizaciones
    {
        private readonly Conexion _conexion = null;

        public DACotizaciones()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Cotizacion>> ObtenerCotizaciones(int cotizacion, DateTime fechaDesde, DateTime fechaHasta, int codUsuario)
        {
            Result<List<Cotizacion>> result = new Result<List<Cotizacion>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pCotizacion", ConexionDbType.Int, cotizacion);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                //parametros.Add("@pUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Cotizacion>("ProcCotizacionesCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarCotizacion(CotizacionModel cotizacion, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioCotizacion", ConexionDbType.Int, cotizacion.FolioCotizacion);
                parametros.Add("@pCodCliente", ConexionDbType.Int, cotizacion.CodCliente);
                parametros.Add("@pCodObra", ConexionDbType.Int, cotizacion.CodObra);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, cotizacion.codVendedor);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, cotizacion.Cantidad);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pCodProducto", ConexionDbType.Int, cotizacion.codProducto);
                parametros.Add("@pTieneDescuento", ConexionDbType.Bit, cotizacion.TieneDescuento);
                parametros.Add("@pPorcentajeDescuento", ConexionDbType.Decimal, cotizacion.PorcentajeDescuento);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, cotizacion.Observacion);
                parametros.Add("@pTieneImper", ConexionDbType.Bit, cotizacion.TieneImper);
                parametros.Add("@pTieneFibra", ConexionDbType.Bit, cotizacion.TieneFibra);
                parametros.Add("@pBombeado", ConexionDbType.Bit, cotizacion.Bombeado);
                parametros.Add("@pPrecioOriginal", ConexionDbType.Decimal, cotizacion.PrecioOriginal);
                parametros.Add("@pPrecioDescuento", ConexionDbType.Decimal, cotizacion.PrecioDescuento);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCotizacionesGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DescuentoXClienteModel>> ObtenerUltimoDescuento(int codAgente, int codCliente)
        {
            Result<List<DescuentoXClienteModel>> result = new Result<List<DescuentoXClienteModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodAgente", ConexionDbType.Int, codAgente);
                parametros.Add("@pCodCliente", ConexionDbType.Int, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<DescuentoXClienteModel>("ProcCatDescuentoPedidoXClienteCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
