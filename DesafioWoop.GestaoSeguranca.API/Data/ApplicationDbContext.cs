using DesafioWoop.GestaoSeguranca.API.Model;
using Microsoft.EntityFrameworkCore;

namespace DesafioWoop.GestaoSeguranca.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<UserLogin>? UserLogin { get; set; }
        public virtual DbSet<QuestionarioUsuario>? QuestionarioUsuario { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserLogin>(entity =>
        //    {
        //        entity.ToTable("UserLogin");
        //        entity.HasKey("Id").HasName("Pk_UserId");
        //        entity.HasMany(e => e.q)
        //        entity.Property(e => e.Email).HasMaxLength(100).IsUnicode(false);
        //        entity.Property(e => e.Senha).HasMaxLength(100).IsUnicode(false);
        //    });

        //    modelBuilder.Entity<QuestionarioUsuario>(entity =>
        //    {
        //        entity.ToTable("QuestionarioUsuario");
        //        entity.HasKey("Id").HasName("Pk_QuestionarioUsuarioId");
        //        entity.Property(e => e.Pergunta).HasMaxLength(2000).IsUnicode(false);
        //        entity.Property(e => e.Resposta).HasMaxLength(2000).IsUnicode(false);
        //    });

            
        //}
    }
}
