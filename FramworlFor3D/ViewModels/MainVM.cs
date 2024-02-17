using RetroEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworlFor3D.ViewModels
{
    public class MainVM:BaseVM
    {public MenuCommands menuCommand { get; set; }
      public   MainVM()
        {
            menuCommand = new MenuCommands(this);
        }
    }
}
