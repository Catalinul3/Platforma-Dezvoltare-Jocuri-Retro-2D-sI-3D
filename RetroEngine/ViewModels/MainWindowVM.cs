using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RetroEngine.ViewModels
{
    public class MainWindowVM:BaseVM
    {
        private string _searchBar;
        public string SearchBar{
            get { return _searchBar; }
            set
            {
                _searchBar = value;
                NotifyPropertyChanged("SearchBar");
            }
        }
        public MainWindowVM()
        {
            _searchBar = "../images/find.png";
        }
    }
}
