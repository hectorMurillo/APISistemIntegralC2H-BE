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
    public class DAContactosXProveedor
    {
        private readonly Conexion _conexion = null;

        public DAContactosXProveedor()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result consultaContactos(int IdProveedor)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pIdProveedor", ConexionDbType.Int, IdProveedor);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcCatContactosProveedorCon", parametros);

                var contacto = _conexion.RecordsetsResults<Models.Proveedores.ContactosXProveedorModel>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { contacto }
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
                result.Data = null;
            }

            return result;
        }

        public Result ContactoDesactivar(Model.ContactosXProveedorModel Contacto)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();

                parametros.Add("@pIdContacto", ConexionDbType.Int, Contacto.IdContacto);
                parametros.Add("@pActivado", ConexionDbType.Bit, Contacto.Activado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatContactosProveedorGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarContacto(Model.ContactosXProveedorModel Contacto)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();

                Contacto.FechaRegistro = DateTime.Now;
                parametros.Add("@pIdContacto", ConexionDbType.Int, Contacto.IdContacto);
                parametros.Add("@pIdProveedor", ConexionDbType.Int, Contacto.IdProveedor);
                parametros.Add("@pNombre", ConexionDbType.VarChar, Contacto.Nombre);
                parametros.Add("@pTelefono", ConexionDbType.VarChar, Contacto.Telefono);
                parametros.Add("@pTelefonoMovil", ConexionDbType.VarChar, Contacto.TelefonoMovil);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, Contacto.Correo);
                parametros.Add("@pCBPrincipal", ConexionDbType.Bit, Contacto.CBPrincipal);
                parametros.Add("@pFechaRegistro", ConexionDbType.DateTime, Contacto.FechaRegistro);

                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatContactosProveedorGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result consultaContacto(int IdContacto)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdContacto", ConexionDbType.Int, IdContacto);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcCatContactosProveedorCon", parametros);

                var contacto = _conexion.RecordsetsResults<Models.Proveedores.ContactosXProveedorModel>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { contacto }
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
                result.Data = null;
            }
            return result;
        }
    }
}
