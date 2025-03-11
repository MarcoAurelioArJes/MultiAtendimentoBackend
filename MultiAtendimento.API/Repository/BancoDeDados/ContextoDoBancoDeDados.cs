using Microsoft.EntityFrameworkCore;
using MultiAtendimento.API.Models;

namespace MultiAtendimento.API.Repository.BancoDeDados
{
    public class ContextoDoBancoDeDados : DbContext
    {
        private readonly IConfiguration _configuration;
        public ContextoDoBancoDeDados(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            string connectionString = environment != "Production" 
                                    ? _configuration.GetConnectionString("DefaultConnection")
                                    : Environment.GetEnvironmentVariable("CONNECTION_STRING");

            if (environment != null && !string.IsNullOrWhiteSpace(connectionString))
            {
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Mensagens)
                .WithOne(c => c.Chat)
                .HasForeignKey(c => c.ChatId);

            modelBuilder.Entity<Mensagem>()
                .HasOne(c => c.Chat)
                .WithMany(c => c.Mensagens)
                .HasForeignKey(c => c.ChatId);
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Setor> Setores { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Mensagem> Mensagens { get; set; }
    }
}
