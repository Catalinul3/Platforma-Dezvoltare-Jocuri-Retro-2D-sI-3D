using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using HelixToolkit.Wpf;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FramworkFor3D.ViewModels.TreeViewVM
{
   public class ComponentsVM:BaseVM
    {
        private String? _componentName;
        private ComponentsType _componentType;
        private String? _componentImage;
        private CubeVisual3D _cube;
        
        private ObservableCollection<ComponentsVM> _components;
        public ObservableCollection<ComponentsVM> Components
        {
            get { return _components; }
            set
            {
                _components = value;
                NotifyPropertyChanged("Components");
            }
        }
        public String? ComponentName
        {
            get { return _componentName; }
            set
            {
                _componentName = value;
                NotifyPropertyChanged("ComponentName");
            }
        }
        public ComponentsType ComponentType
        {
            get { return _componentType; }
            set
            {
                _componentType = value;
                NotifyPropertyChanged("ComponentType");
            }
        }
        public String? ComponentImage
        {
            get { return _componentImage; }
            set
            {
                _componentImage = value;
                NotifyPropertyChanged("ComponentImage");
            }
        }
        public CubeVisual3D Cube
        {
            get { return _cube; }
            set
            {
                _cube = value;
                NotifyPropertyChanged(nameof(Cube));
            }
        }
        public ComponentsVM()
        {
            _components = new ObservableCollection<ComponentsVM>();
            
            //_componentName = "Camera";
            //_componentType = ComponentsType.CAMERA;
            //_componentImage = "../Resources/camera.png";
           
        }
     
    }
}
