using Models;
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

        public Result<List<RptMensualMetros>> ReporteMensualMetrosCuadrados()
        {
            Result<List<RptMensualMetros>> result = new Result<List<RptMensualMetros>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<RptMensualMetros>("ProcVentasRptMensualMetrosCuadrados", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;

        }

    }
}
