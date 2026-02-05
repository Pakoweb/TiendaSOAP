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
    /// Servicio Web para gestión de pedidos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WsPedidos : System.Web.Services.WebService
    {
        /// <summary>
        /// Crea un nuevo pedido con sus detalles usando transacciones
        /// </summary>
        [WebMethod(Description = "Crea un nuevo pedido con sus detalles. Formato detalles: ProductoID,Cantidad,PrecioUnitario|ProductoID,Cantidad,PrecioUnitario")]
        public string CrearPedido(int usuarioID, string detalles)
        {
            using (Conexion oConexion = new Conexion())
            {
                MySqlConnection conn = oConexion.ObtenerConexion();
                MySqlTransaction transaction = null;

                try
                {
                    // Iniciar transacción
                    transaction = conn.BeginTransaction();

                    // Insertar el pedido
                    string queryPedido = "INSERT INTO Pedidos (UsuarioID, Estado) VALUES (@usuarioID, 'Pendiente'); SELECT LAST_INSERT_ID();";
                    int pedidoID;

                    using (MySqlCommand cmdPedido = new MySqlCommand(queryPedido, conn, transaction))
                    {
                        cmdPedido.Parameters.AddWithValue("@usuarioID", usuarioID);
                        pedidoID = Convert.ToInt32(cmdPedido.ExecuteScalar());
                    }

                    // Procesar los detalles del pedido
                    // Formato esperado: "ProductoID,Cantidad,PrecioUnitario|ProductoID,Cantidad,PrecioUnitario"
                    string[] lineas = detalles.Split('|');
                    string queryDetalle = "INSERT INTO DetallePedidos (PedidoID, ProductoID, Cantidad, PrecioUnitario) " +
                                          "VALUES (@pedidoID, @productoID, @cantidad, @precio)";

                    foreach (string linea in lineas)
                    {
                        string[] partes = linea.Split(',');
                        if (partes.Length == 3)
                        {
                            using (MySqlCommand cmdDetalle = new MySqlCommand(queryDetalle, conn, transaction))
                            {
                                cmdDetalle.Parameters.AddWithValue("@pedidoID", pedidoID);
                                cmdDetalle.Parameters.AddWithValue("@productoID", Convert.ToInt32(partes[0]));
                                cmdDetalle.Parameters.AddWithValue("@cantidad", Convert.ToInt32(partes[1]));
                                cmdDetalle.Parameters.AddWithValue("@precio", Convert.ToDecimal(partes[2]));
                                cmdDetalle.ExecuteNonQuery();
                            }
                        }
                    }

                    // Confirmar transacción
                    transaction.Commit();
                    return "<Success>Pedido creado correctamente con ID: " + pedidoID + "</Success>";
                }
                catch (Exception ex)
                {
                    // Revertir transacción en caso de error
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    return "<Error>Error al crear pedido: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Obtiene todos los pedidos de un usuario específico
        /// </summary>
        [WebMethod(Description = "Obtiene todos los pedidos de un usuario específico")]
        public string ObtenerPedidosPorUsuario(int usuarioID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    List<Pedido> listaPedidos = new List<Pedido>();

                    string query = "SELECT p.PedidoID, p.UsuarioID, p.FechaPedido, p.Estado, u.NombreUsuario " +
                                   "FROM Pedidos p " +
                                   "INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID " +
                                   "WHERE p.UsuarioID = @usuarioID " +
                                   "ORDER BY p.FechaPedido DESC";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@usuarioID", usuarioID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Pedido pedido = new Pedido
                                {
                                    PedidoID = Convert.ToInt32(reader["PedidoID"]),
                                    UsuarioID = Convert.ToInt32(reader["UsuarioID"]),
                                    FechaPedido = Convert.ToDateTime(reader["FechaPedido"]),
                                    Estado = reader["Estado"].ToString(),
                                    NombreUsuario = reader["NombreUsuario"].ToString()
                                };
                                listaPedidos.Add(pedido);
                            }
                        }
                    }

                    if (listaPedidos.Count == 0)
                    {
                        return "<Pedidos />";
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pedido>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaPedidos);
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al obtener pedidos: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Actualiza el estado de un pedido
        /// </summary>
        [WebMethod(Description = "Actualiza el estado de un pedido (Pendiente, Enviado, Entregado)")]
        public string ActualizarEstadoPedido(int pedidoID, string nuevoEstado)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();

                    string query = "UPDATE Pedidos SET Estado = @estado WHERE PedidoID = @id";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", pedidoID);
                        cmd.Parameters.AddWithValue("@estado", nuevoEstado);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return "<Success>Estado del pedido actualizado correctamente</Success>";
                        }
                        else
                        {
                            return "<Error>No se encontró el pedido con ID: " + pedidoID + "</Error>";
                        }
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al actualizar estado del pedido: " + ex.Message + "</Error>";
                }
            }
        }

        /// <summary>
        /// Obtiene el historial completo de compras de un usuario con detalles
        /// </summary>
        [WebMethod(Description = "Obtiene el historial completo de compras de un usuario con detalles")]
        public string HistorialCompras(int usuarioID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    List<Pedido> listaPedidos = new List<Pedido>();

                    // Obtener pedidos
                    string queryPedidos = "SELECT p.PedidoID, p.UsuarioID, p.FechaPedido, p.Estado, u.NombreUsuario " +
                                          "FROM Pedidos p " +
                                          "INNER JOIN Usuarios u ON p.UsuarioID = u.UsuarioID " +
                                          "WHERE p.UsuarioID = @usuarioID " +
                                          "ORDER BY p.FechaPedido DESC";

                    using (MySqlCommand cmdPedidos = new MySqlCommand(queryPedidos, conn))
                    {
                        cmdPedidos.Parameters.AddWithValue("@usuarioID", usuarioID);

                        using (MySqlDataReader readerPedidos = cmdPedidos.ExecuteReader())
                        {
                            while (readerPedidos.Read())
                            {
                                Pedido pedido = new Pedido
                                {
                                    PedidoID = Convert.ToInt32(readerPedidos["PedidoID"]),
                                    UsuarioID = Convert.ToInt32(readerPedidos["UsuarioID"]),
                                    FechaPedido = Convert.ToDateTime(readerPedidos["FechaPedido"]),
                                    Estado = readerPedidos["Estado"].ToString(),
                                    NombreUsuario = readerPedidos["NombreUsuario"].ToString()
                                };
                                listaPedidos.Add(pedido);
                            }
                        }
                    }

                    // Obtener detalles para cada pedido
                    string queryDetalles = "SELECT d.DetalleID, d.PedidoID, d.ProductoID, d.Cantidad, d.PrecioUnitario, p.Nombre " +
                                           "FROM DetallePedidos d " +
                                           "INNER JOIN Productos p ON d.ProductoID = p.ProductoID " +
                                           "WHERE d.PedidoID = @pedidoID";

                    foreach (Pedido pedido in listaPedidos)
                    {
                        using (MySqlCommand cmdDetalles = new MySqlCommand(queryDetalles, conn))
                        {
                            cmdDetalles.Parameters.AddWithValue("@pedidoID", pedido.PedidoID);

                            using (MySqlDataReader readerDetalles = cmdDetalles.ExecuteReader())
                            {
                                while (readerDetalles.Read())
                                {
                                    DetallePedido detalle = new DetallePedido
                                    {
                                        DetalleID = Convert.ToInt32(readerDetalles["DetalleID"]),
                                        PedidoID = Convert.ToInt32(readerDetalles["PedidoID"]),
                                        ProductoID = Convert.ToInt32(readerDetalles["ProductoID"]),
                                        Cantidad = Convert.ToInt32(readerDetalles["Cantidad"]),
                                        PrecioUnitario = Convert.ToDecimal(readerDetalles["PrecioUnitario"]),
                                        NombreProducto = readerDetalles["Nombre"].ToString()
                                    };
                                    pedido.Detalles.Add(detalle);
                                }
                            }
                        }
                    }

                    if (listaPedidos.Count == 0)
                    {
                        return "<Pedidos />";
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<Pedido>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaPedidos);
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al obtener historial de compras: " + ex.Message + "</Error>";
                }
            }
        }
    }
}
