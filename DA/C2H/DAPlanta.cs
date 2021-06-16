using Models;
using Models.Herramientas;
using System;
using System.Collections.Generic;
using WarmPack.Classes;
using WarmPack.Database;

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
            var r = _DAHerramientas.ObtenerParametro("Coordenadas-Planta");

            return r;
        }
    }
}