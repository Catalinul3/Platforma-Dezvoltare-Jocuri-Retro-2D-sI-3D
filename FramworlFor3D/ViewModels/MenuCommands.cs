using FramworlFor3D.helpers;
using RetroEngine.Helpers;
using RetroEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FramworlFor3D.ViewModels
{
   public class MenuCommands:BaseVM
    {
        private readonly MainVM _mainVM;
        public MenuCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
        }
        private RelayCommand _loadObjectCommand;
        public RelayCommand LoadObjectCommand
        {
            get
            {
                if (_loadObjectCommand == null)
                    _loadObjectCommand = new RelayCommand(Load);
                return _loadObjectCommand;
            }
        }

        private void Load(object parameter)
        {


            string fileName = LoadFileDialog("Select a 3d model");
            if (fileName != null)
            {
                MessageBox.Show("succes");
            }
        }
    }
}

