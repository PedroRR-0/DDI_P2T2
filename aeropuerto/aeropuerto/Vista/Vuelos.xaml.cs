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
using aeropuerto.Vista;

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
                    conex.cerrarConexion();
                }
               
            }

            catch (Exception ex) { MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message); 
            }
     }



        private void Añadir(object sender, RoutedEventArgs e)
        {
           AEVuelos ventana = new AEVuelos(true);
            ventana.ShowDialog();
            RefrescarTabla();
        }

        private void Editar(object sender, RoutedEventArgs e)
        {
            if (ColeccionDeVuelos.SelectedItem != null)
            {
                AEVuelos ventana = new AEVuelos(false, (DataRowView)ColeccionDeVuelos.SelectedItem);
                ventana.ShowDialog();
                RefrescarTabla();

            }
            else
            {
                MessageBox.Show("Por favor, selecciona un vuelo para editar.", "Editar Vuelo", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Eliminar(object sender, RoutedEventArgs e)
        {
            try
            {
                // Verificar si hay un vuelo seleccionado en el DataGrid
                if (ColeccionDeVuelos.SelectedItem != null)
                {
                    // Obtener la fila seleccionada
                    DataRowView filaSeleccionada = (DataRowView)ColeccionDeVuelos.SelectedItem;

                    // Obtener los valores de IDVuelo e IDAvion de la fila seleccionada
                    string idVuelo = filaSeleccionada["idVuelo"].ToString();


                    // Consulta SQL para eliminar el vuelo específico
                    string consulta = $"DELETE FROM vuelos WHERE idVuelo = {idVuelo} ";

                    // Establecer la conexión y ejecutar la consulta
                    Modelo.Conexion conex = Modelo.Conexion.getInstance();
                    MySqlConnection conexion = conex.obtenerConexion();

                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                        {
                            comando.ExecuteNonQuery();
                            MessageBox.Show("Vuelo eliminado exitosamente.", "Eliminar Vuelo", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    conex.cerrarConexion();
                    
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un vuelo para eliminar.", "Eliminar Vuelo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar eliminar el vuelo. Detalles: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Actualizar la colección de vuelos después de eliminar
            RefrescarTabla();
        }





        private void RefrescarTabla()
        {
            try
            {
                string connectionString = "Server=localhost;Database=aeropuertocharp;User ID=root;Password=root"; // Reemplaza con tu cadena de conexión
                using (MySqlConnection c = new MySqlConnection(connectionString))
                {
                    c.Open(); // Abre la conexión

                    string consulta = "SELECT v.idVuelo, v.fecha, v.horaSalida, v.horaLlegada, v.idAvion, v.idTrayecto, t.origen, t.destino FROM vuelos v JOIN trayectos t ON v.idTrayecto = t.idTrayecto;";

                    using (DbCommand comando = new MySqlCommand(consulta, c))
                    {
                        using (DbDataAdapter adaptador = new MySqlDataAdapter((MySqlCommand)comando))
                        {
                            DataTable dataTable = new DataTable();
                            adaptador.Fill(dataTable);

                            // Aquí puedes realizar cualquier modificación adicional al DataTable si es necesario

                            ColeccionDeVuelos.ItemsSource = null;
                            ColeccionDeVuelos.ItemsSource = dataTable.DefaultView;
                        }
                    }

                    c.Close(); // Cierra la conexión    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message);
            }
        }



    }
}
