using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aeropuerto.Modelo
{
    public class Conexion
    {
        private MySqlConnection conexion;
        // Instancia estática de la clase Conexion para implementar el patrón Singleton.
        private static Conexion? instancia;

        private Conexion()
        {
            string cadenaConexion = "Server=localhost;Database=aeropuertocharp;User ID=root;Password=root";
            conexion = new MySqlConnection(cadenaConexion);
        }

        public MySqlConnection obtenerConexion()
        {
            conexion.Open();
            return conexion;
        }

        public void cerrarConexion()
        {
            conexion.Close();
        }
        public static Conexion getInstance()
        {
            instancia ??= new Conexion();
            return instancia;
        }
    }
}
