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
        // Variables para llevar la cuenta del progreso del juego.
        private int contador;
        private int aciertos;
        private int fallos;

        // Se obtiene la instancia de la clase LogicaDatos.
        private LogicaDatos logica;

        // Variable para almacenar la palabra actual del juego.
        private KeyValuePair<string, string> palabra;

        // Lista para almacenar las palabras que ya han sido usadas en el juego.
        private List<string> palabrasUsadas = new List<string>();

        // Constructor de la clase Juego.
        public Juego()
        {
            InitializeComponent();
            logica = LogicaDatos.getInstance();
            EmpezarJuego(); // Inicia el juego al crear una instancia de la clase Juego.
        }

        // Método para iniciar el juego.
        public void EmpezarJuego()
        {
            campoRespJuego.Clear(); // Limpia el campo de respuesta del juego.
            campoRespJuego.IsEnabled = true; // Habilita el campo de respuesta del juego.
            contador = 0; // Inicializa el contador a 0.
            aciertos = 0; // Inicializa los aciertos a 0.
            fallos = 0; // Inicializa los fallos a 0.
            setLetreros(); // Actualiza los letreros del juego.
            palabra = palabraAleat(logica.getLista()); // Obtiene una palabra aleatoria de la lista de palabras.
            palabraSelecJuego.Content = palabra.Key; // Muestra la palabra en inglés en el letrero de la palabra seleccionada.
        }

        // Método para actualizar los letreros del juego.
        public void setLetreros()
        {
            labelProgresoJuego.Content = contador + "/10"; // Muestra el progreso del juego.
            labelAciertos.Content = aciertos; // Muestra el número de aciertos.
            labelFallos.Content = fallos; // Muestra el número de fallos.
        }

        // Método para obtener una palabra aleatoria de la lista de palabras.
        public KeyValuePair<string, string> palabraAleat(ObservableCollection<KeyValuePair<string, string>> c)
        {
            try
            {
                Random random = new Random(); // Crea una nueva instancia de la clase Random.
                int indiceAleatorio = random.Next(0, c.Count); // Obtiene un índice aleatorio.
                return c[indiceAleatorio]; // Devuelve la palabra en el índice aleatorio.
            }
            catch (ArgumentOutOfRangeException e)
            {
                return new KeyValuePair<string, string>("No hay palabras", ""); // Devuelve un par vacío si no hay palabras en la lista.
            }
        }

        // Método que se ejecuta cuando se hace clic en el botón btnSigJuego.
        private void btnSigJuego_Click(object sender, RoutedEventArgs e)
        {
            if (contador < 10)
            {
                escribirPalabraAleat(); // Escribe una palabra aleatoria si el contador es menor que 10.
            }

        }

        // Método para escribir una palabra aleatoria.
        private void escribirPalabraAleat()
        {
            // Comprueba si la respuesta del usuario es correcta.
            if (campoRespJuego.Text.ToLower().Equals(logica.TraducirEoI(true, palabra.Key).ToLower()))
            {
                aciertos++; // Incrementa los aciertos si la respuesta es correcta.
            }
            else
            {
                fallos++; // Incrementa los fallos si la respuesta es incorrecta.
            }
            if (contador < 9)
            {
                palabra = palabraAleat(logica.getLista()); // Obtiene una nueva palabra aleatoria.
                // Comprueba si la palabra ya ha sido usada.
                while (palabrasUsadas.Contains(palabra.Key))
                { 
                    palabra = palabraAleat(logica.getLista()); // Obtiene una nueva palabra si la palabra ya ha sido usada.
                }
                palabraSelecJuego.Content = palabra.Key; // Muestra la nueva palabra en el letrero de la palabra seleccionada.
                palabrasUsadas.Add(palabra.Key); // Añade la palabra a la lista de palabras usadas.
            }
            else
            {
                palabrasUsadas.Clear(); // Limpia la lista de palabras usadas.
                palabraSelecJuego.Content = "Juego terminado"; // Muestra "Juego terminado" si el contador es 9.
                campoRespJuego.IsEnabled = false; // Deshabilita el campo de respuesta del juego.
            }
            contador++; // Incrementa el contador.
            campoRespJuego.Clear(); // Limpia el campo de respuesta del juego.
            setLetreros(); // Actualiza los letreros del juego.
        }

        // Método que se ejecuta cuando se hace clic en el botón btnEmpezar.
        private void btnEmpezar_Click(object sender, RoutedEventArgs e)
        {
            EmpezarJuego(); // Inicia el juego.
        }
    }
}
