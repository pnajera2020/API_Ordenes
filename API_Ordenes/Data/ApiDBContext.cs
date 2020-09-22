using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class ApiDBContext : DbContext
    {
        public ApiDBContext(DbContextOptions<ApiDBContext> options) : base(options) { }

        //Creación de tablas
        public DbSet<CtEstatus> CtEstatus { get; set; }
        public DbSet<CtMetodoPago> CtMetodoPago { get; set; }
        public DbSet<TbOrden> TbOrden { get; set; }
        public DbSet<TbOrdenProducto> TbOrdenProducto { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<CtEstatus>(entity =>
            {
                entity.HasKey(e => e.PKIdEstatus);
                entity.ToTable("cat_estatus");
                entity.Property(e => e.PKIdEstatus).HasColumnName("id_estatus");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Activo).HasColumnName("activo");
            });

            modelBuilder.Entity<CtMetodoPago>(entity =>
            {
                entity.HasKey(e => e.PKIdMetodoPago);
                entity.ToTable("cat_metodo_pago");
                entity.Property(e => e.PKIdMetodoPago).HasColumnName("id_metodo_pago");
                entity.Property(e => e.Descripcion).HasColumnName("descripcion");
                entity.Property(e => e.Activo).HasColumnName("activo");
            });

            modelBuilder.Entity<TbOrden>(entity =>
            {
                entity.HasKey(e => e.PKIdOrden);
                entity.ToTable("tb_orden");
                entity.Property(e => e.PKIdOrden).HasColumnName("id_orden");
                entity.Property(e => e.Fecha).HasColumnName("fecha");
                entity.Property(e => e.Total).HasColumnName("total");
                entity.Property(e => e.FKIdMetodoPago).HasColumnName("id_metodo_pago");
                entity.Property(e => e.FKIdEstatus).HasColumnName("id_estatus");
                entity.Property(e => e.FKIdUsuario).HasColumnName("id_usuario");

                entity.HasOne(d => d.FKIdMetodoPagoNavigation)
                    .WithMany(p => p.TbOrden)
                    .HasForeignKey(d => d.FKIdMetodoPago)
                    .HasConstraintName("fk_cat_metodo_pago_id_metodo_pago");

                entity.HasOne(d => d.FKIdEstatusNavigation)
                    .WithMany(p => p.TbOrden)
                    .HasForeignKey(d => d.FKIdEstatus)
                    .HasConstraintName("fk_cat_orden_id_orden");
            });

            modelBuilder.Entity<TbOrdenProducto>(entity =>
            {
                entity.HasKey(e => e.PKIdOrdenProducto);
                entity.ToTable("tb_orden_producto");
                entity.Property(e => e.PKIdOrdenProducto).HasColumnName("id_orden");
                entity.Property(e => e.FKIdOrden).HasColumnName("id_orden");
                entity.Property(e => e.FKIdProducto).HasColumnName("id_producto");
                entity.Property(e => e.Cantidad).HasColumnName("cantidad");
                entity.Property(e => e.Activo).HasColumnName("activo");

                entity.HasOne(d => d.FKIdOrdenNavigation)
                    .WithMany(p => p.TbOrdenProducto)
                    .HasForeignKey(d => d.FKIdOrden)
                    .HasConstraintName("fk_cat_orden_id_orden");
            });
        }
    }
}

