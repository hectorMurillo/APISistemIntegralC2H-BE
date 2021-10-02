using Models;
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
    public class DAPedidos
    {
        private readonly Conexion _conexion = null;

        public DAPedidos()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
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

        public Result<List<Pedido>> ObtenerPedidos(int pedido, DateTime fechaDesde, DateTime fechaHasta, int codUsuario)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pPedido", ConexionDbType.Int, pedido);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                //parametros.Add("@pUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Pedido>("ProcPedidosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarPedido(PedidoModel pedido, int codUsuario)
         {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, pedido.FolioPedido);
                parametros.Add("@pHoraSalida", ConexionDbType.VarChar, pedido.HoraSalida);
                parametros.Add("@pFechaSalida", ConexionDbType.VarChar, pedido.FechaSalida);
                parametros.Add("@pCodCliente", ConexionDbType.Int, pedido.CodCliente);
                parametros.Add("@pCodObra", ConexionDbType.Int, pedido.CodObra);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, pedido.codVendedor);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, pedido.Cantidad);
                parametros.Add("@pCierre", ConexionDbType.Bit, pedido.Cierre);
                parametros.Add("@pCantidadCierre", ConexionDbType.Decimal, pedido.CantidadCierre);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pCodProducto", ConexionDbType.Int, pedido.codProducto);
                parametros.Add("@pTieneDescuento", ConexionDbType.Bit, pedido.TieneDescuento);
                parametros.Add("@pPorcentajeDescuento", ConexionDbType.Decimal, pedido.PorcentajeDescuento);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, pedido.Observacion);
                parametros.Add("@pTieneImper", ConexionDbType.Bit, pedido.TieneImper);
                parametros.Add("@pTieneFibra", ConexionDbType.Bit, pedido.TieneFibra);
                parametros.Add("@pCodPlanta", ConexionDbType.Int, pedido.CodPlanta);
                parametros.Add("@pBombeado", ConexionDbType.Bit, pedido.Bombeado);
                parametros.Add("@pPrecioOriginal", ConexionDbType.Decimal, pedido.PrecioOriginal);
                parametros.Add("@pPrecioDescuento", ConexionDbType.Decimal, pedido.PrecioDescuento);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPedidosGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<PedidoCierre>> ObtenerPedidosCierres(int folioPedido)
        {
            Result<List<PedidoCierre>> result = new Result<List<PedidoCierre>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.ExecuteWithResults<PedidoCierre>("ProcPedidosCierresCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<PedidoCierre>> GuardarCierre(int folioPedido, decimal cantidadCierreNuevo)
        {
            Result<List<PedidoCierre>> result = new Result<List<PedidoCierre>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
                parametros.Add("@pCantidadCierre", ConexionDbType.Decimal, cantidadCierreNuevo);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, 1);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.ExecuteWithResults<PedidoCierre>("ProcPedidosCierresGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result CancelarCierre(int folioPedido, int idCatPedidosCierres)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
                parametros.Add("@pIdCatPedidosCierres", ConexionDbType.Int, idCatPedidosCierres);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, 1);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcPedidosCierresCancelar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Pedido>> ObtenerPedidosDetenidos(int folioPedido, int codUsuario)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Pedido>("ProcPedidosDetenidosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result AutorizarPedidoDetenido(int folio, bool autorizado, string observacion)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folio);
                parametros.Add("@pAutorizado", ConexionDbType.Bit, autorizado);
                parametros.Add("@pObservacion", ConexionDbType.VarChar, observacion);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPedidosGuardarPedidoDetenido", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result CambiarEstatusPedido(int folio, bool confirmado, string motivo, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folio);
                parametros.Add("@pConfirmado", ConexionDbType.Bit, confirmado);
                parametros.Add("@pMotivo", ConexionDbType.VarChar, motivo);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPedidosEstatusCambiar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result CambiarEstatus(int folio, string estatus, string motivo)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folio);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, estatus);
                parametros.Add("@pMotivo", ConexionDbType.VarChar, motivo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPedidosEstatusChange", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result ReagendarPedido(PedidoReagendarModel pedido, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, pedido.FolioPedido);
                parametros.Add("@pHoraSalida", ConexionDbType.VarChar, pedido.HoraSalida);
                parametros.Add("@pFechaSalida", ConexionDbType.VarChar, pedido.FechaSalida);
                parametros.Add("@pMotivo", ConexionDbType.VarChar, pedido.Motivo);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPedidoReagendadoGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
