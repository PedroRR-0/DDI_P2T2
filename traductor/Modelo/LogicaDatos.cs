using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traductor.Modelo
{
    public class LogicaDatos
    {
        // Colección observable de pares clave-valor para almacenar palabras en inglés y español.
        private ObservableCollection<KeyValuePair<string, string>> palabras = [];

        // Instancia estática de la clase LogicaDatos para implementar el patrón Singleton.
        private static LogicaDatos? instancia;

        // Constructor privado que inicializa la colección de palabras con algunas traducciones predefinidas.
        private LogicaDatos()
        {
            palabras.Add(new KeyValuePair<string, string>("Cat", "Gato"));
            palabras.Add(new KeyValuePair<string, string>("Dog", "Perro"));
            palabras.Add(new KeyValuePair<string, string>("Giraffe", "Girafa"));
            palabras.Add(new KeyValuePair<string, string>("Tyrant", "Tirano"));
            palabras.Add(new KeyValuePair<string, string>("Thwart", "Frustrar"));
            palabras.Add(new KeyValuePair<string, string>("Instance", "Instancia"));
            palabras.Add(new KeyValuePair<string, string>("Night", "Noche"));
            palabras.Add(new KeyValuePair<string, string>("Path", "Camino"));
            palabras.Add(new KeyValuePair<string, string>("Warning", "Aviso"));
            palabras.Add(new KeyValuePair<string, string>("Information", "Información"));
            palabras.Add(new KeyValuePair<string, string>("Progress", "Progreso"));
        }

        // Método para añadir una nueva palabra y su traducción a la colección 'palabras'.
        public void anadirPalabra(string palabraIng, string palabraEsp)
        {
            palabras.Add(new KeyValuePair<string, string>(palabraIng, palabraEsp));
        }

        // Método para eliminar una palabra y su traducción de la colección 'palabras'.
        public void borrarPalabra(KeyValuePair<string, string> palabraSeleccionada)
        {
            palabras.Remove(palabraSeleccionada);
        }

        // Método que implementa el patrón Singleton para asegurar que solo existe una instancia de la clase LogicaDatos.
        public static LogicaDatos getInstance()
        {
            instancia ??= new LogicaDatos();
            return instancia;
        }

        // Método que devuelve la colección 'palabras'.
        public ObservableCollection<KeyValuePair<string, string>> getLista()
        {
            return palabras;
        }

        // Método que realiza la traducción de una palabra. Si el parámetro 'traducir' es verdadero, busca la palabra en inglés y devuelve su traducción al español. Si es falso, hace lo contrario. Si la palabra no se encuentra en la colección, devuelve el mensaje "Palabra no encontrada".
        public String TraducirEoI(bool traducir, String palabraTra)
        {
            if (traducir)
            {
                foreach (KeyValuePair<string, string> palabra in palabras)
                {
                    if (palabra.Key.ToLower() == palabraTra.ToLower())
                    {
                        return palabra.Value;
                    }

                }
            }
            else
            {
                foreach (KeyValuePair<string, string> palabra in palabras)
                {
                    if (palabra.Value.ToLower() == palabraTra.ToLower())
                    {
                        return palabra.Key;
                    }
                }
            }
            return "Palabra no encontrada";
        }
    }
}
