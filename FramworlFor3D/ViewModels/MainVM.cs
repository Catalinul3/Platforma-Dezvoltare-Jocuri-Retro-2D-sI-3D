using FramworkFor3D.Commands;
using FramworkFor3D.helpers;
using FramworkFor3D.ViewModels.TreeViewVM;
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

namespace FramworlFor3D.ViewModels
{
    public class MainVM:BaseVM
    {public MenuCommands menuCommand { get; set; }
        public GameObjectCommands gameObjCommands { get; set; }
     public ObservableCollection<ComponentsVM> treeViewComponentsVM { get; set; }
        public ObservableCollection<ComponentsVM> components { get; set; }
      public  MainVM()
        {
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
