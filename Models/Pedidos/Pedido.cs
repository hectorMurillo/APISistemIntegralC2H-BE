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
        public int CodPlanta { get; set; }
        public string Vendedor { get; set; }
        public int CodObra { get; set; }
        public string Obra { get; set; }
        public string Usuario { get; set; }
        public string FechaSalida { get; set; }
        public string HoraSalida { get; set; }
        public string HoraSalidaYPlanta
        {
            get
            {
                return HoraSalida + " " + NombrePlanta;
            }
        }
        public decimal Cantidad { get; set; }
        public bool Cierre { get; set; }
        public decimal CantidadCierre { get; set; }
        //public string FechaFormato { get; set; }
        public string Estatus { get; set; }
        public bool TieneCierres { get; set; }
        public int CodProducto { get; set; }
        public string Nomenclatura { get; set; }
        public string FormatoCantidad { get; set; }

        public bool TieneDescuento { get; set; }
        public decimal PorcentajeDescuento { get; set; }
        public string Observacion { get; set; }
        public bool TieneFibra { get; set; }
        public bool TieneImper { get; set; }
        public string NombrePlanta { get; set; }
        public bool Bombeado { get; set; }

        public string FormatoPorcentajeDescuento
        {
            get
            {
                return PorcentajeDescuento + " %";
            }
        }

        public decimal PrecioUnitario { get; set; }
        public decimal PrecioTotal{ get; set; }

        public string PrecioUnitarioFormato
        {
            get
            {
                return string.Format("{0:C}", PrecioUnitario);
            }
        }

        public string PrecioTotalFormato
        {
            get
            {
                return string.Format("{0:C}", PrecioTotal);
            }
        }

    }
}
