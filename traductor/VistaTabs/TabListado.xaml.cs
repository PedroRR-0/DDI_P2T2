﻿using System;
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
    public partial class Listado : UserControl
    {
        private LogicaDatos logica;
        
        public Listado()
        {
            InitializeComponent();
            logica = LogicaDatos.getInstance();

            listaPalabras.ItemsSource = logica.getLista().OrderBy(pair => pair.Key);
        }

        private void btnAnadListado_Click(object sender, RoutedEventArgs e)
        {
            string ing = campoIngListado.Text.Trim();
            ing = char.ToUpper(ing[0])+ing.Substring(1);
            string esp = campoEspListado.Text.Trim();
            esp = char.ToUpper(esp[0]) + esp.Substring(1);
            logica.anadirPalabra(ing, esp);
            campoIngListado.Clear();
            campoEspListado.Clear();
            refresh();
                        
        }

        private void btnElimListado_Click(object sender, RoutedEventArgs e)
        {
            KeyValuePair<string, string> palabraSeleccionada = (KeyValuePair<string, string>)listaPalabras.SelectedItem;

            logica.borrarPalabra(palabraSeleccionada);
            refresh();
            
        }

        private void refresh()
        {
            listaPalabras.ItemsSource = logica.getLista().OrderBy(pair => pair.Key);
        }

        
    }
}
