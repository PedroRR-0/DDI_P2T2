using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class Juego : UserControl
    {
        private int contador;
        private int aciertos;
        private int fallos;
        private LogicaDatos logica;
        private KeyValuePair<string, string> palabra;

        public Juego() { 
            InitializeComponent();
            logica = LogicaDatos.getInstance();
            EmpezarJuego();
        }

        public void EmpezarJuego()
        {
            campoRespJuego.Clear();
            contador = 0;
            aciertos = 0;
            fallos = 0;
            setLetreros();
            palabra = palabraAleat(logica.getLista());
            palabraSelecJuego.Content = palabra.Key;
        }

        public void setLetreros()
        {
            labelProgresoJuego.Content = contador+"/10";
            labelAciertos.Content = aciertos;
            labelFallos.Content = fallos;
        }

        public KeyValuePair<string,string> palabraAleat(ObservableCollection<KeyValuePair<string, string>> c)
        {
            try
            {
                Random random = new Random();
                int indiceAleatorio = random.Next(0, c.Count);
                return c[indiceAleatorio];
            } catch(ArgumentOutOfRangeException e)
            {
                return new KeyValuePair<string, string>("No hay palabras","");
            }
        }

        private void btnSigJuego_Click(object sender, RoutedEventArgs e)
        {
            if(contador < 10) { escribirPalabraAleat(); }

        }

        private void escribirPalabraAleat()
        {

            if (campoRespJuego.Text.ToLower().Equals(logica.TraducirEoI(true, palabra.Key).ToLower())){
                aciertos++;

            } else
            {
                fallos++;
            }
            palabra = palabraAleat(logica.getLista());
            palabraSelecJuego.Content = palabra.Key;
            contador++;
            campoRespJuego.Clear();
            setLetreros();
        }

        private void btnEmpezar_Click(object sender, RoutedEventArgs e)
        {
            EmpezarJuego();
        }
    }
}
