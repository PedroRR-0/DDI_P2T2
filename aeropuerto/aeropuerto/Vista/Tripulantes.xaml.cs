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
using aeropuerto.Vista;

namespace aeropuerto
{
    /// <summary>
    /// Lógica de interacción para Tripulantes.xaml
    /// </summary>
    public partial class Tripulantes : UserControl
    {
        

        public Tripulantes()
        {
            InitializeComponent();

            CargarDatos();
        }

        private void CargarDatos()
        {
            try
            {
                Modelo.Conexion conex = Modelo.Conexion.getInstance();
                MySqlConnection c = conex.obtenerConexion(); // Crear el comando SQL
                string consulta = "SELECT * FROM miembros;";
                using (DbCommand comando = new MySqlCommand(consulta, c))
                {
                    // Crear el adaptador de datos y llenar el DataTable
                    using (DbDataAdapter adaptador = new MySqlDataAdapter((MySqlCommand)comando))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.Fill(dataTable);

                        //Limpiar la fuente de datos
                        tablaTripulantes.ItemsSource = null;

                        // Asignar el DataTable como origen de datos para el DataGrid
                        tablaTripulantes.ItemsSource = dataTable.DefaultView;

                    }
                    conex.cerrarConexion();
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message);
            }
        }

        private void AñadirTripulante(object sender, RoutedEventArgs e)
        {
            ATripulante ventana = new ATripulante(true);
            ventana.ShowDialog();
            CargarDatos();
        }

        private void EditarTripulante(object sender, RoutedEventArgs e)
        {
            if (tablaTripulantes.SelectedItem != null)
            {
                ATripulante ventana = new ATripulante(false, (DataRowView)tablaTripulantes.SelectedItem);
                ventana.ShowDialog();
                CargarDatos();

            }
        }

        private void EliminarTripulante(object sender, RoutedEventArgs e)
        {
            try
            {                 
                if (tablaTripulantes.SelectedItem != null)
                {
                    DataRowView fila = (DataRowView)tablaTripulantes.SelectedItem;
                    Modelo.Conexion conex = Modelo.Conexion.getInstance();
                    MySqlConnection c = conex.obtenerConexion();
                    string consulta = "DELETE FROM miembros WHERE idTripulacion = @id;";
                    using (DbCommand comando = new MySqlCommand(consulta, c))
                    {
                        comando.Parameters.Add(new MySqlParameter("@id", fila["idTripulacion"]));
                        comando.ExecuteNonQuery();
                        conex.cerrarConexion();
                        
                        MessageBox.Show("Tripulante eliminado exitosamente.", "Eliminar Tripulante", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    CargarDatos();
                }
                else
                {
                    MessageBox.Show("Por favor, selecciona un tripulante para eliminar.", "Eliminar Tripulante", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar el tripulante: " + ex.Message);
            }
        }

        
    }
}
