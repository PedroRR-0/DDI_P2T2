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
        private LogicaDatos logica;
        
        public Listado()
        {
            InitializeComponent();
            logica = LogicaDatos.getInstance();

            listaPalabras.ItemsSource = logica.getLista().OrderBy(pair => pair.Key);
        }

        private void btnAnadListado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string ing = campoIngListado.Text.Trim();
                ing = char.ToUpper(ing[0]) + ing.Substring(1);
                string esp = campoEspListado.Text.Trim();
                esp = char.ToUpper(esp[0]) + esp.Substring(1);
                string pattern = @"^[\p{L}\s-]+$";
                bool matchIng = Regex.IsMatch(ing, pattern);
                bool matchEsp = Regex.IsMatch(esp, pattern);
                if(!(matchIng && matchEsp))
                {
                    MessageBox.Show("¡Las palabras no llevan números ni símbolos raros!", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                logica.anadirPalabra(ing, esp);
                campoIngListado.Clear();
                campoEspListado.Clear();
                refresh();
            } catch(IndexOutOfRangeException ex)
            {
                MessageBox.Show("Rellena los campos Inglés y Español", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
                        
        }

        private void btnElimListado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                KeyValuePair<string, string> palabraSeleccionada = (KeyValuePair<string, string>)listaPalabras.SelectedItem;
                logica.borrarPalabra(palabraSeleccionada);
                refresh();

            } catch(NullReferenceException ex)
            {
                MessageBox.Show("Selecciona un elemento de la lista", "Warning", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }

        private void refresh()
        {
            listaPalabras.ItemsSource = logica.getLista().OrderBy(pair => pair.Key);
        }

        
    }
}
