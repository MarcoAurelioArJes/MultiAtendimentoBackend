using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MultiAtendimento.API.Migrations
{
    /// <inheritdoc />
    public partial class MigracaoInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Empresas",
                columns: table => new
                {
                    Cnpj = table.Column<string>(type: "character varying(14)", maxLength: 14, nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empresas", x => x.Cnpj);
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    EmpresaCnpj = table.Column<string>(type: "character varying(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setores_Empresas_EmpresaCnpj",
                        column: x => x.EmpresaCnpj,
                        principalTable: "Empresas",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    SetorId = table.Column<int>(type: "integer", nullable: false),
                    EmpresaCnpj = table.Column<string>(type: "character varying(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Empresas_EmpresaCnpj",
                        column: x => x.EmpresaCnpj,
                        principalTable: "Empresas",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Clientes_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Senha = table.Column<string>(type: "text", nullable: false),
                    Cargo = table.Column<int>(type: "integer", nullable: false),
                    AdministradorPrincipal = table.Column<bool>(type: "boolean", nullable: false),
                    SetorId = table.Column<int>(type: "integer", nullable: true),
                    EmpresaCnpj = table.Column<string>(type: "character varying(14)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Empresas_EmpresaCnpj",
                        column: x => x.EmpresaCnpj,
                        principalTable: "Empresas",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chats",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    EmpresaCnpj = table.Column<string>(type: "character varying(14)", nullable: false),
                    AtendenteId = table.Column<int>(type: "integer", nullable: true),
                    SetorId = table.Column<int>(type: "integer", nullable: false),
                    ClienteId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chats_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Empresas_EmpresaCnpj",
                        column: x => x.EmpresaCnpj,
                        principalTable: "Empresas",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Setores_SetorId",
                        column: x => x.SetorId,
                        principalTable: "Setores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Chats_Usuarios_AtendenteId",
                        column: x => x.AtendenteId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Mensagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Conteudo = table.Column<string>(type: "text", nullable: false),
                    Remetente = table.Column<int>(type: "integer", nullable: false),
                    EmpresaCnpj = table.Column<string>(type: "character varying(14)", nullable: false),
                    ChatId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mensagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mensagens_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Mensagens_Empresas_EmpresaCnpj",
                        column: x => x.EmpresaCnpj,
                        principalTable: "Empresas",
                        principalColumn: "Cnpj",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chats_AtendenteId",
                table: "Chats",
                column: "AtendenteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_ClienteId",
                table: "Chats",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_EmpresaCnpj",
                table: "Chats",
                column: "EmpresaCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SetorId",
                table: "Chats",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EmpresaCnpj",
                table: "Clientes",
                column: "EmpresaCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_SetorId",
                table: "Clientes",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_ChatId",
                table: "Mensagens",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_Mensagens_EmpresaCnpj",
                table: "Mensagens",
                column: "EmpresaCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Setores_EmpresaCnpj",
                table: "Setores",
                column: "EmpresaCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_EmpresaCnpj",
                table: "Usuarios",
                column: "EmpresaCnpj");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_SetorId",
                table: "Usuarios",
                column: "SetorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Mensagens");

            migrationBuilder.DropTable(
                name: "Chats");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropTable(
                name: "Empresas");
        }
    }
}
