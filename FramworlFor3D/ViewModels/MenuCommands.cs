using FramworkFor3D.helpers;
using FramworlFor3D.helpers;

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
        private RelayCommands3D _loadObjectCommand;
        public RelayCommands3D LoadObjectCommand
        {
            get
            {
                if (_loadObjectCommand == null)
                    _loadObjectCommand = new RelayCommands3D(Load);
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

