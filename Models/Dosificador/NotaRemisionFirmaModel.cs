using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2HApiControlInterno.Modules
{
    public class NotaRemisionFirmaModel
    {
        public int IdNotaRemisionFirma { get; set; }
        public int IdNotaRemisionEnc { get; set; }
        public byte[] FirmaImagen { get; set; }
        public int Usuario { get; set; }
        public string Estatus { get; set; }
    }
}