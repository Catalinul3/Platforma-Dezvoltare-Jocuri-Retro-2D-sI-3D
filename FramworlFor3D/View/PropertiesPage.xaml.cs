﻿using FramworkFor3D.ViewModels;
using System;
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
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FramworkFor3D.View
{
    /// <summary>
    /// Interaction logic for PropertiesPage.xaml
    /// </summary>
    public partial class PropertiesPage : UserControl
    {
        public PropertiesPage()
        {
            InitializeComponent();
           
        }
        public void SetModel(ModelVisual3D model)
        {
            PropertiesVM page = new PropertiesVM(model);
            this.DataContext = page;

        }
    }
}
