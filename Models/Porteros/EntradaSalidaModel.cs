using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Porteros
{
    public class EntradaSalidaModel
    {
        public object codEquipo { get; set; }
        public object codOperador { get; set; }
        public decimal kilometraje { get; set; }
        public decimal horometraje { get; set; }
        public bool entrada { get; set; }
        public object notaRemision { get; set; }
        public string observacion { get; set; }
        public int idNotasRemisionEnc { get; set; }
        public int codPlanta { get; set; }
    }
}
