using Models;
using Models.Comisiones;
using Models.Empleados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using WarmPack.Extensions;

namespace DA.C2H
{
    public class DAComisiones
    {
        private readonly Conexion _conexion = null;
        public DAComisiones()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        

        public Result<List<TipoEmpleado>> ObtenerTipoEmpleado()
        {
            var r = new Result<List<TipoEmpleado>>();
            try
            {

                var parametros = new ConexionParameters();

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.ExecuteWithResults<TipoEmpleado>("ProcCatTipoEmpleadoComisionCon", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result<List<Empleado>> ObtenerEmpleadosPorTipoEmpleado(int codTipoEmpleado)
        {
            var r = new Result<List<Empleado>>();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pTipoEmpleado", ConexionDbType.Int, codTipoEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.ExecuteWithResults<Empleado>("ProcEmpleadosTipoEmpleadoCon", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result<List<ReporteComisionesModel>> ObtenerDatosComisiones(DateTime fechaInicial, DateTime fechaFinal, int tipoEmpleado, int codEmpleado)
        {
            var r = new Result<List<ReporteComisionesModel>>();
            try
            {

                var parametros = new ConexionParameters();
                parametros.Add("@pFechaInicial", ConexionDbType.Date, fechaInicial);
                parametros.Add("@pFechaFinal", ConexionDbType.Date, fechaFinal);
                parametros.Add("@pTipoBusqueda", ConexionDbType.Int, tipoEmpleado);
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.ExecuteWithResults<ReporteComisionesModel>("ProcRptComisiones", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        
          public Result AsignarComisionEmpleado(List<ComisionesXEmpleadoModel> comisiones, int codEmpleado, DateTime fechaComision)
        {
            var r = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.VarChar, codEmpleado);
                parametros.Add("@pComisiones", ConexionDbType.Xml, comisiones.ToXml("Comisiones"));
                parametros.Add("@pFechaComision", ConexionDbType.Date, fechaComision);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                r = _conexion.Execute("ProcCatComisionesAsignadasGuardar", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result GuardarComisionesEmpleado(List<ComisionesXEmpleadoModel> comisiones, int codEmpleado, DateTime fechaComision)
        {
            var r = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.VarChar, codEmpleado);
                parametros.Add("@pComisiones", ConexionDbType.Xml, comisiones.ToXml("Comisiones"));
                parametros.Add("@pFechaComision", ConexionDbType.Date, fechaComision);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

               r = _conexion.Execute("ProcCatComisionesXEmpleadoGuardar", parametros);
            }
            catch (Exception ex)
            {
                r.Value = false;
                r.Message = ex.Message;
            }
            return r;
        }

        public Result<List<ComisionModel>> ObtenerComisiones(int codTipoEmpleado)
        {
            Result<List<ComisionModel>> result = new Result<List<ComisionModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pTipoEmpleado", ConexionDbType.Int, codTipoEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ComisionModel>("ProcCatComisionesCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }


        public Result<List<EmpleadosComisionModel>> ObtenerEmpleadosConComisiones()
        {
            Result<List<EmpleadosComisionModel>> result = new Result<List<EmpleadosComisionModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<EmpleadosComisionModel>("ProcComisionesEmpleadosGeneralCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ComisionesXEmpleadoModel>> ObtenerComisionesPorEmpleado(int codEmpleado, DateTime diaComision)
        {
            Result<List<ComisionesXEmpleadoModel>> result = new Result<List<ComisionesXEmpleadoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
                parametros.Add("@pFecha", ConexionDbType.Date, diaComision);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ComisionesXEmpleadoModel>("ProcComisionesXEmpleadoDetalleCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
