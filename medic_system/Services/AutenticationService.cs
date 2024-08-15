using medic_system.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class AutenticationService
{
    private readonly medical_systemContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AutenticationService(medical_systemContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<Usuario> ValidateUser(string loginUsuario, string claveUsuario)
    {
        if (string.IsNullOrEmpty(loginUsuario) || string.IsNullOrEmpty(claveUsuario))
        {
            throw new ArgumentException("El login y la clave no pueden estar vacíos.");
        }

        var parameterLoginUsuario = new SqlParameter("@login_usuario", loginUsuario);
        var parameterClaveUsuario = new SqlParameter("@clave_usuario", claveUsuario);

        Usuario user = null;
        try
        {
            using (var connection = new SqlConnection(_context.Database.GetDbConnection().ConnectionString))
            {
                await connection.OpenAsync().ConfigureAwait(false);

                using (var command = new SqlCommand("[dbo].[sp_ValidarCredenciales]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(parameterLoginUsuario);
                    command.Parameters.Add(parameterClaveUsuario);

                    using (var reader = await command.ExecuteReaderAsync().ConfigureAwait(false))
                    {
                        if (await reader.ReadAsync().ConfigureAwait(false))
                        {
                            user = new Usuario
                            {
                                IdUsuario = GetValueOrDefault<int>(reader, "id_usuario"),
                                PerfilId = GetValueOrDefault<int>(reader, "perfil_id"),
                                NombresUsuario = GetValueOrDefault<string>(reader, "nombres_usuario"),
                                ApellidosUsuario = GetValueOrDefault<string>(reader, "apellidos_usuario"),
                                LoginUsuario = GetValueOrDefault<string>(reader, "login_usuario"),
                                Especialidad = new Especialidad
                                {
                                    IdEspecialidad = GetValueOrDefault<int>(reader, "especialidad_id"),
                                    NombreEspecialidad = GetValueOrDefault<string>(reader, "especialidad_usuario")
                                },
                                Perfil = new Perfil
                                {
                                    DescripcionPerfil = GetValueOrDefault<string>(reader, "descripcion_usuario")
                                },
                                Establecimiento = new Establecimiento
                                {
                                    DireccionEstablecimiento = SafeGetString(reader, "direccion_usuario") // Uso de método seguro
                                }
                            };

                            // Store user details in session
                            var session = _httpContextAccessor.HttpContext.Session;
                            session.SetString("UsuarioNombre", user.NombresUsuario);
                            session.SetInt32("UsuarioId", user.IdUsuario);
                            session.SetString("UsuarioApellido", user.ApellidosUsuario);
                            session.SetString("UsuarioDescripcion", string.IsNullOrEmpty(user.Perfil.DescripcionPerfil) ? "Default Description" : user.Perfil.DescripcionPerfil);
                            session.SetString("UsuarioEspecialidad", user.Especialidad.NombreEspecialidad);
                            session.SetString("UsuarioDireccion", user.Establecimiento.DireccionEstablecimiento);
                            if (user.PerfilId != null)
                            {
                                session.SetInt32("PerfilId", user.PerfilId.Value);
                            }
                            if (user.Especialidad != null && user.Especialidad.IdEspecialidad > 0)
                            {
                                session.SetInt32("UsuarioIdEspecialidad", user.Especialidad.IdEspecialidad);
                            }
                        }
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            // Log SQL exceptions
            // _logger.LogError(ex, "An error occurred while validating the user.");
            throw new Exception("Ocurrió un error al validar el usuario.", ex);
        }
        catch (Exception ex)
        {
            // Log other exceptions
            // _logger.LogError(ex, "An unexpected error occurred while validating the user.");
            throw new Exception("Ocurrió un error al validar el usuario.", ex);
        }

        return user;
    }

    private T GetValueOrDefault<T>(SqlDataReader reader, string columnName)
    {
        try
        {
            var columnIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(columnIndex) ? default(T) : reader.GetFieldValue<T>(columnIndex);
        }
        catch (IndexOutOfRangeException)
        {
            // Log the error or handle specifically if the column does not exist
            Console.WriteLine($"Column '{columnName}' does not exist in the result set.");
            return default(T);
        }
    }

    private string SafeGetString(SqlDataReader reader, string columnName)
    {
        try
        {
            int colIndex = reader.GetOrdinal(columnName);
            return reader.IsDBNull(colIndex) ? null : reader.GetString(colIndex);
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine($"Column '{columnName}' does not exist in the result set.");
            return null;
        }
    }


}
