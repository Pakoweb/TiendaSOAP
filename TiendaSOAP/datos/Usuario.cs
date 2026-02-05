using System;
using System.Xml.Serialization;

namespace tiendasoap.modelo
{
    [Serializable]
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        // No incluir contraseña en respuestas por seguridad
        [XmlIgnore]
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public DateTime FechaRegistro { get; set; }
    }
}
