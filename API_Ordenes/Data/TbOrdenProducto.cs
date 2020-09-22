using System.Collections.Generic;

namespace Data
{
    public partial class TbOrdenProducto
    {
        public long PKIdOrdenProducto { get; set; }
        public long FKIdOrden { get; set; }
        public long FKIdProducto { get; set; }
        public decimal Cantidad { get; set; }
        public bool Activo { get; set; }
        public virtual TbOrden FKIdOrdenNavigation { get; set; }
    }
}