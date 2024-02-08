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
    public partial class EspIng : UserControl
    {
        // Se obtiene la instancia de la clase LogicaDatos.
        LogicaDatos logica = LogicaDatos.getInstance();

        // Constructor de la clase EspIng.
        public EspIng()
        {
            InitializeComponent();
        }

        // Método para realizar la traducción de español a inglés.
        public void traducir()
        {
            // Se llama al método TraducirEoI de la clase LogicaDatos, pasando 'false' como primer argumento para indicar que se quiere traducir de español a inglés.
            // El resultado de la traducción se muestra en el labelTradIng.
            labelTradIng.Content = logica.TraducirEoI(false, campoEsp.Text);
        }

        // Método que se ejecuta cuando se hace clic en el botón btnEspIng.
        private void btnEspIng_Click(object sender, RoutedEventArgs e)
        {
            // Cuando se hace clic en el botón, se llama al método traducir.
            traducir();
        }
    }
}

