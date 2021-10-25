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

        public Result<List<Vendedor>> ObtenerVendedores(int codVendedor)
        {
            Result<List<Vendedor>> result = new Result<List<Vendedor>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodVendedor", ConexionDbType.Int, codVendedor);
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

        public Result GuardarVendedor(VendedorModel vendedor, int codUsuario)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, vendedor.codigo);
                parametros.Add("@pNombre", ConexionDbType.VarChar, vendedor.nombre);
                parametros.Add("@pApellidoP", ConexionDbType.VarChar, vendedor.apellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, vendedor.apellidoM);
                parametros.Add("@pRFC", ConexionDbType.VarChar, vendedor.rFC);
                parametros.Add("@pCodigoTipoEmpleado", ConexionDbType.Int, vendedor.codigoTipoEmpleado);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, vendedor.correo);
                parametros.Add("@pCelular", ConexionDbType.VarChar, vendedor.celular);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, vendedor.estatus);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcVendedoresGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
