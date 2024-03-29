﻿using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarmPack.Classes;
using WarmPack.Database;
using Model = Models.AgenteVentas;

namespace DA.C2H
{
    public class DAAgenteVentas
    {
        private readonly Conexion _conexion = null;

        public DAAgenteVentas()
        {
            _conexion = new Conexion(ConexionType.MSSQLServer, Globales.ConexionPrincipal);
        }
        public Result<List<Model.AgenteVentasCombo>> ConsultaAgenteVentasCombo()
        {
            Result<List<Model.AgenteVentasCombo>> result = new Result<List<Model.AgenteVentasCombo>>();
            try
            {
                var parametros = new ConexionParameters();
                parametros.Add("@pResultado", ConexionDbType.Bit, System.Data.ParameterDirection.Output);
                parametros.Add("@pMsg", ConexionDbType.VarChar, System.Data.ParameterDirection.Output, 300);

                result = _conexion.ExecuteWithResults<Model.AgenteVentasCombo>("ProcAgenteVentaCon", parametros);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
