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

        public bool anadirPalabra(string palabraIng, string palabraEsp)
        {
            palabras.Add(new KeyValuePair<string, string>(palabraIng, palabraEsp));
            bool correcto = true;
            return correcto;
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
                    if (palabra.Key == palabraTra)
                    {
                        return palabra.Value;
                    }

                }
            }
            else
            {
                foreach (KeyValuePair<string, string> palabra in palabras)
                {
                    if (palabra.Value == palabraTra)
                    {
                        return palabra.Key;
                    }
                }
            }
            return "No se ha encontrado la palabra";
            }

        }
    }




