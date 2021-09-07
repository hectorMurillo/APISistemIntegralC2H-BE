using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Productos
{
    public class TipoProductoModel
    {
        public int IdTipoProducto { get; set; }
        public int IdTipoUnidadMedida { get; set; }
        public string DescripcionTipoUnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
    }
}
