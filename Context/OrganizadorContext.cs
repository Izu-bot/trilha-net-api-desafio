using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public OrganizadorContext(DbContextOptions<OrganizadorContext> options) : base(options) { }
        public DbSet<Tarefa> Tarefas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Titulo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(t => t.Descricao)
                    .HasMaxLength(100);

                entity.Property(t => t.Data)
                    .IsRequired();

                entity.Property(t => t.Status)
                    .IsRequired()
                    .HasConversion<string>();
            });


        }
    }
}