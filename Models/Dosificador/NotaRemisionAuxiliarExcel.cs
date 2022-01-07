using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class NotaRemisionAuxiliarExcel
    {
        public int Folio { get; set; }
        public string Equipo { get; set; }
        public bool Maquilado { get; set; }
        public decimal Cantidad { get; set; }
        public string Nomenclatura { get; set; }
        public string FechaString { get; set; }
        public string Obra { get; set; }
        public string Cliente { get; set; }
        public string HoraSalida { get; set; }
        public string Estatus { get; set; }
        public string Dosificador { get; set; }
        public int FolioCarga { get; set; }
        public string Vendedor { get; set; }
        public bool Fibra { get; set; }
        public bool Imper { get; set; }
        public string Referencia { get; set; }
    }
}
