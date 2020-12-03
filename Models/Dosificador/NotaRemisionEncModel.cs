using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class NotaRemisionEncModel
    {
        public int FolioGinco { get; set; }

        public int FolioNotaRemision { get; set; }
        public string HoraSalida { get; set; }
        public int CodCliente { get; set; }
        public int CodObra { get; set; }
        public int CodFormula { get; set; }
        public int codVendedor { get; set; }
        public int CodOperador_1 { get; set; }
        public int CodOperador_2 { get; set; }
        public int CodEquipo_CR { get; set; }
        public int CodEquipo_BB { get; set; }
        public float Cantidad { get; set; }
        public bool ChKBombeable { get; set; }
        public bool ChKFibra { get; set; }
        public bool ChKImper { get; set; }
        public string Producto { get; set; }
        
    }
}

  