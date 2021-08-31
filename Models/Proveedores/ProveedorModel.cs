using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Proveedores
{
    public class ProveedorModel
    {
        public int IdProveedor { get; set; }

        public string Nombre { get; set; }

        public string ApellidoP { get; set; }

        public string APellidoM { get; set; }

        public  string  NombreComercial { get; set; }

        public string RFC { get; set; }

        public string CP { get; set; }

        public string Estado { get; set; }

        public string Municipio { get; set; }

        public string Ciudad { get; set; }

        public string Colonia { get; set; }

        public string CalleNumero { get; set; }

        public string Tipo { get; set; }

        public string Usuario { get; set; }

        public string Contraseña { get; set; }

        public string ConfirmarContraseña { get; set; }

        public string Regimen { get; set; }

        public DateTime FechaRegistro { get; set; }
    } 
}
