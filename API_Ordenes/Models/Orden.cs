namespace Entities
{
    public class Orden
    {
        public long IdOrden { get; set; }
        public string Clave { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public long IdUsuario { get; set; }
        public long IdEstatus { get; set; }
        public long IdMetodoPago { get; set; }
    }
}