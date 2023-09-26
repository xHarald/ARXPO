using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ITC2._0.Models;

public partial class ArxpoContext : DbContext
{
    public ArxpoContext()
    {
    }

    public ArxpoContext(DbContextOptions<ArxpoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Actividade> Actividades { get; set; }

    public virtual DbSet<Administradore> Administradores { get; set; }

    public virtual DbSet<Docente> Docentes { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Facultade> Facultades { get; set; }

    public virtual DbSet<Presentacione> Presentaciones { get; set; }

    public virtual DbSet<Programa> Programas { get; set; }

    public virtual DbSet<Proyecto> Proyectos { get; set; }

    public virtual DbSet<Supervisore> Supervisores { get; set; }

    public virtual DbSet<Tarjeta> Tarjetas { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-E1EBHHH; Database=ARXPO; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actividade>(entity =>
        {
            entity.ToTable("actividades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Horas).HasColumnName("horas");
            entity.Property(e => e.IdEstudiante).HasColumnName("id_estudiante");
            entity.Property(e => e.Terminar).HasColumnName("terminar");
            entity.Property(e => e.TituloActividad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo_actividad");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Actividades)
                .HasForeignKey(d => d.IdEstudiante)
                .HasConstraintName("FK_actividades_estudiantes");
        });

        modelBuilder.Entity<Administradore>(entity =>
        {
            entity.ToTable("administradores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Administradores)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_administradores_usuarios");
        });

        modelBuilder.Entity<Docente>(entity =>
        {
            entity.ToTable("docentes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPresentacion).HasColumnName("id_presentacion");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdPresentacionNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdPresentacion)
                .HasConstraintName("FK_docentes_presentaciones");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Docentes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_docentes_usuarios");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.ToTable("estudiantes");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdPrograma).HasColumnName("id_programa");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");
            entity.Property(e => e.Identificacion).HasColumnName("identificacion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TipoIdentificacion)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("tipo_identificacion");

            entity.HasOne(d => d.IdProgramaNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdPrograma)
                .HasConstraintName("FK_estudiantes_programas");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK_estudiantes_proyectos");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Estudiantes)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_estudiantes_usuarios");
        });

        modelBuilder.Entity<Facultade>(entity =>
        {
            entity.ToTable("facultades");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("correo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TelefonoContacto)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("telefono_contacto");
        });

        modelBuilder.Entity<Presentacione>(entity =>
        {
            entity.ToTable("presentaciones");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DiaPresentacion)
                .HasColumnType("datetime")
                .HasColumnName("dia_presentacion");
            entity.Property(e => e.IdAdministrador).HasColumnName("id_administrador");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Salon)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("salon");

            entity.HasOne(d => d.IdAdministradorNavigation).WithMany(p => p.Presentaciones)
                .HasForeignKey(d => d.IdAdministrador)
                .HasConstraintName("FK_presentaciones_administradores");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Presentaciones)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK_presentaciones_proyectos");
        });

        modelBuilder.Entity<Programa>(entity =>
        {
            entity.ToTable("programas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.IdFacultad).HasColumnName("id_facultad");
            entity.Property(e => e.NombrePrograma)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_programa");

            entity.HasOne(d => d.IdFacultadNavigation).WithMany(p => p.Programas)
                .HasForeignKey(d => d.IdFacultad)
                .HasConstraintName("FK_programas_facultades");
        });

        modelBuilder.Entity<Proyecto>(entity =>
        {
            entity.ToTable("proyectos");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.IdTarjeta).HasColumnName("id_tarjeta");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.NumeroIntegrantes).HasColumnName("numero_integrantes");
            entity.Property(e => e.UltimaActualizacion)
                .HasColumnType("datetime")
                .HasColumnName("ultima_actualizacion");

            entity.HasOne(d => d.IdTarjetaNavigation).WithMany(p => p.Proyectos)
                .HasForeignKey(d => d.IdTarjeta)
                .HasConstraintName("FK_proyectos_tarjetas");
        });

        modelBuilder.Entity<Supervisore>(entity =>
        {
            entity.ToTable("supervisores");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdDocente).HasColumnName("id_docente");
            entity.Property(e => e.IdProyecto).HasColumnName("id_proyecto");
            entity.Property(e => e.Motivo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("motivo");

            entity.HasOne(d => d.IdDocenteNavigation).WithMany(p => p.Supervisores)
                .HasForeignKey(d => d.IdDocente)
                .HasConstraintName("FK_supervisores_docentes");

            entity.HasOne(d => d.IdProyectoNavigation).WithMany(p => p.Supervisores)
                .HasForeignKey(d => d.IdProyecto)
                .HasConstraintName("FK_supervisores_proyectos");
        });

        modelBuilder.Entity<Tarjeta>(entity =>
        {
            entity.ToTable("tarjetas");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("estado");
            entity.Property(e => e.Extension)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("extension");
            entity.Property(e => e.FechaSubida)
                .HasColumnType("datetime")
                .HasColumnName("fecha_subida");
            entity.Property(e => e.FechaTerminado)
                .HasColumnType("datetime")
                .HasColumnName("fecha_terminado");
            entity.Property(e => e.Link)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("link");
            entity.Property(e => e.Observacion)
                .HasMaxLength(240)
                .IsUnicode(false)
                .HasColumnName("observacion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("titulo");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contraseña)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("contraseña");
            entity.Property(e => e.Correo)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("correo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
