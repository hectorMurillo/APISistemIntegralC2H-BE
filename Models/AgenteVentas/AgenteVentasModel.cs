using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AgenteVentas
{
    public class AgenteVentasModel
    {
        public int Codigo { get; set; }
        public string Nombre { get; set; }
        public string ApellidoP { get; set; }
        public string ApellidoM { get; set; }
        public string Alias { get; set; }
        public string NombreCompleto
        {
            get
            {
                return Nombre + " " + ApellidoP + " " + ApellidoM;
            }
        }
        public DateTime FechaNacimiento { get; set; }
        public string FechaNacimientoCorta
        {
            get
            {
                return FechaNacimiento.ToString("dd-MM-yyyy");
            }
        }
        public string NSS { get; set; }
        public string Domicilio { get; set; }
        public string Telefono { get; set; }

        public string Celular { get; set; }
        public string CorreoElectronico { get; set; }
        public string Fotografia { get; set; }
    }
}
