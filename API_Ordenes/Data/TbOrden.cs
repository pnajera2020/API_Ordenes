using System.Collections.Generic;

namespace Data
{
    public partial class TbOrden
    {
        public TbOrden()
        {
            TbOrdenProducto = new HashSet<TbOrdenProducto>();
        }
        public long PKIdOrden { get; set; }
        public string Fecha { get; set; }
        public decimal Total { get; set; }
        public long FKIdMetodoPago { get; set; }
        public long FKIdEstatus { get; set; }
        public long FKIdUsuario { get; set; }
        public virtual CtMetodoPago FKIdMetodoPagoNavigation { get; set; }
        public virtual CtEstatus FKIdEstatusNavigation { get; set; }
        public virtual ICollection<TbOrdenProducto> TbOrdenProducto { get; set; }
    }
}
