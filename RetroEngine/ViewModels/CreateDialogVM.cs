using RetroEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RetroEngine.ViewModels
{
    public class CreateDialogVM:BaseVM
    {
        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }
        private RelayCommand _createBtn;
        public RelayCommand CreateBtn
        {
            get
            {
                if (_createBtn == null)
                    _createBtn = new RelayCommand(Create);
                return _createBtn;
            }
        }
        public CreateDialogVM()
        {
            Title = "";
        }
        private void Create(object parameter)
        {
            var frameWorkWindow = new FramworlFor3D.MainWindow
            {
                Title = _title
            };
            var currentWindow = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            if (currentWindow != null)
            {
               currentWindow.Close();
                frameWorkWindow.Show();
            }
            
            
        }
      

    }
}
