using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace TiendaSOAP
{
    /// <summary>
    /// Descripción breve de usuarios
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class usuarios : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hola a todos";
        }



        [WebMethod]
        public string EresMayorDeEdad(int edad)
        {
            if (edad >= 18)
            {
                return "Eres mayor de edad";
            }
            else
            {
                return "Eres menor de edad";
            }
        }
    }
}
