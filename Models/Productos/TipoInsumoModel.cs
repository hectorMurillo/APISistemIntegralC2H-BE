using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Productos
{
    public class TipoInsumoModel
    {
        public int Codigo { get; set; }
        public int CodigoUnidadMedida { get; set; }
        public string DescripcionUnidadMedida { get; set; }
        public string Descripcion { get; set; }
        public string Observacion { get; set; }
        public string Estatus { get; set; }
    }
}
