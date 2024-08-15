using medic_system.Models;
using medic_system.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace medic_system.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly medical_systemContext _context;

        public UserController(UserService userService, medical_systemContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet]
        public IActionResult CrearUsuario()
        {   
            ViewBag.Perfiles = _context.Perfils.ToList();
            ViewBag.Establecimientos = _context.Establecimientos.ToList();
            ViewBag.Especialidades = _context.Especialidads.ToList(); // Asegúrate de que estás obteniendo las especialidades
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CrearUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    usuario.FechacreacionUsuario = DateTime.Now;
                    usuario.FechamodificacionUsuario = DateTime.Now;
                    usuario.EstadoUsuario = 1; // Por defecto, el usuario está activo
                    int newUserId = await _userService.CreateUserAsync(usuario);
                    TempData["SuccessMessage"] = "Usuario creado exitosamente.";
                    return RedirectToAction("ListarUsuarios");
                }
                catch (SqlException sqlEx) when (sqlEx.Number == 50000 || sqlEx.Number == 50001)
                {
                    // Manejo de excepciones específicas de CI o nombre de usuario duplicado
                    TempData["ErrorMessage"] = sqlEx.Message;
                }
                catch (Exception ex)
                {
                    // Manejo de otras excepciones
                    TempData["ErrorMessage"] = $"Error al crear el usuario: {ex.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Modelo no válido.";
            }

            // Si el modelo no es válido o ocurre un error, vuelve a cargar las listas para los select
            await CargarListasDesplegables();
            return View(usuario);
        }

        private async Task CargarListasDesplegables()
        {
            ViewBag.Perfiles = await _context.Perfils.ToListAsync();
            ViewBag.Establecimientos = await _context.Establecimientos.ToListAsync();
            ViewBag.Especialidades = await _context.Especialidads.ToListAsync(); // Asegúrate de que estás obteniendo las especialidades
        }


        [HttpGet]
        public IActionResult EditarUsuario(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.Perfiles = _context.Perfils.ToList();
            ViewBag.Establecimientos = _context.Establecimientos.ToList();
            ViewBag.Especialidades = _context.Especialidads.ToList();
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EditarUsuario(Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Asignar la fecha de modificación actual
                    usuario.FechamodificacionUsuario = DateTime.Now;

                    // Llamar al método del servicio para actualizar el usuario
                    await _userService.EditUserAsync(usuario);

                    TempData["SuccessMessage"] = "Usuario actualizado exitosamente.";
                    return RedirectToAction("ListarUsuarios");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Error al actualizar el usuario: {ex.Message}");
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Modelo no válido.";
            }

            ViewBag.Perfiles = _context.Perfils.ToList();
            ViewBag.Establecimientos = _context.Establecimientos.ToList();
            ViewBag.Especialidades = _context.Especialidads.ToList();
            return View(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
                TempData["SuccessMessage"] = "Usuario eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Error al eliminar el usuario: {ex.Message}";
            }

            return RedirectToAction("ListarUsuarios");
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var usuarios = await _userService.GetAllUserAsync();
            return View(usuarios);
        }
    }
}
