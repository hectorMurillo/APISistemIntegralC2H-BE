using Models.Comisiones;
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
        //CultureInfo ci = new CultureInfo("es-MX");
        public string apellidoP { get; set; }
        public string apellidoM { get; set; }
        public string curp { get; set; }
        public string celular { get; set; }
        //public int codigoTipo { get; set; }
        public int codigoTipoEmpleado { get; set; }
        public string correo { get; set; }
        public DireccionXEmpleado direccion { get; set; }
        public string estatus { get; set; }
        public string motivo { get; set; }
        public string nombre { get; set; }
        public string nombreCompleto
        {
            get
            {
                return nombre + " " + apellidoP + " " + apellidoM;
            }
            set
            {

            }
        }
        public string rFC { get; set; }
        public string telefono { get; set; }

        public int codigo { get; set; }
        public string tipo { get; set; }
        public DateTime fechaRegistro { get; set; }
        //public string fechaCorta
        //{
        //    get
        //    {
        //        return fechaRegistro.ToString("dd-MMMM-yyyy", ci);
        //    }
        //}

        public List<ComisionModel> comisiones { get; set; }
        public string NumSeguroSocial { get; set; }
        public string FechaNacimiento { get; set; }
        public DateTime FechaNacimientoDate {
            get
            {
                return Convert.ToDateTime(FechaNacimiento);
            }
        }
        public int Salario { get; set; }
    }
}
