﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Empleados
{
    public class TipoDocumento
    {
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public bool Obligatorio { get; set; }
        public int CodigoTipoProceso { get; set; }
    }
}
