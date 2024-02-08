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
using traductor.Modelo;

namespace traductor
{
    public partial class IngEsp : UserControl
    {
        // Se obtiene la instancia de la clase LogicaDatos.
        LogicaDatos logica = LogicaDatos.getInstance();

        // Constructor de la clase IngEsp.
        public IngEsp()
        {
            InitializeComponent();
        }

        // Método para realizar la traducción de inglés a español.
        public void traducir()
        {
            // Se llama al método TraducirEoI de la clase LogicaDatos, pasando 'true' como primer argumento para indicar que se quiere traducir de inglés a español.
            // El resultado de la traducción se muestra en el labelTradEsp.
            labelTradEsp.Content = logica.TraducirEoI(true, campoIng.Text);
        }

        // Método que se ejecuta cuando se hace clic en el botón Button_Click.
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Cuando se hace clic en el botón, se llama al método traducir.
            traducir();
        }
    }
}
