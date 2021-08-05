﻿using Models;
using Models.Comisiones;
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

        

        public Result<List<ComisionModel>> GuardarComisionesEmpleado(List<ComisionesXEmpleadoModel> comisiones, int codEmpleado)
        {
            Result<List<ComisionModel>> result = new Result<List<ComisionModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.VarChar, codEmpleado);
                parametros.Add("@pComisiones", ConexionDbType.Xml, comisiones.ToXml("Comisiones"));
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<ComisionModel>("ProcCatComisionesXEmpleadoGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<ComisionModel>> ObtenerNotasRemision()
        {
            Result<List<ComisionModel>> result = new Result<List<ComisionModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pBuscar", ConexionDbType.VarChar,"");
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

        public Result<List<ComisionesXEmpleadoModel>> ObtenerComisionesPorEmpleado(int codEmpleado)
        {
            Result<List<ComisionesXEmpleadoModel>> result = new Result<List<ComisionesXEmpleadoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
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
