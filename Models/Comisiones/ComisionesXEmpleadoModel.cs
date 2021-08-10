using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comisiones
{
    public class ComisionesXEmpleadoModel
    {
        public int Codigo { get; set; }
        public int CodTipoComision { get; set; }
        public decimal Monto { get; set; }
        public decimal CantidadMaximaComision { get; set; }
        public string Descripcion { get; set; }
        public bool Seleccionado { get; set; } = false;
    }
}
