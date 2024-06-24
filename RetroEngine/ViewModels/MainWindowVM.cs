using RetroEngine.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace RetroEngine.ViewModels
{
    public class MainWindowVM : BaseVM
    {
        private CommandClass _buttons;
        private RelayCommand _create;
        private RelayCommand _exit;
        private bool Create, Leave;

        private ImageSource _imageState;
     
    
        
        public RelayCommand create
        {
            get
            {
                if (_create == null)
                    _create = _buttons.createGame;
            
                return _create;
            }

        }
        public RelayCommand exit
        {
            get
            {
                if (_exit == null)
                    _exit = _buttons.exitGame;
             
                return _exit;
            }
        }
        public MainWindowVM()
        {
            _buttons = new CommandClass();
           
        }
    }
}
