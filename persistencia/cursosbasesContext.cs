using System;
using Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace persistencia
{
    public partial class cursosbasesContext : DbContext
    {
        public cursosbasesContext()
        {
        }

        public cursosbasesContext(DbContextOptions<cursosbasesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comentario> Comentarios { get; set; }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Cursoinstructor> Cursoinstructors { get; set; }
        public virtual DbSet<Instructor> Instructors { get; set; }
        public virtual DbSet<Precio> Precios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.Idcomentario)
                    .HasName("PRIMARY");

                entity.ToTable("comentario");

                entity.HasIndex(e => e.CursoId, "fkCurso_idx");

                entity.Property(e => e.Idcomentario).HasColumnName("idcomentario");

                entity.Property(e => e.AlumnoName).HasMaxLength(155);

                entity.Property(e => e.Comentario1)
                    .HasMaxLength(255)
                    .HasColumnName("Comentario");

                entity.Property(e => e.CursoId).HasColumnName("cursoId");

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.CursoId)
                    .HasConstraintName("fkComentario");
            });

            modelBuilder.Entity<Curso>(entity =>
            {
                entity.HasKey(e => e.Idcurso)
                    .HasName("PRIMARY");

                entity.ToTable("curso");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Descripcion).HasMaxLength(45);

                entity.Property(e => e.Fotoportada)
                    .HasMaxLength(255)
                    .HasColumnName("fotoportada");

                entity.Property(e => e.Nombre).HasMaxLength(255);
            });

            modelBuilder.Entity<Cursoinstructor>(entity =>
            {
                entity.HasKey(e => new { e.Idcurso, e.Idinstructor })
                    .HasName("PRIMARY");

                entity.ToTable("cursoinstructor");

                entity.HasIndex(e => e.Idinstructor, "fkInstructor_idx");

                entity.Property(e => e.Idcurso).HasColumnName("idcurso");

                entity.Property(e => e.Idinstructor).HasColumnName("idinstructor");

                entity.HasOne(d => d.IdcursoNavigation)
                    .WithMany(p => p.Cursoinstructors)
                    .HasForeignKey(d => d.Idcurso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkCurso");

                entity.HasOne(d => d.IdinstructorNavigation)
                    .WithMany(p => p.Cursoinstructors)
                    .HasForeignKey(d => d.Idinstructor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkInstructor");
            });

            modelBuilder.Entity<Instructor>(entity =>
            {
                entity.HasKey(e => e.Idinstructor)
                    .HasName("PRIMARY");

                entity.ToTable("instructor");

                entity.Property(e => e.Idinstructor).HasColumnName("idinstructor");

                entity.Property(e => e.Apellidos).HasMaxLength(150);

                entity.Property(e => e.Foto).HasMaxLength(255);

                entity.Property(e => e.Grado).HasMaxLength(150);

                entity.Property(e => e.Nombre).HasMaxLength(150);
            });

            modelBuilder.Entity<Precio>(entity =>
            {
                entity.ToTable("precios");

                entity.HasIndex(e => e.CursoId, "IX_Precios_CursoId");

                entity.Property(e => e.PrecioActual).HasColumnType("decimal(18,4)");

                entity.Property(e => e.Promocion).HasColumnType("decimal(18,4)");

                entity.HasOne(d => d.Curso)
                    .WithMany(p => p.Precios)
                    .HasForeignKey(d => d.CursoId)
                    .HasConstraintName("FK_Precios_curso_CursoId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
