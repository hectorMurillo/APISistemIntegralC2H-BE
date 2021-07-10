using Models.Dosificador;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Cobranza
{
    public class NotaRemisionCobranza
    {
        public int IdNotasRemisionEnc { get; set; }
        public int Folio { get; set; }
        public int FolioPedido { get; set; }
        public string Cliente { get; set; }
        public string Obra { get; set; }
        public string Vendedor { get; set; }
        public string Equipo { get; set; }
        public string Operador { get; set; }
        public string Producto { get; set; }
        public string ProductoDescripcion { get; set; }
        public string FechaFormato { get; set; }
        public string NombreUsuario { get; set; }
        public decimal Cantidad { get; set; }
        public string Estatus { get; set; }
        public string EstatusPago { get; set; }
    }
}
