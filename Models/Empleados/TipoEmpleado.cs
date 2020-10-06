using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class TipoEmpleado
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public int CodTipoEquipo { get; set; }
        public string NombreEquipo { get; set; }
    }
}
