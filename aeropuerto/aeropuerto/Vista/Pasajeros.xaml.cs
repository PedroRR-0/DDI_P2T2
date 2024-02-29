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
            cargarPasajeros();

        }

        private void cargarPasajeros()
        {
            try
            {
                MySqlConnection c = conex.obtenerConexion();

                // Crear el comando SQL
                string consulta = "SELECT * FROM pasajeros";
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message);
            }
        }
    }


}
