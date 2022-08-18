using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OTMF_NETCORE_MVC.Models
{
    public partial class OTMFContext : DbContext
    {
        public OTMFContext()
        {
        }

        public OTMFContext(DbContextOptions<OTMFContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accesorio> Accesorios { get; set; } = null!;
        public virtual DbSet<Caja> Cajas { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Color> Colors { get; set; } = null!;
        public virtual DbSet<DetalleCajasRecibida> DetalleCajasRecibidas { get; set; } = null!;
        public virtual DbSet<Empleado> Empleados { get; set; } = null!;
        public virtual DbSet<Ensamble> Ensambles { get; set; } = null!;
        public virtual DbSet<EstadoOrden> EstadoOrdens { get; set; } = null!;
        public virtual DbSet<Estandar> Estandars { get; set; } = null!;
        public virtual DbSet<EstandarConRelevo> EstandarConRelevos { get; set; } = null!;
        public virtual DbSet<EstandarPorHora> EstandarPorHoras { get; set; } = null!;
        public virtual DbSet<EtiquetaCaja> EtiquetaCajas { get; set; } = null!;
        public virtual DbSet<Etiquetum> Etiqueta { get; set; } = null!;
        public virtual DbSet<FraccionEstandarRelevo> FraccionEstandarRelevos { get; set; } = null!;
        public virtual DbSet<Hule> Hules { get; set; } = null!;
        public virtual DbSet<Inserto> Insertos { get; set; } = null!;
        public virtual DbSet<Instructivo> Instructivos { get; set; } = null!;
        public virtual DbSet<InstructivoPieza> InstructivoPiezas { get; set; } = null!;
        public virtual DbSet<Maquina> Maquinas { get; set; } = null!;
        public virtual DbSet<MaquinaOrdenTrabajo> MaquinaOrdenTrabajos { get; set; } = null!;
        public virtual DbSet<Molde> Moldes { get; set; } = null!;
        public virtual DbSet<OrdenTrabajo> OrdenTrabajos { get; set; } = null!;
        public virtual DbSet<OrdenTrabajoEmpleado> OrdenTrabajoEmpleados { get; set; } = null!;
        public virtual DbSet<Parte> Partes { get; set; } = null!;
        public virtual DbSet<ParteAccesorio> ParteAccesorios { get; set; } = null!;
        public virtual DbSet<Pintura> Pinturas { get; set; } = null!;
        public virtual DbSet<PrefixOt> PrefixOts { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolesUsuario> RolesUsuarios { get; set; } = null!;
        public virtual DbSet<ScrapPermitido> ScrapPermitidos { get; set; } = null!;
        public virtual DbSet<Tarima> Tarimas { get; set; } = null!;
        public virtual DbSet<TipoEmpleado> TipoEmpleados { get; set; } = null!;
        public virtual DbSet<Turno> Turnos { get; set; } = null!;
        public virtual DbSet<TurnoOt> TurnoOts { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS;Initial Catalog=OTMF;Integrated Security=True;Persist Security Info=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accesorio>(entity =>
            {
                entity.HasKey(e => e.IdAccesorio)
                    .HasName("PK__Accesori__3AC1FE9172B73029");

                entity.ToTable("Accesorio");

                entity.Property(e => e.NombreAccesorio)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Caja>(entity =>
            {
                entity.HasKey(e => e.IdCaja)
                    .HasName("PK__Caja__3B7BF2C5D6E23E46");

                entity.ToTable("Caja");

                entity.Property(e => e.EtiquetaDeCaja)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCaja)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.IdCliente)
                    .HasName("PK__Cliente__D59466421101C303");

                entity.ToTable("Cliente");

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Color>(entity =>
            {
                entity.HasKey(e => e.IdColor)
                    .HasName("PK__Color__E83D55CB02532C6A");

                entity.ToTable("Color");

                entity.Property(e => e.NombreColor)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DetalleCajasRecibida>(entity =>
            {
                entity.HasKey(e => e.IdDetalleCajasRecibidas)
                    .HasName("PK__DetalleC__DB33E7DD49FF47C7");

                entity.Property(e => e.IdDetalleCajasRecibidas).HasColumnName("idDetalleCajasRecibidas");
            });

            modelBuilder.Entity<Empleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleado)
                    .HasName("PK__Empleado__CE6D8B9E4BFD1279");

                entity.ToTable("Empleado");

                entity.Property(e => e.ClaveEmpleado)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdTipoEmpleadoFk).HasColumnName("IdTipoEmpleadoFK");

                entity.Property(e => e.IdTurnoFk).HasColumnName("IdTurnoFK");

                entity.Property(e => e.NombreEmpleado)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdTipoEmpleadoFkNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdTipoEmpleadoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Empleado_IdTipoEmpleadoFK");

                entity.HasOne(d => d.IdTurnoFkNavigation)
                    .WithMany(p => p.Empleados)
                    .HasForeignKey(d => d.IdTurnoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Empleado_IdTurnoFK");
            });

            modelBuilder.Entity<Ensamble>(entity =>
            {
                entity.HasKey(e => e.IdEnsamble)
                    .HasName("PK__Ensamble__1208026138BF0FD7");

                entity.ToTable("Ensamble");

                entity.Property(e => e.NombreEnsamble)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstadoOrden>(entity =>
            {
                entity.HasKey(e => e.IdEstadoOrden)
                    .HasName("PK__EstadoOr__F2E6940E6570F41D");

                entity.ToTable("EstadoOrden");

                entity.Property(e => e.NombreEstadoOrden)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Estandar>(entity =>
            {
                entity.HasKey(e => e.IdEstandar)
                    .HasName("PK__Estandar__BB570ABD03ECE938");

                entity.ToTable("Estandar");

                entity.Property(e => e.NombreEstandar)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstandarConRelevo>(entity =>
            {
                entity.HasKey(e => e.IdEstandarConRelevo)
                    .HasName("PK__Estandar__ADA81589127258D1");

                entity.ToTable("EstandarConRelevo");

                entity.Property(e => e.NombreEstandarconRelevo)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EstandarPorHora>(entity =>
            {
                entity.HasKey(e => e.IdEstandarPorHora)
                    .HasName("PK__Estandar__8A90348331BD5A93");

                entity.ToTable("EstandarPorHora");
            });

            modelBuilder.Entity<EtiquetaCaja>(entity =>
            {
                entity.HasKey(e => e.IdEtiquetaDeCaja)
                    .HasName("PK__Etiqueta__591980CF82D0057E");

                entity.ToTable("EtiquetaCaja");

                entity.Property(e => e.NombreEtiquetaDeCaja)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PathEtiquetaDeCaja).IsUnicode(false);
            });

            modelBuilder.Entity<Etiquetum>(entity =>
            {
                entity.HasKey(e => e.IdEtiqueta)
                    .HasName("PK__Etiqueta__5041D72371186E81");

                entity.Property(e => e.NombreEtiqueta)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PathNombreEtiqueta).IsUnicode(false);
            });

            modelBuilder.Entity<FraccionEstandarRelevo>(entity =>
            {
                entity.HasKey(e => e.IdFraccionEstandarRelevo)
                    .HasName("PK__Fraccion__72BEC92AD9353AC0");

                entity.ToTable("FraccionEstandarRelevo");

                entity.Property(e => e.FracEstandarRelevo).HasColumnType("decimal(5, 5)");
            });

            modelBuilder.Entity<Hule>(entity =>
            {
                entity.HasKey(e => e.IdHule)
                    .HasName("PK__Hule__5D089DA5FE87A571");

                entity.ToTable("Hule");

                entity.Property(e => e.NombreHule)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Inserto>(entity =>
            {
                entity.HasKey(e => e.IdInserto)
                    .HasName("PK__Inserto__BC60CC6B9390FB88");

                entity.ToTable("Inserto");

                entity.Property(e => e.NombreInserto)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Instructivo>(entity =>
            {
                entity.HasKey(e => e.IdInstructivo)
                    .HasName("PK__Instruct__2ECAA4540DF4FA4C");

                entity.ToTable("Instructivo");

                entity.Property(e => e.NombreInstructivo)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<InstructivoPieza>(entity =>
            {
                entity.HasKey(e => e.IdInstructivoPieza)
                    .HasName("PK__Instruct__31F08B13B3C1819B");

                entity.ToTable("InstructivoPieza");

                entity.Property(e => e.NombreInstructivoPieza).IsUnicode(false);
            });

            modelBuilder.Entity<Maquina>(entity =>
            {
                entity.HasKey(e => e.IdMaquina)
                    .HasName("PK__Maquina__08E38C83290F5AE5");

                entity.ToTable("Maquina");

                entity.Property(e => e.NombreMaquina)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MaquinaOrdenTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdMaquinaOrdeTrabajo)
                    .HasName("PK__MaquinaO__7BAF6A96CE9CB9EB");

                entity.ToTable("MaquinaOrdenTrabajo");

                entity.Property(e => e.IdMaquinaFk).HasColumnName("IdMaquinaFK");

                entity.Property(e => e.IdOrdenTrabajoFk).HasColumnName("IdOrdenTrabajoFK");

                entity.HasOne(d => d.IdMaquinaFkNavigation)
                    .WithMany(p => p.MaquinaOrdenTrabajos)
                    .HasForeignKey(d => d.IdMaquinaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("MaquinaOrdenTrabajo_IdMaquinaFK");

                entity.HasOne(d => d.IdOrdenTrabajoFkNavigation)
                    .WithMany(p => p.MaquinaOrdenTrabajos)
                    .HasForeignKey(d => d.IdOrdenTrabajoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("MaquinOrdenTrabajo_IdOrdenTrabajoFK");
            });

            modelBuilder.Entity<Molde>(entity =>
            {
                entity.HasKey(e => e.IdMolde)
                    .HasName("PK__Molde__DCE06E79CAD3AA66");

                entity.ToTable("Molde");

                entity.Property(e => e.NombreMolde)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrdenTrabajo>(entity =>
            {
                entity.HasKey(e => e.IdOrdenTrabajo)
                    .HasName("PK__OrdenTra__48B02B96C5C3360F");

                entity.ToTable("OrdenTrabajo");

                entity.Property(e => e.IdOrdenTrabajo).ValueGeneratedNever();

                entity.Property(e => e.EstandarCalculado).HasColumnType("decimal(30, 10)");

                entity.Property(e => e.EstandarConRelevoCalculado).HasColumnType("decimal(30, 10)");

                entity.Property(e => e.EstandarPorHorasCalculado).HasColumnType("decimal(30, 10)");

                entity.Property(e => e.FechaOrdenTrabajo).HasColumnType("datetime");

                entity.Property(e => e.FracEstandarConRelevo).HasColumnType("decimal(30, 10)");

                entity.Property(e => e.HoraFinalizacion).HasColumnType("datetime");

                entity.Property(e => e.HoraInicio).HasColumnType("datetime");

                entity.Property(e => e.HorasTrabajadasCalculado).HasColumnType("decimal(30, 10)");

                entity.Property(e => e.IdCodigoOrdenTrabajo)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdEmpeadoSupervisorFk).HasColumnName("IdEmpeadoSupervisorFK");

                entity.Property(e => e.IdEmpleadoEmpacadorFk).HasColumnName("IdEmpleadoEmpacadorFK");

                entity.Property(e => e.IdEmpleadoMoldeadorFk).HasColumnName("IdEmpleadoMoldeadorFK");

                entity.Property(e => e.IdEstadoOrdenFk).HasColumnName("IdEstadoOrdenFK");

                entity.Property(e => e.IdEstandarConRelevoFk).HasColumnName("IdEstandarConRelevoFK");

                entity.Property(e => e.IdEstandarPorHoraFk).HasColumnName("IdEstandarPorHoraFK");

                entity.Property(e => e.IdInstructivoFk).HasColumnName("IdInstructivoFK");

                entity.Property(e => e.IdMaquinaFk).HasColumnName("IdMaquinaFK");

                entity.Property(e => e.IdParteFk).HasColumnName("IdParteFK");

                entity.Property(e => e.IdTurnoOtFk).HasColumnName("IdTurnoOtFK");

                entity.Property(e => e.PorcentajeScrapCalculado).HasColumnType("decimal(30, 10)");

                entity.Property(e => e.ScrapCalculado).HasColumnType("decimal(30, 10)");

                entity.HasOne(d => d.IdEstadoOrdenFkNavigation)
                    .WithMany(p => p.OrdenTrabajos)
                    .HasForeignKey(d => d.IdEstadoOrdenFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrdenTrabajo_IdEstadoOrdenFK");

                entity.HasOne(d => d.IdInstructivoFkNavigation)
                    .WithMany(p => p.OrdenTrabajos)
                    .HasForeignKey(d => d.IdInstructivoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrdenTrabajo_IdInstructivoFK");

                entity.HasOne(d => d.IdParteFkNavigation)
                    .WithMany(p => p.OrdenTrabajos)
                    .HasForeignKey(d => d.IdParteFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrdenTrabajo_IdParteFK");

                entity.HasOne(d => d.IdTurnoOtFkNavigation)
                    .WithMany(p => p.OrdenTrabajos)
                    .HasForeignKey(d => d.IdTurnoOtFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrdenTrabajo_IdTurnoOtFK");
            });

            modelBuilder.Entity<OrdenTrabajoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdEmpleadoOrdenTrabajo)
                    .HasName("PK__OrdenTra__B7CA9787544CA8A7");

                entity.ToTable("OrdenTrabajoEmpleado");

                entity.Property(e => e.IdEmpleadoFk).HasColumnName("IdEmpleadoFK");

                entity.Property(e => e.IdOrdenTrabajoFk).HasColumnName("IdOrdenTrabajoFK");

                entity.HasOne(d => d.IdEmpleadoFkNavigation)
                    .WithMany(p => p.OrdenTrabajoEmpleados)
                    .HasForeignKey(d => d.IdEmpleadoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrdenTrabajoEmpleado_IdEmpleadoFK");

                entity.HasOne(d => d.IdOrdenTrabajoFkNavigation)
                    .WithMany(p => p.OrdenTrabajoEmpleados)
                    .HasForeignKey(d => d.IdOrdenTrabajoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("OrdenTrabajoEmpleado_IdOrdenTrabajoFK");
            });

            modelBuilder.Entity<Parte>(entity =>
            {
                entity.HasKey(e => e.IdParte)
                    .HasName("PK__Parte__19B4EECD74441F1B");

                entity.ToTable("Parte");

                entity.Property(e => e.Aluminio).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.Costo).HasColumnType("decimal(19, 4)");

                entity.Property(e => e.IdCajaFk).HasColumnName("IdCajaFK");

                entity.Property(e => e.IdClienteFk).HasColumnName("IdClienteFK");

                entity.Property(e => e.IdCodigoParte)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.IdColorFk).HasColumnName("IdColorFK");

                entity.Property(e => e.IdEnsambleFk).HasColumnName("IdEnsambleFK");

                entity.Property(e => e.IdEstandarConRelevoFk).HasColumnName("IdEstandarConRelevoFK");

                entity.Property(e => e.IdEstandarFk).HasColumnName("IdEstandarFK");

                entity.Property(e => e.IdEstandarPorHoraFk).HasColumnName("IdEstandarPorHoraFK");

                entity.Property(e => e.IdEtiquetaCajaFk).HasColumnName("IdEtiquetaCajaFK");

                entity.Property(e => e.IdEtiquetaFk).HasColumnName("IdEtiquetaFK");

                entity.Property(e => e.IdHuleFk).HasColumnName("IdHuleFK");

                entity.Property(e => e.IdInsertoFk).HasColumnName("IdInsertoFK");

                entity.Property(e => e.IdMoldeFk).HasColumnName("IdMoldeFK");

                entity.Property(e => e.IdParteAccesorioFk).HasColumnName("IdParteAccesorioFK");

                entity.Property(e => e.IdPinturaFk).HasColumnName("IdPinturaFK");

                entity.Property(e => e.IdTarimaFk).HasColumnName("IdTarimaFK");

                entity.Property(e => e.StdPintura).HasColumnName("Std_Pintura");

                entity.HasOne(d => d.IdCajaFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdCajaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdCajaFK");

                entity.HasOne(d => d.IdClienteFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdClienteFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdClienteFK");

                entity.HasOne(d => d.IdColorFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdColorFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdColorFK");

                entity.HasOne(d => d.IdEnsambleFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdEnsambleFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdEnsambleFK");

                entity.HasOne(d => d.IdEstandarConRelevoFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdEstandarConRelevoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdEstandarConRelevoFK");

                entity.HasOne(d => d.IdEstandarFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdEstandarFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdEstandarFK");

                entity.HasOne(d => d.IdEstandarPorHoraFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdEstandarPorHoraFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdEstandarPorHoraFK");

                entity.HasOne(d => d.IdEtiquetaCajaFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdEtiquetaCajaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdEtiquetaDeCajaFK");

                entity.HasOne(d => d.IdEtiquetaFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdEtiquetaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdEtiquetaFK");

                entity.HasOne(d => d.IdHuleFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdHuleFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdHuleFK");

                entity.HasOne(d => d.IdInsertoFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdInsertoFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdInsertoFK");

                entity.HasOne(d => d.IdInstructivoPiezaFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdInstructivoPiezaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdInstructivoPiezaFK");

                entity.HasOne(d => d.IdMoldeFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdMoldeFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdMoldeFK");

                entity.HasOne(d => d.IdPinturaFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdPinturaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdPinturaFK");

                entity.HasOne(d => d.IdTarimaFkNavigation)
                    .WithMany(p => p.Partes)
                    .HasForeignKey(d => d.IdTarimaFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("Parte_IdTarimaFK");
            });

            modelBuilder.Entity<ParteAccesorio>(entity =>
            {
                entity.HasKey(e => e.IdParteAccesorio)
                    .HasName("PK__ParteAcc__1A38DF73985D3B7C");

                entity.ToTable("ParteAccesorio");

                entity.Property(e => e.IdAccesorioFk).HasColumnName("IdAccesorioFK");

                entity.Property(e => e.IdParteFk).HasColumnName("IdParteFK");

                entity.HasOne(d => d.IdAccesorioFkNavigation)
                    .WithMany(p => p.ParteAccesorios)
                    .HasForeignKey(d => d.IdAccesorioFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ParteAccesorio_IdAccesorio");

                entity.HasOne(d => d.IdParteFkNavigation)
                    .WithMany(p => p.ParteAccesorios)
                    .HasForeignKey(d => d.IdParteFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("ParteAccesorio_IdParteFK");
            });

            modelBuilder.Entity<Pintura>(entity =>
            {
                entity.HasKey(e => e.IdPintura)
                    .HasName("PK__Pintura__C5ECC4638F6F943A");

                entity.ToTable("Pintura");

                entity.Property(e => e.NombrePintura)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PrefixOt>(entity =>
            {
                entity.HasKey(e => e.IdPrefixOt)
                    .HasName("PK__PrefixOT__C52375458CF8FD8F");

                entity.ToTable("PrefixOT");

                entity.Property(e => e.IdPrefixOt).HasColumnName("IdPrefixOT");

                entity.Property(e => e.NombrePrefix)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRolUsuario)
                    .HasName("PK__Roles__3FC7F91F5567A1E4");

                entity.Property(e => e.NombreRolUsuario)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RolesUsuario>(entity =>
            {
                entity.HasKey(e => e.IdRolesUsuarios)
                    .HasName("PK__RolesUsu__B7E8A9E513B81230");

                entity.Property(e => e.IdRolUsuarioFk).HasColumnName("IdRolUsuarioFK");

                entity.Property(e => e.IdUsuariosFk).HasColumnName("IdUsuariosFK");

                entity.HasOne(d => d.IdRolUsuarioFkNavigation)
                    .WithMany(p => p.RolesUsuarios)
                    .HasForeignKey(d => d.IdRolUsuarioFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("RolesUsuarios_IdRolUsuarioFK");

                entity.HasOne(d => d.IdUsuariosFkNavigation)
                    .WithMany(p => p.RolesUsuarios)
                    .HasForeignKey(d => d.IdUsuariosFk)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("RolesUsuarios_IdUsuariosFK");
            });

            modelBuilder.Entity<ScrapPermitido>(entity =>
            {
                entity.HasKey(e => e.IdScrapPermitido)
                    .HasName("PK__ScrapPer__9EE2C7590D64B1A7");

                entity.ToTable("ScrapPermitido");

                entity.Property(e => e.PorcentajeScrapPermitido).HasColumnType("decimal(19, 5)");
            });

            modelBuilder.Entity<Tarima>(entity =>
            {
                entity.HasKey(e => e.IdTarima)
                    .HasName("PK__Tarima__78EA58FA673B053D");

                entity.ToTable("Tarima");

                entity.Property(e => e.NombreTarima)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TipoEmpleado>(entity =>
            {
                entity.HasKey(e => e.IdTipoEmpleado)
                    .HasName("PK__TipoEmpl__BD39922544EA224C");

                entity.ToTable("TipoEmpleado");

                entity.Property(e => e.NombreTipoEmpleado)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Turno>(entity =>
            {
                entity.HasKey(e => e.IdTurno)
                    .HasName("PK__Turno__C1ECF79A043DE7FA");

                entity.ToTable("Turno");

                entity.Property(e => e.HoraEntrada).HasColumnType("datetime");

                entity.Property(e => e.HoraSalida).HasColumnType("datetime");

                entity.Property(e => e.NombreTurno)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TurnoOt>(entity =>
            {
                entity.HasKey(e => e.IdTurnoOt)
                    .HasName("PK__TurnoOt__7844CFB8500BC306");

                entity.ToTable("TurnoOt");

                entity.Property(e => e.HorasTrabajadas).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.NombreTurno)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuarios)
                    .HasName("PK__Usuarios__EAEBAC8FF0F1632D");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmailNormalizado)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash).IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
