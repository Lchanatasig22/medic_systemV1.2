using Microsoft.AspNetCore.Mvc;
using medic_system.Models;
using medic_system.Services;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace medic_system.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _pacienteService;
        private readonly CatalogService _catalogService;
        private readonly ILogger<PatientController> _logger;


        public PatientController(PatientService pacienteService, CatalogService catalogService, ILogger<PatientController> logger)
        {
            _pacienteService = pacienteService;
            _catalogService = catalogService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> CrearPaciente()
        {
            ViewBag.UsuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
            var model = new Paciente
            {
                NacionalidadPacientesPa = 14 // Nacionalidad por defecto
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPaciente(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var usuarioNombre = HttpContext.Session.GetString("UsuarioNombre");

                 
                    paciente.UsuariocreacionPacientes = usuarioNombre;
                    paciente.FechacreacionPacientes = DateTime.Now;
                    paciente.FechamodificacionPacientes = DateTime.Now;
                    paciente.UsuariomodificacionPacientes = usuarioNombre;
                    paciente.EstadoPacientes = 1;

                    await _pacienteService.CreatePatientAsync(paciente);
                    TempData["SuccessMessage"] = "Paciente creado exitosamente.";
                    return RedirectToAction("ListarPacientes");
                }
                catch (SqlException ex)
                {
                    // Logging the error
                    _logger.LogError(ex, "SQL error while creating patient");

                    // Specific SQL error handling
                    if (ex.Number == 50000 && ex.Message.Contains("El paciente con este CI ya existe."))
                    {
                        TempData["ErrorMessage"] = "El CI ya existe en la base de datos.";
                    }
                    else
                    {
                        TempData["ErrorMessage"] = $"Error al crear el paciente: {ex.Message}";
                    }
                }
                catch (Exception ex)
                {
                    // Logging the error
                    _logger.LogError(ex, "Unexpected error while creating patient");
                    TempData["ErrorMessage"] = $"Error inesperado al crear el paciente: {ex.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Hay errores de validación en el formulario:";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        TempData["ErrorMessage"] += $" {error.ErrorMessage}";
                    }
                }
            }

            // Re-cargar los ViewBags en caso de error
            await LoadViewBagsAsync();

            return View(paciente);
        }

        private async Task LoadViewBagsAsync()
        {
            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
        }

        [HttpGet]
        public async Task<IActionResult> EditarPaciente(int id)
        {
            var paciente = await _pacienteService.GetPacienteByIdAsync(id);
            if (paciente == null)
            {
                return NotFound();
            }

            await CargarListasDesplegables();
            return View(paciente);
        }

        private async Task CargarListasDesplegables()
        {
            ViewBag.TiposDocumentos = await _catalogService.ObtenerTiposDocumentosAsync();
            ViewBag.TiposSangre = await _catalogService.ObtenerTiposDeSangreAsync();
            ViewBag.TiposGenero = await _catalogService.ObtenerTiposDeGeneroAsync();
            ViewBag.TiposEstadoCivil = await _catalogService.ObtenerTiposDeEstadoCivilAsync();
            ViewBag.TiposFormacion = await _catalogService.ObtenerTiposDeFormacionPAsync();
            ViewBag.TiposNacionalidad = await _catalogService.ObtenerTiposDeNacionalidadPAsync();
            ViewBag.TiposProvincia = await _catalogService.ObtenerTiposDeProvinciaPAsync();
            ViewBag.TiposSeguro = await _catalogService.ObtenerTiposDeSeguroAsync();
        }

        [HttpPost]
        public async Task<IActionResult> EditarPaciente(Paciente paciente)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    paciente.UsuariomodificacionPacientes = HttpContext.Session.GetString("UsuarioNombre");
                    paciente.FechamodificacionPacientes = DateTime.Now;
                    paciente.EstadoPacientes = 1;
 
                    await _pacienteService.EditPatientAsync(paciente);

                    TempData["SuccessMessage"] = "Paciente actualizado exitosamente.";
                    return RedirectToAction("ListarPacientes");
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError(string.Empty, $"Error de base de datos al actualizar el paciente: {dbEx.Message}");
                }
                catch (ArgumentException argEx)
                {
                    ModelState.AddModelError(string.Empty, $"Error en los argumentos al actualizar el paciente: {argEx.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error inesperado al actualizar el paciente: {ex.Message}");
                }
            }
            else
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    ModelState.AddModelError(string.Empty, $"Error de validación: {error.ErrorMessage}");
                }
            }

            await CargarListasDesplegables();
            return View(paciente);
        }


        [HttpPost]
        public async Task<IActionResult> EliminarPaciente(int id)
        {
            try
            {
                await _pacienteService.DeletePatientAsync(id);
                TempData["SuccessMessage"] = "Paciente eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el paciente: {ex.Message}";
            }

            return RedirectToAction("ListarPacientes");
        }


        [HttpGet]
        public async Task<IActionResult> ListarPacientes()
        {
            var pacientes = await _pacienteService.GetAllPacientesAsync();
            return View(pacientes);
        }
    }
}
