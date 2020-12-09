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
        public int RazonSocial { get; set; }
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
    }
}
