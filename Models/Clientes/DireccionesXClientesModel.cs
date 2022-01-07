using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Clientes
{
    public class DireccionesXClientesModel
    {
        public int Codigo { get; set; }
        public int CodCliente { get; set; }
        public string CP { get; set; } = "";
        public int CodEstado { get; set; }
        public int CodColonia { get; set; }
        public string Colonia { get; set; }
        public string CalleNumero { get; set; } = "";
        public string Municipio { get; set; } = "";
        public string Domicilio
        {
            get
            {
                return $"{CalleNumero}, {Colonia}, {Municipio}";
            }
        }
        public int CodTipoObra { get; set; }
        public string TipoObra { get; set; } = "";
        public string Referencia { get; set; } = "";
        public string URLGoogleMaps { get; set; } = "";
        public decimal Latitud { get; set; }
        public decimal Longitud { get; set; }
        public bool Fiscal { get; set; }
        public bool Entrega { get; set; }
        public List<float> costoCaseta { get; set; }
        public string Alias { get; set; }
    }
}
