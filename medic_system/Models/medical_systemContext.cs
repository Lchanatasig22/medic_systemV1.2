using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace medic_system.Models
{
    public partial class medical_systemContext : DbContext
    {
        public medical_systemContext()
        {
        }

        public medical_systemContext(DbContextOptions<medical_systemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AntecedentesFamiliare> AntecedentesFamiliares { get; set; } = null!;
        public virtual DbSet<Catalogo> Catalogos { get; set; } = null!;
        public virtual DbSet<Citum> Cita { get; set; } = null!;
        public virtual DbSet<ConsultaDiagnostico> ConsultaDiagnosticos { get; set; } = null!;
        public virtual DbSet<ConsultaImagen> ConsultaImagens { get; set; } = null!;
        public virtual DbSet<ConsultaLaboratorio> ConsultaLaboratorios { get; set; } = null!;
        public virtual DbSet<ConsultaMedicamento> ConsultaMedicamentos { get; set; } = null!;
        public virtual DbSet<Consultum> Consulta { get; set; } = null!;
        public virtual DbSet<Diagnostico> Diagnosticos { get; set; } = null!;
        public virtual DbSet<Especialidad> Especialidads { get; set; } = null!;
        public virtual DbSet<Establecimiento> Establecimientos { get; set; } = null!;
        public virtual DbSet<ExamenFisico> ExamenFisicos { get; set; } = null!;
        public virtual DbSet<Facturacion> Facturacions { get; set; } = null!;
        public virtual DbSet<Imagen> Imagens { get; set; } = null!;
        public virtual DbSet<Laboratorio> Laboratorios { get; set; } = null!;
        public virtual DbSet<Localidad> Localidads { get; set; } = null!;
        public virtual DbSet<Medicamento> Medicamentos { get; set; } = null!;
        public virtual DbSet<OrganosSistema> OrganosSistemas { get; set; } = null!;
        public virtual DbSet<Paciente> Pacientes { get; set; } = null!;
        public virtual DbSet<Pai> Pais { get; set; } = null!;
        public virtual DbSet<Perfil> Perfils { get; set; } = null!;
        public virtual DbSet<Usuario> Usuarios { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=localhost;Database=bdmedicossystem;User Id=sa;Password=1717;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AntecedentesFamiliare>(entity =>
            {
                entity.HasKey(e => e.IdAntecedente)
                    .HasName("PK__antecede__3E0146419B5A2384");

                entity.ToTable("antecedentes_familiares");

                entity.Property(e => e.IdAntecedente).HasColumnName("id_antecedente");

                entity.Property(e => e.Cancer).HasColumnName("cancer");

                entity.Property(e => e.Cardiopatia).HasColumnName("cardiopatia");

                entity.Property(e => e.Diabetes).HasColumnName("diabetes");

                entity.Property(e => e.EnfCardiovascular).HasColumnName("enf_cardiovascular");

                entity.Property(e => e.EnfInfecciosa).HasColumnName("enf_infecciosa");

                entity.Property(e => e.EnfMental).HasColumnName("enf_mental");

                entity.Property(e => e.Hipertension).HasColumnName("hipertension");

                entity.Property(e => e.MalFormacion).HasColumnName("mal_formacion");

                entity.Property(e => e.ObserCancer).HasColumnName("obser_cancer");

                entity.Property(e => e.ObserCardiopatia).HasColumnName("obser_cardiopatia");

                entity.Property(e => e.ObserDiabetes).HasColumnName("obser_diabetes");

                entity.Property(e => e.ObserEnfCardiovascular).HasColumnName("obser_enf_cardiovascular");

                entity.Property(e => e.ObserEnfInfecciosa).HasColumnName("obser_enf_infecciosa");

                entity.Property(e => e.ObserEnfMental).HasColumnName("obser_enf_mental");

                entity.Property(e => e.ObserHipertension).HasColumnName("obser_hipertension");

                entity.Property(e => e.ObserMalFormacion).HasColumnName("obser_mal_formacion");

                entity.Property(e => e.ObserOtro).HasColumnName("obser_otro");

                entity.Property(e => e.ObserTuberculosis).HasColumnName("obser_tuberculosis");

                entity.Property(e => e.Otro).HasColumnName("otro");

                entity.Property(e => e.ParentescoCatalogoCancer).HasColumnName("parentesco_catalogo_cancer");

                entity.Property(e => e.ParentescoCatalogoCardiopatia).HasColumnName("parentesco_catalogo_cardiopatia");

                entity.Property(e => e.ParentescoCatalogoDiabetes).HasColumnName("parentesco_catalogo_diabetes");

                entity.Property(e => e.ParentescoCatalogoEnfCardiovascular).HasColumnName("parentesco_catalogo_enf_cardiovascular");

                entity.Property(e => e.ParentescoCatalogoEnfInfecciosa).HasColumnName("parentesco_catalogo_enf_infecciosa");

                entity.Property(e => e.ParentescoCatalogoEnfMental).HasColumnName("parentesco_catalogo_enf_mental");

                entity.Property(e => e.ParentescoCatalogoHipertension).HasColumnName("parentesco_catalogo_hipertension");

                entity.Property(e => e.ParentescoCatalogoMalFormacion).HasColumnName("parentesco_catalogo_mal_formacion");

                entity.Property(e => e.ParentescoCatalogoOtro).HasColumnName("parentesco_catalogo_otro");

                entity.Property(e => e.ParentescoCatalogoTuberculosis).HasColumnName("parentesco_catalogo_tuberculosis");

                entity.Property(e => e.Tuberculosis).HasColumnName("tuberculosis");

                entity.HasOne(d => d.ParentescoCatalogoCancerNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoCancerNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoCancer)
                    .HasConstraintName("FK_Cancer_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoCardiopatiaNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoCardiopatiaNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoCardiopatia)
                    .HasConstraintName("FK_Cardiopatia_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoDiabetesNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoDiabetesNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoDiabetes)
                    .HasConstraintName("FK_Diabetes_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoEnfCardiovascularNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoEnfCardiovascularNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoEnfCardiovascular)
                    .HasConstraintName("FK_Enf_Cardiovascular_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoEnfInfecciosaNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoEnfInfecciosaNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoEnfInfecciosa)
                    .HasConstraintName("FK_Enf_Infecciosa_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoEnfMentalNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoEnfMentalNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoEnfMental)
                    .HasConstraintName("FK_Enf_Mental_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoHipertensionNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoHipertensionNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoHipertension)
                    .HasConstraintName("FK_Hipertension_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoMalFormacionNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoMalFormacionNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoMalFormacion)
                    .HasConstraintName("FK_Mal_Formacion_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoOtroNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoOtroNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoOtro)
                    .HasConstraintName("FK_Otro_Catalogo");

                entity.HasOne(d => d.ParentescoCatalogoTuberculosisNavigation)
                    .WithMany(p => p.AntecedentesFamiliareParentescoCatalogoTuberculosisNavigations)
                    .HasForeignKey(d => d.ParentescoCatalogoTuberculosis)
                    .HasConstraintName("FK_Tuberculosis_Catalogo");
            });

            modelBuilder.Entity<Catalogo>(entity =>
            {
                entity.HasKey(e => e.IdCatalogo)
                    .HasName("PK__catalogo__4B673DCA9131E79A");

                entity.ToTable("catalogo");

                entity.HasIndex(e => e.UuidCatalogo, "UQ__catalogo__C58C4DEBCA29A5F2")
                    .IsUnique();

                entity.Property(e => e.IdCatalogo).HasColumnName("id_catalogo");

                entity.Property(e => e.CategoriaCatalogo)
                    .HasMaxLength(255)
                    .HasColumnName("categoria_catalogo");

                entity.Property(e => e.DescripcionCatalogo).HasColumnName("descripcion_catalogo");

                entity.Property(e => e.EstadoCatalogo).HasColumnName("estado_catalogo");

                entity.Property(e => e.FechacreacionCatalogo)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_catalogo")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechamodificacionCatalogo)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_catalogo")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UsuariocreacionCatalogo)
                    .HasMaxLength(255)
                    .HasColumnName("usuariocreacion_catalogo");

                entity.Property(e => e.UsuariomodificacionCatalogo)
                    .HasMaxLength(255)
                    .HasColumnName("usuariomodificacion_catalogo");

                entity.Property(e => e.UuidCatalogo).HasColumnName("uuid_catalogo");
            });

            modelBuilder.Entity<Citum>(entity =>
            {
                entity.HasKey(e => e.IdCita)
                    .HasName("PK__cita__6AEC3C09EECAFDCF");

                entity.ToTable("cita");

                entity.Property(e => e.IdCita).HasColumnName("id_cita");

                entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");

                entity.Property(e => e.FechacreacionCita)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_cita");

                entity.Property(e => e.FechadelacitaCita)
                    .HasColumnType("date")
                    .HasColumnName("fechadelacita_cita");

                entity.Property(e => e.HoradelacitaCita).HasColumnName("horadelacita_cita");

                entity.Property(e => e.Motivo).HasColumnName("motivo");

                entity.Property(e => e.PacienteId).HasColumnName("paciente_id");

                entity.Property(e => e.UsuarioId).HasColumnName("usuario_id");

                entity.Property(e => e.UsuariocreacionCita)
                    .HasMaxLength(255)
                    .HasColumnName("usuariocreacion_cita");

                entity.HasOne(d => d.Consulta)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.ConsultaId)
                    .HasConstraintName("FK_cita_consulta");

                entity.HasOne(d => d.Paciente)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.PacienteId)
                    .HasConstraintName("FK_cita_paciente");

                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Cita)
                    .HasForeignKey(d => d.UsuarioId)
                    .HasConstraintName("FK_cita_usuario");
            });

            modelBuilder.Entity<ConsultaDiagnostico>(entity =>
            {
                entity.HasKey(e => e.IdConsultaDiagnostico)
                    .HasName("PK__consulta__D8B1E6BD6F33BB02");

                entity.ToTable("consulta_diagnostico");

                entity.Property(e => e.IdConsultaDiagnostico).HasColumnName("id_consulta_diagnostico");

                entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");

                entity.Property(e => e.DefinitivoDiagnosticos).HasColumnName("definitivo_diagnosticos");

                entity.Property(e => e.DiagnosticoId).HasColumnName("diagnostico_id");

                entity.Property(e => e.EstadoDiagnostico).HasColumnName("estado_diagnostico");

                entity.Property(e => e.ObservacionDiagnostico).HasColumnName("observacion_diagnostico");

                entity.Property(e => e.PresuntivoDiagnosticos).HasColumnName("presuntivo_diagnosticos");

                entity.Property(e => e.SecuencialConsultaDiag).HasColumnName("secuencial_consulta_diag");

                entity.HasOne(d => d.Diagnostico)
                    .WithMany(p => p.ConsultaDiagnosticos)
                    .HasForeignKey(d => d.DiagnosticoId)
                    .HasConstraintName("FK_consulta_diagnostico_diagnostico");
            });

            modelBuilder.Entity<ConsultaImagen>(entity =>
            {
                entity.HasKey(e => e.IdConsultaImagen)
                    .HasName("PK__consulta__43A0875DA44E8344");

                entity.ToTable("consulta_imagen");

                entity.Property(e => e.IdConsultaImagen).HasColumnName("id_consulta_imagen");

                entity.Property(e => e.CantidadImagen).HasColumnName("cantidad_imagen");

                entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");

                entity.Property(e => e.EstadoImagen).HasColumnName("estado_imagen");

                entity.Property(e => e.ImagenId).HasColumnName("imagen_id");

                entity.Property(e => e.ObservacionImagen).HasColumnName("observacion_imagen");

                entity.Property(e => e.SecuencialConsultaImg).HasColumnName("secuencial_consulta_img");

                entity.HasOne(d => d.Imagen)
                    .WithMany(p => p.ConsultaImagens)
                    .HasForeignKey(d => d.ImagenId)
                    .HasConstraintName("FK_consulta_imagen_imagen");
            });

            modelBuilder.Entity<ConsultaLaboratorio>(entity =>
            {
                entity.HasKey(e => e.IdLaboratorioConsulta)
                    .HasName("PK__consulta__14E407964824D99A");

                entity.ToTable("consulta_laboratorio");

                entity.Property(e => e.IdLaboratorioConsulta).HasColumnName("id_laboratorio_consulta");

                entity.Property(e => e.CantidadLaboratorio).HasColumnName("cantidad_laboratorio");

                entity.Property(e => e.CatalogoLaboratorioId).HasColumnName("catalogo_laboratorio_id");

                entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");

                entity.Property(e => e.EstadoLaboratorio).HasColumnName("estado_laboratorio");

                entity.Property(e => e.Observacion).HasColumnName("observacion");

                entity.Property(e => e.SecuencialConsultaLab).HasColumnName("secuencial_consulta_lab");

                entity.HasOne(d => d.CatalogoLaboratorio)
                    .WithMany(p => p.ConsultaLaboratorios)
                    .HasForeignKey(d => d.CatalogoLaboratorioId)
                    .HasConstraintName("FK_consulta_laboratorio_catalogo");
            });

            modelBuilder.Entity<ConsultaMedicamento>(entity =>
            {
                entity.HasKey(e => e.IdConsultaMedicamento)
                    .HasName("PK__consulta__12CF7342572BB25C");

                entity.ToTable("consulta_medicamentos");

                entity.Property(e => e.IdConsultaMedicamento).HasColumnName("id_consulta_medicamento");

                entity.Property(e => e.ConsultaId).HasColumnName("consulta_id");

                entity.Property(e => e.DosisMedicamento).HasColumnName("dosis_medicamento");

                entity.Property(e => e.EstadoMedicamento).HasColumnName("estado_medicamento");

                entity.Property(e => e.FechacreacionMedicamento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_medicamento");

                entity.Property(e => e.MedicamentoId).HasColumnName("medicamento_id");

                entity.Property(e => e.ObservacionMedicamento)
                    .HasMaxLength(255)
                    .HasColumnName("observacion_medicamento");

                entity.Property(e => e.SecuencialConsultaMed).HasColumnName("secuencial_consulta_med");

                entity.HasOne(d => d.Medicamento)
                    .WithMany(p => p.ConsultaMedicamentos)
                    .HasForeignKey(d => d.MedicamentoId)
                    .HasConstraintName("FK_consulta_medicamentos_medicamento");
            });

            modelBuilder.Entity<Consultum>(entity =>
            {
                entity.HasKey(e => e.IdConsulta)
                    .HasName("PK__consulta__6F53588B73370ECF");

                entity.ToTable("consulta");

                entity.Property(e => e.IdConsulta).HasColumnName("id_consulta");

                entity.Property(e => e.ActivoConsulta).HasColumnName("activo_consulta");

                entity.Property(e => e.AlergiasConsultaId).HasColumnName("alergias_consulta_id");

                entity.Property(e => e.AntecedentespersonalesConsulta).HasColumnName("antecedentespersonales_consulta");

                entity.Property(e => e.CirugiasConsultaId).HasColumnName("cirugias_consulta_id");

                entity.Property(e => e.ConsultaAntecedentesFamiliaresId).HasColumnName("consulta_antecedentes_familiares_id");

                entity.Property(e => e.ConsultaDiagnosticoId).HasColumnName("consulta_diagnostico_id");

                entity.Property(e => e.ConsultaExamenFisicoId).HasColumnName("consulta_examen_fisico_id");

                entity.Property(e => e.ConsultaImagenId).HasColumnName("consulta_imagen_id");

                entity.Property(e => e.ConsultaLaboratorioId).HasColumnName("consulta_laboratorio_id");

                entity.Property(e => e.ConsultaMedicamentoId).HasColumnName("consulta_medicamento_id");

                entity.Property(e => e.ConsultaOrganosSistemasId).HasColumnName("consulta_organos_sistemas_id");

                entity.Property(e => e.ConsultaprincipalConsulta).HasColumnName("consultaprincipal_consulta");

                entity.Property(e => e.DiasincapacidadConsulta).HasColumnName("diasincapacidad_consulta");

                entity.Property(e => e.EnfermedadConsulta).HasColumnName("enfermedad_consulta");

                entity.Property(e => e.EspecialidadId).HasColumnName("especialidad_id");

                entity.Property(e => e.EstadoConsultaC).HasColumnName("estado_consulta_c");

                entity.Property(e => e.FechaactualConsulta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechaactual_consulta")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechacreacionConsulta)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_consulta");

                entity.Property(e => e.FrecuenciarespiratoriaConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("frecuenciarespiratoria_consulta");

                entity.Property(e => e.HistorialConsulta).HasColumnName("historial_consulta");

                entity.Property(e => e.MedicoConsultaD).HasColumnName("medico_consulta_d");

                entity.Property(e => e.MotivoConsulta).HasColumnName("motivo_consulta");

                entity.Property(e => e.NombreparienteConsulta)
                    .HasMaxLength(255)
                    .HasColumnName("nombrepariente_consulta");

                entity.Property(e => e.NotasevolucionConsulta).HasColumnName("notasevolucion_consulta");

                entity.Property(e => e.Obseralergias)
                    .HasMaxLength(255)
                    .HasColumnName("obseralergias");

                entity.Property(e => e.ObsercirugiasId)
                    .HasMaxLength(255)
                    .HasColumnName("obsercirugias_id");

                entity.Property(e => e.ObservacionConsulta).HasColumnName("observacion_consulta");

                entity.Property(e => e.PacienteConsultaP).HasColumnName("paciente_consulta_p");

                entity.Property(e => e.PesoConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("peso_consulta");

                entity.Property(e => e.PlantratamientoConsulta).HasColumnName("plantratamiento_consulta");

                entity.Property(e => e.PresionarterialdiastolicaConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("presionarterialdiastolica_consulta");

                entity.Property(e => e.PresionarterialsistolicaConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("presionarterialsistolica_consulta");

                entity.Property(e => e.PulsoConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("pulso_consulta");

                entity.Property(e => e.Reconofarmacologicas).HasColumnName("reconofarmacologicas");

                entity.Property(e => e.SecuencialConsulta)
                    .HasMaxLength(50)
                    .HasColumnName("secuencial_consulta");

                entity.Property(e => e.SignosalarmaConsulta).HasColumnName("signosalarma_consulta");

                entity.Property(e => e.TallaConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("talla_consulta");

                entity.Property(e => e.TelefonoConsulta)
                    .HasMaxLength(20)
                    .HasColumnName("telefono_consulta");

                entity.Property(e => e.TemperaturaConsulta)
                    .HasMaxLength(10)
                    .HasColumnName("temperatura_consulta");

                entity.Property(e => e.TipoConsultaC).HasColumnName("tipo_consulta_c");

                entity.Property(e => e.TipoparienteConsulta).HasColumnName("tipopariente_consulta");

                entity.Property(e => e.UsuariocreacionConsulta)
                    .HasMaxLength(255)
                    .HasColumnName("usuariocreacion_consulta");

                entity.HasOne(d => d.AlergiasConsulta)
                    .WithMany(p => p.ConsultumAlergiasConsulta)
                    .HasForeignKey(d => d.AlergiasConsultaId)
                    .HasConstraintName("FK_consulta_alergias");

                entity.HasOne(d => d.CirugiasConsulta)
                    .WithMany(p => p.ConsultumCirugiasConsulta)
                    .HasForeignKey(d => d.CirugiasConsultaId)
                    .HasConstraintName("FK_consulta_cirugias");

                entity.HasOne(d => d.ConsultaAntecedentesFamiliares)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaAntecedentesFamiliaresId)
                    .HasConstraintName("FK_consulta_antecedentes_familiares");

                entity.HasOne(d => d.ConsultaDiagnostico)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaDiagnosticoId)
                    .HasConstraintName("FK_consulta_diagnostico");

                entity.HasOne(d => d.ConsultaExamenFisico)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaExamenFisicoId)
                    .HasConstraintName("FK_consulta_examen_fisico");

                entity.HasOne(d => d.ConsultaImagen)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaImagenId)
                    .HasConstraintName("FK_consulta_imagen");

                entity.HasOne(d => d.ConsultaLaboratorio)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaLaboratorioId)
                    .HasConstraintName("FK_consulta_laboratorio");

                entity.HasOne(d => d.ConsultaMedicamento)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaMedicamentoId)
                    .HasConstraintName("FK_consulta_medicamento");

                entity.HasOne(d => d.ConsultaOrganosSistemas)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.ConsultaOrganosSistemasId)
                    .HasConstraintName("FK_consulta_organos_sistemas");

                entity.HasOne(d => d.Especialidad)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.EspecialidadId)
                    .HasConstraintName("FK_consulta_especialidad");

                entity.HasOne(d => d.MedicoConsultaDNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.MedicoConsultaD)
                    .HasConstraintName("FK_consulta_medico");

                entity.HasOne(d => d.PacienteConsultaPNavigation)
                    .WithMany(p => p.Consulta)
                    .HasForeignKey(d => d.PacienteConsultaP)
                    .HasConstraintName("FK_consulta_paciente");
            });

            modelBuilder.Entity<Diagnostico>(entity =>
            {
                entity.HasKey(e => e.IdDiagnostico)
                    .HasName("PK__diagnost__1384B7453D6547E7");

                entity.ToTable("diagnostico");

                entity.Property(e => e.IdDiagnostico).HasColumnName("id_diagnostico");

                entity.Property(e => e.CategoriaDiagnostico)
                    .HasMaxLength(255)
                    .HasColumnName("categoria_diagnostico");

                entity.Property(e => e.CodigoDiagnostico)
                    .HasMaxLength(20)
                    .HasColumnName("codigo_diagnostico");

                entity.Property(e => e.DescripcionDiagnostico).HasColumnName("descripcion_diagnostico");

                entity.Property(e => e.EstadoDiagnostico).HasColumnName("estado_diagnostico");

                entity.Property(e => e.NombreDiagnostico)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_diagnostico");
            });

            modelBuilder.Entity<Especialidad>(entity =>
            {
                entity.HasKey(e => e.IdEspecialidad)
                    .HasName("PK__especial__C1D13763AD1C2269");

                entity.ToTable("especialidad");

                entity.HasIndex(e => e.UuidEspecialidad, "UQ__especial__A95965212849148B")
                    .IsUnique();

                entity.Property(e => e.IdEspecialidad).HasColumnName("id_especialidad");

                entity.Property(e => e.CategoriaEspecialidad)
                    .HasMaxLength(255)
                    .HasColumnName("categoria_especialidad");

                entity.Property(e => e.DescripcionEspecialidad).HasColumnName("descripcion_especialidad");

                entity.Property(e => e.EstadoEspecialidad).HasColumnName("estado_especialidad");

                entity.Property(e => e.NombreEspecialidad)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_especialidad");

                entity.Property(e => e.UuidEspecialidad).HasColumnName("uuid_especialidad");
            });

            modelBuilder.Entity<Establecimiento>(entity =>
            {
                entity.HasKey(e => e.IdEstablecimiento)
                    .HasName("PK__establec__AFEAEA2067401278");

                entity.ToTable("establecimiento");

                entity.Property(e => e.IdEstablecimiento).HasColumnName("id_establecimiento");

                entity.Property(e => e.CiudadEstablecimiento)
                    .HasMaxLength(255)
                    .HasColumnName("ciudad_establecimiento");

                entity.Property(e => e.DescripcionEstablecimiento).HasColumnName("descripcion_establecimiento");

                entity.Property(e => e.DireccionEstablecimiento).HasColumnName("direccion_establecimiento");

                entity.Property(e => e.EstadoEstablecimiento).HasColumnName("estado_establecimiento");

                entity.Property(e => e.FechacreacionEstablecimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_establecimiento")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechamodificacionEstablecimiento)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_establecimiento")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProvinciaEstablecimiento)
                    .HasMaxLength(255)
                    .HasColumnName("provincia_establecimiento");
            });

            modelBuilder.Entity<ExamenFisico>(entity =>
            {
                entity.HasKey(e => e.IdExamenFisico)
                    .HasName("PK__examen_f__B5B777B90C5A99B8");

                entity.ToTable("examen_fisico");

                entity.Property(e => e.IdExamenFisico).HasColumnName("id_examen_fisico");

                entity.Property(e => e.Abdomen).HasColumnName("abdomen");

                entity.Property(e => e.Cabeza).HasColumnName("cabeza");

                entity.Property(e => e.Cuello).HasColumnName("cuello");

                entity.Property(e => e.Extremidades).HasColumnName("extremidades");

                entity.Property(e => e.ObserAbdomen).HasColumnName("obser_abdomen");

                entity.Property(e => e.ObserCabeza).HasColumnName("obser_cabeza");

                entity.Property(e => e.ObserCuello).HasColumnName("obser_cuello");

                entity.Property(e => e.ObserExtremidades).HasColumnName("obser_extremidades");

                entity.Property(e => e.ObserPelvis).HasColumnName("obser_pelvis");

                entity.Property(e => e.ObserTorax).HasColumnName("obser_torax");

                entity.Property(e => e.Pelvis).HasColumnName("pelvis");

                entity.Property(e => e.Torax).HasColumnName("torax");
            });

            modelBuilder.Entity<Facturacion>(entity =>
            {
                entity.HasKey(e => e.IdFacturacion)
                    .HasName("PK__facturac__AC4FC89454CB249D");

                entity.ToTable("facturacion");

                entity.Property(e => e.IdFacturacion).HasColumnName("id_facturacion");

                entity.Property(e => e.CitaId).HasColumnName("cita_id");

                entity.Property(e => e.EstadoFactura)
                    .HasMaxLength(255)
                    .HasColumnName("estado_factura");

                entity.Property(e => e.FechaFacturacion)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha_facturacion");

                entity.Property(e => e.MetodoPago)
                    .HasMaxLength(255)
                    .HasColumnName("metodo_pago");

                entity.Property(e => e.TotalFactura)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("total_factura");

                entity.HasOne(d => d.Cita)
                    .WithMany(p => p.Facturacions)
                    .HasForeignKey(d => d.CitaId)
                    .HasConstraintName("FK_facturacion_cita");
            });

            modelBuilder.Entity<Imagen>(entity =>
            {
                entity.HasKey(e => e.IdImagen)
                    .HasName("PK__imagen__27CC2689B2A7DD74");

                entity.ToTable("imagen");

                entity.HasIndex(e => e.UuidImagen, "UQ__imagen__448604A08864C97A")
                    .IsUnique();

                entity.Property(e => e.IdImagen).HasColumnName("id_imagen");

                entity.Property(e => e.CategoriaImagen)
                    .HasMaxLength(255)
                    .HasColumnName("categoria_imagen");

                entity.Property(e => e.DescripcionImagen).HasColumnName("descripcion_imagen");

                entity.Property(e => e.EstadoImagen).HasColumnName("estado_imagen");

                entity.Property(e => e.NombreImagen)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_imagen");

                entity.Property(e => e.UuidImagen)
                    .HasMaxLength(255)
                    .HasColumnName("uuid_imagen");
            });

            modelBuilder.Entity<Laboratorio>(entity =>
            {
                entity.HasKey(e => e.IdLaboratorio)
                    .HasName("PK__laborato__781B42E2119E4DC2");

                entity.ToTable("laboratorio");

                entity.HasIndex(e => e.UuidLaboratorios, "UQ__laborato__4D3AAC175643A8C1")
                    .IsUnique();

                entity.Property(e => e.IdLaboratorio).HasColumnName("id_laboratorio");

                entity.Property(e => e.CategoriaLaboratorios)
                    .HasMaxLength(255)
                    .HasColumnName("categoria_laboratorios");

                entity.Property(e => e.DescripcionLaboratorio).HasColumnName("descripcion_laboratorio");

                entity.Property(e => e.EstadoLaboratorios).HasColumnName("estado_laboratorios");

                entity.Property(e => e.NombreLaboratorio)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_laboratorio");

                entity.Property(e => e.UuidLaboratorios)
                    .HasMaxLength(255)
                    .HasColumnName("uuid_laboratorios");
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad)
                    .HasName("PK__localida__9A5E82AA91B17BEF");

                entity.ToTable("localidad");

                entity.Property(e => e.IdLocalidad).HasColumnName("id_localidad");

                entity.Property(e => e.CiaLocalidad)
                    .HasMaxLength(10)
                    .HasColumnName("cia_localidad");

                entity.Property(e => e.CodigoLocalidad)
                    .HasMaxLength(10)
                    .HasColumnName("codigo_localidad");

                entity.Property(e => e.EstadoLocalidad).HasColumnName("estado_localidad");

                entity.Property(e => e.FechacreacionLocalidad)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_localidad")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechamodificacionLocalidad)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_localidad")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.GentilicioLocalidad)
                    .HasMaxLength(255)
                    .HasColumnName("gentilicio_localidad");

                entity.Property(e => e.IsoLocalidad)
                    .HasMaxLength(10)
                    .HasColumnName("iso_localidad");

                entity.Property(e => e.IsoadLocalidad)
                    .HasMaxLength(10)
                    .HasColumnName("isoad_localidad");

                entity.Property(e => e.NombreLocalidad)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_localidad");

                entity.Property(e => e.PaisId).HasColumnName("pais_id");

                entity.Property(e => e.PrefijoLocalidad)
                    .HasMaxLength(10)
                    .HasColumnName("prefijo_localidad");

                entity.Property(e => e.UsuariocreacionLocalidad)
                    .HasMaxLength(255)
                    .HasColumnName("usuariocreacion_localidad");

                entity.Property(e => e.UsuariomodificacionLocalidad)
                    .HasMaxLength(255)
                    .HasColumnName("usuariomodificacion_localidad");

                entity.HasOne(d => d.Pais)
                    .WithMany(p => p.Localidads)
                    .HasForeignKey(d => d.PaisId)
                    .HasConstraintName("FK_localidad_pais");
            });

            modelBuilder.Entity<Medicamento>(entity =>
            {
                entity.HasKey(e => e.IdMedicamento)
                    .HasName("PK__medicame__2588C0320E6CE23B");

                entity.ToTable("medicamentos");

                entity.HasIndex(e => e.UuidMedicamento, "UQ__medicame__307E4C1D67676EBC")
                    .IsUnique();

                entity.Property(e => e.IdMedicamento).HasColumnName("id_medicamento");

                entity.Property(e => e.CategoriaMedicamento)
                    .HasMaxLength(255)
                    .HasColumnName("categoria_medicamento");

                entity.Property(e => e.ConcentracionMedicamento)
                    .HasMaxLength(50)
                    .HasColumnName("concentracion_medicamento");

                entity.Property(e => e.DescripcionMedicamento).HasColumnName("descripcion_medicamento");

                entity.Property(e => e.DistintivoMedicamento)
                    .HasMaxLength(50)
                    .HasColumnName("distintivo_medicamento");

                entity.Property(e => e.EstadoMedicamento).HasColumnName("estado_medicamento");

                entity.Property(e => e.NombreMedicamento)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_medicamento");

                entity.Property(e => e.UuidMedicamento)
                    .HasMaxLength(500)
                    .HasColumnName("uuid_medicamento");
            });

            modelBuilder.Entity<OrganosSistema>(entity =>
            {
                entity.HasKey(e => e.IdOrganosSistemas)
                    .HasName("PK__organos___222F2383DE2AC9C2");

                entity.ToTable("organos_sistemas");

                entity.Property(e => e.IdOrganosSistemas).HasColumnName("id_organos_sistemas");

                entity.Property(e => e.CardioVascular).HasColumnName("cardio_vascular");

                entity.Property(e => e.Digestivo).HasColumnName("digestivo");

                entity.Property(e => e.Endocrino).HasColumnName("endocrino");

                entity.Property(e => e.Genital).HasColumnName("genital");

                entity.Property(e => e.Linfatico).HasColumnName("linfatico");

                entity.Property(e => e.MEsqueletico).HasColumnName("m_esqueletico");

                entity.Property(e => e.Nervioso).HasColumnName("nervioso");

                entity.Property(e => e.ObserCardioVascular).HasColumnName("obser_cardio_vascular");

                entity.Property(e => e.ObserDigestivo).HasColumnName("obser_digestivo");

                entity.Property(e => e.ObserEndocrino).HasColumnName("obser_endocrino");

                entity.Property(e => e.ObserGenital).HasColumnName("obser_genital");

                entity.Property(e => e.ObserLinfatico).HasColumnName("obser_linfatico");

                entity.Property(e => e.ObserMEsqueletico).HasColumnName("obser_m_esqueletico");

                entity.Property(e => e.ObserNervioso).HasColumnName("obser_nervioso");

                entity.Property(e => e.ObserOrgSentidos).HasColumnName("obser_org_sentidos");

                entity.Property(e => e.ObserRespiratorio).HasColumnName("obser_respiratorio");

                entity.Property(e => e.ObserUrinario).HasColumnName("obser_urinario");

                entity.Property(e => e.OrgSentidos).HasColumnName("org_sentidos");

                entity.Property(e => e.Respiratorio).HasColumnName("respiratorio");

                entity.Property(e => e.Urinario).HasColumnName("urinario");
            });

            modelBuilder.Entity<Paciente>(entity =>
            {
                entity.HasKey(e => e.IdPacientes)
                    .HasName("PK__paciente__D80336DA8AAB2E32");

                entity.ToTable("pacientes");

                entity.Property(e => e.IdPacientes).HasColumnName("id_pacientes");

                entity.Property(e => e.CiPacientes).HasColumnName("ci_pacientes");

                entity.Property(e => e.DireccionPacientes).HasColumnName("direccion_pacientes");

                entity.Property(e => e.DonantePacientes)
                    .HasMaxLength(50)
                    .HasColumnName("donante_pacientes");

                entity.Property(e => e.EdadPacientes).HasColumnName("edad_pacientes");

                entity.Property(e => e.EmailPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("email_pacientes");

                entity.Property(e => e.EmpresaPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("empresa_pacientes");

                entity.Property(e => e.EstadoPacientes).HasColumnName("estado_pacientes");

                entity.Property(e => e.EstadocivilPacientesCa).HasColumnName("estadocivil_pacientes_ca");

                entity.Property(e => e.FechacreacionPacientes)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_pacientes")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechamodificacionPacientes)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_pacientes")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechanacimientoPacientes)
                    .HasColumnType("date")
                    .HasColumnName("fechanacimiento_pacientes");

                entity.Property(e => e.FormacionprofesionalPacientesCa).HasColumnName("formacionprofesional_pacientes_ca");

                entity.Property(e => e.NacionalidadPacientesPa).HasColumnName("nacionalidad_pacientes_pa");

                entity.Property(e => e.OcupacionPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("ocupacion_pacientes");

                entity.Property(e => e.PrimerapellidoPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("primerapellido_pacientes");

                entity.Property(e => e.PrimernombrePacientes)
                    .HasMaxLength(255)
                    .HasColumnName("primernombre_pacientes");

                entity.Property(e => e.ProvinciaPacientesL).HasColumnName("provincia_pacientes_l");

                entity.Property(e => e.SegundoapellidoPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("segundoapellido_pacientes");

                entity.Property(e => e.SegundonombrePacientes)
                    .HasMaxLength(255)
                    .HasColumnName("segundonombre_pacientes");

                entity.Property(e => e.SegurosaludPacientesCa).HasColumnName("segurosalud_pacientes_ca");

                entity.Property(e => e.SexoPacientesCa).HasColumnName("sexo_pacientes_ca");

                entity.Property(e => e.TelefonocelularPacientes)
                    .HasMaxLength(20)
                    .HasColumnName("telefonocelular_pacientes");

                entity.Property(e => e.TelefonofijoPacientes)
                    .HasMaxLength(20)
                    .HasColumnName("telefonofijo_pacientes");

                entity.Property(e => e.TipodocumentoPacientesCa).HasColumnName("tipodocumento_pacientes_ca");

                entity.Property(e => e.TiposangrePacientesCa).HasColumnName("tiposangre_pacientes_ca");

                entity.Property(e => e.UsuariocreacionPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("usuariocreacion_pacientes");

                entity.Property(e => e.UsuariomodificacionPacientes)
                    .HasMaxLength(255)
                    .HasColumnName("usuariomodificacion_pacientes");

                entity.HasOne(d => d.EstadocivilPacientesCaNavigation)
                    .WithMany(p => p.PacienteEstadocivilPacientesCaNavigations)
                    .HasForeignKey(d => d.EstadocivilPacientesCa)
                    .HasConstraintName("FK_pacientes_estadocivil");

                entity.HasOne(d => d.FormacionprofesionalPacientesCaNavigation)
                    .WithMany(p => p.PacienteFormacionprofesionalPacientesCaNavigations)
                    .HasForeignKey(d => d.FormacionprofesionalPacientesCa)
                    .HasConstraintName("FK_pacientes_formacionprofesional");

                entity.HasOne(d => d.NacionalidadPacientesPaNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.NacionalidadPacientesPa)
                    .HasConstraintName("FK_pacientes_nacionalidad");

                entity.HasOne(d => d.ProvinciaPacientesLNavigation)
                    .WithMany(p => p.Pacientes)
                    .HasForeignKey(d => d.ProvinciaPacientesL)
                    .HasConstraintName("FK_pacientes_provincia");

                entity.HasOne(d => d.SegurosaludPacientesCaNavigation)
                    .WithMany(p => p.PacienteSegurosaludPacientesCaNavigations)
                    .HasForeignKey(d => d.SegurosaludPacientesCa)
                    .HasConstraintName("FK_pacientes_segurosalud");

                entity.HasOne(d => d.SexoPacientesCaNavigation)
                    .WithMany(p => p.PacienteSexoPacientesCaNavigations)
                    .HasForeignKey(d => d.SexoPacientesCa)
                    .HasConstraintName("FK_pacientes_sexo");

                entity.HasOne(d => d.TipodocumentoPacientesCaNavigation)
                    .WithMany(p => p.PacienteTipodocumentoPacientesCaNavigations)
                    .HasForeignKey(d => d.TipodocumentoPacientesCa)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_pacientes_tipodocumento");

                entity.HasOne(d => d.TiposangrePacientesCaNavigation)
                    .WithMany(p => p.PacienteTiposangrePacientesCaNavigations)
                    .HasForeignKey(d => d.TiposangrePacientesCa)
                    .HasConstraintName("FK_pacientes_tiposangre");
            });

            modelBuilder.Entity<Pai>(entity =>
            {
                entity.HasKey(e => e.IdPais)
                    .HasName("PK__pais__0941A3A7032E06FA");

                entity.ToTable("pais");

                entity.HasIndex(e => e.IsoPais, "UQ__pais__5515698E28C2B1F7")
                    .IsUnique();

                entity.Property(e => e.IdPais).HasColumnName("id_pais");

                entity.Property(e => e.CodigoPais)
                    .HasMaxLength(5)
                    .HasColumnName("codigo_pais");

                entity.Property(e => e.EstadoPais).HasColumnName("estado_pais");

                entity.Property(e => e.GentilicioPais)
                    .HasMaxLength(255)
                    .HasColumnName("gentilicio_pais");

                entity.Property(e => e.IsoPais)
                    .HasMaxLength(3)
                    .HasColumnName("iso_pais");

                entity.Property(e => e.NombrePais)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_pais");
            });

            modelBuilder.Entity<Perfil>(entity =>
            {
                entity.HasKey(e => e.IdPerfil)
                    .HasName("PK__perfil__1D1C87684E5F2731");

                entity.ToTable("perfil");

                entity.Property(e => e.IdPerfil).HasColumnName("id_perfil");

                entity.Property(e => e.DescripcionPerfil).HasColumnName("descripcion_perfil");

                entity.Property(e => e.EstadoPerfil).HasColumnName("estado_perfil");

                entity.Property(e => e.FechacreacionPerfil)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_perfil")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NombrePerfil)
                    .HasMaxLength(255)
                    .HasColumnName("nombre_perfil");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("PK__usuario__4E3E04AD8878E2DC");

                entity.ToTable("usuario");

                entity.HasIndex(e => e.EmailUsuario, "UQ__usuario__CD3151FF02FCF376")
                    .IsUnique();

                entity.Property(e => e.IdUsuario).HasColumnName("id_usuario");

                entity.Property(e => e.ApellidosUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("apellidos_usuario");

                entity.Property(e => e.CiUsuario).HasColumnName("ci_usuario");

                entity.Property(e => e.ClaveUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("clave_usuario");

                entity.Property(e => e.CodigoSenecyt)
                    .HasMaxLength(255)
                    .HasColumnName("codigo_senecyt");

                entity.Property(e => e.CodigoUsuario)
                    .HasMaxLength(20)
                    .HasColumnName("codigo_usuario");

                entity.Property(e => e.CodigoqrUsuario).HasColumnName("codigoqr_usuario");

                entity.Property(e => e.DireccionUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("direccion_usuario");

                entity.Property(e => e.EmailUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("email_usuario");

                entity.Property(e => e.EspecialidadId).HasColumnName("especialidad_id");

                entity.Property(e => e.EstablecimientoId).HasColumnName("establecimiento_id");

                entity.Property(e => e.EstadoUsuario).HasColumnName("estado_usuario");

                entity.Property(e => e.FechacreacionUsuario)
                    .HasColumnType("datetime")
                    .HasColumnName("fechacreacion_usuario")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FechamodificacionUsuario)
                    .HasColumnType("datetime")
                    .HasColumnName("fechamodificacion_usuario")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FirmadigitalUsuario).HasColumnName("firmadigital_usuario");

                entity.Property(e => e.LoginUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("login_usuario");

                entity.Property(e => e.NombresUsuario)
                    .HasMaxLength(255)
                    .HasColumnName("nombres_usuario");

                entity.Property(e => e.PerfilId).HasColumnName("perfil_id");

                entity.Property(e => e.TelefonoUsuario)
                    .HasMaxLength(20)
                    .HasColumnName("telefono_usuario");

                entity.HasOne(d => d.Especialidad)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.EspecialidadId)
                    .HasConstraintName("FK_usuario_especialidad");

                entity.HasOne(d => d.Establecimiento)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.EstablecimientoId)
                    .HasConstraintName("FK_usuario_establecimiento");

                entity.HasOne(d => d.Perfil)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.PerfilId)
                    .HasConstraintName("FK_usuario_perfil");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
