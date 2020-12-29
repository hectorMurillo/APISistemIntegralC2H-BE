﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Dosificador
{
    public class DatosNotaRemision
    {
        public int IdNotasRemisionEnc { get; set; }
        public int Folio { get; set; }
        public int FolioGinco { get; set; }
        public string Cliente { get; set; }
        public string Estatus { get; set; }
        public string Obra { get; set; }
        public string HoraSalida { get; set; }
        public string Formula { get; set; }
        public string Producto { get; set; }
        public string Operador { get; set; }
        public string Equipo { get; set; }
        public string Vendedor { get; set; }
        public string BombaEquipo { get; set; }
        public DateTime Fecha { get; set; }
        public string FechaConFormato { get; set; }
        public string CodOperador_1 { get; set; }
        public string CodOperador_2 { get; set; }
        public string CodEquipo_CR { get; set; }
        public string CodEquipo_BB { get; set; }
        public bool Bombeable { get; set; }
        public bool Fibra { get; set; }
        public bool Imper { get; set; }
        public double Cantidad { get; set; }

        public List<OperadorModel> Operadores { get; set; }
    }
}