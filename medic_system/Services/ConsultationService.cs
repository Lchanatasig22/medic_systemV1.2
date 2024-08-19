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


    public async Task<int> CreateConsultationAsync(ConsultationRequest request)
    {
        using (var connection = new SqlConnection(_context.Database.GetConnectionString()))
        using (var command = new SqlCommand("sp_Create_Consultations3", connection))
        {
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@fechacreacion_consulta", request.FechaCreacionConsulta);
            command.Parameters.AddWithValue("@usuariocreacion_consulta", request.UsuarioCreacionConsulta);
            command.Parameters.AddWithValue("@historial_consulta", request.HistorialConsulta);
            command.Parameters.AddWithValue("@secuencial_consulta", request.SecuencialConsulta);
            command.Parameters.AddWithValue("@paciente_consulta_p", request.PacienteConsultaP);
            command.Parameters.AddWithValue("@motivo_consulta", request.MotivoConsulta);
            command.Parameters.AddWithValue("@enfermedad_consulta", request.EnfermedadConsulta);
            command.Parameters.AddWithValue("@nombrepariente_consulta", request.NombreParienteConsulta);
            command.Parameters.AddWithValue("@signosalarma_consulta", request.SignosAlarmaConsulta);
            command.Parameters.AddWithValue("@reconofarmacologicas", request.ReconoFarmacologicas);
            command.Parameters.AddWithValue("@tipopariente_consulta", request.TipoParienteConsulta);
            command.Parameters.AddWithValue("@telefono_consulta", request.TelefonoConsulta);
            command.Parameters.AddWithValue("@temperatura_consulta", request.TemperaturaConsulta);
            command.Parameters.AddWithValue("@frecuenciarespiratoria_consulta", request.FrecuenciaRespiratoriaConsulta);
            command.Parameters.AddWithValue("@presionarterialsistolica_consulta", request.PresionArterialSistolicaConsulta);
            command.Parameters.AddWithValue("@presionarterialdiastolica_consulta", request.PresionArterialDiastolicaConsulta);
            command.Parameters.AddWithValue("@pulso_consulta", request.PulsoConsulta);
            command.Parameters.AddWithValue("@peso_consulta", request.PesoConsulta);
            command.Parameters.AddWithValue("@talla_consulta", request.TallaConsulta);
            command.Parameters.AddWithValue("@plantratamiento_consulta", request.PlanTratamientoConsulta);
            command.Parameters.AddWithValue("@observacion_consulta", request.ObservacionConsulta);
            command.Parameters.AddWithValue("@antecedentespersonales_consulta", request.AntecedentesPersonalesConsulta);
            command.Parameters.AddWithValue("@alergias_consulta_id", request.AlergiasConsultaId);
            command.Parameters.AddWithValue("@obseralergias", request.ObserAlergias);
            command.Parameters.AddWithValue("@cirugias_consulta_id", request.CirugiasConsultaId);
            command.Parameters.AddWithValue("@obsercirugias_id", request.ObserCirugiasId);
            command.Parameters.AddWithValue("@diasincapacidad_consulta", request.DiasIncapacidadConsulta);
            command.Parameters.AddWithValue("@medico_consulta_d", request.MedicoConsultaD);
            command.Parameters.AddWithValue("@especialidad_id", request.EspecialidadId);
            command.Parameters.AddWithValue("@estado_consulta_c", request.EstadoConsultaC);
            command.Parameters.AddWithValue("@tipo_consulta_c", request.TipoConsultaC);
            command.Parameters.AddWithValue("@notasevolucion_consulta", request.NotasEvolucionConsulta);
            command.Parameters.AddWithValue("@consultaprincipal_consulta", request.ConsultaPrincipalConsulta);
            command.Parameters.AddWithValue("@activo_consulta", request.ActivoConsulta);
            command.Parameters.AddWithValue("@fechaactual_consulta", request.FechaActualConsulta);
            command.Parameters.AddWithValue("@medicamentos", request.Medicamentos);
            command.Parameters.AddWithValue("@laboratorios", request.Laboratorios);
            command.Parameters.AddWithValue("@imagenes", request.Imagenes);
            command.Parameters.AddWithValue("@diagnosticos", request.Diagnosticos);

            // Antecedentes Familiares
            command.Parameters.AddWithValue("@cardiopatia", request.Cardiopatia);
            command.Parameters.AddWithValue("@obser_cardiopatia", request.ObserCardiopatia);
            command.Parameters.AddWithValue("@diabetes", request.Diabetes);
            command.Parameters.AddWithValue("@obser_diabetes", request.ObserDiabetes);
            command.Parameters.AddWithValue("@enf_cardiovascular", request.EnfCardiovascular);
            command.Parameters.AddWithValue("@obser_enf_cardiovascular", request.ObserEnfCardiovascular);
            command.Parameters.AddWithValue("@hipertension", request.Hipertension);
            command.Parameters.AddWithValue("@obser_hipertension", request.ObserHipertension);
            command.Parameters.AddWithValue("@cancer", request.Cancer);
            command.Parameters.AddWithValue("@obser_cancer", request.ObserCancer);
            command.Parameters.AddWithValue("@tuberculosis", request.Tuberculosis);
            command.Parameters.AddWithValue("@obser_tuberculosis", request.ObserTuberculosis);
            command.Parameters.AddWithValue("@enf_mental", request.EnfMental);
            command.Parameters.AddWithValue("@obser_enf_mental", request.ObserEnfMental);
            command.Parameters.AddWithValue("@enf_infecciosa", request.EnfInfecciosa);
            command.Parameters.AddWithValue("@obser_enf_infecciosa", request.ObserEnfInfecciosa);
            command.Parameters.AddWithValue("@mal_formacion", request.MalFormacion);
            command.Parameters.AddWithValue("@obser_mal_formacion", request.ObserMalFormacion);
            command.Parameters.AddWithValue("@otro", request.Otro);
            command.Parameters.AddWithValue("@obser_otro", request.ObserOtro);

            // Órganos y Sistemas
            command.Parameters.AddWithValue("@org_sentidos", request.OrgSentidos);
            command.Parameters.AddWithValue("@obser_org_sentidos", request.ObserOrgSentidos);
            command.Parameters.AddWithValue("@respiratorio", request.Respiratorio);
            command.Parameters.AddWithValue("@obser_respiratorio", request.ObserRespiratorio);
            command.Parameters.AddWithValue("@cardio_vascular", request.CardioVascular);
            command.Parameters.AddWithValue("@obser_cardio_vascular", request.ObserCardioVascular);
            command.Parameters.AddWithValue("@digestivo", request.Digestivo);
            command.Parameters.AddWithValue("@obser_digestivo", request.ObserDigestivo);
            command.Parameters.AddWithValue("@genital", request.Genital);
            command.Parameters.AddWithValue("@obser_genital", request.ObserGenital);
            command.Parameters.AddWithValue("@urinario", request.Urinario);
            command.Parameters.AddWithValue("@obser_urinario", request.ObserUrinario);
            command.Parameters.AddWithValue("@m_esqueletico", request.MEsqueletico);
            command.Parameters.AddWithValue("@obser_m_esqueletico", request.ObserMEsqueletico);
            command.Parameters.AddWithValue("@endocrino", request.Endocrino);
            command.Parameters.AddWithValue("@obser_endocrino", request.ObserEndocrino);
            command.Parameters.AddWithValue("@linfatico", request.Linfatico);
            command.Parameters.AddWithValue("@obser_linfatico", request.ObserLinfatico);
            command.Parameters.AddWithValue("@nervioso", request.Nervioso);
            command.Parameters.AddWithValue("@obser_nervioso", request.ObserNervioso);

            // Examen Físico
            command.Parameters.AddWithValue("@cabeza", request.Cabeza);
            command.Parameters.AddWithValue("@obser_cabeza", request.ObserCabeza);
            command.Parameters.AddWithValue("@cuello", request.Cuello);
            command.Parameters.AddWithValue("@obser_cuello", request.ObserCuello);
            command.Parameters.AddWithValue("@torax", request.Torax);
            command.Parameters.AddWithValue("@obser_torax", request.ObserTorax);
            command.Parameters.AddWithValue("@abdomen", request.Abdomen);
            command.Parameters.AddWithValue("@obser_abdomen", request.ObserAbdomen);
            command.Parameters.AddWithValue("@pelvis", request.Pelvis);
            command.Parameters.AddWithValue("@obser_pelvis", request.ObserPelvis);
            command.Parameters.AddWithValue("@extremidades", request.Extremidades);
            command.Parameters.AddWithValue("@obser_extremidades", request.ObserExtremidades);

            // Output parameter
            var newConsultaIdParam = new SqlParameter("@NewConsultaID", SqlDbType.Int) { Direction = ParameterDirection.Output };
            command.Parameters.Add(newConsultaIdParam);

            await connection.OpenAsync();
            await command.ExecuteNonQueryAsync();

            return (int)newConsultaIdParam.Value;
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



