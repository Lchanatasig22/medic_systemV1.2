namespace medic_system.Models
{
    public class CrearConsultaMedicaRequest
    {
        public DateTime fechacreacion_consulta { get; set; }
        public string usuariocreacion_consulta { get; set; }
        public string historial_consulta { get; set; }
        public string secuencial_consulta { get; set; }
        public int paciente_consulta_p { get; set; }
        public string motivo_consulta { get; set; }
        public string enfermedad_consulta { get; set; }
        public string nombrepariente_consulta { get; set; }
        public string signosalarma_consulta { get; set; }
        public string reconofarmacologicas { get; set; }
        public int tipopariente_consulta { get; set; }
        public string telefono_consulta { get; set; }
        public string temperatura_consulta { get; set; }
        public string frecuenciarespiratoria_consulta { get; set; }
        public string presionarterialsistolica_consulta { get; set; }
        public string presionarterialdiastolica_consulta { get; set; }
        public string pulso_consulta { get; set; }
        public string peso_consulta { get; set; }
        public string talla_consulta { get; set; }
        public string plantratamiento_consulta { get; set; }
        public string observacion_consulta { get; set; }
        public string antecedentespersonales_consulta { get; set; }
        public int diasincapacidad_consulta { get; set; }
        public int medico_consulta_d { get; set; }
        public int especialidad_id { get; set; }
        public int estado_consulta_c { get; set; }
        public int tipo_consulta_c { get; set; }
        public string notasevolucion_consulta { get; set; }
        public string consultaprincipal_consulta { get; set; }
        public int activo_consulta { get; set; }
        public DateTime fechaactual_consulta { get; set; }
        public List<ConsultaMedicamento> medicamentos { get; set; }
        public List<ConsultaLaboratorio> laboratorios { get; set; }
        public List<ConsultaImagen> imagenes { get; set; }
        public List<ConsultaDiagnostico> diagnosticos { get; set; }
        public List<AntecedentesFamiliare> antecedentesfamiliares { get; set; }
        public List<OrganosSistema> organossistemas { get; set; }
        public List<ExamenFisico> examenesfisicos { get; set; }
    }

}
