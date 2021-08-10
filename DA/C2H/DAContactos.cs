using Models;
using Models.Contactos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;

namespace DA.C2H
{
    public class DAContactos
    {
        private readonly Conexion _conexion = null;
        public DAContactos()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }
        public Result<List<Models.Contactos.PuestosContactosModel>> consultaPuesto(int codPuesto=0, string Tipo = "")
        {
            var result = new Result<List<Models.Contactos.PuestosContactosModel>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodPuesto", ConexionDbType.Int, codPuesto);
                parametros.Add("@pTipoPuesto", ConexionDbType.VarChar, Tipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Models.Contactos.PuestosContactosModel>("ProcCatPuestosContactosCon", parametros);

            }catch(Exception ex)
            {
                result.Message = ex.Message;
                result.Value = false;
                result.Data = null;
            }
            return result;
        }

        public Result GuardarPuestoContactos(Models.Contactos.PuestosContactosModel puestosContactos)
        {
            Result result = new Result();
            try { 
            //{
            //                @pCodPuesto as INT = 0,	
	           // @pDescripcion AS VARCHAR(MAX),
	           // @pTipo as VARCHAR(max),
	           // @pResultado as BIT = 0 OUTPUT,
	           // @pMsg as VARCHAR(300) = '' OUTPUT

                var parametros = new ConexionParameters();
                parametros.Add("@pCodPuesto", ConexionDbType.Int, puestosContactos.IdPuesto);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, puestosContactos.Descripcion);
                parametros.Add("@pTipo", ConexionDbType.VarChar, puestosContactos.Tipo);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcPuestosContactosGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
