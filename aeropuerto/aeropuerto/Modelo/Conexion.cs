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

        public List<int> ObtenerAvionesConEstado(int estado)
        {
            List<int> aviones = new List<int>();

            Conexion conex = Conexion.getInstance();
            MySqlConnection conexion = conex.obtenerConexion();

            using (var cmd = new MySqlCommand("SELECT idAvion FROM aviones WHERE estado = @estado", conexion))
            {
                cmd.Parameters.AddWithValue("@estado", estado);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aviones.Add(Convert.ToInt32(reader["idAvion"]));
                    }
                }
            }
            conex.cerrarConexion();
            return aviones;
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
