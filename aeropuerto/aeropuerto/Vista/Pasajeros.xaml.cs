using aeropuerto.Modelo;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
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
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Windows.Interop;

namespace aeropuerto
{
    /// <summary>
    /// Lógica de interacción para Pasajeros.xaml
    /// </summary>
    public partial class Pasajeros : UserControl
    {
        private Conexion conex = Conexion.getInstance();
        private int id = 0;
        DataRowView filaSeleccionada;
        public Pasajeros()
        {
            InitializeComponent();
            cargarPasajeros();

        }

        private void cargarPasajeros()
        {
            try
            {
                MySqlConnection c = conex.obtenerConexion();

                // Crear el comando SQL
                string consulta = "SELECT p.idPasajeros, nombre, concat(apellido1,' ',apellido2) AS apellidos, edad(fechaNacimiento) as edad, telefono, direccion, dni, ecorreo, " +
                    "CONCAT(v.idVuelo,': ', t.origen,'-',t.destino) AS vuelo FROM pasajeros p " +
                    "JOIN pasajeros_vuelos pv ON p.idPasajeros = pv.idPasajeros " +
                    "JOIN vuelos v ON v.idVuelo = pv.idVuelo " +
                    "JOIN trayectos t ON v.idTrayecto = t.idTrayecto;";
                using (DbCommand comando = new MySqlCommand(consulta, c))
                {
                    // Crear el adaptador de datos y llenar el DataTable
                    using (DbDataAdapter adaptador = new MySqlDataAdapter((MySqlCommand)comando))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.Fill(dataTable);

                        //Limpiar la fuente de datos
                        tablaPasaj.ItemsSource = null;
                        // Asignar el DataTable como origen de datos para el DataGrid                         
                        tablaPasaj.ItemsSource = dataTable.DefaultView;
                    }
                }
                conex.cerrarConexion();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message);
            }
        }

        

        private void anadirPasaj_Click(object sender, RoutedEventArgs e)
        {
            AEPasajeros aeP = new AEPasajeros(true,id);
            aeP.ShowDialog();
            cargarPasajeros();
        }

        private void editarPasaj_Click(object sender, RoutedEventArgs e)
        {
            if (filaSeleccionada == null) {
                MessageBox.Show("Seleccione un pasajero", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            AEPasajeros aeP = new AEPasajeros(false,id);
            aeP.ShowDialog();
            cargarPasajeros();
        }

        private void elimPasaj_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tablaPasaj_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            filaSeleccionada = (DataRowView)tablaPasaj.SelectedItem;
            if (filaSeleccionada != null)
            {
                id = int.Parse(filaSeleccionada["idPasajeros"].ToString());
            }
        }
    }


}
