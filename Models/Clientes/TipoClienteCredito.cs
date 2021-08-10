using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class TipoClienteCredito
    {
        public int CodTipoClienteCredito { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public int DiasPlazo { get; set; }
        public decimal CreditoLimite { get; set; }
        public string Estatus { get; set; }

        public string CreditoLimiteFormato { get { return String.Format("${0:n}", CreditoLimite); } }

    }
}
