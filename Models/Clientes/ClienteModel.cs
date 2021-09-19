using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class ClientesModel
    {
        CultureInfo ci = new CultureInfo("es-MX");
        public int Indice { get; set; }
        public int Codigo { get; set; }
        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + ApellidoP + " " + ApellidoM;
            }
        }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }

        public string RFC { get; set; }
        public string RazonSocial { get; set; }
        public string NombreComercial { get; set; }
        public string Alias { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public string Correo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaCorta
        {
            get
            {
                return FechaRegistro.ToString("dd-MMMM-yyyy", ci);
            }
        }
        public decimal Credito { get; set; }
        public string CreditoFormat { get { return String.Format("${0:n}", Credito); } }
        public int codEmpleadoVendedor { get; set; }
        public string NombreVendedor { get; set; }
        public string Usuario { get; set; }
        public int codVendedor { get; set; }
        public string estatusNuevo { get; set; }
        public string regimenFiscal { get; set; }
        //public int Plazo { get; set; }
        public decimal CreditoDisponible { get; set; }
        public string CreditoDisponibleFormato { get { return String.Format("${0:n}", CreditoDisponible); } }
        public int DiaRevision { get; set; }
        public int CodTipoCliente { get; set; }
        public int CodSegmento { get; set; }
        public int CodTipoClienteCredito { get; set; }
        public int CodTipoListaPrecio { get; set; }

        public bool FacturarPublicoGeneral { get; set; }

        public string CodigoNombreComercial
        {
            get
            {
                return Codigo + " - " + NombreComercial;
            }
        }
    }
}
