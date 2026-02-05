using System;
using System.Xml.Serialization;

namespace tiendasoap.modelo
{
    [Serializable]
    public class DetallePedido
    {
        public int DetalleID { get; set; }
        public int PedidoID { get; set; }
        public int ProductoID { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        
        // Propiedades adicionales para información completa
        public string NombreProducto { get; set; }
        
        // Propiedad calculada
        public decimal Subtotal 
        { 
            get { return Cantidad * PrecioUnitario; } 
            set { } // Necesario para serialización XML
        }
    }
}
