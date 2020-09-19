using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Usuario
{
    public class Menu
    {
        public string Modulo { get; set; }
        public string Funcion { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public string URL { get; set; }
    }
    /// <summary>
    /// Este metodo me ayuda a personalizar la forma en la que quiero el json 
    /// </summary>
    public class EstructuraModulo
    {
        public string Modulo { get; set; }
        public List<EstructuraFuncion> Funciones { get; set; }
    }
    public class EstructuraFuncion
    {
        public string Funcion { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
        public string URL { get; set; }
    }
}

