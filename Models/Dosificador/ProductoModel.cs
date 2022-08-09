using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class ProductoModel
    {
        public int Codigo { get; set; }
        public string Nomenclatura { get; set; }
        public string Descripcion { get; set; }
        public string Edad { get; set; }
        public string Resistencia { get; set; }
        public int TMA { get; set; }
        public string Revenimiento { get; set; }
        public decimal PrecioMinimo { get; set; }
        public decimal PrecioMaximo { get; set; }
        public string Adicionantes { get; set; }
        public bool Fibra { get; set; }
        public bool Imper { get; set; }
        public int CodUnidadMedida { get; set; }
        public string UnidadMedida { get; set; }
    }
}
