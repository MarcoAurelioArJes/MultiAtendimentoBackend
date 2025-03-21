﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MultiAtendimento.API.Repository.BancoDeDados;

#nullable disable

namespace MultiAtendimento.API.Repository.BancoDeDados.Migrations
{
    [DbContext(typeof(ContextoDoBancoDeDados))]
    partial class ContextoDoBancoDeDadosModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MultiAtendimento.API.Models.Chat", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("AtendenteId")
                        .HasColumnType("int");

                    b.Property<int>("ClienteId")
                        .HasColumnType("int");

                    b.Property<string>("EmpresaCnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<int>("SetorId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AtendenteId");

                    b.HasIndex("ClienteId");

                    b.HasIndex("EmpresaCnpj");

                    b.HasIndex("SetorId");

                    b.ToTable("Chats");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmpresaCnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("SetorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaCnpj");

                    b.HasIndex("SetorId");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Empresa", b =>
                {
                    b.Property<string>("Cnpj")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Cnpj");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Mensagem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ChatId")
                        .HasColumnType("int");

                    b.Property<string>("Conteudo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmpresaCnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<int>("Remetente")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ChatId");

                    b.HasIndex("EmpresaCnpj");

                    b.ToTable("Mensagens");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Setor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmpresaCnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaCnpj");

                    b.ToTable("Setores");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AdministradorPrincipal")
                        .HasColumnType("tinyint(1)");

                    b.Property<int>("Cargo")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("EmpresaCnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int?>("SetorId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaCnpj");

                    b.HasIndex("SetorId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Chat", b =>
                {
                    b.HasOne("MultiAtendimento.API.Models.Usuario", "Atendente")
                        .WithMany()
                        .HasForeignKey("AtendenteId");

                    b.HasOne("MultiAtendimento.API.Models.Cliente", "Cliente")
                        .WithMany()
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiAtendimento.API.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiAtendimento.API.Models.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendente");

                    b.Navigation("Cliente");

                    b.Navigation("Empresa");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Cliente", b =>
                {
                    b.HasOne("MultiAtendimento.API.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiAtendimento.API.Models.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Mensagem", b =>
                {
                    b.HasOne("MultiAtendimento.API.Models.Chat", "Chat")
                        .WithMany("Mensagens")
                        .HasForeignKey("ChatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiAtendimento.API.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Chat");

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Setor", b =>
                {
                    b.HasOne("MultiAtendimento.API.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Usuario", b =>
                {
                    b.HasOne("MultiAtendimento.API.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaCnpj")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MultiAtendimento.API.Models.Setor", "Setor")
                        .WithMany()
                        .HasForeignKey("SetorId");

                    b.Navigation("Empresa");

                    b.Navigation("Setor");
                });

            modelBuilder.Entity("MultiAtendimento.API.Models.Chat", b =>
                {
                    b.Navigation("Mensagens");
                });
#pragma warning restore 612, 618
        }
    }
}
