using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using traductor.Modelo;

namespace traductor
{
    public partial class Listado : UserControl
    {
        // Se obtiene la instancia de la clase LogicaDatos.
        private LogicaDatos logica;

        // Constructor de la clase Listado.
        public Listado()
        {
            InitializeComponent();
            logica = LogicaDatos.getInstance();

            // Se establece el origen de los datos del control listaPalabras a la lista de palabras de la clase LogicaDatos.
            // Las palabras se ordenan por la palabra en inglés.
            listaPalabras.ItemsSource = logica.getLista().OrderBy(pair => pair.Key);
        }

        // Método que se ejecuta cuando se hace clic en el botón btnAnadListado.
        private void btnAnadListado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se obtienen las palabras ingresadas por el usuario en los campos de texto campoIngListado y campoEspListado.
                string ing = campoIngListado.Text.Trim();
                ing = char.ToUpper(ing[0]) + ing.Substring(1);
                string esp = campoEspListado.Text.Trim();
                esp = char.ToUpper(esp[0]) + esp.Substring(1);

                // Se verifica que las palabras ingresadas solo contengan letras y espacios y guión.
                string pattern = @"^[\p{L}\s-]+$";
                bool matchIng = Regex.IsMatch(ing, pattern);
                bool matchEsp = Regex.IsMatch(esp, pattern);

                // Si las palabras ingresadas contienen caracteres no permitidos, se muestra un mensaje de error y se termina el método.
                if (!(matchIng && matchEsp))
                {
                    MessageBox.Show("¡Las palabras no llevan números ni símbolos raros!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Se añade la nueva palabra y su traducción a la lista de palabras de la clase LogicaDatos.
                logica.anadirPalabra(ing, esp);

                // Se limpian los campos de texto.
                campoIngListado.Clear();
                campoEspListado.Clear();

                // Se actualiza la lista de palabras.
                refresh();
            }
            catch (IndexOutOfRangeException ex)
            {
                // Si los campos de texto están vacíos, se muestra un mensaje de error.
                MessageBox.Show("Rellena los campos Inglés y Español", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        // Método que se ejecuta cuando se hace clic en el botón btnElimListado.
        private void btnElimListado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Se obtiene la palabra seleccionada en la lista de palabras.
                KeyValuePair<string, string> palabraSeleccionada = (KeyValuePair<string, string>)listaPalabras.SelectedItem;

                // Se elimina la palabra seleccionada de la lista de palabras de la clase LogicaDatos.
                logica.borrarPalabra(palabraSeleccionada);

                // Se actualiza la lista de palabras.
                refresh();

            }
            catch (NullReferenceException ex)
            {
                // Si no se ha seleccionado ninguna palabra en la lista, se muestra un mensaje de error.
                MessageBox.Show("Selecciona un elemento de la lista", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        // Método para actualizar la lista de palabras.
        private void refresh()
        {
            // Se establece el origen de los datos del control listaPalabras a la lista de palabras de la clase LogicaDatos.
            // Las palabras se ordenan por la palabra en inglés.
            listaPalabras.ItemsSource = logica.getLista().OrderBy(pair => pair.Key);
        }


    }
}
