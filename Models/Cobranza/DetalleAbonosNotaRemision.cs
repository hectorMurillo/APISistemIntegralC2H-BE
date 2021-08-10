using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cobranza
{
    public class DetalleAbonosNotaRemision
    {
        public int IdCobranza { get; set; }
        public decimal Importe { get; set; }
        public string Fecha { get; set; }
        public int CodUsuario { get; set; }
        public string Usuario { get; set; }

        public string ImporteFormato
        {
            get
            {
                return String.Format("{0:C}", Importe);
            }
        }
    }
}
