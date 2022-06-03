using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class NotaRemisionEncModel
    {
        public int IdNotasRemisionEnc { get; set; }
        public int Folio { get; set; }
        public int FolioGinco { get; set; }
        public int FolioPedido { get; set; }
        public int FolioPadre { get; set; }
        public int CodFormula { get; set; }
        public int CodCliente { get; set; }
        public int CodObra { get; set; }
        public int CodVendedor { get; set; }
        public int CodProducto { get; set; }
        public int CodOperador_1 { get; set; }
        public int CodOperador_2 { get; set; }
        public int CodEquipo_CR { get; set; }
        public int CodEquipo_BB { get; set; }
        public bool ChKBombeable { get; set; }
        public string Producto { get; set; }
        public float Cantidad { get; set; }
        public string Estatus { get; set; }
        public string HoraSalida { get; set; } = "12:00";
        public bool ChKFibra { get; set; }
        public bool ChKImper { get; set; }
        public DateTime Fecha { get; set; }
        public int CodUsuario { get; set; }
        public Decimal CantidadRestantePedido { get; set; }
        public bool Foraneo { get; set; }
        public ParametrosEspecialesModel parametrosEspeciales { get; set; }
        public string OperadorExterno { get; set; }
        public string EquipoExterno { get; set; }
        public int SelloGarantiaFolio { get; set; }
        public string SelloGarantiaFormato { get; set; }
    }
}

  