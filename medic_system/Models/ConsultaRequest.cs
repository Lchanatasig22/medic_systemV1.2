using System.Data;

namespace medic_system.Models
{
    public class ConsultaRequest
    {

        // Asegúrate de que las propiedades coincidan con los parámetros del servicio
        public DateTime FechacreacionConsulta { get; set; }
        public string UsuariocreacionConsulta { get; set; }
        public string HistorialConsulta { get; set; }
        public string SecuencialConsulta { get; set; }
        public int PacienteConsultaP { get; set; }
        public string MotivoConsulta { get; set; }
        public string EnfermedadConsulta { get; set; }
        public string NombreparienteConsulta { get; set; }
        public string SignosalarmaConsulta { get; set; }
        public string Reconofarmacologicas { get; set; }
        public int TipoparienteConsulta { get; set; }
        public string TelefonoConsulta { get; set; }
        public string TemperaturaConsulta { get; set; }
        public string FrecuenciarespiratoriaConsulta { get; set; }
        public string PresionarterialsistolicaConsulta { get; set; }
        public string PresionarterialdiastolicaConsulta { get; set; }
        public string PulsoConsulta { get; set; }
        public string PesoConsulta { get; set; }
        public string TallaConsulta { get; set; }
        public string PlantratamientoConsulta { get; set; }
        public string ObservacionConsulta { get; set; }
        public string AntecedentespersonalesConsulta { get; set; }
        public int AlergiasConsultaId { get; set; }
        public string Obseralergias { get; set; }
        public int CirugiasConsultaId { get; set; }
        public string ObsercirugiasId { get; set; }
        public int DiasincapacidadConsulta { get; set; }
        public int MedicoConsultaD { get; set; }
        public int EspecialidadId { get; set; }
        public int EstadoConsultaC { get; set; }
        public int TipoConsultaC { get; set; }
        public string NotasevolucionConsulta { get; set; }
        public string ConsultaprincipalConsulta { get; set; }
        public int ActivoConsulta { get; set; }
        public DateTime FechaactualConsulta { get; set; }
        public string Medicamentos { get; set; }
        public string Laboratorios { get; set; }
        public string Imagenes { get; set; }
        public string Diagnosticos { get; set; }
        public bool Cardiopatia { get; set; }
        public string ObserCardiopatia { get; set; }
        public bool Diabetes { get; set; }
        public string ObserDiabetes { get; set; }
        public bool EnfCardiovascular { get; set; }
        public string ObserEnfCardiovascular { get; set; }
        public bool Hipertension { get; set; }
        public string ObserHipertension { get; set; }
        public bool Cancer { get; set; }
        public string ObserCancer { get; set; }
        public bool Tuberculosis { get; set; }
        public string ObserTuberculosis { get; set; }
        public bool EnfMental { get; set; }
        public string ObserEnfMental { get; set; }
        public bool EnfInfecciosa { get; set; }
        public string ObserEnfInfecciosa { get; set; }
        public bool MalFormacion { get; set; }
        public string ObserMalFormacion { get; set; }
        public bool Otro { get; set; }
        public string ObserOtro { get; set; }
        public bool OrgSentidos { get; set; }
        public string ObserOrgSentidos { get; set; }
        public bool Respiratorio { get; set; }
        public string ObserRespiratorio { get; set; }
        public bool CardioVascular { get; set; }
        public string ObserCardioVascular { get; set; }
        public bool Digestivo { get; set; }
        public string ObserDigestivo { get; set; }
        public bool Genital { get; set; }
        public string ObserGenital { get; set; }
        public bool Urinario { get; set; }
        public string ObserUrinario { get; set; }
        public bool MEsqueletico { get; set; }
        public string ObserMEsqueletico { get; set; }
        public bool Endocrino { get; set; }
        public string ObserEndocrino { get; set; }
        public bool Linfatico { get; set; }
        public string ObserLinfatico { get; set; }
        public bool Nervioso { get; set; }
        public string ObserNervioso { get; set; }
        public bool Cabeza { get; set; }
        public string ObserCabeza { get; set; }
        public bool Cuello { get; set; }
        public string ObserCuello { get; set; }
        public bool Torax { get; set; }
        public string ObserTorax { get; set; }
        public bool Abdomen { get; set; }
        public string ObserAbdomen { get; set; }
        public bool Pelvis { get; set; }
        public string ObserPelvis { get; set; }
        public bool Extremidades { get; set; }
        public string ObserExtremidades { get; set; }
        public int ParentescoCatalogoCardiopatia { get; set; }
        public int ParentescoCatalogoDiabetes { get; set; }
        public int ParentescoCatalogoEnfCardiovascular { get; set; }
        public int ParentescoCatalogoHipertension { get; set; }
        public int ParentescoCatalogoCancer { get; set; }
        public int ParentescoCatalogoTuberculosis { get; set; }
        public int ParentescoCatalogoEnfMental { get; set; }
        public int ParentescoCatalogoEnfInfecciosa { get; set; }
        public int ParentescoCatalogoMalFormacion { get; set; }
        public int ParentescoCatalogoOtro { get; set; }


        public bool? PresuntivoDiagnosticos { get; set; }
        public bool? DefinitivoDiagnosticos { get; set; }

    }



}
