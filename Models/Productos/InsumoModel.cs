using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Productos
{
    public class InsumoModel
    {
        public int IdInsumo { get; set; }
        public int IdTipoInsumo { get; set; }
        public int IdProveedor { get; set; }
        public string Descripcion { get; set; }
        public decimal CostoXUnidadMedida { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal ValorMaximo { get; set; }
        public decimal ValorIdeal { get; set; }
        public string Observaciones { get; set; }
        public string Estatus { get; set; }
    }
}
