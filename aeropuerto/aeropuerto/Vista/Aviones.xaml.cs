using MySqlConnector;
using System;
using System.Data.Common;
using System.Data;
using System.Windows;
using System.Windows.Controls;


namespace aeropuerto
{
    /// <summary>
    /// Lógica de interacción para Aviones.xaml
    /// </summary>
    public partial class Aviones : UserControl
    {
        public Aviones()
        {
            InitializeComponent();
            RefreshDatos();
        }

        private void CargarAviones(bool estado)
        {
            try
            {
                Modelo.Conexion conex = Modelo.Conexion.getInstance();
                MySqlConnection c = conex.obtenerConexion();

                string consulta = $"SELECT idAvion, numAsientos, matricula, modelo FROM aviones WHERE estado = {Convert.ToInt32(estado)}";
                using (DbCommand comando = new MySqlCommand(consulta, c))
                {
                    // Crear el adaptador de datos y llenar el DataTable
                    using (DbDataAdapter adaptador = new MySqlDataAdapter((MySqlCommand)comando))
                    {
                        DataTable dataTable = new DataTable();
                        adaptador.Fill(dataTable);

                        if (estado)
                        {
                            tablaAvionesOp.ItemsSource = null;
                            tablaAvionesOp.ItemsSource = dataTable.DefaultView;
                        }
                        else
                        {
                            tablaAvionesNoOp.ItemsSource = null;
                            tablaAvionesNoOp.ItemsSource = dataTable.DefaultView;
                        }
                    }
                }
                c.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message);
            }
        }

        private void btnAddOperativo_Click(object sender, RoutedEventArgs e)
        {
            Modelo.Conexion conex = Modelo.Conexion.getInstance();
            MySqlConnection c = conex.obtenerConexion();
            try
            {
                DataRowView row = (DataRowView)tablaAvionesNoOp.SelectedItem;
                int idAvion = Convert.ToInt32(row.Row.ItemArray[0]);
                String consulta = $"UPDATE aviones SET estado = 1 WHERE idAvion = {idAvion}";
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.ExecuteNonQuery();
                    Console.WriteLine("Se actualizó");
                }
                c.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error de conexión: " + ex.Message);
            }
            finally
            {
                RefreshDatos();
            }
        }

        private void RefreshDatos()
        {
            CargarAviones(true);
            CargarAviones(false);
        }
    }
}
