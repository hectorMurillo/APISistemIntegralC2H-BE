using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pedidos
{
    public class PedidoModel
    {
        public string HoraSalida { get; set; }
        public string FechaSalida { get; set; }
        public int CodCliente { get; set; }
        public int CodObra { get; set; }
        public int codVendedor { get; set; }
        public float Cantidad { get; set; }
        public bool Cierre { get; set; }
        public float CantidadCierre { get; set; }

    }
}
