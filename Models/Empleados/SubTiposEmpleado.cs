using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class SubTiposEmpleado
    {
        public int CodSubTipoEmpleado { get; set; }
        public int CodTipoEmpleado { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
    }
}
