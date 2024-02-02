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

        public void anadirPalabra(string palabraIng, string palabraEsp)
        {
            palabras.Add(new KeyValuePair<string, string>(palabraIng, palabraEsp));
        }

        public static LogicaDatos getInstance()
        {
            instancia??=new LogicaDatos();
            return instancia;
        }
        public ObservableCollection<KeyValuePair<string, string>> getLista() {
            return palabras;
                }

    }
}
