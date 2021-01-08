using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Porteros
{
    public class SuministroModel
    {
        public int codEquipo { get; set; }
        public int codOperador { get; set; }
        public decimal diesel { get; set; }
        public decimal anticongelante { get; set; }
        public decimal aceite { get; set; }
    }
}
