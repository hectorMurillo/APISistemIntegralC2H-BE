using Models;
using Models.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAHerramientas
    {
        private readonly Conexion _conexion = null;
        public DAHerramientas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }


        public Result<ParametrosModel> ObtenerParametro(String parametro)
        {
            var r = new Result<ParametrosModel>();
            try
            {
                var parametros = new ConexionParameters();
                ParametrosModel p = new ParametrosModel();
                parametros.Add("@pBuscar", ConexionDbType.VarChar, parametro);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.ExecuteWithResults("ProcParametrosCon", parametros, row =>
                {
                    r.Data = new ParametrosModel();

                    r.Data.Id = row["Id"].ToInt32();
                    r.Data.Nombre = row["Nombre"].ToString();
                    r.Data.Descripcion = row["Descripcion"].ToString();
                    r.Data.Valor = row["Valor"].ToString();

                });

                r.Value = parametros.Value("@pResultado").ToBoolean();
                r.Message = parametros.Value("@pMsg").ToString();

            }
            catch (Exception ex)
            {
                r.Message = "Error "+ ex.Message.ToString();
                r.Value = false;
            }
           
            return r;
        }
    }
}
