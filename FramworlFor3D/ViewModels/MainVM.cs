using FramworkFor3D.Commands;
using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using FramworkFor3D.ViewModels.TreeViewVM;
using FramworlFor3D.helpers;
using RetroEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace FramworlFor3D.ViewModels
{
    public class MainVM:BaseVM
    {
        #region Variables
        public MenuCommands menuCommand { get; set; }
        public GameObjectCommands gameObjCommands { get; set; }
     public ObservableCollection<ComponentsVM> treeViewComponentsVM { get; set; }
        public ObservableCollection<ModelVisual3D> grid { get; set; }
      public GeometryModel3D geometry3D { get; set; }
        public ObservableCollection<ComponentsVM> components { get; set; }
     #endregion

        public MainVM()
        {
            Scenes scenes = new Scenes();
            grid = new ObservableCollection<ModelVisual3D>();
            List<ModelVisual3D> root = scenes.getGrid();
            SetCamera();
          
            foreach (var item in root)
            {
                grid.Add(item);
            }
            menuCommand = new MenuCommands(this);
            

            gameObjCommands = new GameObjectCommands(this);
            treeViewComponentsVM = new ObservableCollection<ComponentsVM>();
            components = new ObservableCollection<ComponentsVM>();
            var camera = new ComponentsVM
            {
                ComponentName = "Camera",
                ComponentType = ComponentsType.CAMERA,
                
            };
            camera.ComponentImage= setImageOfComponents(camera.ComponentType);
            var scene=new ComponentsVM
            {
                ComponentName = "Scene",
                ComponentType = ComponentsType.SCENE,
            };
            scene.ComponentImage = setImageOfComponents(scene.ComponentType);
           treeViewComponentsVM.Add(camera);
            treeViewComponentsVM.Add(scene);
          
            //components.Add(camera);
            //components.Add(scene);
            
            
        }
        #region Camera
      
        private PerspectiveCamera _camera;
        public PerspectiveCamera Camera
        {
            get { return _camera; }
            set
            {
                _camera = value;
                NotifyPropertyChanged(nameof(Camera));
            }
        }
        public void SetCamera()
        {
            Camera = new PerspectiveCamera(new Point3D(1, 0.5, 2), new Vector3D(0, 0, -1), new Vector3D(0, 1, 0), 45);
        }
        private RelayCommand rotateMouseCommand { get; set; }
        private RelayCommand zoomMouseCommand { get; set; }
        public RelayCommand RotateCommand
        {
            get
            {
                if (rotateMouseCommand == null)
                {
                    rotateMouseCommand = new RelayCommand(Rotate);
                }
                return rotateMouseCommand;
            }
        }

        //metode
        private void Rotate(object parameter)
        {
            if (parameter is Key key)
            {
                Camera = ProjectionCameraEX.RotateBy(Camera, key, 0.1);
            }
        }

    
    #endregion

    private string setImageOfComponents(ComponentsType type)
        {

            if (type.Equals(ComponentsType.CAMERA))
            {
                return "../Resources/camera.png";
            }
            if (type.Equals(ComponentsType.OBJECT_3D))
            {
                return "../Resources/object.png";
            }
            if (type.Equals(ComponentsType.SCENE))
            {
                return "../Resources/scene.png";
            }
            return "";
        }

        private object selectedItem;
        public object SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }
    }
}
