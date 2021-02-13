using Models;
using Models.AgenteVentas;
using Models.Clientes;
using Models.Dosificador;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.AgenteVentas;

namespace DA.C2H
{
    public class DAAgenteVentas
    {
        private readonly Conexion _conexion = null;

        public DAAgenteVentas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }
        public Result<List<Model.AgenteVentasCombo>> ConsultaAgenteVentasCombo()
        {
            Result<List<Model.AgenteVentasCombo>> result = new Result<List<Model.AgenteVentasCombo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.AgenteVentasCombo>("ProcAgenteVentaCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ClientesModel>> ObtenerClientesPorAgente(int codAgente)
        {
            Result<List<ClientesModel>> result = new Result<List<ClientesModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codAgente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ClientesModel>("ProcClientesVendedorCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ProductosClienteModel>> ObtenerProductosPorCliente(int codAgente, int codCliente)
        {
            Result<List<ProductosClienteModel>> result = new Result<List<ProductosClienteModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodAgente", ConexionDbType.Int, codAgente);
                parametros.Add("@pCodCliente", ConexionDbType.Int, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ProductosClienteModel>("ProcProductosXClienteCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<OperadorModel>> ObtenerDetalleClientesPorAgente(int idNotaRemision)
        {
            Result<List<OperadorModel>> result = new Result<List<OperadorModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNotaRemision", ConexionDbType.Int, idNotaRemision);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<OperadorModel>("ProcNotaRemisionDetalleCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result<List<DatosNotaRemision>> ObtenerNotasRemisionAgente(int codAgente)
        {
            Result<List<DatosNotaRemision>> result = new Result<List<DatosNotaRemision>>();
            try
            {
                var parametros = new ConexionParameters();
                DataSet table = new DataSet();
                DatosNotaRemision notaRemision = new DatosNotaRemision();
                parametros.Add("@pCodAgente", ConexionDbType.Int, codAgente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

               result = _conexion.ExecuteWithResults<DatosNotaRemision>("ProcNotasRemisionAgente", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarPrecioProductoXCliente(PrecioProductoModel producto)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodProductoXCliente", ConexionDbType.Int, producto.CodProductoXCliente);
                parametros.Add("@pCodAgente", ConexionDbType.Int, producto.CodAgente);
                parametros.Add("@pCodCliente", ConexionDbType.Int, producto.CodCliente);
                parametros.Add("@pCodProducto", ConexionDbType.Int, producto.CodProducto);
                parametros.Add("@pPrecio", ConexionDbType.Decimal, producto.Precio);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcProductosXClienteGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        
    }
}
