using System.Data;

namespace medic_system.Models
{
    public class CrearConsultaMedicaRequest
    {
        public DateTime? FechacreacionConsulta { get; set; } = DateTime.Now;
        public string? UsuariocreacionConsulta { get; set; }
        public string? HistorialConsulta { get; set; }
        public string? SecuencialConsulta { get; set; }
        public int? PacienteConsultaP { get; set; }
        public string? MotivoConsulta { get; set; }
        public string? EnfermedadConsulta { get; set; }
        public string? NombreparienteConsulta { get; set; }
        public string? SignosalarmaConsulta { get; set; }
        public string? Reconofarmacologicas { get; set; }
        public int? TipoparienteConsulta { get; set; }
        public string? TelefonoConsulta { get; set; }
        public string? TemperaturaConsulta { get; set; }
        public string? FrecuenciarespiratoriaConsulta { get; set; }
        public string? PresionarterialsistolicaConsulta { get; set; }
        public string? PresionarterialdiastolicaConsulta { get; set; }
        public string? PulsoConsulta { get; set; }
        public string? PesoConsulta { get; set; }
        public string? TallaConsulta { get; set; }
        public string? PlantratamientoConsulta { get; set; }
        public string? ObservacionConsulta { get; set; }
        public string? AntecedentespersonalesConsulta { get; set; }
        public int? DiasincapacidadConsulta { get; set; }
        public int? MedicoConsultaD { get; set; }
        public int? EspecialidadId { get; set; }
        public int? EstadoConsultaC { get; set; }
        public int? TipoConsultaC { get; set; }
        public string? NotasevolucionConsulta { get; set; }
        public string? ConsultaprincipalConsulta { get; set; }
        public int? ActivoConsulta { get; set; }
        public DateTime? FechaactualConsulta { get; set; }

        // Aquí puedes incluir listas u objetos complejos según sea necesario
        public List<Medicamento> Medicamentos { get; set; }
        public List<Laboratorio> Laboratorios { get; set; }
        public List<Imagen> Imagenes { get; set; }
        public List<Diagnostico> Diagnosticos { get; set; }
        public List<AntecedentesFamiliare> AntecedentesFamiliares { get; set; }
        public List<OrganosSistema> OrganosSistemas { get; set; }
        public List<ExamenFisico> ExamenesFisicos { get; set; }
    }


}
