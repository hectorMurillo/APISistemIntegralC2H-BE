using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cobranza
{
    public class ListaPreciosDet
    {
        public int CodigoDet { get; set; }
        public string Nomenclatura { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioMin { get; set; }
        public decimal PrecioMax { get; set; }
    }
}
