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
        private ObservableCollection<KeyValuePair<string, string>> palabras = [];
        private static LogicaDatos? instancia;

        private LogicaDatos() {
            palabras.Add(new KeyValuePair<string, string>("Cat", "Gato"));
            palabras.Add(new KeyValuePair<string, string>("Dog", "Perro"));
            palabras.Add(new KeyValuePair<string, string>("Giraffe", "Girafa"));
            palabras.Add(new KeyValuePair<string, string>("Tyrant", "Tirano"));
            palabras.Add(new KeyValuePair<string, string>("Thwart", "Frustrar"));
            palabras.Add(new KeyValuePair<string, string>("Instance", "Instancia"));
            palabras.Add(new KeyValuePair<string, string>("Night", "Noche"));
            palabras.Add(new KeyValuePair<string, string>("Path", "Camino"));
            palabras.Add(new KeyValuePair<string, string>("Warning", "Aviso"));
        }

        public void anadirPalabra(string palabraIng, string palabraEsp)
        {
            palabras.Add(new KeyValuePair<string, string>(palabraIng, palabraEsp));
           
        }


        public void borrarPalabra(KeyValuePair<string, string> palabraSeleccionada)
        {
            palabras.Remove(palabraSeleccionada);
        }


        public static LogicaDatos getInstance()
        {
            instancia??=new LogicaDatos();
            return instancia;
        }

        public ObservableCollection<KeyValuePair<string, string>> getLista() {
            return palabras;
        }



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




