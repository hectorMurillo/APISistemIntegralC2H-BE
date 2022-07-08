using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Materiales
{
    public class ProductosAgregadosModel
    {
        public int Codigo { get; set; }
        public int IdProducto { get; set; }
        public string Nomenclatura { get; set; }
        public decimal Arena { get; set; }
        public decimal Grava38 { get; set; }
        public decimal Grava34 { get; set; }
        public decimal Grava112 { get; set; }
        public decimal Jal { get; set; }
        public decimal Aditivo1 { get; set; }
        public decimal Aditivo2 { get; set; }
        public decimal Agua { get; set; }
    }
}
