using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Comisiones
{
    public class ComisionModel
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public decimal Minimo { get; set; }
        public decimal Maximo { get; set; }
        public string Estatus { get; set; }
        public bool FijaParaTodos { get; set; }
    }
}
