using aeropuerto.Modelo;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace aeropuerto.Vista
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class AEVuelos : Window
    {
        private DataRowView filaSeleccionada;  // Declarar filaSeleccionada a nivel de clase

        public AEVuelos(Boolean ae, DataRowView filaSeleccionada = null)
        {
            InitializeComponent();

            LlenarComboBoxAviones();
            LlenarComboBoxTrayectos();

            if (ae)
            {
                limpiarcampos();
                editar.Visibility = Visibility.Hidden;
                aceptar.Visibility = Visibility.Visible;
                origen.IsEnabled = false;
                destino.IsEnabled = false;
            }
            else
            {
                editar.Visibility = Visibility.Visible;
                aceptar.Visibility = Visibility.Hidden;
                origen.IsEnabled = false;
                destino.IsEnabled = false;

                this.filaSeleccionada = filaSeleccionada;  // Asignar filaSeleccionada al campo de clase
                RellenarCamposDesdeFila(filaSeleccionada);

            }

        }

        private void RellenarCamposDesdeFila(DataRowView filaSeleccionada)
        {
            if (filaSeleccionada != null)
            {
                // Rellenar los campos con los valores de la fila seleccionada
                fecha.SelectedDate = Convert.ToDateTime(filaSeleccionada["fecha"]);
                hora_salida.Text = filaSeleccionada["horaSalida"].ToString();
                hora_llegada.Text = filaSeleccionada["horaLlegada"].ToString();
                idAvion.SelectedItem = Convert.ToInt32(filaSeleccionada["idAvion"]);
                idTrayectos.SelectedItem = Convert.ToInt32(filaSeleccionada["idTrayecto"]);

                // Actualizar campos de origen y destino según el idTrayecto seleccionado
                int idTrayectoSeleccionado = Convert.ToInt32(filaSeleccionada["idTrayecto"]);
                ActualizarCamposOrigenDestino(idTrayectoSeleccionado);
            }
        }

        private void LlenarComboBoxAviones()
        {
            // Asumiendo que tu ComboBox de aviones se llama idAvion
            idAvion.ItemsSource = ObtenerAvionesConEstado(1);  // 1 representa el estado que deseas (activo)
        }

        private void LlenarComboBoxTrayectos()
        {
            // Asumiendo que tu ComboBox de trayectos se llama idTrayecto
            idTrayectos.ItemsSource = ObtenerTodosTrayectos();
        }

        private void limpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiarcampos();
        }

        private void editar_Click(object sender, RoutedEventArgs e)
        {

            if (TodosLosCamposLlenos())
            {
                // Obtener los valores de los campos
                string fechaVuelo = fecha.SelectedDate?.ToString("yyyy/MM/dd");
                string horaSalida = hora_salida.Text;
                string horaLlegada = hora_llegada.Text;
                int IdAvion = (int)idAvion.SelectedItem;
                int idTrayectoSeleccionado = (int)idTrayectos.SelectedItem;


                // Obtener el ID del vuelo
                int idVuelo = Convert.ToInt32(filaSeleccionada["idVuelo"]);

                // Realizar la inserción en la base de datos
                if (ActualizarVuelo(idVuelo, fechaVuelo, horaSalida, horaLlegada, IdAvion, idTrayectoSeleccionado))
                {
                    MessageBox.Show("Vuelo editado correctamente.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Error al editar el vuelo. Verifica los datos.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, completa todos los campos antes de editar.");
            }
            
        }

        private bool ActualizarVuelo(int idVuelo, string fecha, string horaSalida, string horaLlegada, int idAvion, int idTrayecto)
        {
            Modelo.Conexion conex = Modelo.Conexion.getInstance();
            MySqlConnection conexion = conex.obtenerConexion();

            try
            {
                using (var cmd = new MySqlCommand("UPDATE vuelos SET fecha = @fecha, horaSalida = @horaSalida, " +
                                                  "horaLlegada = @horaLlegada, idAvion = @idAvion, idTrayecto = @idTrayecto " +
                                                  "WHERE idVuelo = @idVuelo", conexion))
                {
                    cmd.Parameters.AddWithValue("@idVuelo", idVuelo);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@horaSalida", horaSalida);
                    cmd.Parameters.AddWithValue("@horaLlegada", horaLlegada);
                    cmd.Parameters.AddWithValue("@idAvion", idAvion);
                    cmd.Parameters.AddWithValue("@idTrayecto", idTrayecto);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el vuelo: " + ex.Message);
                return false;
            }
            finally
            {
                conex.cerrarConexion();
            }
        }



        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (TodosLosCamposLlenos())
            {
                // Obtener los valores de los campos
                string fechaVuelo = fecha.SelectedDate?.ToString("yyyy/MM/dd");
                string horaSalida = hora_salida.Text;
                string horaLlegada = hora_llegada.Text;
                int IdAvion = (int)idAvion.SelectedItem;
                int idTrayectoSeleccionado = (int)idTrayectos.SelectedItem;


                // Realizar la inserción en la base de datos
                if (InsertarVuelo(fechaVuelo, horaSalida, horaLlegada, IdAvion, idTrayectoSeleccionado))
                {
                    MessageBox.Show("Vuelo insertado correctamente.");
                    limpiarcampos();
                }
                else
                {
                    MessageBox.Show("Error al insertar el vuelo. Verifica los datos.");
                }
            }
            else
            {
                MessageBox.Show("Por favor, completa todos los campos antes de aceptar.");
            }
        }

        private bool TodosLosCamposLlenos()
        {
            return !string.IsNullOrEmpty(fecha.Text) &&
                   !string.IsNullOrEmpty(hora_salida.Text) &&
                   !string.IsNullOrEmpty(hora_llegada.Text) &&
                   idAvion.SelectedItem != null &&
                   idTrayectos.SelectedItem != null;
        }

        private bool InsertarVuelo(string fecha, string horaSalida, string horaLlegada, int idAvion, int idTrayecto)
        {
            Modelo.Conexion conex = Modelo.Conexion.getInstance();
            MySqlConnection conexion = conex.obtenerConexion();

            try
            {
                using (var cmd = new MySqlCommand("INSERT INTO vuelos (fecha, horaSalida, horaLlegada, idAvion, idTrayecto ) " +
                                                  "VALUES (@fecha, @horaSalida, @horaLlegada, @idAvion, @idTrayecto)", conexion))
                {
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@horaSalida", horaSalida);
                    cmd.Parameters.AddWithValue("@horaLlegada", horaLlegada);
                    cmd.Parameters.AddWithValue("@idAvion", idAvion);
                    cmd.Parameters.AddWithValue("@idTrayecto", idTrayecto);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar el vuelo: " + ex.Message);
                return false;
            }
            finally
            {
                conex.cerrarConexion();
            }
        }


      




        private List<int> ObtenerAvionesConEstado(int estado)
        {
            List<int> aviones = new List<int>();

            Modelo.Conexion conex = Modelo.Conexion.getInstance();
            MySqlConnection conexion = conex.obtenerConexion();

            // Debes implementar la lógica para obtener aviones con el estado especificado desde tu base de datos
            // Aquí un ejemplo de cómo podrías hacerlo (debes ajustarlo según tu conexión y esquema)
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


        private List<int> ObtenerTodosTrayectos()
        {
            List<int> trayectos = new List<int>();
            Modelo.Conexion conex = Modelo.Conexion.getInstance();
            MySqlConnection conexion = conex.obtenerConexion();
            // Debes implementar la lógica para obtener todos los trayectos desde tu base de datos
            // Aquí un ejemplo de cómo podrías hacerlo (debes ajustarlo según tu conexión y esquema)
            using (var cmd = new MySqlCommand("SELECT idTrayecto FROM trayectos", conexion))
            {
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        trayectos.Add(Convert.ToInt32(reader["idTrayecto"]));
                    }
                }
            }
            conex.cerrarConexion();

            return trayectos;
        }

    

        private void ActualizarCamposOrigenDestino(int idTrayecto)

        {
            Modelo.Conexion conex = Modelo.Conexion.getInstance();
            MySqlConnection conexion = conex.obtenerConexion();
            // Debes implementar la lógica para obtener origen y destino según el idTrayecto desde tu base de datos
            // Aquí un ejemplo de cómo podrías hacerlo (debes ajustarlo según tu conexión y esquema)
            using (var cmd = new MySqlCommand("SELECT origen, destino FROM trayectos WHERE idTrayecto = @idTrayecto", conexion))
            {
                cmd.Parameters.AddWithValue("@idTrayecto", idTrayecto);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        origen.Text = reader["origen"].ToString();
                        destino.Text = reader["destino"].ToString();
                    }
                }
            }
            conex.cerrarConexion();
        }

        private void limpiarcampos()
        {
            fecha.Text = "";
            hora_salida.Text = "";
            hora_llegada.Text = "";
            idAvion.Text = "";
            idTrayectos.Text = "";
            destino.Text = "";
            origen.Text = "";
        }



        private void idAvion_Copiar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (idTrayectos.SelectedItem != null)
            {
                int idTrayectoSeleccionado = (int)idTrayectos.SelectedItem;
                ActualizarCamposOrigenDestino(idTrayectoSeleccionado);
            }
        }




        private void origen_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}