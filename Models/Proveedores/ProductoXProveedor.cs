using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Proveedores
{
    public class ProductoXProveedor
    {
        public int Codigo { get; set; }
        public string Tipo { get; set; }
        public int CodProveedor { get; set; }
        public string Descripcion { get; set; }
        public string NombreProveedor { get; set; } 
        public string NombreProducto { get; set; }
        public decimal Costo { get; set; }
        public bool Inventario { get; set; } 
    }
}
