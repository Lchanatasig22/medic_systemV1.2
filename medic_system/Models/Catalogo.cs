using System;
using System.Collections.Generic;

namespace medic_system.Models
{
    public partial class Catalogo
    {
        public Catalogo()
        {
            AntecedentesFamiliareParentescoCatalogoCancerNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoCardiopatiaNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoDiabetesNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoEnfCardiovascularNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoEnfInfecciosaNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoEnfMentalNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoHipertensionNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoMalFormacionNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoOtroNavigations = new HashSet<AntecedentesFamiliare>();
            AntecedentesFamiliareParentescoCatalogoTuberculosisNavigations = new HashSet<AntecedentesFamiliare>();
            ConsultumAlergiasConsulta = new HashSet<Consultum>();
            ConsultumCirugiasConsulta = new HashSet<Consultum>();
            PacienteEstadocivilPacientesCaNavigations = new HashSet<Paciente>();
            PacienteFormacionprofesionalPacientesCaNavigations = new HashSet<Paciente>();
            PacienteSegurosaludPacientesCaNavigations = new HashSet<Paciente>();
            PacienteSexoPacientesCaNavigations = new HashSet<Paciente>();
            PacienteTipodocumentoPacientesCaNavigations = new HashSet<Paciente>();
            PacienteTiposangrePacientesCaNavigations = new HashSet<Paciente>();
        }

        public int IdCatalogo { get; set; }
        public DateTime? FechacreacionCatalogo { get; set; }
        public string? UsuariocreacionCatalogo { get; set; }
        public DateTime? FechamodificacionCatalogo { get; set; }
        public string? UsuariomodificacionCatalogo { get; set; }
        public string? DescripcionCatalogo { get; set; }
        public string? CategoriaCatalogo { get; set; }
        public Guid UuidCatalogo { get; set; }
        public int? EstadoCatalogo { get; set; }

        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoCancerNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoCardiopatiaNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoDiabetesNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoEnfCardiovascularNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoEnfInfecciosaNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoEnfMentalNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoHipertensionNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoMalFormacionNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoOtroNavigations { get; set; }
        public virtual ICollection<AntecedentesFamiliare> AntecedentesFamiliareParentescoCatalogoTuberculosisNavigations { get; set; }
        public virtual ICollection<Consultum> ConsultumAlergiasConsulta { get; set; }
        public virtual ICollection<Consultum> ConsultumCirugiasConsulta { get; set; }
        public virtual ICollection<Paciente> PacienteEstadocivilPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteFormacionprofesionalPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteSegurosaludPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteSexoPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteTipodocumentoPacientesCaNavigations { get; set; }
        public virtual ICollection<Paciente> PacienteTiposangrePacientesCaNavigations { get; set; }
    }
}
