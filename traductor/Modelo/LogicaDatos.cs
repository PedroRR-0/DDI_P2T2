﻿using System;
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



