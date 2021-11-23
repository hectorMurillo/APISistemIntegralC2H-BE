using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class DatosNotaRemision
    {
        public int IdNotasRemisionEnc { get; set; }
        public int FolioPedido { get; set; }
        public int Folio { get; set; }
        public int FolioGinco { get; set; }
        public string Cliente { get; set; }
        public string Domicilio { get; set; }
        public string Estatus { get; set; }
        public string Obra { get; set; }
        public string HoraSalida { get; set; }
        public string Formula { get; set; }
        public string Producto { get; set; }
        public string Operador { get; set; }
        public string OperadorBomba { get; set; }
        public string Equipo { get; set; }
        public string EquipoBomba { get; set; }
        public int CodEquipoBomba { get; set; }
        public string Vendedor { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaConFormato { get; set; }
        public string CodOperador_1 { get; set; }
        public string CodOperador_2 { get; set; }
        public string CodEquipo_CR { get; set; }
        public string CodEquipo_BB { get; set; }
        public bool Bombeable { get; set; }
        public bool Fibra { get; set; }
        public bool Imper { get; set; }
        public bool Maquilado { get; set; }
        public decimal Cantidad { get; set; }
        public decimal CantidadPedido { get; set; }
        public string FechaFormato { get; set; }
        public string NombreUsuario { get; set; }
        public string Nomenclatura { get; set; }
        public string CelularVendedor { get; set; }
        public string TelefonoEmpresa { get; set; }
        public string CorreoCliente { get; set; }
        public int CodOperador { get; set; }
        public int CodEquipo { get; set; }
        public string Observacion { get; set; }
        public decimal UltimoHorometraje { get; set; }
        public decimal UltimoKilometraje { get; set; }
        public string HoraSalidaPlanta { get; set; }
    }
}
