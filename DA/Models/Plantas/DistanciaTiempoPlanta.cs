namespace Models.Plantas
{
    public class DistanciaTiempoPlanta
    {
        public int CodPlanta { get; set; }
        public string NombrePlanta { get; set; }
        public int CodObra { get; set; }
        public decimal Tiempo { get; set; }
        public decimal Distancia { get; set; }
        public bool EsForaneo { get; set; }
        public decimal DistanciaForanea { get; set; }
        public decimal TotalPagarForaneo { get; set; }
    }
}