namespace Entities
{
    public class OrdenProducto
    {
        public long IdOrdenProducto { get; set; }
        public long IdOrden { get; set; }
        public long IdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public bool Activo { get; set; }
    }
}