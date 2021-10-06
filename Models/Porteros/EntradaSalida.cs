using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Porteros
{
    public class EntradaSalida
    {
        public string Equipo { get; set; }
        public string Operador { get; set; }
        public decimal Kilometraje { get; set; }
        public decimal Horometraje { get; set; }
        public string Fecha { get; set; }
        public string Usuario { get; set; }
        public bool Entrada { get; set; }
        public int NotaRemision { get; set; }
        public string Observacion { get; set; }
        public int FolioPedido { get; set; }
        public string Planta { get; set; }
    }
}
