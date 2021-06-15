using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Generales;
namespace DA.C2H
{
    public class DAGenerales
    {
        private readonly Conexion _conexion = null;
        public DAGenerales()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }


        public Result<List<Model.CodigoPostal>> DetallesCodigoPostal(string codigoPostal)
        {
            Result<List<Model.CodigoPostal>> result = new Result<List<Model.CodigoPostal>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodigoCP", ConexionDbType.VarChar, codigoPostal);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.CodigoPostal>("ProcConsultarDatosCP", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result GuardarValoracionServicio(int codPedido, int general, int personal, int producto, string comentarios)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pCodPedido", ConexionDbType.Int, codPedido);
                parametros.Add("@pGeneral", ConexionDbType.Int, general);
                parametros.Add("@pPersonal", ConexionDbType.Int, personal);
                parametros.Add("@pProducto", ConexionDbType.Int, producto);
                parametros.Add("@pComentarios", ConexionDbType.VarChar, comentarios);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcValoracionServicioGuardar", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result VerificarEncuesta(int codPedido)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdCatPedido", ConexionDbType.Int, codPedido);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.Execute("ProcValoracionServicioCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        
    }
}
