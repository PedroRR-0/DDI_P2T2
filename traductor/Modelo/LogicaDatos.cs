using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace traductor.Modelo
{
    public class LogicaDatos
    {
        private ConcurrentDictionary<string, string> parejaPalabra = new ConcurrentDictionary<string, string>();
        private static LogicaDatos? instancia;

        public bool anadirPalabra(string palabraIng, string palabraEsp)
        {
            bool correcto = parejaPalabra.TryAdd(palabraIng, palabraEsp);
            return correcto;
        }

        public static LogicaDatos getInstance()
        {
            instancia??=new LogicaDatos();
            return instancia;
        }
        
    }
}
