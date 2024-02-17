using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroEngine.ViewModels
{
    public class BaseVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static string LoadFileDialog(string title)
        {
            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Title = title,
                Filter = "3d files (*.stl)| *stl"
            };

            if (fileDialog.ShowDialog() == false || fileDialog.FileName.CompareTo("") == 0)
                return null;

            return fileDialog.FileName;
        }
    }
}
