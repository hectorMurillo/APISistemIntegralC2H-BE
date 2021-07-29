using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cobranza
{
    public class DatosCredito
    {
        public decimal Credito { get; set; }
        public decimal CreditoDisponible { get; set; }

        public string CreditoFormato
        {
            get
            {
                return String.Format("{0:C}", Credito);
            }
        }

        public string CreditoDisponibleFormato
        {
            get
            {
                return String.Format("{0:C}", CreditoDisponible);
            }
        }
    }
}
