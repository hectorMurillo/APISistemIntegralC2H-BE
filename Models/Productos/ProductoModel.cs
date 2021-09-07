using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Productos
{
    public class ProductoModel
    {
        public int IdTipoProducto { get; set; }
        public int IdTipoResistencia { get; set; }
        public int IdTipoListaPrecio { get; set; }
        public int IdInsumo { get; set; }
        public string Descripcion { get; set; }
        public string Formula { get; set; }
        public int Cantidad { get; set; }
        public Decimal CostoXUnidadMedida { get; set; }
        public Decimal PrecioXUnidadMedida { get; set; }
        public string Especificaciones { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
    }
}
