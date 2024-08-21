using System.Data;
using System.Data.SqlClient;
using medic_system.Models;
using medic_system.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
public class ConsultationService
{
    private readonly medical_systemContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<PatientService> _logger;

    public ConsultationService(medical_systemContext context, IHttpContextAccessor httpContextAccessor, ILogger<PatientService> logger)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;


    }
    public async Task<List<Consultum>> GetAllConsultasAsync()
    {
        // Obtener el nombre de usuario de la sesión
        var loginUsuario = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

        if (string.IsNullOrEmpty(loginUsuario))
        {
            throw new Exception("El nombre de usuario no está disponible en la sesión.");
        }

        // Filtrar las consultas por el usuario de creación y el estado igual a 0
        var consultas = await _context.Consulta
            .Where(c => c.UsuariocreacionConsulta == loginUsuario)
            .Include(c => c.ConsultaDiagnostico)
            .Include(c => c.ConsultaImagen)
            .Include(c => c.ConsultaLaboratorio)
            .Include(c => c.ConsultaMedicamento)
            .Include(c => c.PacienteConsultaPNavigation)
            .OrderBy(c => c.FechacreacionConsulta) // Ordenar por fecha de la consulta Ocupar esto mismo para cualquier tabla 

            .ToListAsync();

        return consultas;
    }

    public async Task DeleteConsultasByPacienteIdAsync(int pacienteId)
    {
        var consultas = await _context.Consulta.Where(c => c.PacienteConsultaP == pacienteId).ToListAsync();
        _context.Consulta.RemoveRange(consultas);
        await _context.SaveChangesAsync();
    }
    public async Task<Consultum> GetConsultaByIdAsync(int id)
    {
        return await _context.Consulta
            .Include(c => c.ConsultaMedicamento) // Incluye las relaciones necesarias
            .Include(c => c.ConsultaLaboratorio)
            .Include(c => c.ConsultaImagen)
            .Include(c => c.ConsultaDiagnostico)
            .Include(c => c.ConsultaAntecedentesFamiliares)
            .Include(c => c.ConsultaOrganosSistemas)
            .Include(c => c.ConsultaExamenFisico)
            .FirstOrDefaultAsync(c => c.IdConsulta == id);
    }

    public async Task<int> CreateConsultationAsync(
           DateTime fechacreacion_consulta,
           string usuariocreacion_consulta,
           string historial_consulta,
           string secuencial_consulta,
           int paciente_consulta_p,
           string motivo_consulta,
           string enfermedad_consulta,
           string nombrepariente_consulta,
           string signosalarma_consulta,
           string reconofarmacologicas,
           int tipopariente_consulta,
           string telefono_consulta,
           string temperatura_consulta,
           string frecuenciarespiratoria_consulta,
           string presionarterialsistolica_consulta,
           string presionarterialdiastolica_consulta,
           string pulso_consulta,
           string peso_consulta,
           string talla_consulta,
           string plantratamiento_consulta,
           string observacion_consulta,
           string antecedentespersonales_consulta,
           int alergias_consulta_id,
           string obseralergias,
           int cirugias_consulta_id,
           string obsercirugias_id,
           int diasincapacidad_consulta,
           int medico_consulta_d,
           int especialidad_id,
           int estado_consulta_c,
           int tipo_consulta_c,
           string notasevolucion_consulta,
           string consultaprincipal_consulta,
           int activo_consulta,
           DateTime fechaactual_consulta,
           string medicamentos,
           string laboratorios,
           string imagenes,
           string diagnosticos,
           bool cardiopatia,
           string obser_cardiopatia,
           bool diabetes,
           string obser_diabetes,
           bool enf_cardiovascular,
           string obser_enf_cardiovascular,
           bool hipertension,
           string obser_hipertension,
           bool cancer,
           string obser_cancer,
           bool tuberculosis,
           string obser_tuberculosis,
           bool enf_mental,
           string obser_enf_mental,
           bool enf_infecciosa,
           string obser_enf_infecciosa,
           bool mal_formacion,
           string obser_mal_formacion,
           bool otro,
           string obser_otro,
           bool org_sentidos,
           string obser_org_sentidos,
           bool respiratorio,
           string obser_respiratorio,
           bool cardio_vascular,
           string obser_cardio_vascular,
           bool digestivo,
           string obser_digestivo,
           bool genital,
           string obser_genital,
           bool urinario,
           string obser_urinario,
           bool m_esqueletico,
           string obser_m_esqueletico,
           bool endocrino,
           string obser_endocrino,
           bool linfatico,
           string obser_linfatico,
           bool nervioso,
           string obser_nervioso,
           bool cabeza,
           string obser_cabeza,
           bool cuello,
           string obser_cuello,
           bool torax,
           string obser_torax,
           bool abdomen,
           string obser_abdomen,
           bool pelvis,
           string obser_pelvis,
           bool extremidades,
           string obser_extremidades,
           int parentesco_catalogo_cardiopatia,
           int parentesco_catalogo_diabetes,
           int parentesco_catalogo_enf_cardiovascular,
           int parentesco_catalogo_hipertension,
           int parentesco_catalogo_cancer,
           int parentesco_catalogo_tuberculosis,
           int parentesco_catalogo_enf_mental,
           int parentesco_catalogo_enf_infecciosa,
           int parentesco_catalogo_mal_formacion,
           int parentesco_catalogo_otro)
    {
        using (SqlConnection conn = new SqlConnection(_context.Database.GetConnectionString()))
        {
            using (SqlCommand cmd = new SqlCommand("sp_Create_Consultations3", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@fechacreacion_consulta",  DateTime.Now);
                cmd.Parameters.AddWithValue("@usuariocreacion_consulta", usuariocreacion_consulta);
                cmd.Parameters.AddWithValue("@historial_consulta", historial_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@secuencial_consulta", secuencial_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@paciente_consulta_p", paciente_consulta_p);
                cmd.Parameters.AddWithValue("@motivo_consulta", motivo_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@enfermedad_consulta", enfermedad_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nombrepariente_consulta", nombrepariente_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@signosalarma_consulta", signosalarma_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@reconofarmacologicas", reconofarmacologicas ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@tipopariente_consulta", tipopariente_consulta);
                cmd.Parameters.AddWithValue("@telefono_consulta", telefono_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@temperatura_consulta", temperatura_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", frecuenciarespiratoria_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@presionarterialsistolica_consulta", presionarterialsistolica_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@presionarterialdiastolica_consulta", presionarterialdiastolica_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@pulso_consulta", pulso_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@peso_consulta", peso_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@talla_consulta", talla_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@plantratamiento_consulta", plantratamiento_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@observacion_consulta", observacion_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@antecedentespersonales_consulta", antecedentespersonales_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@alergias_consulta_id", alergias_consulta_id);
                cmd.Parameters.AddWithValue("@obseralergias", obseralergias ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cirugias_consulta_id", cirugias_consulta_id);
                cmd.Parameters.AddWithValue("@obsercirugias_id", obsercirugias_id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@diasincapacidad_consulta", diasincapacidad_consulta);
                cmd.Parameters.AddWithValue("@medico_consulta_d", medico_consulta_d);
                cmd.Parameters.AddWithValue("@especialidad_id", especialidad_id);
                cmd.Parameters.AddWithValue("@estado_consulta_c", estado_consulta_c);
                cmd.Parameters.AddWithValue("@tipo_consulta_c", tipo_consulta_c);
                cmd.Parameters.AddWithValue("@notasevolucion_consulta", notasevolucion_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@consultaprincipal_consulta", consultaprincipal_consulta ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@activo_consulta", activo_consulta);
                cmd.Parameters.AddWithValue("@fechaactual_consulta", DateTime.Now);

                // JSON Parameters
                cmd.Parameters.AddWithValue("@medicamentos", medicamentos ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@laboratorios", laboratorios ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@imagenes", imagenes ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@diagnosticos", diagnosticos ?? (object)DBNull.Value);

                // Antecedentes Familiares
                cmd.Parameters.AddWithValue("@cardiopatia", cardiopatia);
                cmd.Parameters.AddWithValue("@obser_cardiopatia", obser_cardiopatia ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@diabetes", diabetes);
                cmd.Parameters.AddWithValue("@obser_diabetes", obser_diabetes ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@enf_cardiovascular", enf_cardiovascular);
                cmd.Parameters.AddWithValue("@obser_enf_cardiovascular", obser_enf_cardiovascular ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@hipertension", hipertension);
                cmd.Parameters.AddWithValue("@obser_hipertension", obser_hipertension ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cancer", cancer);
                cmd.Parameters.AddWithValue("@obser_cancer", obser_cancer ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@tuberculosis", tuberculosis);
                cmd.Parameters.AddWithValue("@obser_tuberculosis", obser_tuberculosis ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@enf_mental", enf_mental);
                cmd.Parameters.AddWithValue("@obser_enf_mental", obser_enf_mental ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@enf_infecciosa", enf_infecciosa);
                cmd.Parameters.AddWithValue("@obser_enf_infecciosa", obser_enf_infecciosa ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@mal_formacion", mal_formacion);
                cmd.Parameters.AddWithValue("@obser_mal_formacion", obser_mal_formacion ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@otro", otro);
                cmd.Parameters.AddWithValue("@obser_otro", obser_otro ?? (object)DBNull.Value);

                // Órganos y Sistemas
                cmd.Parameters.AddWithValue("@org_sentidos", org_sentidos);
                cmd.Parameters.AddWithValue("@obser_org_sentidos", obser_org_sentidos ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@respiratorio", respiratorio);
                cmd.Parameters.AddWithValue("@obser_respiratorio", obser_respiratorio ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cardio_vascular", cardio_vascular);
                cmd.Parameters.AddWithValue("@obser_cardio_vascular", obser_cardio_vascular ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@digestivo", digestivo);
                cmd.Parameters.AddWithValue("@obser_digestivo", obser_digestivo ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@genital", genital);
                cmd.Parameters.AddWithValue("@obser_genital", obser_genital ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@urinario", urinario);
                cmd.Parameters.AddWithValue("@obser_urinario", obser_urinario ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@m_esqueletico", m_esqueletico);
                cmd.Parameters.AddWithValue("@obser_m_esqueletico", obser_m_esqueletico ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@endocrino", endocrino);
                cmd.Parameters.AddWithValue("@obser_endocrino", obser_endocrino ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@linfatico", linfatico);
                cmd.Parameters.AddWithValue("@obser_linfatico", obser_linfatico ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@nervioso", nervioso);
                cmd.Parameters.AddWithValue("@obser_nervioso", obser_nervioso ?? (object)DBNull.Value);

                // Examen Físico
                cmd.Parameters.AddWithValue("@cabeza", cabeza);
                cmd.Parameters.AddWithValue("@obser_cabeza", obser_cabeza ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@cuello", cuello);
                cmd.Parameters.AddWithValue("@obser_cuello", obser_cuello ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@torax", torax);
                cmd.Parameters.AddWithValue("@obser_torax", obser_torax ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@abdomen", abdomen);
                cmd.Parameters.AddWithValue("@obser_abdomen", obser_abdomen ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@pelvis", pelvis);
                cmd.Parameters.AddWithValue("@obser_pelvis", obser_pelvis ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@extremidades", extremidades);
                cmd.Parameters.AddWithValue("@obser_extremidades", obser_extremidades ?? (object)DBNull.Value);

                // Parentesco catalogo
                cmd.Parameters.AddWithValue("@parentesco_catalogo_cardiopatia", parentesco_catalogo_cardiopatia);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_diabetes", parentesco_catalogo_diabetes);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_enf_cardiovascular", parentesco_catalogo_enf_cardiovascular);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_hipertension", parentesco_catalogo_hipertension);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_cancer", parentesco_catalogo_cancer);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_tuberculosis", parentesco_catalogo_tuberculosis);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_enf_mental", parentesco_catalogo_enf_mental);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_enf_infecciosa", parentesco_catalogo_enf_infecciosa);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_mal_formacion", parentesco_catalogo_mal_formacion);
                cmd.Parameters.AddWithValue("@parentesco_catalogo_otro", parentesco_catalogo_otro);

                // Output Parameter
                var newConsultaIdParam = new SqlParameter("@NewConsultaID", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                };
                cmd.Parameters.Add(newConsultaIdParam);

                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                return (int)newConsultaIdParam.Value;
            }
        }
    }

    public async Task UpdateConsultationAsync(Consultum consultation)
    {
        using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
        {
            var command = new SqlCommand("sp_Update_Consultation", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            // Add parameters for consultation
            command.Parameters.AddWithValue("@consulta_id", consultation.IdConsulta);
            command.Parameters.AddWithValue("@fechacreacion_consulta", consultation.FechacreacionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@usuariocreacion_consulta", consultation.UsuariocreacionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@historial_consulta", consultation.HistorialConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@secuencial_consulta", consultation.SecuencialConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@paciente_consulta_p", consultation.PacienteConsultaP);
            command.Parameters.AddWithValue("@motivo_consulta", consultation.MotivoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enfermedad_consulta", consultation.EnfermedadConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@nombrepariente_consulta", consultation.NombreparienteConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@alergias_consulta", consultation.AlergiasConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@reconofarmacologicas", consultation.Reconofarmacologicas ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@tipopariente_consulta", consultation.TipoparienteConsulta);
            command.Parameters.AddWithValue("@telefono_consulta", consultation.TelefonoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@temperatura_consulta", consultation.TemperaturaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", consultation.FrecuenciarespiratoriaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@presionarterialsistolica_consulta", consultation.PresionarterialsistolicaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", consultation.PresionarterialdiastolicaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@pulso_consulta", consultation.PulsoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@peso_consulta", consultation.PesoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@talla_consulta", consultation.TallaConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@plantratamiento_consulta", consultation.PlantratamientoConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@observacion_consulta", consultation.ObservacionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@antecedentespersonales_consulta", consultation.AntecedentespersonalesConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@diasincapacidad_consulta", consultation.DiasincapacidadConsulta);
            command.Parameters.AddWithValue("@medico_consulta_d", consultation.MedicoConsultaD);
            command.Parameters.AddWithValue("@especialidad_id", consultation.EspecialidadId);
            command.Parameters.AddWithValue("@estado_consulta_c", consultation.EstadoConsultaC);
            command.Parameters.AddWithValue("@tipo_consulta_c", consultation.TipoConsultaC);
            command.Parameters.AddWithValue("@notasevolucion_consulta", consultation.NotasevolucionConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@consultaprincipal_consulta", consultation.ConsultaprincipalConsulta ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@activo_consulta", consultation.ActivoConsulta);

            // Add parameters for medication
            var medication = consultation.ConsultaMedicamento;
            command.Parameters.AddWithValue("@fechacreacion_medicamento", medication.FechacreacionMedicamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@medicamento_id", medication.MedicamentoId);
            command.Parameters.AddWithValue("@dosis_medicamento", medication.DosisMedicamento);
            command.Parameters.AddWithValue("@observacion_medicamento", medication.ObservacionMedicamento ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@estado_medicamento", medication.EstadoMedicamento);

            // Add parameters for laboratory
            var laboratory = consultation.ConsultaLaboratorio;
            command.Parameters.AddWithValue("@cantidad_laboratorio", laboratory.CantidadLaboratorio);
            command.Parameters.AddWithValue("@observacion_laboratorio", laboratory.Observacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@catalogo_laboratorio_id", laboratory.CatalogoLaboratorioId);
            command.Parameters.AddWithValue("@estado_laboratorio", laboratory.EstadoLaboratorio);

            // Add parameters for image
            var image = consultation.ConsultaImagen;
            command.Parameters.AddWithValue("@imagen_id", image.ImagenId);
            command.Parameters.AddWithValue("@observacion_imagen", image.ObservacionImagen ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cantidad_imagen", image.CantidadImagen);
            command.Parameters.AddWithValue("@estado_imagen", image.EstadoImagen);

            // Add parameters for diagnosis
            var diagnosis = consultation.ConsultaDiagnostico;
            command.Parameters.AddWithValue("@diagnostico_id", diagnosis.DiagnosticoId);
            command.Parameters.AddWithValue("@observacion_diagnostico", diagnosis.ObservacionDiagnostico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@presuntivo_diagnosticos", diagnosis.PresuntivoDiagnosticos);
            command.Parameters.AddWithValue("@definitivo_diagnosticos", diagnosis.DefinitivoDiagnosticos);
            command.Parameters.AddWithValue("@estado_diagnostico", diagnosis.EstadoDiagnostico);

            // Add parameters for family history
            var familyHistory = consultation.ConsultaAntecedentesFamiliares;
            command.Parameters.AddWithValue("@cardiopatia", familyHistory.Cardiopatia);
            command.Parameters.AddWithValue("@obser_cardiopatia", familyHistory.ObserCardiopatia ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@diabetes", familyHistory.Diabetes);
            command.Parameters.AddWithValue("@obser_diabetes", familyHistory.ObserDiabetes ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enf_cardiovascular", familyHistory.EnfCardiovascular);
            command.Parameters.AddWithValue("@obser_enf_cardiovascular", familyHistory.ObserEnfCardiovascular ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@hipertension", familyHistory.Hipertension);
            command.Parameters.AddWithValue("@obser_hipertension", familyHistory.ObserHipertension ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cancer", familyHistory.Cancer);
            command.Parameters.AddWithValue("@obser_cancer", familyHistory.ObserCancer ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@tuberculosis", familyHistory.Tuberculosis);
            command.Parameters.AddWithValue("@obser_tuberculosis", familyHistory.ObserTuberculosis ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enf_mental", familyHistory.EnfMental);
            command.Parameters.AddWithValue("@obser_enf_mental", familyHistory.ObserEnfMental ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@enf_infecciosa", familyHistory.EnfInfecciosa);
            command.Parameters.AddWithValue("@obser_enf_infecciosa", familyHistory.ObserEnfInfecciosa ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@mal_formacion", familyHistory.MalFormacion);
            command.Parameters.AddWithValue("@obser_mal_formacion", familyHistory.ObserMalFormacion ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@otro", familyHistory.Otro );
            command.Parameters.AddWithValue("@obser_otro", familyHistory.ObserOtro ?? (object)DBNull.Value);
            //command.Parameters.AddWithValue("@alergias_antecedentes", familyHistory.Alergias);
            //command.Parameters.AddWithValue("@obser_alergias", familyHistory.ObserAlergias ?? (object)DBNull.Value);
            //command.Parameters.AddWithValue("@cirugias", familyHistory.Cirugias);
            //command.Parameters.AddWithValue("@obser_cirugias", familyHistory.ObserCirugias ?? (object)DBNull.Value);

            // Add parameters for organ systems
            var organSystems = consultation.ConsultaOrganosSistemas;
            command.Parameters.AddWithValue("@org_sentidos", organSystems.OrgSentidos);
            command.Parameters.AddWithValue("@obser_org_sentidos", organSystems.ObserOrgSentidos ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@respiratorio", organSystems.Respiratorio);
            command.Parameters.AddWithValue("@obser_respiratorio", organSystems.ObserRespiratorio ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cardio_vascular", organSystems.CardioVascular);
            command.Parameters.AddWithValue("@obser_cardio_vascular", organSystems.ObserCardioVascular ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@digestivo", organSystems.Digestivo);
            command.Parameters.AddWithValue("@obser_digestivo", organSystems.ObserDigestivo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@genital", organSystems.Genital);
            command.Parameters.AddWithValue("@obser_genital", organSystems.ObserGenital ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@urinario", organSystems.Urinario);
            command.Parameters.AddWithValue("@obser_urinario", organSystems.ObserUrinario ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@m_esqueletico", organSystems.MEsqueletico);
            command.Parameters.AddWithValue("@obser_m_esqueletico", organSystems.ObserMEsqueletico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@endocrino", organSystems.Endocrino);
            command.Parameters.AddWithValue("@obser_endocrino", organSystems.ObserEndocrino ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@linfatico", organSystems.Linfatico);
            command.Parameters.AddWithValue("@obser_linfatico", organSystems.ObserLinfatico ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@nervioso", organSystems.Nervioso);
            command.Parameters.AddWithValue("@obser_nervioso", organSystems.ObserNervioso ?? (object)DBNull.Value);

            // Add parameters for physical exam
            var physicalExam = consultation.ConsultaExamenFisico;
            command.Parameters.AddWithValue("@cabeza", physicalExam.Cabeza);
            command.Parameters.AddWithValue("@obser_cabeza", physicalExam.ObserCabeza ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@cuello", physicalExam.Cuello);
            command.Parameters.AddWithValue("@obser_cuello", physicalExam.ObserCuello ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@torax", physicalExam.Torax);
            command.Parameters.AddWithValue("@obser_torax", physicalExam.ObserTorax ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@abdomen", physicalExam.Abdomen);
            command.Parameters.AddWithValue("@obser_abdomen", physicalExam.ObserAbdomen ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@pelvis", physicalExam.Pelvis);
            command.Parameters.AddWithValue("@obser_pelvis", physicalExam.ObserPelvis ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@extremidades", physicalExam.Extremidades);
            command.Parameters.AddWithValue("@obser_extremidades", physicalExam.ObserExtremidades ?? (object)DBNull.Value);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();
        }
    }


    public async Task<Paciente> BuscarPacientePorCiAsync(int ci)
    {
        if (ci <= 0)
        {
            throw new ArgumentException("El número de identificación debe ser un entero positivo.", nameof(ci));
        }

        try
        {
            return await _context.Pacientes
                .SingleOrDefaultAsync(p => p.CiPacientes == ci);
        }
        catch (DbUpdateException dbEx)
        {
            // Manejo de excepciones específicas de la base de datos
            _logger.LogError(dbEx, "Error de base de datos al buscar el paciente por número de identificación.");
            throw new Exception("Error de base de datos al buscar el paciente.", dbEx);
        }
        catch (Exception ex)
        {
            // Manejo de otras excepciones
            _logger.LogError(ex, "Error al buscar el paciente por número de identificación.");
            throw new Exception("Error al buscar el paciente por número de identificación.", ex);
        }
    }






}



