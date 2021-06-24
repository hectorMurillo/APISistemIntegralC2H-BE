using Models;
using Models.Dosificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DABuscador
    {
        private readonly Conexion _conexion = null;
        public DABuscador()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        

        public Result<List<PedidoModel>> ObtenerPedidos(string buscar)
        {
            Result<List<PedidoModel>> result = new Result<List<PedidoModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pbuscar", ConexionDbType.VarChar, buscar);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<PedidoModel>("ProcBuscadorPedidosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
