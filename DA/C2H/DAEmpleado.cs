using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Database;
using Models.Empleados;
using WarmPack.Classes;

namespace DA.C2H
{
    public class DAEmpleado
    {
        private readonly Conexion _conexion = null;
        public DAEmpleado()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Empleado>> ConsultaEmpleados()
        {
            Result<List<Empleado>> result = new Result<List<Empleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Empleado>("ProcCatEmpleadosCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarPersonalCargaDiesel(PersonalCargaDiesel personalCargaDiesel)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, personalCargaDiesel.Codigo);
                parametros.Add("@pNombre", ConexionDbType.VarChar, personalCargaDiesel.Nombre);
                parametros.Add("@pApellidoP", ConexionDbType.VarChar, personalCargaDiesel.ApellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, personalCargaDiesel.ApellidoM);                
                parametros.Add("@pEstatus", ConexionDbType.Bit, personalCargaDiesel.Estatus);                
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcPersonalCargaDieselGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<PersonalCargaDiesel>> consultaPersonalCargaDiesel(int codPersonal)
        {
            Result<List<PersonalCargaDiesel>> result = new Result<List<PersonalCargaDiesel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodPersonal", ConexionDbType.Int, codPersonal);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<PersonalCargaDiesel>("ProcPersonalCargaDieselCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<DocumentosEmpleado>> consultaDocumentos(int codEmpleado)
        {
            Result<List<DocumentosEmpleado>> result = new Result<List<DocumentosEmpleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);


                result = _conexion.ExecuteWithResults<DocumentosEmpleado>("ProcEmpleadoDocumentacionCon", parametros);

                if (result.Data != null)
                    result.Data.ForEach(x => {
                        x.ArchivoBase64 = Convert.ToBase64String(x.Archivo);
                        x.Archivo = null;
                    });
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<TipoEmpleado>> consultaTiposUtilizados()
        {
            Result<List<TipoEmpleado>> result = new Result<List<TipoEmpleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<TipoEmpleado>("ProcObtenerTiposEmpleadosUtilizados", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<TipoEmpleado>> consultaTipos()
        {
            Result<List<TipoEmpleado>> result = new Result<List<TipoEmpleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<TipoEmpleado>("ProcTiposEmpleadoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result<List<SubTiposEmpleado>> consultaSubTipos(int codigoTipo)
        {
            Result<List<SubTiposEmpleado>> result = new Result<List<SubTiposEmpleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodTipoEmpleado", ConexionDbType.Int, codigoTipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<SubTiposEmpleado>("ProcSubTipoEmpleadoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public Result GuardarEmpleado(Empleado empleado)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                empleado.FechaRegistro = DateTime.Now;
                parametros.Add("@pNombre", ConexionDbType.VarChar, empleado.Nombre);
                parametros.Add("@pApellidoP", ConexionDbType.VarChar, empleado.ApellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, empleado.ApellidoM);
                parametros.Add("@pRFC", ConexionDbType.VarChar, empleado.RFC);
                parametros.Add("@pTipo", ConexionDbType.Int, empleado.CodigoTipo);
                parametros.Add("@pEstatus", ConexionDbType.Bit, empleado.Estatus);
                parametros.Add("@pTelefono", ConexionDbType.VarChar, empleado.Telefono);
                parametros.Add("@pCelular", ConexionDbType.VarChar, empleado.Celular);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, empleado.Correo);
                parametros.Add("@pFechaRegistro", ConexionDbType.DateTime, empleado.FechaRegistro);
                parametros.Add("@pCP", ConexionDbType.VarChar, empleado.Direccion.CP);
                parametros.Add("@pCodColonia", ConexionDbType.Int, empleado.Direccion.CodColonia);
                parametros.Add("@pCodMunicipio", ConexionDbType.Int, empleado.Direccion.CodMunicipio);
                parametros.Add("@pCodEstado", ConexionDbType.Int, empleado.Direccion.CodEstado);
                parametros.Add("@pCalleNumero", ConexionDbType.VarChar, empleado.Direccion.CalleNumero);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatEmpleadosGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
















        public Result ObtenerEmpleado(int codEmpleado)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcCatEmpleadoCon", parametros);

                var empleado = _conexion.RecordsetsResults<Empleado>();

                return new Result()
                {
                    Value = parametros.Value("@pResultado").ToBoolean(),
                    Message = parametros.Value("@pMsg").ToString(),
                    Data = new { empleado }
                };
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
