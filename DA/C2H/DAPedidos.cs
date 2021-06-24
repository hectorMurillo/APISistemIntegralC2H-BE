﻿using Models;
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

        public Result<List<Pedido>> ObtenerPedidos(int pedido, DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pPedido", ConexionDbType.Int, pedido);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
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

        public Result<List<Pedido>> ObtenerPedidosDetenidos(int folioPedido)
        {
            Result<List<Pedido>> result = new Result<List<Pedido>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pFolioPedido", ConexionDbType.Int, folioPedido);
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

    }
}
