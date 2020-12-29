using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class OperadorModel
    {
        public int Codigo { get; set; }
        public string  NombreCompleto { get; set; }
		public string RFC { get; set; }
        public int CodEquipoAsignado { get; set; }
        public string Equipo { get; set; }
        public int CodigoTipoEmpleado { get; set; }
		public bool Estatus { get; set; }
		public string Correo { get; set; }
		public string Telefono { get; set; }
		public string celular { get; set; }
        public bool Bombeable { get; set; }
    }
}
