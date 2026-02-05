using System;
using System.Xml.Serialization;

namespace tiendasoap.modelo
{
    [Serializable]
    public class Producto
    {
        public int ProductoID { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public int CategoriaID { get; set; }
        
        // Propiedad adicional para incluir nombre de categoría en respuestas
        public string NombreCategoria { get; set; }
    }
}
