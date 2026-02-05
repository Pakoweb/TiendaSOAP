using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace tiendasoap.conexion
{
    public class Conexion : IDisposable
    {
        private MySqlConnection conexion;
        private bool disposed = false;

        public Conexion()
        {
            // Leer cadena de conexión desde Web.config
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["TiendaDB"].ConnectionString;
            conexion = new MySqlConnection(connString);
        }

        /// <summary>
        /// Obtiene la conexión MySQL. Abre la conexión si está cerrada.
        /// </summary>
        public MySqlConnection ObtenerConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
            {
                conexion.Open();
            }
            return conexion;
        }

        /// <summary>
        /// Método obsoleto para compatibilidad con código existente
        /// </summary>
        public MySqlConnection Conector()
        {
            return ObtenerConexion();
        }

        /// <summary>
        /// Libera los recursos de la conexión
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (conexion != null)
                    {
                        if (conexion.State == System.Data.ConnectionState.Open)
                        {
                            conexion.Close();
                        }
                        conexion.Dispose();
                    }
                }
                disposed = true;
            }
        }

        ~Conexion()
        {
            Dispose(false);
        }
    }
}