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
    /// Descripción breve de WsCategorias
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class WsCategorias : System.Web.Services.WebService
    {
        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }
        [WebMethod(Description = "Crea una nueva categoría")]
        public string CrearCategoria(string nombreCategoria)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    // Verificar si la categoría ya existe
                    string queryCheck = "SELECT COUNT(*) FROM Categorias WHERE NombreCategoria = @nombre";
                    using (MySqlCommand cmdCheck = new MySqlCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@nombre", nombreCategoria);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (count > 0)
                        {
                            return "<Error>La categoría ya existe</Error>";
                        }
                    }

                    // Insertar nueva categoría
                    string queryInsert = "INSERT INTO Categorias (NombreCategoria) VALUES (@nombre); SELECT LAST_INSERT_ID();";
                    using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombreCategoria);
                        int nuevoID = Convert.ToInt32(cmd.ExecuteScalar());
                        return "<Success>Categoría creada correctamente con ID: " + nuevoID + "</Success>";
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al crear categoría: " + ex.Message + "</Error>";
                }
            }
        }

        [WebMethod(Description = "Obtiene todas las categorías")]
        public string ObtenerCategorias()
        {
            return BuscarCategoria(); // Reutilizar el método existente
        }

        [WebMethod(Description = "Actualiza una categoría existente")]
        public string ActualizarCategoria(int categoriaID, string nombreCategoria)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "UPDATE Categorias SET NombreCategoria = @nombre WHERE CategoriaID = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", categoriaID);
                        cmd.Parameters.AddWithValue("@nombre", nombreCategoria);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Categoría actualizada correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró la categoría con ID: " + categoriaID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al actualizar categoría: " + ex.Message + "</Error>";
                }
            }
        }

        [WebMethod(Description = "Elimina una categoría")]
        public string BorrarCategoria(int categoriaID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "DELETE FROM Categorias WHERE CategoriaID = @id";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", categoriaID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Categoría eliminada correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró la categoría con ID: " + categoriaID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al eliminar categoría: " + ex.Message + "</Error>";
                }
            }
        }
        [WebMethod]
        public string BuscarCategoria()
        {
            Conexion oconexion = new Conexion();
            MySqlConnection oMySqlConnection = oconexion.Conector();
            List<Categorias> listaCategorias = new List<Categorias>();
            using (oMySqlConnection)
            {
                try
                {

                    string query = "SELECT id_categoria, nombre_categoria FROM categorias";

                    using (MySqlCommand cmd = new MySqlCommand(query, oMySqlConnection))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                //como pasar a xml
                                Categorias cat = new Categorias();
                                cat.id_categoria = Convert.ToInt32(reader["id_categoria"]);
                                cat.nombre_categoria = reader["nombre_categoria"].ToString();
                                listaCategorias.Add(cat);
                            }
                        }
                    }
                    if (listaCategorias.Count == 0) return "<Categorias />";
                    XmlSerializer serializer = new XmlSerializer(typeof(List<Categorias>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaCategorias);
                        return sw.ToString();

                    }
                }
                catch (Exception ex)
                {
                    return "ERROR REAL: " + ex.Message + " | " + ex.InnerException?.Message;



                }

            }
        }
    }
}
