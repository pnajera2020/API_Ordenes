using System.Collections.Generic;

namespace Data
{
    public partial class CtEstatus
    {
        public CtEstatus()
        {
            TbOrden = new HashSet<TbOrden>();
        }

        public long PKIdEstatus { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        public virtual ICollection<TbOrden> TbOrden { get; set; }
    }
}