using medic_system.Models;
using medic_system.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace medic_system.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly AppointmentService _citaService;
        private readonly PatientService _patientService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentController(AppointmentService citaService, PatientService patientService, IHttpContextAccessor httpContextAccessor)
        {
            _citaService = citaService;
            _patientService = patientService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet]
        public async Task<IActionResult> GenerarCita(int pacienteId)
        {
            var usuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
            var usuarioNombre = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");

            if (usuarioId == null || string.IsNullOrEmpty(usuarioNombre))
            {
                return Unauthorized();
            }

            var paciente = await _patientService.GetPacienteByIdAsync(pacienteId);
            if (paciente == null)
            {
                return NotFound("Paciente no encontrado");
            }

            ViewBag.MedicoNombre = usuarioNombre;
            ViewBag.UsuarioId = usuarioId;

            var cita = new Citum
            {
                PacienteId = pacienteId,
                Paciente = paciente,
                UsuarioId = usuarioId.Value,
                Usuario = new Usuario { NombresUsuario = usuarioNombre }
            };

            return View(cita);
        }
        [HttpPost]
        public async Task<IActionResult> GenerarCita(Citum cita)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cita.FechacreacionCita = DateTime.Now;
                    cita.UsuariocreacionCita = _httpContextAccessor.HttpContext.Session.GetString("UsuarioNombre");
                    cita.UsuarioId = _httpContextAccessor.HttpContext.Session.GetInt32("UsuarioId");
                    await _citaService.CreateAppointmentAsync(cita);
                    cita.ConsultaId = null;
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    return Json(new
                    {
                        success = false,
                        message = "Ocurrió un error al generar la cita.",
                        errorDetails = new
                        {
                            ex.Message,
                            ex.StackTrace,
                            InnerExceptionMessage = ex.InnerException?.Message,
                            InnerExceptionStackTrace = ex.InnerException?.StackTrace
                        }
                    });
                }
            }
            else
            {
                var errorList = ModelState.ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                );

                foreach (var error in errorList)
                {
                    Console.WriteLine($"Error en el campo {error.Key}: {string.Join(", ", error.Value)}");
                }

                return Json(new
                {
                    success = false,
                    message = "Modelo no válido",
                    errors = errorList
                });
            }
        }





        // Método para mostrar la vista de actualización
        [HttpGet]
        public async Task<IActionResult> EditarCita(int id)
        {
            var cita = await _citaService.GetAppointmentByIdAsync(id); // Método para obtener la cita por ID
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarCita(Citum cita)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    cita.FechacreacionCita = DateTime.Now;
                    cita.UsuariocreacionCita = HttpContext.Session.GetString("UsuarioNombre");
                    await _citaService.UpdateAppointmentAsync(cita);
                    return Json(new { success = true });
                }
                catch (Exception ex)
                {
                    // Manejar el error (log, etc.)
                    return Json(new { success = false, message = ex.Message });
                }
            }
            else
            {
                // Obtener los errores de validación del ModelState
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return Json(new { success = false, message = "Modelo no válido", errors });
            }
        }


        // Método para eliminar una cita
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarCita(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "ID de cita inválido.";
                return RedirectToAction("ListarCitas");
            }

            try
            {
                var result = await _citaService.DeleteAppointmentAsync(id);
                if (result)
                {
                    TempData["SuccessMessage"] = "Cita eliminada exitosamente.";
                }
                else
                {
                    TempData["ErrorMessage"] = "No se pudo eliminar la cita.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar la cita: {ex.Message}";
            }

            return RedirectToAction("ListarCitas");
        }

        [HttpPost]
        public async Task<IActionResult> ObtenerHorasDisponibles(DateTime fechaCita, int medicoId)
        {
            try
            {
                List<TimeSpan> horasDisponibles = await _citaService.ObtenerHorasDisponiblesAsync(fechaCita, medicoId);

                // Convertir los TimeSpan a cadenas de hora en formato HH:mm
                var horasDisponiblesFormatted = horasDisponibles.Select(ts => ts.ToString(@"hh\:mm")).ToList();

                return Json(horasDisponiblesFormatted);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        [HttpGet]
        public async Task<IActionResult> ListarCitas()
        {
            var usuarioEspecialidad = _httpContextAccessor.HttpContext.Session.GetString("UsuarioEspecialidad");

            ViewBag.UsuarioEspecialidad = usuarioEspecialidad;

            var citas = await _citaService.GetAllAppointmentsAsync();
            return View(citas);
        }


    }
}
