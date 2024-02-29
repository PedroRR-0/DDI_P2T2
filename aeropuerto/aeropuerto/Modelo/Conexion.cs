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

        /**
         * Ejemplo de conexion
         * 
         * Conexion conex = Conexion.getInstance();
         * MySqlConnection c = conex.obtenerConexion();
            try
            {
                String consulta = "insert into aviones (numAsientos, matricula, estado, modelo) values(12,'123',1,'jet')";
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Se insertó");
                }
            }catch(Exception ex) {
                Console.WriteLine("Error de conexión: " + ex.Message);
            }
         */
    }
}
