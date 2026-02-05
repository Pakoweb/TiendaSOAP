using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace tiendasoap.modelo
{
    [Serializable]
    public class Pedido
    {
        public int PedidoID { get; set; }
        public int UsuarioID { get; set; }
        public DateTime FechaPedido { get; set; }
        public string Estado { get; set; }
        
        // Propiedades adicionales para información completa
        public string NombreUsuario { get; set; }
        
        // Lista de detalles del pedido
        public List<DetallePedido> Detalles { get; set; }
        
        public Pedido()
        {
            Detalles = new List<DetallePedido>();
        }
    }
}
