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
    /// Servicio Web para gestión de productos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WsProductos : System.Web.Services.WebService
    {
        /// <summary>
        /// Crea un nuevo producto en el catálogo
        /// </summary>
        [WebMethod(Description = "Crea un nuevo producto en el catálogo")]
        public string CrearProducto(string nombre, string descripcion, decimal precio, int stock, int categoriaID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    // Verificar que la categoría existe
                    string queryCheck = "SELECT COUNT(*) FROM Categorias WHERE CategoriaID = @catID";
                    using (MySqlCommand cmdCheck = new MySqlCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("@catID", categoriaID);
                        int count = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (count == 0)
                        {
                            return "<Error>La categoría especificada no existe</Error>";
                        }
                    }

                    // Insertar nuevo producto
                    string queryInsert = "INSERT INTO Productos (Nombre, Descripcion, Precio, Stock, CategoriaID) " +
                                         "VALUES (@nombre, @desc, @precio, @stock, @catID); SELECT LAST_INSERT_ID();";

                    using (MySqlCommand cmd = new MySqlCommand(queryInsert, conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@desc", descripcion);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@catID", categoriaID);

                        int nuevoID = Convert.ToInt32(cmd.ExecuteScalar());
                        return "<Success>Producto creado correctamente con ID: " + nuevoID + "</Success>";
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al crear producto: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Actualiza la información de un producto existente
        /// </summary>
        [WebMethod(Description = "Actualiza la información de un producto existente")]
        public string ActualizarProducto(int productoID, string nombre, string descripcion, decimal precio, int stock, int categoriaID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "UPDATE Productos SET Nombre = @nombre, Descripcion = @desc, " +
                                   "Precio = @precio, Stock = @stock, CategoriaID = @catID WHERE ProductoID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productoID);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@desc", descripcion);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@catID", categoriaID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Producto actualizado correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró el producto con ID: " + productoID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al actualizar producto: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Elimina un producto del catálogo
        /// </summary>
        [WebMethod(Description = "Elimina un producto del catálogo")]
        public string EliminarProducto(int productoID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "DELETE FROM Productos WHERE ProductoID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", productoID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Producto eliminado correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró el producto con ID: " + productoID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al eliminar producto: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Obtiene la lista de todos los productos con información de categoría
        /// </summary>
        [WebMethod(Description = "Obtiene la lista de todos los productos con información de categoría")]
        public string ObtenerProductos()
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    List<Producto> listaProductos = new List<Producto>();

                    string query = "SELECT p.ProductoID, p.Nombre, p.Descripcion, p.Precio, p.Stock, " +
                                   "p.CategoriaID, c.NombreCategoria " +
                                   "FROM Productos p " +
                                   "INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto
                                {
                                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                                    NombreCategoria = reader["NombreCategoria"].ToString()
                                };
                                listaProductos.Add(producto);
                            }
                        }
                    }

                    if (listaProductos.Count == 0)
                    {
                        return "<Productos />";
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Producto>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaProductos);
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al obtener productos: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Busca productos por nombre o categoría
        /// </summary>
        [WebMethod(Description = "Busca productos por nombre o categoría")]
        public string BuscarProductos(string criterio, int categoriaID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    List<Producto> listaProductos = new List<Producto>();

                    string query = "SELECT p.ProductoID, p.Nombre, p.Descripcion, p.Precio, p.Stock, " +
                                   "p.CategoriaID, c.NombreCategoria " +
                                   "FROM Productos p " +
                                   "INNER JOIN Categorias c ON p.CategoriaID = c.CategoriaID " +
                                   "WHERE (@criterio = '' OR p.Nombre LIKE @criterioLike) " +
                                   "AND (@catID = 0 OR p.CategoriaID = @catID)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@criterio", criterio ?? "");
                        cmd.Parameters.AddWithValue("@criterioLike", "%" + (criterio ?? "") + "%");
                        cmd.Parameters.AddWithValue("@catID", categoriaID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto
                                {
                                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    Precio = Convert.ToDecimal(reader["Precio"]),
                                    Stock = Convert.ToInt32(reader["Stock"]),
                                    CategoriaID = Convert.ToInt32(reader["CategoriaID"]),
                                    NombreCategoria = reader["NombreCategoria"].ToString()
                                };
                                listaProductos.Add(producto);
                            }
                        }
                    }

                    if (listaProductos.Count == 0)
                    {
                        return "<Productos />";
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Producto>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaProductos);
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al buscar productos: " + ex.Message + "</Error>";
                }
            }
        }
    }
}
