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
                parametros.Add("@pHoraSalida", ConexionDbType.VarChar, pedido.HoraSalida);
                parametros.Add("@pFechaSalida", ConexionDbType.VarChar, pedido.FechaSalida);
                parametros.Add("@pCodCliente", ConexionDbType.Int, pedido.CodCliente);
                parametros.Add("@pCodObra", ConexionDbType.Int, pedido.CodObra);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, pedido.codVendedor);
                parametros.Add("@pCantidad", ConexionDbType.Decimal, pedido.Cantidad);
                parametros.Add("@pCierre", ConexionDbType.Decimal, !pedido.Cierre);
                parametros.Add("@pCantidadCierre", ConexionDbType.Decimal, pedido.CantidadCierre);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
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

    }
}
