using medic_system.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace medic_system.Services
{
    public class AppointmentService
    {
        private readonly medical_systemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<AppointmentService> _logger;

        public AppointmentService(medical_systemContext context, IHttpContextAccessor httpContextAccessor, ILogger<AppointmentService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<List<Citum>> GetAllAppointmentsAsync()
        {
            // Obtener el nombre de usuario de la sesión fuera del contexto asincrónico
            var nombreUsuario = _httpContextAccessor.HttpContext?.Session?.GetString("UsuarioNombre");

            // Validar que el nombre de usuario esté disponible
            if (string.IsNullOrEmpty(nombreUsuario))
            {
                throw new InvalidOperationException("El nombre de usuario no está disponible en la sesión.");
            }

            // Obtener todas las citas filtradas por el usuario de creación
            try
            {
                var citas = await _context.Cita
                    .Where(c => c.UsuariocreacionCita == nombreUsuario)
                    .Include(c => c.Paciente)
                    .Include(c => c.Usuario)
                    .ToListAsync();

                return citas;
            }
            catch (Exception ex)
            {
                // Manejo de errores específico y con un mensaje claro
                throw new Exception("Ocurrió un error al obtener la lista de citas.", ex);
            }
        }

        public async Task<Citum> GetAppointmentByIdAsync(int id)
        {
            try
            {
                var appointment = await _context.Cita
                    .Include(c => c.Paciente)
                    .Include(c => c.Usuario)
                    .FirstOrDefaultAsync(c => c.IdCita == id);

                if (appointment == null)
                {
                    throw new KeyNotFoundException("Cita no encontrada.");
                }

                return appointment;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al obtener la cita por ID.", ex);
            }
        }

        public async Task<bool> CreateAppointmentAsync(Citum cita)
        {
            try
            {
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_CreateAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("@fechacreacion_cita", cita.FechacreacionCita);
                        command.Parameters.AddWithValue("@usuariocreacion_cita", cita.UsuariocreacionCita);
                        command.Parameters.AddWithValue("@fechadelacita_cita", cita.FechadelacitaCita);
                        command.Parameters.AddWithValue("@horadelacita_cita", cita.HoradelacitaCita);
                        command.Parameters.AddWithValue("@usuario_id", cita.UsuarioId);
                        command.Parameters.AddWithValue("@paciente_id", cita.PacienteId);

                        // Manejar consulta_id que puede ser null
                        if (cita.ConsultaId.HasValue)
                        {
                            command.Parameters.AddWithValue("@consulta_id", cita.ConsultaId.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@consulta_id", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@motivo", cita.Motivo);

                        // Parámetro de salida
                        var idCitaParam = new SqlParameter("@id_cita", SqlDbType.Int)
                        {
                            Direction = ParameterDirection.Output
                        };
                        command.Parameters.Add(idCitaParam);

                        await command.ExecuteNonQueryAsync();

                        // Obtener el valor del parámetro de salida
                        cita.IdCita = (int)idCitaParam.Value;
                    }
                }

                return true;
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores SQL
                _logger.LogError(ex, $"Error SQL al intentar crear la cita: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                _logger.LogError(ex, $"Error al intentar crear la cita: {ex.Message}");
                return false;
            }
        }



        public async Task<bool> UpdateAppointmentAsync(Citum cita)
        {
            try
            {
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_EditAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros de entrada
                        command.Parameters.AddWithValue("@id_cita", cita.IdCita);
                        command.Parameters.AddWithValue("@fechadelacita_cita", cita.FechadelacitaCita);
                        command.Parameters.AddWithValue("@horadelacita_cita", cita.HoradelacitaCita);
                        command.Parameters.AddWithValue("@usuario_id", cita.UsuarioId);
                        command.Parameters.AddWithValue("@paciente_id", cita.PacienteId);

                        // Manejar consulta_id que puede ser null
                        if (cita.ConsultaId.HasValue)
                        {
                            command.Parameters.AddWithValue("@consulta_id", cita.ConsultaId.Value);
                        }
                        else
                        {
                            command.Parameters.AddWithValue("@consulta_id", DBNull.Value);
                        }

                        command.Parameters.AddWithValue("@motivo", cita.Motivo);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores SQL
                _logger.LogError(ex, $"Error SQL al intentar actualizar la cita: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                _logger.LogError(ex, $"Error al intentar actualizar la cita: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> DeleteAppointmentAsync(int citaId)
        {
            try
            {
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    using (var command = new SqlCommand("sp_DeleteAppointment", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@cita_id", citaId);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                _logger.LogError(ex, "Error deleting appointment");
                return false;
            }
        }

        public async Task<List<TimeSpan>> ObtenerHorasDisponiblesAsync(DateTime fechaCita, int usuarioId)
        {
            List<TimeSpan> horasDisponibles = new List<TimeSpan>();

            try
            {
                using (SqlConnection connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand("sp_GetAvailableHours", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Parámetros del procedimiento almacenado
                        command.Parameters.AddWithValue("@fecha", fechaCita.Date);
                        command.Parameters.AddWithValue("@usuario_id", usuarioId);

                        using (SqlDataReader reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                TimeSpan hora = reader.GetTimeSpan(0); // Asegúrate de que el índice coincida con la columna
                                horasDisponibles.Add(hora);
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores SQL
                _logger.LogError(ex, $"Error SQL al intentar obtener horas disponibles: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                _logger.LogError(ex, $"Error al intentar obtener horas disponibles: {ex.Message}");
            }

            return horasDisponibles;
        }




    }
}
