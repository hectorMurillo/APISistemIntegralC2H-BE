using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class FacturaCliente
    {
        public int Codigo { get; set; }
        public int CodCliente { get; set; }
        public string Tipo { get; set; }
        public string RazonSocial { get; set; }
        public string RFC { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
