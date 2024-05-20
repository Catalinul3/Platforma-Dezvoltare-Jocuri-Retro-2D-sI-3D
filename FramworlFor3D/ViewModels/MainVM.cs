using BulletSharp;
using FramworkFor3D.Commands;
using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using FramworkFor3D.ViewModels.TreeViewVM;
using FramworlFor3D.helpers;
using MDriven.WPF.Media3D;
using RetroEngine.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private Rectangle margins;
        public  Rectangle Margins
        {
            get { return margins; }
            set
            {
                margins = value;
                NotifyPropertyChanged(nameof(Margins));
            }
        }
        #endregion

        #region Constructors
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
        #endregion

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
            Camera = new PerspectiveCamera(new Point3D(2, 1.2, 2), new Vector3D(0, 0, -1), new Vector3D(0, 0.3, 0), 45);
        }
        public void UpdateCamera(MatrixTransform3D updateConfig)
        {
            Camera.Transform = updateConfig;
        }



        #endregion

        #region Helpers
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
        #endregion

        #region Physics
        

       

        
        #endregion
    }
}
