using aeropuerto.Modelo;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace aeropuerto
{
    /// <summary>
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class AEPasajeros : Window
    {
        private bool nuevo;
        private Conexion conex = Conexion.getInstance();
        private int id;
        public AEPasajeros(bool nuevo, int id)
        {
            InitializeComponent();
            rellenarVuelos();
            this.nuevo = nuevo;
            this.id = id;
            if (nuevo)
            {
                this.Title = "Añadir Pasajero";
            } else
            {
                this.Title = "Editar Pasajero";
                try
                {
                    MySqlConnection c = conex.obtenerConexion();

                    // Crear el comando SQL
                    string consulta = "select * from pasajeros where idPasajeros=@idp";
                    using (MySqlCommand comando = new MySqlCommand(consulta, c))
                    {
                        comando.Parameters.AddWithValue("@idp", id);
                        using (MySqlDataReader reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Acceder a los valores de las columnas por nombre o índice
                                campoNom.Text = reader["nombre"].ToString();
                                campoApe1.Text = reader["apellido1"].ToString();
                                campoApe2.Text = reader["apellido2"].ToString();
                                campoDir.Text = reader["direccion"].ToString();
                                campoDNI.Text = reader["dni"].ToString();
                                campoMail.Text = reader["ecorreo"].ToString();
                                campoTLF.Text = reader["telefono"].ToString();
                                //fechaPicker.SelectedDate = reader["fechaNacimiento"].ToString();
                                // Y así sucesivamente para todas las columnas necesarias
                            }
                            else
                            {
                                // Si no se encontraron resultados
                                Console.WriteLine("No se encontraron resultados para el id proporcionado.");
                            }
                        }

                    }
                    conex.cerrarConexion();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al insertar: " + ex.Message);
                }
            }
        }

        private void Foto_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Aceptar_Click(object sender, RoutedEventArgs e)
        {
            if(!camposLlenos())
            {
                string msg = "Rellene todos los campos";
                MessageBox.Show(msg, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (nuevo) {
                insertarPasaj();
                return;
            }
            modifPasaj();
            


        }

        private void Limpiar_Click(object sender, RoutedEventArgs e)
        {
            limpiarCampos();
        }

        private void limpiarCampos()
        {
            campoNom.Clear();
            campoApe1.Clear();
            campoApe2.Clear();
            campoMail.Clear();
            campoDNI.Clear();
            campoTLF.Clear();
            campoDir.Clear();
            fechaPicker.SelectedDate = null;
            fechaPicker.Text = string.Empty;
        }

        private void insertarPasaj()
        {
            try
            {
                MySqlConnection c = conex.obtenerConexion();

                // Crear el comando SQL
                string consulta = "insert into pasajeros (nombre, apellido1, apellido2, fechaNacimiento, ecorreo, telefono, direccion, dni) " +
                    "values (@nombre, @apellido1, @apellido2, @fechaNacimiento, @ecorreo, @telefono, @direccion, @dni)";
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.Parameters.AddWithValue("@nombre", campoNom.Text);
                    comando.Parameters.AddWithValue("@apellido1", campoApe1.Text);
                    comando.Parameters.AddWithValue("@apellido2", campoApe2.Text);
                    comando.Parameters.AddWithValue("@fechaNacimiento", fechaPicker.SelectedDate);
                    comando.Parameters.AddWithValue("@ecorreo", campoMail.Text);
                    comando.Parameters.AddWithValue("@telefono", campoTLF.Text);
                    comando.Parameters.AddWithValue("@direccion", campoDir.Text);
                    comando.Parameters.AddWithValue("@dni", campoDNI.Text);
                    comando.ExecuteNonQuery();

                }
                string consultaId = "SELECT LAST_INSERT_ID()";
                int idPasaj;
                using (MySqlCommand comandoId = new MySqlCommand(consultaId, c))
                {
                    // Ejecutar la consulta para obtener el ID asignado
                    idPasaj = Convert.ToInt32(comandoId.ExecuteScalar());

                }
                consulta = "select idAvion from vuelos where idVuelo = @idvuelo";
                string idAvion;
                string idVuelo = boxVuelos.SelectedItem as string;
                char idVuelochar = idVuelo[0];
                int idVuelo2 = (int)(idVuelochar - '0');
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.Parameters.AddWithValue("@idvuelo", idVuelo2);
                    idAvion = comando.ExecuteScalar().ToString();

                }
                consulta = "insert into pasajeros_vuelos values(@idvuelo,@idavion,@idpasajero)";
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.Parameters.AddWithValue("@idvuelo", Convert.ToInt32(idVuelo2));
                    comando.Parameters.AddWithValue("@idavion", idAvion);
                    comando.Parameters.AddWithValue("@idpasajero", idPasaj);
                    comando.ExecuteNonQuery();

                }
                conex.cerrarConexion();
                limpiarCampos();
                string msg = "Insertado correctamente";
                MessageBox.Show(msg, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message);
            }
        }

        private void modifPasaj()
        {
            try
            {
                MySqlConnection c = conex.obtenerConexion();

                // Crear el comando SQL
                string consulta = "update pasajeros set nombre = @nombre, apellido1 = @apellido1, apellido2 = @apellido2, " +
                    "fechaNacimiento = @fechaNacimiento, ecorreo = @ecorreo, telefono = @telefono, direccion = @direccion, dni = @dni " +
                    "where idPasajeros = @idpasajero";
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.Parameters.AddWithValue("@nombre", campoNom.Text);
                    comando.Parameters.AddWithValue("@apellido1", campoApe1.Text);
                    comando.Parameters.AddWithValue("@apellido2", campoApe2.Text);
                    comando.Parameters.AddWithValue("@fechaNacimiento", fechaPicker.SelectedDate);
                    comando.Parameters.AddWithValue("@ecorreo", campoMail.Text);
                    comando.Parameters.AddWithValue("@telefono", campoTLF.Text);
                    comando.Parameters.AddWithValue("@direccion", campoDir.Text);
                    comando.Parameters.AddWithValue("@dni", campoDNI.Text);
                    comando.Parameters.AddWithValue("@idpasajero", id);
                    comando.ExecuteNonQuery();

                }
                consulta = "select idAvion from vuelos where idVuelo = @idvuelo";
                string idAvion;
                string idVuelo = boxVuelos.SelectedItem as string;
                char idVuelochar = idVuelo[0];
                int idVuelo2 = (int)(idVuelochar - '0');
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.Parameters.AddWithValue("@idvuelo", idVuelo2);
                    idAvion = comando.ExecuteScalar().ToString();

                }
                consulta = "update pasajeros_vuelos set idVuelo = @idvuelo, idAvion = @idavion where idPasajeros = @idpasajero";
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    comando.Parameters.AddWithValue("@idvuelo", Convert.ToInt32(idVuelo2));
                    comando.Parameters.AddWithValue("@idavion", idAvion);
                    comando.Parameters.AddWithValue("@idpasajero", id);
                    comando.ExecuteNonQuery();

                }
                conex.cerrarConexion();
                string msg = "Editado correctamente";
                MessageBox.Show(msg, "Info", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al insertar: " + ex.Message);
            }
        }
        private void rellenarVuelos()
        {
            try
            {
                MySqlConnection c = conex.obtenerConexion();

                // Crear el comando SQL
                string consulta = "SELECT CONCAT(v.idVuelo,': ', t.origen,'-',t.destino) as vuelo from vuelos v JOIN trayectos t ON v.idTrayecto = t.idTrayecto";
                List<string> datos = new List<string>();
                using (MySqlCommand comando = new MySqlCommand(consulta, c))
                {
                    // Crear el adaptador de datos y llenar el lector
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        // Crea una lista para almacenar los datos

                        while (lector.Read())
                        {
                            datos.Add(lector.GetString("vuelo"));
                        }
                        lector.Close();
                    }
                }
                conex.cerrarConexion();
                boxVuelos.ItemsSource = datos;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar datos desde la base de datos: " + ex.Message);
            }
        }

        private bool camposLlenos()
        {
            return !string.IsNullOrWhiteSpace(campoNom.Text)
                   && !string.IsNullOrWhiteSpace(campoApe1.Text)
                   && !string.IsNullOrWhiteSpace(campoApe2.Text)
                   && !string.IsNullOrWhiteSpace(campoMail.Text)
                   && !string.IsNullOrWhiteSpace(campoDNI.Text)
                   && !string.IsNullOrWhiteSpace(campoTLF.Text)
                   && fechaPicker.SelectedDate != null;
        }
    }
}
