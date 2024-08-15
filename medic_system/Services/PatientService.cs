using medic_system.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace medic_system.Services
{
    public class PatientService
    {
        private readonly medical_systemContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ConsultationService _consultaService;
        private readonly ILogger<PatientService> _logger;



        /// <summary>
        /// Siempre que se cree un servicio se tiene que instanciar el DbContext
        /// </summary>
        /// <param name="context"></param>
        public PatientService(medical_systemContext context, IHttpContextAccessor httpContextAccessor,ConsultationService consultationService, ILogger<PatientService> logger)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _consultaService = consultationService;
            _logger = logger;

        }

        public async Task<List<Paciente>> GetAllPacientesAsync()
        {
            // Obtener el nombre de usuario de la sesión fuera del contexto asincrónico
            var NombreUsuario = _httpContextAccessor.HttpContext?.Session?.GetString("UsuarioNombre");

            // Validar que el nombre de usuario esté disponible
            if (string.IsNullOrEmpty(NombreUsuario))
            {
                throw new InvalidOperationException("El nombre de usuario no está disponible en la sesión.");
            }

            // Filtrar los pacientes por el usuario de creación y estado activo
            try
            {
                var pacientes = await _context.Pacientes
                    .Where(p => p.UsuariocreacionPacientes == NombreUsuario && p.EstadoPacientes == 1)
                    .Include(p => p.NacionalidadPacientesPaNavigation)
                    .ToListAsync();

                return pacientes;
            }
            catch (Exception ex)
            {
                // Manejo de errores específico y con un mensaje claro
                throw new Exception("Ocurrió un error al obtener la lista de pacientes.", ex);
            }
        }


        public async Task<int> CreatePatientAsync(Paciente paciente)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_CreatePatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Asignar los parámetros, manejando los valores nulos
                    command.Parameters.Add("@fechacreacion_pacientes", SqlDbType.DateTime).Value = (object)paciente.FechacreacionPacientes ?? DBNull.Value;
                    command.Parameters.Add("@usuariocreacion_pacientes", SqlDbType.NVarChar, 255).Value = (object)paciente.UsuariocreacionPacientes ?? DBNull.Value;
                    command.Parameters.Add("@fechamodificacion_pacientes", SqlDbType.DateTime).Value = (object)paciente.FechamodificacionPacientes ?? DBNull.Value;
                    command.Parameters.Add("@usuariomodificacion_pacientes", SqlDbType.NVarChar, 255).Value = (object)paciente.UsuariomodificacionPacientes ?? DBNull.Value;
                    command.Parameters.Add("@tipodocumento_pacientes_ca", SqlDbType.Int).Value = paciente.TipodocumentoPacientesCa;
                    command.Parameters.Add("@ci_pacientes", SqlDbType.Int).Value = paciente.CiPacientes;
                    command.Parameters.Add("@primernombre_pacientes", SqlDbType.NVarChar, 255).Value = paciente.PrimernombrePacientes;
                    command.Parameters.Add("@segundonombre_pacientes", SqlDbType.NVarChar, 255).Value = (object)paciente.SegundonombrePacientes ?? DBNull.Value;
                    command.Parameters.Add("@primerapellido_pacientes", SqlDbType.NVarChar, 255).Value = paciente.PrimerapellidoPacientes;
                    command.Parameters.Add("@segundoapellido_pacientes", SqlDbType.NVarChar, 255).Value = (object)paciente.SegundoapellidoPacientes ?? DBNull.Value;
                    command.Parameters.Add("@sexo_pacientes_ca", SqlDbType.Int).Value = (object)paciente.SexoPacientesCa ?? DBNull.Value;
                    command.Parameters.Add("@fechanacimiento_pacientes", SqlDbType.Date).Value = (object)paciente.FechanacimientoPacientes ?? DBNull.Value;
                    command.Parameters.Add("@edad_pacientes", SqlDbType.Int).Value = (object)paciente.EdadPacientes ?? DBNull.Value;
                    command.Parameters.Add("@tiposangre_pacientes_ca", SqlDbType.Int).Value = (object)paciente.TiposangrePacientesCa ?? DBNull.Value;
                    command.Parameters.Add("@donante_pacientes", SqlDbType.NVarChar, 50).Value = (object)paciente.DonantePacientes ?? DBNull.Value;
                    command.Parameters.Add("@estadocivil_pacientes_ca", SqlDbType.Int).Value = (object)paciente.EstadocivilPacientesCa ?? DBNull.Value;
                    command.Parameters.Add("@formacionprofesional_pacientes_ca", SqlDbType.Int).Value = (object)paciente.FormacionprofesionalPacientesCa ?? DBNull.Value;
                    command.Parameters.Add("@telefonofijo_pacientes", SqlDbType.NVarChar, 20).Value = (object)paciente.TelefonofijoPacientes ?? DBNull.Value;
                    command.Parameters.Add("@telefonocelular_pacientes", SqlDbType.NVarChar, 20).Value = (object)paciente.TelefonocelularPacientes ?? DBNull.Value;
                    command.Parameters.Add("@email_pacientes", SqlDbType.NVarChar, 255).Value = paciente.EmailPacientes;
                    command.Parameters.Add("@nacionalidad_pacientes_pa", SqlDbType.Int).Value = (object)paciente.NacionalidadPacientesPa ?? DBNull.Value;
                    command.Parameters.Add("@provincia_pacientes_l", SqlDbType.Int).Value = (object)paciente.ProvinciaPacientesL ?? DBNull.Value;
                    command.Parameters.Add("@direccion_pacientes", SqlDbType.NVarChar, -1).Value = (object)paciente.DireccionPacientes ?? DBNull.Value;
                    command.Parameters.Add("@ocupacion_pacientes", SqlDbType.NVarChar, 255).Value = (object)paciente.OcupacionPacientes ?? DBNull.Value;
                    command.Parameters.Add("@empresa_pacientes", SqlDbType.NVarChar, 255).Value = (object)paciente.EmpresaPacientes ?? DBNull.Value;
                    command.Parameters.Add("@segurosalud_pacientes_ca", SqlDbType.Int).Value = (object)paciente.SegurosaludPacientesCa ?? DBNull.Value;
                    command.Parameters.Add("@estado_pacientes", SqlDbType.Int).Value = (object)paciente.EstadoPacientes ?? DBNull.Value;

                    var outputParam = new SqlParameter("@id_pacientes", SqlDbType.Int) { Direction = ParameterDirection.Output };
                    command.Parameters.Add(outputParam);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        return (int)outputParam.Value;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50000 && ex.Message.Contains("El paciente con este CI ya existe."))
                        {
                            throw new Exception("El CI ya existe en la base de datos.");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
        }




        public async Task EditPatientAsync(Paciente paciente)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_EditPatient", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_pacientes", paciente.IdPacientes);
                    command.Parameters.AddWithValue("@fechamodificacion_pacientes", (object?)paciente.FechamodificacionPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@usuariomodificacion_pacientes", (object?)paciente.UsuariomodificacionPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tipodocumento_pacientes_ca", (object?)paciente.TipodocumentoPacientesCa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ci_pacientes", paciente.CiPacientes);
                    command.Parameters.AddWithValue("@primernombre_pacientes", paciente.PrimernombrePacientes);
                    command.Parameters.AddWithValue("@segundonombre_pacientes", (object?)paciente.SegundonombrePacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@primerapellido_pacientes", paciente.PrimerapellidoPacientes);
                    command.Parameters.AddWithValue("@segundoapellido_pacientes", (object?)paciente.SegundoapellidoPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@sexo_pacientes_ca", (object?)paciente.SexoPacientesCa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@fechanacimiento_pacientes", (object?)paciente.FechanacimientoPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@edad_pacientes", (object?)paciente.EdadPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@tiposangre_pacientes_ca", (object?)paciente.TiposangrePacientesCa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@donante_pacientes", (object?)paciente.DonantePacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estadocivil_pacientes_ca", (object?)paciente.EstadocivilPacientesCa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@formacionprofesional_pacientes_ca", (object?)paciente.FormacionprofesionalPacientesCa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@telefonofijo_pacientes", (object?)paciente.TelefonofijoPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@telefonocelular_pacientes", (object?)paciente.TelefonocelularPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@email_pacientes", paciente.EmailPacientes);
                    command.Parameters.AddWithValue("@nacionalidad_pacientes_pa", (object?)paciente.NacionalidadPacientesPa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@provincia_pacientes_l", (object?)paciente.ProvinciaPacientesL ?? DBNull.Value);
                    command.Parameters.AddWithValue("@direccion_pacientes", (object?)paciente.DireccionPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@ocupacion_pacientes", (object?)paciente.OcupacionPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@empresa_pacientes", (object?)paciente.EmpresaPacientes ?? DBNull.Value);
                    command.Parameters.AddWithValue("@segurosalud_pacientes_ca", (object?)paciente.SegurosaludPacientesCa ?? DBNull.Value);
                    command.Parameters.AddWithValue("@estado_pacientes", (object?)paciente.EstadoPacientes ?? DBNull.Value);

                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }


        public async Task<Paciente> GetPacienteByIdAsync(int id)
        {
            return await _context.Pacientes.FindAsync(id);
        }

        public async Task<bool> DeletePatientAsync(int idPaciente)
        {
            try
            {
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    await connection.OpenAsync();

                    using (var command = new SqlCommand("sp_DeletePatient", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id_pacientes", idPaciente);

                        await command.ExecuteNonQueryAsync();
                    }
                }

                return true;
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores SQL
                _logger.LogError(ex, $"Error SQL al intentar eliminar el paciente: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                _logger.LogError(ex, $"Error al intentar eliminar el paciente: {ex.Message}");
                return false;
            }
        }



    }
}
