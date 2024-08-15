using medic_system.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace medic_system.Services
{
    public class UserService
    {
        private readonly medical_systemContext _context;

        public UserService(medical_systemContext context)
        {
            _context = context;
        }

        // Método para listar usuarios
        public async Task<List<Usuario>> GetAllUserAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Perfil)          // Incluye la relación con Perfil
                .Include(u => u.Especialidad)    // Incluye la relación con Especialidad
                .Include(u => u.Establecimiento) // Incluye la relación con Establecimiento
                .ToListAsync();
        }

        // Método para crear un usuario
        public async Task<int> CreateUserAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_CreateUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@ci_usuario", usuario.CiUsuario);
                    command.Parameters.AddWithValue("@nombres_usuario", usuario.NombresUsuario);
                    command.Parameters.AddWithValue("@apellidos_usuario", usuario.ApellidosUsuario);
                    command.Parameters.AddWithValue("@telefono_usuario", usuario.TelefonoUsuario);
                    command.Parameters.AddWithValue("@email_usuario", usuario.EmailUsuario);
                    command.Parameters.AddWithValue("@fechacreacion_usuario", usuario.FechacreacionUsuario);
                    command.Parameters.AddWithValue("@fechamodificacion_usuario", usuario.FechamodificacionUsuario);
                    command.Parameters.AddWithValue("@direccion_usuario", usuario.DireccionUsuario);
                    command.Parameters.Add(new SqlParameter("@firmadigital_usuario", SqlDbType.VarBinary)
                    {
                        Value = usuario.FirmadigitalUsuario ?? (object)DBNull.Value
                    });
                    command.Parameters.Add(new SqlParameter("@codigoqr_usuario", SqlDbType.VarBinary)
                    {
                        Value = usuario.CodigoqrUsuario ?? (object)DBNull.Value
                    });
                    command.Parameters.AddWithValue("@codigo_senecyt", usuario.CodigoSenecyt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@login_usuario", usuario.LoginUsuario);
                    command.Parameters.AddWithValue("@clave_usuario", usuario.ClaveUsuario);
                    command.Parameters.AddWithValue("@codigo_usuario", usuario.CodigoUsuario);
                    command.Parameters.AddWithValue("@estado_usuario", usuario.EstadoUsuario);
                    command.Parameters.AddWithValue("@perfil_id", usuario.PerfilId);
                    command.Parameters.AddWithValue("@establecimiento_id", usuario.EstablecimientoId);
                    command.Parameters.AddWithValue("@especialidad_id", usuario.EspecialidadId);

                    // Parámetro de salida
                    var idUsuarioParameter = new SqlParameter("@id_usuario", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    command.Parameters.Add(idUsuarioParameter);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                        return (int)idUsuarioParameter.Value;
                    }
                    catch (SqlException ex)
                    {
                        if (ex.Number == 50000) // CI duplicado
                        {
                            throw new Exception("El usuario con este CI ya existe.");
                        }
                        else if (ex.Number == 50001) // Nombre de usuario duplicado
                        {
                            throw new Exception("El nombre de usuario ya existe.");
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
            }
        }



        // Método para editar un usuario
        public async Task EditUserAsync(Usuario usuario)
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                using (var command = new SqlCommand("sp_EditUser", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@id_usuario", usuario.IdUsuario);
                    command.Parameters.AddWithValue("@ci_usuario", usuario.CiUsuario);
                    command.Parameters.AddWithValue("@nombres_usuario", usuario.NombresUsuario);
                    command.Parameters.AddWithValue("@apellidos_usuario", usuario.ApellidosUsuario);
                    command.Parameters.AddWithValue("@telefono_usuario", usuario.TelefonoUsuario);
                    command.Parameters.AddWithValue("@email_usuario", usuario.EmailUsuario);
                    command.Parameters.AddWithValue("@fechamodificacion_usuario", usuario.FechamodificacionUsuario);
                    command.Parameters.AddWithValue("@direccion_usuario", usuario.DireccionUsuario);
                    command.Parameters.Add(new SqlParameter("@firmadigital_usuario", SqlDbType.VarBinary)
                    {
                        Value = usuario.FirmadigitalUsuario ?? (object)DBNull.Value
                    });
                    command.Parameters.Add(new SqlParameter("@codigoqr_usuario", SqlDbType.VarBinary)
                    {
                        Value = usuario.CodigoqrUsuario ?? (object)DBNull.Value
                    });
                    command.Parameters.AddWithValue("@codigo_senecyt", usuario.CodigoSenecyt ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@login_usuario", usuario.LoginUsuario);
                    command.Parameters.AddWithValue("@clave_usuario", usuario.ClaveUsuario);
                    command.Parameters.AddWithValue("@codigo_usuario", usuario.CodigoUsuario);
                    command.Parameters.AddWithValue("@estado_usuario", usuario.EstadoUsuario);
                    command.Parameters.AddWithValue("@perfil_id", usuario.PerfilId);
                    command.Parameters.AddWithValue("@establecimiento_id", usuario.EstablecimientoId);
                    command.Parameters.AddWithValue("@especialidad_id", usuario.EspecialidadId);

                    try
                    {
                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                    catch (SqlException ex)
                    {
                        // Manejo de excepciones específicas si es necesario
                        throw new Exception($"Error al editar el usuario: {ex.Message}", ex);
                    }
                }
            }
        }



        // Método para eliminar un usuario
        public async Task DeleteUserAsync(int idUsuario)
        {
            try
            {
                using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
                {
                    using (var command = new SqlCommand("sp_DeleteUser", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@id_usuario", idUsuario);

                        await connection.OpenAsync();
                        await command.ExecuteNonQueryAsync();
                    }
                }
            }
            catch (SqlException ex)
            {
                // Manejo específico de errores SQL
                throw new Exception($"Error SQL al intentar eliminar el usuario: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Manejo de otros errores
                throw new Exception($"Error al intentar eliminar el usuario: {ex.Message}", ex);
            }
        }


    }
}
