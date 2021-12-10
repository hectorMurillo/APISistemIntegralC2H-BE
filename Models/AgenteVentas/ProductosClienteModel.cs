﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.AgenteVentas
{
    public class ProductosClienteModel
    {
        public int Codigo { get; set; }
		public int CodCliente { get; set; }
		public int CodAgente { get; set; }
		public int CodProducto { get; set; }
        public decimal Iva { get; set; }
        public string Nomenclatura { get; set; }
		public string DescripcionProducto { get; set; }
		public decimal PrecioSinIva { get; set; }
        public decimal PrecioConIva { get; set; }
        public bool Disabled { get; set; }
		public DateTime Fecha { get; set; }

	}
}