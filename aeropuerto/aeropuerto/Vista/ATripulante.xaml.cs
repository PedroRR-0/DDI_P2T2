using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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
using System.Windows.Shapes;
using MySqlConnector;

namespace aeropuerto.Vista
{
    /// <summary>
    /// Lógica de interacción para ATripulante.xaml
    /// </summary>
    public partial class ATripulante : Window
    {
        private DataRowView filaSeleccionada;
        

        public ATripulante(Boolean modo, DataRowView filaSeleccionada = null)
        {
            InitializeComponent();
            
            if (modo)
            {
                limpiarCampos();
                editar.Visibility = Visibility.Hidden;
                aceptar.Visibility = Visibility.Visible;
            }
            else
            {
                this.filaSeleccionada = filaSeleccionada;
                aceptar.Visibility = Visibility.Hidden;
                editar.Visibility = Visibility.Visible;
                rellenarCampos(filaSeleccionada);
            }
    
        }

        private void rellenarCampos(DataRowView filaSeleccionada)
        {
            categoriaTB.SelectedItem = filaSeleccionada["categoria"].ToString();
            nombreTB.Text = filaSeleccionada["nombre"].ToString();
            apellidoTB.Text = filaSeleccionada["apellido1"].ToString();
            segundoApellidoTB.Text = filaSeleccionada["apellido2"].ToString();
            fechaNacTB.SelectedDate = Convert.ToDateTime(filaSeleccionada["fechaNacimiento"]);
            direccionTB.Text = filaSeleccionada["direccion"].ToString();
            correoTB.Text = filaSeleccionada["ecorreo"].ToString();
            telefonoTB.Text = filaSeleccionada["telefono"].ToString();
        }

        private void limpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
        }

       

        private void editar_Click(object sender, RoutedEventArgs e)
        {
            string categoria = categoriaTB.Text.ToLower();
            string nombre = nombreTB.Text;
            string apellido = apellidoTB.Text;
            string segundoApellido = segundoApellidoTB.Text;
            DateTime fechaNac = fechaNacTB.SelectedDate.Value;
            string direccion = direccionTB.Text;
                
            string correo = correoTB.Text;
            string telefono = telefonoTB.Text;

            int idTripulante = Convert.ToInt32(filaSeleccionada["idTripulacion"]);

            if (categoria != "" && nombre != "" && apellido != "" && segundoApellido != "" && direccion != "" && correo != "" && telefono != "")
            {
                try
                {
                    Modelo.Conexion conex = Modelo.Conexion.getInstance();
                    MySqlConnection conexion = conex.obtenerConexion();
                    string consulta = "UPDATE miembros SET categoria = @categoria, nombre = @nombre, apellido1 = @apellido1, apellido2 = @apellido2, fechaNacimiento = @fechaNacimiento, direccion = @direccion, ecorreo = @ecorreo, telefono = @telefono WHERE idTripulacion = @idTripulacion";
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@categoria", categoria);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@apellido1", apellido);
                        comando.Parameters.AddWithValue("@apellido2", segundoApellido);
                        comando.Parameters.AddWithValue("@fechaNacimiento", fechaNac);
                        comando.Parameters.AddWithValue("@direccion", direccion);
                        comando.Parameters.AddWithValue("@ecorreo", correo);
                        comando.Parameters.AddWithValue("@telefono", telefono);
                        comando.Parameters.AddWithValue("@idTripulacion", idTripulante);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Tripulante editado exitosamente.", "Editar Tripulante", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    conex.cerrarConexion();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al editar el tripulante: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, llena todos los campos.", "Editar Tripulante", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void aceptar_Click(object sender, RoutedEventArgs e)
        {
            if (categoriaTB.Text != "" && nombreTB.Text != "" && apellidoTB.Text != "" && segundoApellidoTB.Text != "" && fechaNacTB.SelectedDate != null && direccionTB.Text != "" && correoTB.Text != "" && telefonoTB.Text != "")
            {
                try
                {
                    string categoria = categoriaTB.Text.ToLower();
                    string nombre = nombreTB.Text;
                    string apellido = apellidoTB.Text;
                    string segundoApellido = segundoApellidoTB.Text;
                    DateTime fechaNac = fechaNacTB.SelectedDate.Value;
                    string direccion = direccionTB.Text;
                    string correo = correoTB.Text;
                    string telefono = telefonoTB.Text;

                    Modelo.Conexion conex = Modelo.Conexion.getInstance();
                    MySqlConnection conexion = conex.obtenerConexion();
                    string consulta = "INSERT INTO miembros (categoria, nombre, apellido1, apellido2, fechaNacimiento, direccion, ecorreo, telefono) VALUES (@categoria, @nombre, @apellido1, @apellido2, @fechaNacimiento, @direccion, @ecorreo, @telefono)";
                    using (MySqlCommand comando = new MySqlCommand(consulta, conexion))
                    {
                        comando.Parameters.AddWithValue("@categoria", categoria);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        comando.Parameters.AddWithValue("@apellido1", apellido);
                        comando.Parameters.AddWithValue("@apellido2", segundoApellido);
                        comando.Parameters.AddWithValue("@fechaNacimiento", fechaNac);
                        comando.Parameters.AddWithValue("@direccion", direccion);
                        comando.Parameters.AddWithValue("@ecorreo", correo);
                        comando.Parameters.AddWithValue("@telefono", telefono);

                        comando.ExecuteNonQuery();
                        MessageBox.Show("Tripulante añadido exitosamente.", "Añadir Tripulante", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    conex.cerrarConexion();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al añadir el tripulante: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Por favor, llena todos los campos.", "Añadir Tripulante", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void limpiarCampos()
        {
            categoriaTB.SelectedItem = -1;
            nombreTB.Text = "";
            apellidoTB.Text = "";
            segundoApellidoTB.Text = "";
            fechaNacTB.SelectedDate = null;
            direccionTB.Text = "";
            correoTB.Text = "";
            telefonoTB.Text = "";

        }

    }

}
