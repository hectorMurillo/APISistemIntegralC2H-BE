using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Reportes
{
    public class RptEquipos
    {
        public int codEquipo { get; set; }
        public string equipo { get; set; }
        public decimal kilometrosRecorridos { get; set; }
        public decimal horometrajeRecorrido { get; set; }
        public int viajes { get; set; }
        public int obras { get; set; }
        public decimal litrosConsumidos { get; set; }
        public decimal metros { get; set; }
    }
}
