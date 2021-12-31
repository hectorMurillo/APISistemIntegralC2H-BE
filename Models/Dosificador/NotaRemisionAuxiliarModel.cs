using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class NotaRemisionAuxiliarModel
    {
        public int FolioCarga { get; set; }
        public int FolioNotaRemision { get; set; }
        public int CodProducto { get; set; }
        public String Cliente { get; set; }
        public String Obra { get; set; }
        public int CodVendedor { get; set; }
        public String OperadorCamionRevolvedor { get; set; }
        public String OperadorCamionBombeable { get; set; }
        public String EquipoCamionRevolvedor { get; set; }
        public String EquipoCamionBombeable { get; set; }
        public bool ChKBombeable { get; set; }
        public string Producto { get; set; }
        public float Cantidad { get; set; }
        public string Estatus { get; set; }
        public string HoraSalida { get; set; } = "12:00";
        public bool ChKFibra { get; set; }
        public bool ChKImper { get; set; }
        public bool Maquilado { get; set; }

        public DateTime Fecha { get; set; }
        public int CodUsuario { get; set; }
        public Decimal CantidadRestantePedido { get; set; }
        public bool Foraneo { get; set; }
        public ParametrosEspecialesModel parametrosEspeciales { get; set; }
        public string OperadorExterno { get; set; }
        public string EquipoExterno { get; set; }
        public string Observacion { get; set; }
        public int IdNotasRemisionEnc { get; set; }
    }
}
