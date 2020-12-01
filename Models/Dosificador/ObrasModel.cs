using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class ObrasModel
    {
        public int Codigo { get; set; }
		public int CodCliente { get; set; }
		public int Consecutivo { get; set; }
		public string CP { get; set; }
		public int CodColonia { get; set; }
		public string CalleNumero { get; set; }
		public string Referencia { get; set; }
		public string URLGoogleMaps { get; set; }
		public int CodTipoObra { get; set; }
		public decimal Latitude { get; set; }

        public decimal Longitud { get; set; }
	
	}
}
