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
    /// Servicio Web para gestión de detalles de pedidos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class WsDetallesPedidos : System.Web.Services.WebService
    {
        /// <summary>
        /// Obtiene todos los detalles (líneas de productos) de un pedido específico
        /// </summary>
        [WebMethod(Description = "Obtiene todos los detalles de un pedido específico con información de productos")]
        public string ObtenerDetallesPorPedido(int pedidoID)
        {
            using (Conexion oConexion = new Conexion())
            {
                try
                {
                    MySqlConnection conn = oConexion.ObtenerConexion();
                    List<DetallePedido> listaDetalles = new List<DetallePedido>();

                    string query = "SELECT d.DetalleID, d.PedidoID, d.ProductoID, d.Cantidad, d.PrecioUnitario, " +
                                   "p.Nombre AS NombreProducto " +
                                   "FROM DetallePedidos d " +
                                   "INNER JOIN Productos p ON d.ProductoID = p.ProductoID " +
                                   "WHERE d.PedidoID = @pedidoID";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@pedidoID", pedidoID);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DetallePedido detalle = new DetallePedido
                                {
                                    DetalleID = Convert.ToInt32(reader["DetalleID"]),
                                    PedidoID = Convert.ToInt32(reader["PedidoID"]),
                                    ProductoID = Convert.ToInt32(reader["ProductoID"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    PrecioUnitario = Convert.ToDecimal(reader["PrecioUnitario"]),
                                    NombreProducto = reader["NombreProducto"].ToString()
                                };
                                listaDetalles.Add(detalle);
                            }
                        }
                    }

                    if (listaDetalles.Count == 0)
                    {
                        return "<DetallePedidos />";
                    }

                    XmlSerializer serializer = new XmlSerializer(typeof(List<DetallePedido>));
                    using (StringWriter sw = new StringWriter())
                    {
                        serializer.Serialize(sw, listaDetalles);
                        return sw.ToString();
                    }
                }
                catch (Exception ex)
                {
                    return "<Error>Error al obtener detalles del pedido: " + ex.Message + "</Error>";
                }
            }
        }
    }
}
