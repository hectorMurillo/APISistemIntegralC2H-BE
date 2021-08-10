using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Models.Noticias;

namespace DA.C2H
{
    public class DANoticias
    {
        private readonly Conexion _conexion = null;

        public DANoticias()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }

        public Result<List<Noticias>> ObtenerNoticias()
        {
            Result<List<Noticias>> result = new Result<List<Noticias>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Noticias>("ProcNoticiasCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }

        public Result RegistrarNoticia(int idNoticias, string titulo, string descripcion, byte[] imagen, int codUsuario, bool vieneImagen)
        {
            Result result = new Result();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pIdNoticias", ConexionDbType.Int, idNoticias);
                parametros.Add("@pTitulo", ConexionDbType.VarChar, titulo);
                parametros.Add("@pDescripcion", ConexionDbType.VarChar, descripcion);
                parametros.Add("@pImagen", ConexionDbType.VarBinary, imagen);
                parametros.Add("@pCodUsuario", ConexionDbType.Int, codUsuario);
                parametros.Add("@pVieneImagen", ConexionDbType.Bit, vieneImagen);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.Execute("ProcNoticiasGuardar", parametros);

            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
