using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class Empleado
    {
        CultureInfo ci = new CultureInfo("es-MX");
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + ApellidoP + " " + ApellidoM;
            }
            set
            {

            }
        }
        public string RFC { get; set; }
        public int CodigoTipo { get; set; }
        public string Tipo { get; set; }
        public bool Estatus { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Celular { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string FechaCorta
        {
            get
            {
                return FechaRegistro.ToString("dd-MMMM-yyyy", ci);
            }
        }
        public DireccionXEmpleado Direccion { get; set; }
    }
}
