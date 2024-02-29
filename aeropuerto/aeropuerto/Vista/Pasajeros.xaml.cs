using aeropuerto.Modelo;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace aeropuerto
{
    /// <summary>
    /// Lógica de interacción para Pasajeros.xaml
    /// </summary>
    public partial class Pasajeros : UserControl
    {
        private Conexion conex = Conexion.getInstance();
        public Pasajeros()
        {
            InitializeComponent();
            MySqlConnection c = conex.obtenerConexion();
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

        }
    }
}
