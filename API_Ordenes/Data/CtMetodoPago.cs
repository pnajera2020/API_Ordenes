using System.Collections.Generic;

namespace Data
{
    public partial class CtMetodoPago
    {
        public CtMetodoPago()
        {
            TbOrden = new HashSet<TbOrden>();
        }

        public long PKIdMetodoPago { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<TbOrden> TbOrden { get; set; }
    }
}