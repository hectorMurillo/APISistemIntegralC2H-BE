using Models;
using Models.Herramientas;
using System;
using System.Collections.Generic;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.Plantas;
namespace DA.C2H
{
    public class DAPlanta
    {
        private readonly Conexion _conexion = null;
        private readonly DAHerramientas _DAHerramientas = null;

        public DAPlanta()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
            _DAHerramientas = new DAHerramientas();
        }
        public Result<ParametrosModel> consultaCoordenadas()
        {
            var r = _DAHerramientas.ObtenerParametro("Coordenadas-Planta-1");

            return r;
        }
        public Result<List<Model.Planta>> consultaPlantas(int codPlanta)
        {

            Result<List<Model.Planta>> result = new Result<List<Model.Planta>>();
            try
            {
                var parametros = new ConexionParameters();
                //@pCodPlanta
                parametros.Add("@pCodPlanta", ConexionDbType.Int, codPlanta);
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);
                result = _conexion.ExecuteWithResults<Model.Planta>("ProcCatPlantasCon", parametros);

            }
            catch(Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}