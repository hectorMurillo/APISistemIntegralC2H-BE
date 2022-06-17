using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Equipos
{
    public class EquipoModel
    {
        public int codEmpleado { get; set; }
        public int Codigo { get; set; }
        public int CodTipoEquipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int CodigoModelo { get; set; }
        public string Modelo { get; set; }
        public int CodigoMarca { get; set; }
        public string Marca { get; set; }
        public string NumeroSerie { get; set; }
        public string Identificador { get; set; }
        public int CodigoTipoEquipo { get; set; }
        public string TipoEquipo { get; set; }
        public bool Estatus { get; set; }
        public string NumeroSerieMotor { get; set; }
        public string Equipo { get; set; }
        public string IdentificadorCorto { get; set; }
        public int CodOperadorTemporal { get; set; }
        public string OperadorTemporal { get; set; }
    }
}
