using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Clientes;
namespace DA.C2H
{
    public class DAClientes
    {
        private readonly Conexion _conexion = null;
        public DAClientes()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }


        public Result ConsultaClientes(int codUsuario, int codCliente)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCliente", ConexionDbType.Int, codCliente);
                parametros.Add("@pCodUsuario", ConexionDbType.VarChar, codUsuario);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                //result = _conexion.ExecuteWithResults<Model.Clientes>("ProcCatClientesCon", parametros);
                _conexion.RecordsetsExecute("ProcCatClientesCon", parametros);

                var cliente = _conexion.RecordsetsResults<Models.Clientes.ClientesModel>();
                var direccionesXCliente = _conexion.RecordsetsResults<Models.Clientes.DireccionesXClientesModel>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { cliente, direccionesXCliente }
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.UsuarioClienteModel>> consultarUsuarioCliente(int codCliente)
        {
            Result<List<Model.UsuarioClienteModel>> result = new Result<List<Model.UsuarioClienteModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCliente", ConexionDbType.VarChar, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.UsuarioClienteModel>("ProcUsuarioClienteCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result UsuarioClienteGuardar(Model.UsuarioClienteModel usuarioCte)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCliente", ConexionDbType.Int, usuarioCte.Codigo);
                parametros.Add("@pNombreUsuario", ConexionDbType.VarChar, usuarioCte.Usuario);
                parametros.Add("@pContraseñaUsuario", ConexionDbType.VarChar, usuarioCte.Contraseña);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcAsignarUsuarioCliente", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<Model.UsuarioClienteModel>> consultaUsuarioSugerido(int codCliente)
        {
            Result<List<Model.UsuarioClienteModel>> result = new Result<List<Model.UsuarioClienteModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCliente", ConexionDbType.VarChar, codCliente);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.UsuarioClienteModel>("ProcSugerirUsuarioCliente", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result DireccionesGuardar(Model.DireccionesXClientesModel direccion)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodUbicacion", ConexionDbType.Int, direccion.Codigo);
                parametros.Add("@pCodCliente", ConexionDbType.Int, direccion.CodCliente);
                parametros.Add("@pCP", ConexionDbType.VarChar, direccion.CP);
                parametros.Add("@pCodColonia", ConexionDbType.Int, direccion.CodColonia);
                parametros.Add("@pCalleNumero", ConexionDbType.VarChar, direccion.CalleNumero);
                parametros.Add("@pReferencia", ConexionDbType.VarChar, direccion.Referencia);
                parametros.Add("@pURLGoogleMaps", ConexionDbType.VarChar, direccion.URLGoogleMaps);
                parametros.Add("@pCodTipoObra", ConexionDbType.Int, direccion.CodTipoObra);
                parametros.Add("@pLatitude", ConexionDbType.Decimal, direccion.Latitud);
                parametros.Add("@pLongitud", ConexionDbType.Decimal, direccion.Longitud);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatDireccionXClienteGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result ClienteGuardar(Model.ClientesModel Cliente)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                Cliente.FechaRegistro = DateTime.Now;
                parametros.Add("@pNombre", ConexionDbType.VarChar, Cliente.Nombre);
                parametros.Add("@pApellidoP", ConexionDbType.VarChar, Cliente.ApellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, Cliente.ApellidoM);
                parametros.Add("@pCodigoCliente", ConexionDbType.Int, Cliente.Codigo);
                parametros.Add("@pRFC", ConexionDbType.VarChar, Cliente.RFC);
                parametros.Add("@pRazonSocial", ConexionDbType.Int, Cliente.RazonSocial);
                parametros.Add("@pAlias", ConexionDbType.VarChar, Cliente.Alias);
                parametros.Add("@pTelefono", ConexionDbType.VarChar, Cliente.Telefono);
                parametros.Add("@pCelular", ConexionDbType.VarChar, Cliente.Celular);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, Cliente.Correo);
                parametros.Add("@pFechaRegistro", ConexionDbType.DateTime, Cliente.FechaRegistro);
                parametros.Add("@pCredito", ConexionDbType.Decimal, Cliente.Credito);
                parametros.Add("@pCodVendedor", ConexionDbType.VarChar, Cliente.codEmpleadoVendedor);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatClienteGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
