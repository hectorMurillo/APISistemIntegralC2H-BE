using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Empresas;
namespace DA.C2H
{
    public class DAEmpresas
    {
        private readonly Conexion _conexion = null;
        public DAEmpresas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Model.EmpresaCombo>> consultarEmpresasCombo()
        {
            Result<List<Model.EmpresaCombo>> result = new Result<List<Model.EmpresaCombo>>();
            try
            {
                var parametros = new ConexionParameters();                
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.EmpresaCombo> ("ProcEmpresasComboCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
