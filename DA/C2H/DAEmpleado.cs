using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Database;
using Models.Empleados;
using WarmPack.Classes;
using System.Data;
using Models.Comisiones;
using WarmPack.Extensions;

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

        public Result<List<Empleado>> ConsultaEmpleadosNoUsuario()
        {
            Result<List<Empleado>> result = new Result<List<Empleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Empleado>("ProcCatEmpleadoNoUsuarioCon", parametros);
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

        public Result consultaDocumentos(int codEmpleado)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                _conexion.RecordsetsExecute("ProcEmpleadoDocumentacionCon", parametros);

                var tiposDocumentos = _conexion.RecordsetsResults<Models.Empleados.TipoDocumento>();
                var documentosEmpleados = _conexion.RecordsetsResults<Models.Empleados.DocumentosEmpleado>();

                result.Data = new { tiposDocumentos, documentosEmpleados };
                //if (result.Data != null)
                //    result.Data.ForEach(x => {
                //        x.ArchivoBase64 = Convert.ToBase64String(x.Archivo);
                //        x.Archivo = null;
                //    });
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

        public Result GuardarEmpleado(Empleado empleado, int idUsuario)
        {
            Result result = new Result();
            try
            {

                var parametros = new ConexionParameters();
                empleado.fechaRegistro = DateTime.Now;
                parametros.Add("@pIdUsuario", ConexionDbType.Int, idUsuario);
                parametros.Add("@pCodigo", ConexionDbType.VarChar, empleado.codigo);
                parametros.Add("@pNombre", ConexionDbType.VarChar, empleado.nombre);
                parametros.Add("@pApellidoP", ConexionDbType.VarChar, empleado.apellidoP);
                parametros.Add("@pApellidoM", ConexionDbType.VarChar, empleado.apellidoM);
                parametros.Add("@pCurp", ConexionDbType.VarChar, empleado.curp);
                parametros.Add("@pRFC", ConexionDbType.VarChar, empleado.rFC);
                parametros.Add("@pCodTipoEmpleado", ConexionDbType.Int, empleado.codigoTipoEmpleado);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, empleado.estatus);
                parametros.Add("@pTelefono", ConexionDbType.VarChar, empleado.telefono);
                parametros.Add("@pCelular", ConexionDbType.VarChar, empleado.celular);
                parametros.Add("@pCorreo", ConexionDbType.VarChar, empleado.correo);
                parametros.Add("@pFechaRegistro", ConexionDbType.DateTime, empleado.fechaRegistro);
                parametros.Add("@pCP", ConexionDbType.VarChar, empleado.direccion.CP);
                parametros.Add("@pCodColonia", ConexionDbType.Int, empleado.direccion.CodColonia);
                parametros.Add("@pCodMunicipio", ConexionDbType.Int, empleado.direccion.CodMunicipio);
                parametros.Add("@pCodEstado", ConexionDbType.Int, empleado.direccion.CodEstado);
                parametros.Add("@pMotivo", ConexionDbType.VarChar, empleado.motivo);
                parametros.Add("@pCalleNumero", ConexionDbType.VarChar, empleado.direccion.CalleNumero);
                parametros.Add("@pComisionesXML", ConexionDbType.Xml,empleado.comisiones.ToXml("Comisiones"));
                parametros.Add("@pNumSeguroSocial", ConexionDbType.VarChar, empleado.NumSeguroSocial);
                parametros.Add("@pFechaNacimiento", ConexionDbType.Date, empleado.FechaNacimientoDate);
                parametros.Add("@pFechaIngreso", ConexionDbType.Date, empleado.FechaIngresooDate);
                parametros.Add("@pSalario", ConexionDbType.Int, empleado.Salario);
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

        public Result<List<TipoEmpleado>> ConsultaTiposEmpleado(int codTipoEmpleado)
        {
            Result<List<TipoEmpleado>> result = new Result<List<TipoEmpleado>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCatTipoEmpleado", ConexionDbType.Int, codTipoEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<TipoEmpleado>("ProcCatTipoEmpleadoCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarTipoEmpleado(TipoEmpleado tipoEmpleado)
        {
            Result result = new Result();

            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodCatTipoEmpleado", ConexionDbType.Int, tipoEmpleado.Codigo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, tipoEmpleado.Descripcion);
                parametros.Add("@pEstatus", ConexionDbType.VarChar, tipoEmpleado.Estatus);
                parametros.Add("@pCodCatTipoEquipo", ConexionDbType.VarChar, tipoEmpleado.CodTipoEquipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcCatTipoEmpleadoGuardar", parametros);

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
            DataSet ds = new DataSet();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodEmpleado", ConexionDbType.Int, codEmpleado);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                //_conexion.RecordsetsExecute("ProcCatEmpleadoCon", parametros);

               result =  _conexion.ExecuteWithResults("ProcCatEmpleadoCon", parametros, out ds);


                if (result.Value)
                {
                    Empleado emp = new Empleado();
               
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        emp.codigo = int.Parse(dr[0].ToString());
                        emp.nombre = dr[1].ToString();
                        emp.apellidoM = dr[2].ToString();
                        emp.apellidoP = dr[3].ToString();
                        emp.rFC = dr[4].ToString();
                        emp.codigoTipoEmpleado = int.Parse(dr[5].ToString());
                        emp.tipo = dr[6].ToString();
                        emp.estatus = dr[7].ToString();
                        emp.correo = dr[8].ToString();
                        emp.telefono = dr[9].ToString();
                        emp.celular = dr[10].ToString();
                        emp.curp = dr[15].ToString();
                        emp.motivo = dr[16].ToString();
                        emp.FechaNacimiento = dr[17].ToString();
                        emp.FechaIngreso = dr[18].ToString();
                        emp.Salario = int.Parse(dr[19].ToString());
                        emp.NumSeguroSocial = dr[20].ToString();
                    }

                    if (ds.Tables.Count > 1){
                        emp.comisiones = ds.Tables[1].ToList<ComisionModel>();
                    } 

                    result.Data = emp;
                }
                   
                //var empleado = _conexion.RecordsetsResults<Empleado>();

                //return new Result()
                //{
                //    Value = parametros.Value("@pResultado").ToBoolean(),
                //    Message = parametros.Value("@pMsg").ToString(),
                //    Data = new { empleado }
                //};
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result guardarArchivo(int codigo, int codigoEmpleado, int codigoTipoDocumento, byte[] imagen, string extension, bool vieneImagen)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigo", ConexionDbType.Int, codigo);
                parametros.Add("@pCodigoEmpleado", ConexionDbType.Int, codigoEmpleado);
                parametros.Add("@pCodigoTipoDocumento", ConexionDbType.Int, codigoTipoDocumento);
                parametros.Add("@pImagen", ConexionDbType.VarBinary, imagen);
                parametros.Add("@pExtension", ConexionDbType.VarChar, extension);
                parametros.Add("@pVieneImagen", ConexionDbType.Bit, vieneImagen);
                result = _conexion.Execute("ProcEmpleadoDocumentacionGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

    }
}
