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

using System.Collections.ObjectModel;
using MySqlConnector;
using aeropuerto.Modelo;
using System.Data;
using System.Data.Common;
using System.Security.Policy;

namespace aeropuerto
{
    public partial class Vuelos : UserControl
    {
        public Vuelos()
        {
            InitializeComponent();

            CargarDatos();
        }

        private void CargarDatos() {
            try
            {
                Modelo.Conexion conex = Modelo.Conexion.getInstance();
                MySqlConnection c = conex.obtenerConexion(); // Crear el comando SQL
                string consulta = "SELECT v.idVuelo, v.fecha, v.horaSalida, v.horaLlegada, v.idAvion, v.idTrayecto, t.origen, t.destino FROM vuelos v JOIN trayectos t ON v.idTrayecto = t.idTrayecto;";
                using (DbCommand comando = new MySqlCommand(consulta, c))
                {
                    // Crear el adaptador de datos y llenar el DataTable
                    using (DbDataAdapter adaptador = new MySqlDataAdapter((MySqlCommand)comando))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.Fill(dataTable);

                        //Limpiar la fuente de datos 
                        ColeccionDeVuelos.ItemsSource = null;

                        // Asignar el DataTable como origen de datos para el DataGrid 
                        ColeccionDeVuelos.ItemsSource = dataTable.DefaultView;

                    }
                }
                conex.cerrarConexion();
            }

            catch (Exception ex) { MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message); }
     }



        private void Añadir(object sender, RoutedEventArgs e)
        {

        }

        private void Editar(object sender, RoutedEventArgs e)
        {

        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {

        }
    }
}
