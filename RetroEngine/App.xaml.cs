using RetroEngine.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace RetroEngine
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        double progress = 0;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var splashScreen = new SplatScreen();
            splashScreen.Show();
            var splashScreenVM = new ViewModels.SplashScreenViewModel();
            splashScreen.DataContext = splashScreenVM;
        
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += (sender, args) =>
            {
                if (splashScreenVM.Progress < 100)
                {
                    
                    
                    progress += 20;
                    splashScreenVM.SetProgress(progress);
                    
               }
                else
               {
                    timer.Stop();
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                    splashScreen.Close();

                }
            };
            timer.Start();

        }
    }
}

   

