using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Pedidos
{
    public class Pedido
    {
        public int Codigo { get; set; }
        public int FolioPedido { get; set; }
        public int CodCliente { get; set; }
        public string Cliente { get; set; }
        public int CodVendedor { get; set; }
        public string Vendedor { get; set; }
        public int CodObra { get; set; }
        public string Obra { get; set; }
        public string Usuario { get; set; }
        public string FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public decimal Cantidad { get; set; }
        public bool Cierre { get; set; }
        public decimal CantidadCierre { get; set; }
        //public string FechaFormato { get; set; }
        public string Estatus { get; set; }
        public bool TieneCierres { get; set; }
    }
}
