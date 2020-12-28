using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Porteros
{
    public class Suministro
    {
        public string Equipo { get; set; }
        public string Operador { get; set; }
        public decimal Diesel { get; set; }
        public decimal Anticongelante { get; set; }
        public decimal Aceite { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
    }
}
