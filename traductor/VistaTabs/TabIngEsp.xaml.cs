﻿using System;
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
    public partial class IngEsp : UserControl
    {
        LogicaDatos logica = LogicaDatos.getInstance();
        public IngEsp() { 
            InitializeComponent();
        }


        public void traducir()
        {
            labelTradEsp.Content = logica.TraducirEoI(true, campoIng.Text);
        }

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            traducir();

        }
    }
}
