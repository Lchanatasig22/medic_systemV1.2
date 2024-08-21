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
using Rotativa.AspNetCore;
using System.Data;
using System.ComponentModel; // Alias para System.Text.Json.JsonSerializer

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
            var model = new ConsultaRequest
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
                Otro = false,

                PresuntivoDiagnosticos = false,
                DefinitivoDiagnosticos = false,

                Abdomen = false,
                Cabeza = false,
                Cuello = false,
                Torax = false,
                Pelvis = false,
                Extremidades = false,

                OrgSentidos = false,
                Respiratorio = false,
                CardioVascular = false,
                Digestivo = false,
                Genital = false,
                Urinario = false,
                MEsqueletico = false,
                Endocrino = false,
                Linfatico = false,
                Nervioso = false,

                NotasevolucionConsulta = "",
                ConsultaprincipalConsulta = "",
                SecuencialConsulta = "",
                Obseralergias = "",
                ObsercirugiasId = "",
            };
            ViewBag.UsuarioId = HttpContext.Session.GetInt32("UsuarioId");
            ViewBag.UsuarioIdEspecialidad = HttpContext.Session.GetInt32("UsuarioIdEspecialidad");
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



        [HttpPost]
        public async Task<IActionResult> CrearConsulta([FromBody] ConsultaRequest request)
        {
            if (request == null)
            {
                return BadRequest(new
                {
                    Message = "Invalid request. The request body is null."
                });
            }

            // Asignar valores predeterminados o valores de la sesión
            request.FechacreacionConsulta = DateTime.Now;
            request.MedicoConsultaD = HttpContext.Session.GetInt32("UsuarioId") ?? 0;
            request.EspecialidadId = HttpContext.Session.GetInt32("UsuarioIdEspecialidad") ?? 0;
            request.UsuariocreacionConsulta = HttpContext.Session.GetString("UsuarioNombre") ?? "Unknown";
            request.SecuencialConsulta = "1";
            request.EstadoConsultaC = 1;
            request.TipoConsultaC = 1;
            request.ActivoConsulta = 1;

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors)
                                              .Select(e => e.ErrorMessage)
                                              .ToList();

                return BadRequest(new
                {
                    Message = "Validation errors occurred.",
                    Errors = errors
                });
            }

            try
            {
                int newConsultaId = await _consultationService.CreateConsultationAsync(
                    request.FechacreacionConsulta,
                    request.UsuariocreacionConsulta,
                    request.HistorialConsulta,
                    request.SecuencialConsulta,
                    request.PacienteConsultaP,
                    request.MotivoConsulta,
                    request.EnfermedadConsulta,
                    request.NombreparienteConsulta,
                    request.SignosalarmaConsulta,
                    request.Reconofarmacologicas,
                    request.TipoparienteConsulta,
                    request.TelefonoConsulta,
                    request.TemperaturaConsulta,
                    request.FrecuenciarespiratoriaConsulta,
                    request.PresionarterialsistolicaConsulta,
                    request.PresionarterialdiastolicaConsulta,
                    request.PulsoConsulta,
                    request.PesoConsulta,
                    request.TallaConsulta,
                    request.PlantratamientoConsulta,
                    request.ObservacionConsulta,
                    request.AntecedentespersonalesConsulta,
                    request.AlergiasConsultaId,
                    request.Obseralergias,
                    request.CirugiasConsultaId,
                    request.ObsercirugiasId,
                    request.DiasincapacidadConsulta,
                    request.MedicoConsultaD,
                    request.EspecialidadId,
                    request.EstadoConsultaC,
                    request.TipoConsultaC,
                    request.NotasevolucionConsulta,
                    request.ConsultaprincipalConsulta,
                    request.ActivoConsulta,
                    request.FechaactualConsulta,
                    request.Medicamentos,
                    request.Laboratorios,
                    request.Imagenes,
                    request.Diagnosticos,
                    request.Cardiopatia ?? false,
                    request.ObserCardiopatia,
                    request.Diabetes ?? false,
                    request.ObserDiabetes,
                    request.EnfCardiovascular ?? false,
                    request.ObserEnfCardiovascular,
                    request.Hipertension ?? false,
                    request.ObserHipertension,
                    request.Cancer ?? false,
                    request.ObserCancer,
                    request.Tuberculosis ?? false,
                    request.ObserTuberculosis,
                    request.EnfMental ?? false,
                    request.ObserEnfMental,
                    request.EnfInfecciosa ?? false,
                    request.ObserEnfInfecciosa,
                    request.MalFormacion ?? false,
                    request.ObserMalFormacion,
                    request.Otro ?? false,
                    request.ObserOtro,
                    request.OrgSentidos ?? false,
                    request.ObserOrgSentidos,
                    request.Respiratorio ?? false,
                    request.ObserRespiratorio,
                    request.CardioVascular ?? false,
                    request.ObserCardioVascular,
                    request.Digestivo ?? false,
                    request.ObserDigestivo,
                    request.Genital ?? false,
                    request.ObserGenital,
                    request.Urinario ?? false,
                    request.ObserUrinario,
                    request.MEsqueletico ?? false,
                    request.ObserMEsqueletico,
                    request.Endocrino ?? false,
                    request.ObserEndocrino,
                    request.Linfatico ?? false,
                    request.ObserLinfatico,
                    request.Nervioso ?? false,
                    request.ObserNervioso,
                    request.Cabeza ?? false,
                    request.ObserCabeza,
                    request.Cuello ?? false,
                    request.ObserCuello,
                    request.Torax ?? false,
                    request.ObserTorax,
                    request.Abdomen ?? false,
                    request.ObserAbdomen,
                    request.Pelvis ?? false,
                    request.ObserPelvis,
                    request.Extremidades ?? false,
                    request.ObserExtremidades,
                    request.ParentescoCatalogoCardiopatia ?? 0,
                    request.ParentescoCatalogoDiabetes ?? 0,
                    request.ParentescoCatalogoEnfCardiovascular ?? 0,
                    request.ParentescoCatalogoHipertension ?? 0,
                    request.ParentescoCatalogoCancer ?? 0,
                    request.ParentescoCatalogoTuberculosis ?? 0,
                    request.ParentescoCatalogoEnfMental ?? 0,
                    request.ParentescoCatalogoEnfInfecciosa ?? 0,
                    request.ParentescoCatalogoMalFormacion ?? 0,
                    request.ParentescoCatalogoOtro ?? 0
                );

                return Ok(new { Id = newConsultaId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating consultation");

                return StatusCode(500, new
                {
                    Message = "Internal server error",
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConsultaById(int id)
        {
            try
            {
                var consulta = await _consultationService.GetConsultaByIdAsync(id);

                if (consulta == null)
                {
                    return NotFound();
                }

                return Ok(consulta);
            }
            catch (Exception ex)
            {
                // Registro del error para diagnóstico
                // _logger.LogError(ex, "Error al obtener la consulta.");

                return StatusCode(500, "Se produjo un error al procesar la solicitud.");
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


    // Clase de extensión dentro del controlador
    public static class DataTableExtensions
    {
        public static DataTable ToDataTable<T>(this IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            }

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }
                table.Rows.Add(row);
            }
            return table;
        }
    }

}
