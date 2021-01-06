using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pedidos
{
    public class Pedido
    {
        public string Equipo { get; set; }
        public string Operador { get; set; }
        public decimal Kilometraje { get; set; }
        public decimal Horometraje { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
        public bool Entrada { get; set; }
        public int NotaRemision { get; set; }
    }
}
