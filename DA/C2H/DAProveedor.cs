using Models;
using Models.Proveedores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Proveedores;

namespace DA.C2H
{
    public class DAProveedor
    {
        private readonly Conexion _conexion = null;
        public DAProveedor()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }



        public Result ConsultaProveedores(int codUsuario, int codProveedor)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodProveedor", ConexionDbType.Int, codProveedor);
                parametros.Add("@pCodUsuario", ConexionDbType.VarChar, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                //result = _conexion.ExecuteWithResults<Model.Clientes>("ProcCatClientesCon", parametros);
                _conexion.RecordsetsExecute("ProcCatProveedoresCon", parametros);

                var proveedor = _conexion.RecordsetsResults<Models.Proveedores.ProveedorModel>();
                //var direccionesXCliente = _conexion.RecordsetsResults<Models.Clientes.DireccionesXClientesModel>();
                //var contactosXCliente = _conexion.RecordsetsResults<Models.Clientes.ContactoXClienteModel>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { proveedor/*, direccionesXCliente, contactosXCliente */}
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<int>> ProveedorGuardar(Model.ProveedorModel Proveedor)
        {
            Result<List<int>> result = new Result<List<int>>();
            try
            {
                var parametros = new ConexionParameters();
                Proveedor.FechaRegistro = DateTime.Now;
                parametros.Add("@pNombre", ConexionDbType.VarChar, Proveedor.Nombre);
                parametros.Add("@pAPellidoP", ConexionDbType.VarChar, Proveedor.ApellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, Proveedor.ApellidoM);
                parametros.Add("@pNombreComercial", ConexionDbType.VarChar, Proveedor.NombreComercial);
                parametros.Add("@pRFC", ConexionDbType.VarChar, Proveedor.RFC);          
                parametros.Add("@pCP", ConexionDbType.VarChar, Proveedor.CP);
                parametros.Add("@pEstado", ConexionDbType.VarChar, Proveedor.Estado);
                parametros.Add("@pMunicipio", ConexionDbType.VarChar, Proveedor.Municipio);
                parametros.Add("@pCiudad", ConexionDbType.VarChar, Proveedor.Ciudad);
                parametros.Add("@pIdColonia", ConexionDbType.Int, Proveedor.IdColonia);
                parametros.Add("@pCalleNumero", ConexionDbType.VarChar, Proveedor.CalleNumero);
                parametros.Add("@pTipo", ConexionDbType.VarChar, Proveedor.Tipo);
                parametros.Add("@pUsuario", ConexionDbType.VarChar, Proveedor.Usuario);
                parametros.Add("@pContraseña", ConexionDbType.VarChar, Proveedor.Contraseña);
                parametros.Add("@pConfirmarContraseña", ConexionDbType.VarChar, Proveedor.ConfirmarContraseña);
                parametros.Add("@pRegimen", ConexionDbType.VarChar, Proveedor.Regimen);
                parametros.Add("@pFechaRegistro", ConexionDbType.DateTime, Proveedor.FechaRegistro);
                parametros.Add("@pCodigoProveedor", ConexionDbType.Int, Proveedor.IdProveedor);


                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<int>("ProcCatProveedorGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }




    }



}
