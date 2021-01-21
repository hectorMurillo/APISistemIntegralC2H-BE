using Models;
using Models.Vendedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAVendedor
    {
        private readonly Conexion _conexion = null;
        public DAVendedor()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Vendedor>> ObtenerVendedores()
        {
            Result<List<Vendedor>> result = new Result<List<Vendedor>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Vendedor>("ProcCatVendedoresCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
