using Models;
using Models.Reportes;
using Models.Ventas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAVentas
    {
        private readonly Conexion _conexion = null;
        public DAVentas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<RptMensualMetros>> ReporteMensualMetrosCubicos(RptMensualMetrosModel reporte, DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<RptMensualMetros>> result = new Result<List<RptMensualMetros>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pAgente", ConexionDbType.Int, reporte.codVendedor);
                parametros.Add("@pCodCliente", ConexionDbType.Int, reporte.codCliente);
                parametros.Add("@pCodProducto", ConexionDbType.Int, reporte.codProducto);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<RptMensualMetros>("ProcVentasRptMensualMetrosCubicos", parametros);
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result<List<RptMensualClientes>> ReporteMensualClientes(DateTime fechaDesde, DateTime fechaHasta, int agente)
        {
            Result<List<RptMensualClientes>> result = new Result<List<RptMensualClientes>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pAgente", ConexionDbType.Int, agente);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<RptMensualClientes>("ProcVentasRptMensualClientes", parametros);

                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
            }
            return result;

        }

        public Result<List<RptMensualMetros>> ReporteVolumenXObras(RptReporteVolumenObras reporte, DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<RptMensualMetros>> result = new Result<List<RptMensualMetros>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pAgente", ConexionDbType.Int, reporte.codVendedor);
                parametros.Add("@pCodCliente", ConexionDbType.Int, reporte.codCliente);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<RptMensualMetros>("ProcVentasRptVolumenXObra", parametros);
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result<List<RptMensualProductos>> ReporteMensualProductos(RptMensualProductos reporte, DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<RptMensualProductos>> result = new Result<List<RptMensualProductos>>();
            try
            {
                var parametros = new ConexionParameters();
                //el parametro del producto despues sera XML, al igual que los otros reportes, por que vendra una lista seleccionada.
                parametros.Add("@pCodProducto", ConexionDbType.Int, reporte.codProducto);
                parametros.Add("@pCodVendedor", ConexionDbType.Int, reporte.codVendedor);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<RptMensualProductos>("ProcVentasRptMensualProductos", parametros);
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

        public Result<List<DemandaArticulo>> ObtenerDemandaArticulo()
        {
            ConexionParameters parametros = new ConexionParameters();
            parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
            parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

            var r = _conexion.ExecuteWithResults<DemandaArticulo>("ProcVentasDemandaArticulos", parametros);

            return r;
        }

        public Result<List<RptEntradasSalidas>> ObtenerReporteEquipos(int codEquipo, DateTime fechaDesde, DateTime fechaHasta)
        {
            Result<List<RptEntradasSalidas>> result = new Result<List<RptEntradasSalidas>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEquipo", ConexionDbType.Int, codEquipo);
                parametros.Add("@pFechaDesde", ConexionDbType.Date, fechaDesde);
                parametros.Add("@pFechaHasta", ConexionDbType.Date, fechaHasta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<RptEntradasSalidas>("ProcReporteEntradasSalidasCon", parametros);
                result.Value = parametros.Value("@pResultado").ToBoolean();
                result.Message = parametros.Value("@pMsg").ToString();

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }


    }
}
