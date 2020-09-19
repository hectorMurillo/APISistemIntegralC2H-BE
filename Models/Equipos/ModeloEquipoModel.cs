using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Equipos
{
    public class ModeloEquipoModel
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }
        public int CodMarca { get; set; }
        public int Año { get; set; }
        public string Marca { get; set; }
    }
}
