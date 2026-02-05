using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml.Serialization;
using tiendasoap.conexion;
using tiendasoap.modelo;

namespace TiendaSOAP
{
    /// <summary>
    /// Servicio Web para gestión de usuarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WsUsuarios : System.Web.Services.WebService
    {
        /// <summary>
        /// Valida las credenciales de un usuario
        /// </summary>
        [WebMethod(Description = "Valida las credenciales de un usuario y retorna sus datos si son correctas")]
        public string ValidarUsuario(string nombreUsuario, string contraseña)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    string query = "SELECT UsuarioID, NombreUsuario, Nombre, Apellido, Email, FechaRegistro " +
                                   "FROM Usuarios WHERE NombreUsuario = @usuario AND Contraseña = @pass";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@pass", contraseña);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                                };

                                XmlSerializer serializer = new XmlSerializer(typeof(Usuario));
                                using (StringWriter sw = new StringWriter())
                                {
                                    serializer.Serialize(sw, usuario);
                                    return sw.ToString();
                                }
                            }
                            else
                            {
                                return "<Error>Credenciales inválidas</Error>";
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al validar usuario: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Registra un nuevo usuario en el sistema
        /// </summary>
        [WebMethod(Description = "Registra un nuevo usuario en el sistema")]
        public string RegistrarUsuario(string nombreUsuario, string contraseña, string nombre, string apellido, string email)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    // Verificar si el usuario o email ya existen
                    string queryCheck = "SELECT COUNT(*) FROM Usuarios WHERE NombreUsuario = @usuario OR Email = @email";
                    using (MySqlCommand cmdCheck = new MySqlCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@usuario", nombreUsuario);
                        cmdCheck.Parameters.AddWithValue("@email", email);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (count > 0)
                        {
                            return "<Error>El nombre de usuario o email ya están registrados</Error>";
                        }
                    }

                    // Insertar nuevo usuario
                    string queryInsert = "INSERT INTO Usuarios (NombreUsuario, Contraseña, Nombre, Apellido, Email) " +
                                         "VALUES (@usuario, @pass, @nombre, @apellido, @email); SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuario", nombreUsuario);
                        cmd.Parameters.AddWithValue("@pass", contraseña);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@email", email);

                        int nuevoID = Convert.ToInt32(cmd.ExecuteScalar());
                        return "<Success>Usuario registrado correctamente con ID: " + nuevoID + "</Success>";
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al registrar usuario: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Actualiza la información de un usuario existente
        /// </summary>
        [WebMethod(Description = "Actualiza la información de un usuario existente")]
        public string ActualizarUsuario(int usuarioID, string contraseña, string nombre, string apellido, string email)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "UPDATE Usuarios SET Contraseña = @pass, Nombre = @nombre, " +
                                   "Apellido = @apellido, Email = @email WHERE UsuarioID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", usuarioID);
                        cmd.Parameters.AddWithValue("@pass", contraseña);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@email", email);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Usuario actualizado correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró el usuario con ID: " + usuarioID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al actualizar usuario: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Elimina un usuario del sistema
        /// </summary>
        [WebMethod(Description = "Elimina un usuario del sistema")]
        public string EliminarUsuario(int usuarioID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "DELETE FROM Usuarios WHERE UsuarioID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", usuarioID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Usuario eliminado correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró el usuario con ID: " + usuarioID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al eliminar usuario: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los usuarios registrados
        /// </summary>
        [WebMethod(Description = "Obtiene la lista de todos los usuarios registrados")]
        public string ObtenerUsuarios()
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    List<Usuario> listaUsuarios = new List<Usuario>();

                    string query = "SELECT UsuarioID, NombreUsuario, Nombre, Apellido, Email, FechaRegistro FROM Usuarios";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario
                                {
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    NombreUsuario = reader["NombreUsuario"].ToString(),
                                    Nombre = reader["Nombre"].ToString(),
                                    Apellido = reader["Apellido"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    FechaRegistro = Convert.ToDateTime(reader["FechaRegistro"])
                                };
                                listaUsuarios.Add(usuario);
                            }
                        }
                    }

                    if (listaUsuarios.Count == 0)
                    {
                        return "<Usuarios />";
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Usuario>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaUsuarios);
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al obtener usuarios: " + ex.Message + "</Error>";
                }
            }
        }
    }
}
