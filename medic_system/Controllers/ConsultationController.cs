using medic_system.Models;
using medic_system.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Data.SqlClient;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.EntityFrameworkCore;
using Rotativa.AspNetCore; // Alias para System.Text.Json.JsonSerializer

namespace medic_system.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly AppointmentService _citaService;
        private readonly PatientService _patientService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConsultationService _consultationService;
        private readonly CatalogService _catalogService;
        private readonly ILogger<ConsultationController> _logger;
        private readonly medical_systemContext _medical_SystemContext;

        public ConsultationController(AppointmentService citaService, PatientService patientService, IHttpContextAccessor httpContextAccessor, ConsultationService consultationService, CatalogService catalogService, ILogger<ConsultationController> logger, medical_systemContext medical_SystemContext)
        {
            _citaService = citaService;
            _patientService = patientService;
            _httpContextAccessor = httpContextAccessor;
            _consultationService = consultationService;
            _catalogService = catalogService;
            _logger = logger;
            _medical_SystemContext = medical_SystemContext;
        }




        public async Task<IActionResult> ListarConsultas()
        {
            try
            {
                var usuarioEspecialidad = _httpContextAccessor.HttpContext.Session.GetString("UsuarioEspecialidad");

                if (string.IsNullOrEmpty(usuarioEspecialidad))
                {
                    throw new Exception("El nombre de usuario no está disponible en la sesión.");
                }

                ViewBag.UsuarioEspecialidad = usuarioEspecialidad;
                var consultas = await _consultationService.GetAllConsultasAsync();
                return View(consultas);
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(new List<Consultum>());
            }
        }

        [HttpGet]
        public async Task<IActionResult> CrearConsulta()
        {
            var model = new Consultum
            {
                FechacreacionConsulta = DateTime.Now,
                ConsultaAntecedentesFamiliares = new AntecedentesFamiliare
                {
                    Cardiopatia = false,
                    Diabetes = false,
                    EnfCardiovascular = false,
                    Hipertension = false,
                    Cancer = false,
                    Tuberculosis = false,
                    EnfMental = false,
                    EnfInfecciosa = false,
                    MalFormacion = false,
                    Otro = false

                },
                ConsultaDiagnostico = new ConsultaDiagnostico
                {
                    PresuntivoDiagnosticos = false,
                    DefinitivoDiagnosticos = false

                },
                ConsultaExamenFisico = new ExamenFisico
                {
                    Abdomen = false,
                    Cabeza = false,
                    Cuello = false,
                    Torax = false,
                    Pelvis = false,
                    Extremidades = false

                },
                ConsultaImagen = new ConsultaImagen(),
                ConsultaLaboratorio = new ConsultaLaboratorio(),
                ConsultaMedicamento = new ConsultaMedicamento(),
                ConsultaOrganosSistemas = new OrganosSistema
                {

                    OrgSentidos = false,
                    Respiratorio = false,
                    CardioVascular = false,
                    Digestivo = false,
                    Genital = false,
                    Urinario = false,
                    MEsqueletico = false,
                    Endocrino = false,
                    Linfatico = false,
                    Nervioso = false

                },
            };

            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
            ViewBag.TiposPariente = await _catalogService.ObtenerParienteAsync();
            ViewBag.TiposMedicamentos = await _catalogService.ObtenerMedicamentosActivasAsync();
            ViewBag.TiposLaboratorios = await _catalogService.ObtenerLaboratorioActivasAsync();
            ViewBag.TiposImagen = await _catalogService.ObtenerImagenActivasAsync();
            ViewBag.TiposDiagnostico = await _catalogService.ObtenerDiagnosticoAsync();
            ViewBag.TiposParienteAntece = await _catalogService.ObtenerTiposDeFamiliarAAsync();
            ViewBag.TiposAlergias = await _catalogService.ObtenerTiposDeAlergiasAsync();
            ViewBag.TiposCirugias = await _catalogService.ObtenerTiposDeCirugiasAsync();

            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> EditarConsulta(int id)
        {
            var consulta = await _consultationService.GetConsultaByIdAsync(id);

            var tiposDiagnostico = await _catalogService.ObtenerDiagnosticoAsync();
            var tiposMedicamentos = await _catalogService.ObtenerMedicamentosActivasAsync();
            var tiposLaboratorios = await _catalogService.ObtenerLaboratorioActivasAsync();
            var tiposImagen = await _catalogService.ObtenerImagenActivasAsync();

            if (tiposDiagnostico == null || tiposMedicamentos == null || tiposLaboratorios == null || tiposImagen == null)
            {
                // Log para verificar si alguno de los datos es null
                System.Diagnostics.Debug.WriteLine("Uno de los tipos es null");
            }

            var jsonOptions = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };

            ViewBag.TiposDiagnosticoJson = System.Text.Json.JsonSerializer.Serialize(tiposDiagnostico, jsonOptions);
            ViewBag.TiposMedicamentosJson = System.Text.Json.JsonSerializer.Serialize(tiposMedicamentos, jsonOptions);
            ViewBag.TiposLaboratoriosJson = System.Text.Json.JsonSerializer.Serialize(tiposLaboratorios, jsonOptions);
            ViewBag.TiposImagenJson = System.Text.Json.JsonSerializer.Serialize(tiposImagen, jsonOptions);

            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
            ViewBag.TiposPariente = await _catalogService.ObtenerParienteAsync();

            return View(consulta);
        }



        [HttpGet]
        public async Task<IActionResult> VerConsulta(int id)
        {
            var consulta = await _consultationService.GetConsultaByIdAsync(id);

            var jsonOptions = new JsonSerializerOptions { ReferenceHandler = ReferenceHandler.Preserve, WriteIndented = true };

            ViewBag.TiposDiagnosticoJson = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Diagnostico>>(ViewBag.TiposDiagnostico, jsonOptions);
            ViewBag.TiposMedicamentosJson = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Medicamento>>(ViewBag.TiposMedicamentos, jsonOptions);
            ViewBag.TiposLaboratoriosJson = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Laboratorio>>(ViewBag.TiposLaboratorios, jsonOptions);
            ViewBag.TiposImagenJson = System.Text.Json.JsonSerializer.Serialize<IEnumerable<Imagen>>(ViewBag.TiposImagen, jsonOptions);

            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
            ViewBag.TiposPariente = await _catalogService.ObtenerParienteAsync();
            ViewBag.TiposMedicamentos = await _catalogService.ObtenerMedicamentosActivasAsync();
            ViewBag.TiposLaboratorios = await _catalogService.ObtenerLaboratorioActivasAsync();
            ViewBag.TiposImagen = await _catalogService.ObtenerImagenActivasAsync();
            ViewBag.TiposDiagnostico = await _catalogService.ObtenerDiagnosticoAsync();

            return View(consulta);
        }

        [HttpPost]
        public async Task<IActionResult> EditarConsulta(Consultum consultaDto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    consultaDto.FechacreacionConsulta = DateTime.Now;
                    consultaDto.MedicoConsultaD = HttpContext.Session.GetInt32("UsuarioId");
                    consultaDto.EspecialidadId = HttpContext.Session.GetInt32("UsuarioIdEspecialidad");
                    consultaDto.UsuariocreacionConsulta = HttpContext.Session.GetString("UsuarioNombre");
                    consultaDto.SecuencialConsulta = "1";
                    consultaDto.EstadoConsultaC = 1;
                    consultaDto.TipoConsultaC = 1;
                    consultaDto.ActivoConsulta = 1;
                    consultaDto.ConsultaMedicamento.FechacreacionMedicamento = DateTime.Now;
                    consultaDto.ConsultaMedicamento.EstadoMedicamento = 1;
                    consultaDto.ConsultaLaboratorio.EstadoLaboratorio = 1;
                    consultaDto.ConsultaImagen.EstadoImagen = 1;
                    consultaDto.ConsultaDiagnostico.EstadoDiagnostico = 1;

                    await _consultationService.UpdateConsultationAsync(consultaDto);
                    return RedirectToAction("EditarConsulta", new { id = consultaDto.IdConsulta });
                }
                catch (SqlException ex)
                {
                    _logger.LogError(ex, "Error al ejecutar el procedimiento almacenado para actualizar la consulta.");
                    ModelState.AddModelError("", $"Error al ejecutar el procedimiento almacenado: {ex.Message}");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al actualizar la consulta.");
                    ModelState.AddModelError("", $"Error al actualizar la consulta: {ex.Message}");
                }
            }

            return View("EditarConsulta", consultaDto);
        }



        //[HttpPost]
        //public async Task<IActionResult> CrearConsulta(Consultum consulta)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            var loginUsuario = HttpContext.Session.GetString("UsuarioNombre");
        //            consulta.EspecialidadId = HttpContext.Session.GetInt32("UsuarioIdEspecialidad");
        //            consulta.MedicoConsultaD = HttpContext.Session.GetInt32("UsuarioId");
        //            consulta.UsuariocreacionConsulta = HttpContext.Session.GetString("UsuarioNombre");
        //            consulta.FechacreacionConsulta = DateTime.Now;
        //            consulta.EstadoConsultaC = 1;
        //            consulta.ConsultaMedicamento.EstadoMedicamento = 1;
        //            consulta.ConsultaLaboratorio.EstadoLaboratorio = 1;
        //            consulta.ConsultaImagen.EstadoImagen = 1;
        //            consulta.ConsultaDiagnostico.EstadoDiagnostico = 1;

        //            if (string.IsNullOrEmpty(loginUsuario))
        //            {
        //                throw new Exception("El nombre de usuario no está disponible en la sesión.");
        //            }

        //            consulta.UsuariocreacionConsulta = loginUsuario;

        //            int idConsulta = await _consultationService.CreateConsultationAsync(
        //                consulta.FechacreacionConsulta ?? DateTime.Now,
        //                consulta.UsuariocreacionConsulta,
        //                consulta.HistorialConsulta ?? string.Empty,
        //                consulta.SecuencialConsulta ?? string.Empty,
        //                consulta.PacienteConsultaP ?? 0,
        //                consulta.MotivoConsulta ?? string.Empty,
        //                consulta.EnfermedadConsulta ?? string.Empty,
        //                consulta.NombreparienteConsulta ?? string.Empty,
        //                //consulta.AlergiasConsulta ?? string.Empty,
        //                consulta.Reconofarmacologicas ?? string.Empty,
        //                consulta.TipoparienteConsulta ?? 0,
        //                consulta.TelefonoConsulta ?? string.Empty,
        //                consulta.TemperaturaConsulta ?? string.Empty,
        //                consulta.FrecuenciarespiratoriaConsulta ?? string.Empty,
        //                consulta.PresionarterialsistolicaConsulta ?? string.Empty,
        //                consulta.PresionarterialdiastolicaConsulta ?? string.Empty,
        //                consulta.PulsoConsulta ?? string.Empty,
        //                consulta.PesoConsulta ?? string.Empty,
        //                consulta.TallaConsulta ?? string.Empty,
        //                consulta.PlantratamientoConsulta ?? string.Empty,
        //                consulta.ObservacionConsulta ?? string.Empty,
        //                consulta.AntecedentespersonalesConsulta ?? string.Empty,
        //                consulta.DiasincapacidadConsulta ?? 0,
        //                consulta.MedicoConsultaD ?? 0,
        //                consulta.EspecialidadId ?? 0,
        //                consulta.EstadoConsultaC ?? 0,
        //                consulta.TipoConsultaC ?? 0,
        //                consulta.NotasevolucionConsulta ?? string.Empty,
        //                consulta.ConsultaprincipalConsulta ?? string.Empty,
        //                consulta.ActivoConsulta ?? 0,
        //                consulta.ConsultaMedicamento?.FechacreacionMedicamento ?? DateTime.Now,
        //                consulta.ConsultaMedicamento?.MedicamentoId ?? 0,
        //                consulta.ConsultaMedicamento?.DosisMedicamento ?? 0,
        //                consulta.ConsultaMedicamento?.ObservacionMedicamento ?? string.Empty,
        //                consulta.ConsultaMedicamento?.EstadoMedicamento ?? 0,
        //                consulta.ConsultaLaboratorio?.CantidadLaboratorio ?? 0,
        //                consulta.ConsultaLaboratorio?.Observacion ?? string.Empty,
        //                consulta.ConsultaLaboratorio?.CatalogoLaboratorioId ?? 0,
        //                consulta.ConsultaLaboratorio?.EstadoLaboratorio ?? 0,
        //                consulta.ConsultaImagen?.ImagenId ?? 0,
        //                consulta.ConsultaImagen?.ObservacionImagen ?? string.Empty,
        //                consulta.ConsultaImagen?.CantidadImagen ?? 0,
        //                consulta.ConsultaImagen?.EstadoImagen ?? 0,
        //                consulta.ConsultaDiagnostico?.DiagnosticoId ?? 0,
        //                consulta.ConsultaDiagnostico?.ObservacionDiagnostico ?? string.Empty,
        //                consulta.ConsultaDiagnostico?.PresuntivoDiagnosticos ?? false,
        //                consulta.ConsultaDiagnostico?.DefinitivoDiagnosticos ?? false,
        //                consulta.ConsultaDiagnostico?.EstadoDiagnostico ?? 0,
        //                consulta.ConsultaAntecedentesFamiliares?.Cardiopatia ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserCardiopatia ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.Diabetes ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserDiabetes ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.EnfCardiovascular ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserEnfCardiovascular ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.Hipertension ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserHipertension ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.Cancer ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserCancer ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.Tuberculosis ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserTuberculosis ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.EnfMental ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserEnfMental ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.EnfInfecciosa ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserEnfInfecciosa ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.MalFormacion ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserMalFormacion ?? string.Empty,
        //                consulta.ConsultaAntecedentesFamiliares?.Otro ?? false,
        //                consulta.ConsultaAntecedentesFamiliares?.ObserOtro ?? string.Empty,
        //                //consulta.ConsultaAntecedentesFamiliares?.Alergias ?? false,
        //                //consulta.ConsultaAntecedentesFamiliares?.ObserAlergias ?? string.Empty,
        //                //consulta.ConsultaAntecedentesFamiliares?.Cirugias ?? false,
        //                //consulta.ConsultaAntecedentesFamiliares?.ObserCirugias ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.OrgSentidos ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserOrgSentidos ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Respiratorio ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserRespiratorio ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.CardioVascular ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserCardioVascular ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Digestivo ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserDigestivo ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Genital ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserGenital ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Urinario ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserUrinario ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.MEsqueletico ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserMEsqueletico ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Endocrino ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserEndocrino ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Linfatico ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserLinfatico ?? string.Empty,
        //                consulta.ConsultaOrganosSistemas?.Nervioso ?? false,
        //                consulta.ConsultaOrganosSistemas?.ObserNervioso ?? string.Empty,
        //                consulta.ConsultaExamenFisico?.Cabeza ?? false,
        //                consulta.ConsultaExamenFisico?.ObserCabeza ?? string.Empty,
        //                consulta.ConsultaExamenFisico?.Cuello ?? false,
        //                consulta.ConsultaExamenFisico?.ObserCuello ?? string.Empty,
        //                consulta.ConsultaExamenFisico?.Torax ?? false,
        //                consulta.ConsultaExamenFisico?.ObserTorax ?? string.Empty,
        //                consulta.ConsultaExamenFisico?.Abdomen ?? false,
        //                consulta.ConsultaExamenFisico?.ObserAbdomen ?? string.Empty,
        //                consulta.ConsultaExamenFisico?.Pelvis ?? false,
        //                consulta.ConsultaExamenFisico?.ObserPelvis ?? string.Empty,
        //                consulta.ConsultaExamenFisico?.Extremidades ?? false,
        //                consulta.ConsultaExamenFisico?.ObserExtremidades ?? string.Empty
        //            );

        //            consulta.IdConsulta = idConsulta;
        //            TempData["ConsultaReciente"] = JsonConvert.SerializeObject(consulta);

        //            return RedirectToAction("EditarConsulta", new { id = idConsulta });
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Error al crear consulta");
        //            ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
        //            if (ex.InnerException != null)
        //            {
        //                ModelState.AddModelError(string.Empty, $"Detalles: {ex.InnerException.Message}");
        //            }
        //        }
        //    }

        //    return View(consulta);
        //}



        [HttpPost]
        public IActionResult CreateConsultation([FromBody] CrearConsultaMedicaRequest request)
        {
            try
            {
                int newConsultaId = _consultationService.CreateConsultation(
                    request.fechacreacion_consulta,
                    request.usuariocreacion_consulta,
                    request.historial_consulta,
                    request.secuencial_consulta,
                    request.paciente_consulta_p,
                    request.motivo_consulta,
                    request.enfermedad_consulta,
                    request.nombrepariente_consulta,
                    request.signosalarma_consulta,
                    request.reconofarmacologicas,
                    request.tipopariente_consulta,
                    request.telefono_consulta,
                    request.temperatura_consulta,
                    request.frecuenciarespiratoria_consulta,
                    request.presionarterialsistolica_consulta,
                    request.presionarterialdiastolica_consulta,
                    request.pulso_consulta,
                    request.peso_consulta,
                    request.talla_consulta,
                    request.plantratamiento_consulta,
                    request.observacion_consulta,
                    request.antecedentespersonales_consulta,
                    request.diasincapacidad_consulta,
                    request.medico_consulta_d,
                    request.especialidad_id,
                    request.estado_consulta_c,
                    request.tipo_consulta_c,
                    request.notasevolucion_consulta,
                    request.consultaprincipal_consulta,
                    request.activo_consulta,
                    request.fechaactual_consulta,
                    request.medicamentos,
                    request.laboratorios,
                    request.imagenes,
                    request.diagnosticos,
                    request.antecedentesfamiliares,
                    request.organossistemas,
                    request.examenesfisicos
                );

                return Ok(new { ConsultaId = newConsultaId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarPacientePorNombre(int ci)
        {
            var paciente = await _consultationService.BuscarPacientePorCiAsync(ci);
            if (paciente != null)
            {
                return Json(new
                {
                    idPaciente = paciente.IdPacientes,
                    primerApellido = paciente.PrimerapellidoPacientes,
                    segundoApellido = paciente.SegundoapellidoPacientes,
                    primerNombre = paciente.PrimernombrePacientes,
                    segundoNombre = paciente.SegundonombrePacientes,
                    tipoDocumento = paciente.TipodocumentoPacientesCa,
                    numeroDocumento = paciente.CiPacientes,
                    tipoSangre = paciente.TiposangrePacientesCa,
                    esDonante = paciente.DonantePacientes,
                    fechaNacimiento = paciente.FechanacimientoPacientes.HasValue ? paciente.FechanacimientoPacientes.Value.ToString("yyyy-MM-dd") : string.Empty,
                    edad = paciente.EdadPacientes,
                    sexo = paciente.SexoPacientesCa,
                    estadoCivil = paciente.EstadocivilPacientesCa,
                    formacionProfesional = paciente.FormacionprofesionalPacientesCa,
                    nacionalidad = paciente.NacionalidadPacientesPa,
                    direccion = paciente.DireccionPacientes,
                    telefono = paciente.TelefonocelularPacientes,
                    telefonoCelular = paciente.TelefonocelularPacientes,
                    email = paciente.EmailPacientes,
                    ocupacion = paciente.OcupacionPacientes,
                    empresa = paciente.EmpresaPacientes,
                    seguroSalud = paciente.SegurosaludPacientesCa
                });
            }
            else
            {
                return NotFound();
            }
        }



        //GENERACION PDF

        [HttpPost]
        public IActionResult GeneratePdf(int id, string tipoDocumento)
        {

            var consultum = _medical_SystemContext.Consulta
                .Include(c => c.PacienteConsultaPNavigation)
                .ThenInclude(se => se.SexoPacientesCaNavigation)
                .Include(c => c.ConsultaAntecedentesFamiliares)
                .Include(c => c.ConsultaOrganosSistemas)
                .Include(c => c.ConsultaExamenFisico)


                .Include(c => c.ConsultaImagen)
                .ThenInclude(ci => ci.Imagen)
                .Include(c => c.MedicoConsultaDNavigation)
                    .ThenInclude(m => m.Especialidad)
                .Include(c => c.MedicoConsultaDNavigation)
                    .ThenInclude(m => m.Establecimiento)
                .Include(c => c.ConsultaDiagnostico)
                    .ThenInclude(cd => cd.Diagnostico)
                .Include(c => c.ConsultaMedicamento)
                    .ThenInclude(cm => cm.Medicamento)
                .Where(c => c.IdConsulta == id)
                .Include(c => c.ConsultaLaboratorio)
                .ThenInclude(cl => cl.CatalogoLaboratorio)
                .FirstOrDefault();

            if (consultum == null)
            {
                return NotFound();
            }

            switch (tipoDocumento.ToLower())
            {
                case "receta":
                    return new ViewAsPdf("PdfReceta", consultum)
                    {
                        FileName = $"Receta_{consultum.SecuencialConsulta}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                        PageSize = Rotativa.AspNetCore.Options.Size.A5,
                    };
                case "laboratorio":
                    return new ViewAsPdf("PdfLaboratorio", consultum)
                    {
                        FileName = $"Laboratorio_{consultum.SecuencialConsulta}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                        PageSize = Rotativa.AspNetCore.Options.Size.A5,
                    };
                case "imagen":
                    return new ViewAsPdf("PdfImagen", consultum)
                    {
                        FileName = $"Imagen_{consultum.SecuencialConsulta}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                        PageSize = Rotativa.AspNetCore.Options.Size.A5,
                    };
                case "justificacion":
                    return new ViewAsPdf("PdfJustificante", consultum)
                    {
                        FileName = $"Justificante_{consultum.SecuencialConsulta}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape,
                        PageSize = Rotativa.AspNetCore.Options.Size.A5,
                    };

                case "consulta":
                    return new ViewAsPdf("PdfConsulta", consultum)
                    {
                        FileName = $"Consulta_{consultum.SecuencialConsulta}.pdf",
                        PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                        PageSize = Rotativa.AspNetCore.Options.Size.A4,
                    };
                // Agregar más casos según sea necesario para otros tipos de documentos
                default:
                    return BadRequest("Tipo de documento no válido");
            }
        }

    }



}
