using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Data.Migrations
{
    public partial class prueba2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "cat_estatus",
                schema: "public",
                columns: table => new
                {
                    id_estatus = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_estatus", x => x.id_estatus);
                });

            migrationBuilder.CreateTable(
                name: "cat_metodo_pago",
                schema: "public",
                columns: table => new
                {
                    id_metodo_pago = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descripcion = table.Column<string>(nullable: true),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cat_metodo_pago", x => x.id_metodo_pago);
                });

            migrationBuilder.CreateTable(
                name: "tb_orden",
                schema: "public",
                columns: table => new
                {
                    id_orden = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fecha = table.Column<string>(nullable: true),
                    total = table.Column<decimal>(nullable: false),
                    id_metodo_pago = table.Column<long>(nullable: false),
                    id_estatus = table.Column<long>(nullable: false),
                    id_usuario = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_orden", x => x.id_orden);
                    table.ForeignKey(
                        name: "fk_cat_orden_id_orden",
                        column: x => x.id_estatus,
                        principalSchema: "public",
                        principalTable: "cat_estatus",
                        principalColumn: "id_estatus",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cat_metodo_pago_id_metodo_pago",
                        column: x => x.id_metodo_pago,
                        principalSchema: "public",
                        principalTable: "cat_metodo_pago",
                        principalColumn: "id_metodo_pago",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_orden_producto",
                schema: "public",
                columns: table => new
                {
                    id_orden = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_producto = table.Column<long>(nullable: false),
                    cantidad = table.Column<decimal>(nullable: false),
                    activo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_orden_producto", x => x.id_orden);
                    table.ForeignKey(
                        name: "fk_cat_orden_id_orden",
                        column: x => x.id_orden,
                        principalSchema: "public",
                        principalTable: "tb_orden",
                        principalColumn: "id_orden",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_orden_id_estatus",
                schema: "public",
                table: "tb_orden",
                column: "id_estatus");

            migrationBuilder.CreateIndex(
                name: "IX_tb_orden_id_metodo_pago",
                schema: "public",
                table: "tb_orden",
                column: "id_metodo_pago");

            migrationBuilder.CreateIndex(
                name: "IX_tb_orden_producto_id_orden",
                schema: "public",
                table: "tb_orden_producto",
                column: "id_orden");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_orden_producto",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tb_orden",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cat_estatus",
                schema: "public");

            migrationBuilder.DropTable(
                name: "cat_metodo_pago",
                schema: "public");
        }
    }
}
