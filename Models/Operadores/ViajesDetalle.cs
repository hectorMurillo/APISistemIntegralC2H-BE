using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Operadores
{
    public class ViajesDetalle
    {
        public int IdNotaRemisionAuxiliar { get; set; }
        public int FolioNotaRemision { get; set; }
        public decimal Cantidad { get; set; }
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string HoraSalida { get; set; }
        public string Equipo { get; set; }
        //public string Vendedor { get; set; }
        public string Nomenclatura { get; set; }
    }
}
