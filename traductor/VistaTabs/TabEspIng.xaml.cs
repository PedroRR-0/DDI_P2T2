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
    public partial class EspIng : UserControl
    {
        LogicaDatos logica = LogicaDatos.getInstance();
        public EspIng() { 
            InitializeComponent();
        }
       
        
        

       public void traducir()
        {
            labelTradIng.Content = logica.TraducirEoI(false, campoEsp.Text);
        }

        private void btnEspIng_Click(object sender, RoutedEventArgs e)
        {
            traducir();
        }
    }
}
