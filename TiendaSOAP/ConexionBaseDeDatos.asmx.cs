using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace TiendaSOAP
{

    public class ConexionBaseDeDatos
    {
        /// <summary>
        /// Este método se encarga de comprobar si mi conexión es correcta.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string PruebaConexion()
        {
            string connString = "Server=localhost;Database=tiendasoap;Uid=root;Pwd=;";
            using (MySqlConnection conexion = new MySqlConnection(connString))
            {
                try
                {
                    conexion.Open();
                    conexion.Close();
                    return "Conexión correcta";

                }
                catch (Exception ex)
                {
                    return "Conexión incorrecta";
                }
            }

        }
    }
}