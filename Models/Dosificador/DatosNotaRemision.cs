using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class DatosNotaRemision
    {
        public int Folio { get; set; }
        public int FolioGinco { get; set; }
        public string Cliente { get; set; }
        public string Obra { get; set; }
        public string Producto { get; set; }
        public string Operador { get; set; }
        public string Equipo { get; set; }
        public string Vendedor { get; set; }
        public string BombaEquipo { get; set; }
        public string Fecha { get; set; }
        public bool Bombeable { get; set; }
        public bool Fibra { get; set; }
        public bool Imper { get; set; }
        public double Cantidad { get; set; }




    }
}
