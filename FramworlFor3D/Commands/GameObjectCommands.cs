using BulletSharp.SoftBody;
using BulletSharp;
using Eco.UmlRt;
using FramworkFor3D._3DObjects;
using FramworkFor3D._3DPhysics;
using FramworkFor3D.helpers;
using FramworkFor3D.Models;
using FramworlFor3D.helpers;
using FramworlFor3D.ViewModels;
using RetroEngine.ViewModels;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;
using System.Windows.Shapes;
using static System.Formats.Asn1.AsnWriter;
using System.Windows.Threading;


namespace FramworkFor3D.Commands
{
    public class GameObjectCommands : BaseVM
    {
        private readonly MainVM _mainVM;

        private DiscreteDynamicsWorld _world;
        private DispatcherTimer _renderTimer;
        public GameObjectCommands(MainVM mainVM)
        {
            _mainVM = mainVM;


        }
        private RelayCommand _add3DCube;
        private RelayCommand _add3DSphere;
        private RelayCommand _add3DOther;
        private Irregular3DObject obj;


        Cube3D cube;
        Sphere3D sphere;
        UIElement3D cubeInteractive, sphereInteractive, objInteractive;
        DispatcherTimer timer;
        Rect3D cubeBounds,sphereBounds, objBounds;

        #region Create Models 
        public RelayCommand add3DCube
        {
            get
            {
                if (_add3DCube == null)
                    _add3DCube = new RelayCommand(addCube);
                return _add3DCube;
            }
        }
        private void addCube(object parameter)
        {
            if (parameter is Viewport3D environment)
            {
                cube = new Cube3D
                {
                    color = Brushes.Red,
                };
                

                cubeInteractive = InteractiveHelper.ConvertToUI(cube);
                environment.Children.Add(cubeInteractive);

                TranslateTransform3D center = new TranslateTransform3D(1, 2.4, 1.3);
                cube.Transform = center;
                cubeBounds = Collider.UpdateBounds(cube, center);
                
               // MessageBox.Show("X = " + cube.Content.Bounds.SizeX + " y = " + cube.Content.Bounds.Y + " Z = " + cube.Content.Bounds.Z);

                cube.position = new Vector3D(center.OffsetX, center.OffsetY, center.OffsetZ);
                cubeInteractive.MouseRightButtonDown += (s, e) => CubePressed(s, e, environment);
                cubeInteractive.Transform = center;
            }
        }
        public RelayCommand add3DSphere
        {
            get
            {
                if (_add3DSphere == null)
                    _add3DSphere = new RelayCommand(addSphere);
                return _add3DSphere;
            }
        }
        private void addSphere(object parameter)
        {
            if (parameter is Viewport3D environment)
            {

                sphere = new Sphere3D();
                sphereInteractive = InteractiveHelper.ConvertToUI(sphere);

                environment.Children.Add(sphereInteractive);
                TranslateTransform3D center = new TranslateTransform3D(3, 2.4, 1.3);
                sphereBounds = Collider.UpdateBounds(sphere, center);
                sphere.Transform = center;
                //MessageBox.Show("X = " + sphere.Content.Bounds.X + " y = " + sphere.Content.Bounds.Y + " Z = " + sphere.Content.Bounds.Z);
               // MessageBox.Show("X = " + sphere.Transform.Value.OffsetX + " y = " + sphere.Transform.Value.OffsetY + " Z = " + sphere.Transform.Value.OffsetZ);
                sphereInteractive.MouseRightButtonDown += (s, e) => SpherePressed(s, e, environment);
                sphereInteractive.Transform = center;
            }
        }
        public RelayCommand add3DOther
        {
            get
            {
                if (_add3DOther == null)
                    _add3DOther = new RelayCommand(addOther);
                return _add3DOther;
            }
        }
        private void addOther(object parameter)
        {
            if (parameter is Viewport3D environment)
            {
                string fileName = LoadFileDialog("Select a 3d model");

                if (fileName != null)
                {
                    obj = new Irregular3DObject(fileName);

                   // MessageBox.Show("X = " + obj.Content.Bounds.X + " y = " + obj.Content.Bounds.Y + " Z = " + obj.Content.Bounds.Z);

                    Transform3DGroup transformGroup = new Transform3DGroup();
                    objInteractive = InteractiveHelper.ConvertToUI(obj);
                    //RotateTransform3D rotate = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), 90));
                    //transformGroup.Children.Add(rotate);
                    obj.Rotate(90, new Vector3D(1, 0, 0));
                    transformGroup.Children.Add(obj.Transform);
                    TranslateTransform3D center = new TranslateTransform3D(2, 2.4, 1.3);

                    transformGroup.Children.Add(center);
                    //obj.Translate(new Vector3D(0, 0, 1));
                    //transformGroup.Children.Add(obj.Transform);
                    //obj.Scale(new Vector3D(0, 0, 1), 0.5);
                    // transformGroup.Children.Add(obj.Transform);


                    objInteractive.Transform = transformGroup;
                    objBounds = Collider.UpdateBounds(obj, center);

                    objInteractive.MouseRightButtonDown += (s, e) => ObjectPressed(s, e, environment);

                    environment.Children.Add(objInteractive);
                }

                //MessageBox.Show("Cube " + cube.SideLength);

            }
        }
        #endregion

        #region Base Events
        private void ObjectPressed(object sender, MouseEventArgs e, Viewport3D environment)
        {
            if (obj.Content is Model3DGroup model3Dgroup)
            {
                foreach (var model in model3Dgroup.Children)
                {
                    if (model is GeometryModel3D geometryModel)
                    {
                        geometryModel.Material = new DiffuseMaterial(Brushes.SaddleBrown);

                    }
                }
            }
            #region Context Menu
            ContextMenu context = new ContextMenu();
            MenuItem delete = new MenuItem();
            delete.Header = "Delete";
            MenuItem addMaterial = new MenuItem();
            addMaterial.Header = "Add Material";
            MenuItem applyPhisycs = new MenuItem();
            applyPhisycs.Header = "Apply Physics";
            MenuItem rigidBody = new MenuItem();
            rigidBody.Header = "Rigid Body";
            MenuItem solidBody = new MenuItem();
            solidBody.Header = "Solid Body";
            MenuItem elasticBody = new MenuItem();
            elasticBody.Header = "Elastic Body";
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);


            delete.Click += (s, ev) => Delete(s, ev, environment, objInteractive);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, objInteractive);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, objInteractive);
                addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, objInteractive);
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);
            context.IsOpen = true;
            #endregion
        }

        private void CubePressed(object sender, RoutedEventArgs e, Viewport3D environment)
        {
            if (cube.Content is Model3DGroup model3Dgroup)
            {
                foreach (var model in model3Dgroup.Children)
                {
                    if (model is GeometryModel3D geometryModel)
                    {
                        geometryModel.Material = new DiffuseMaterial(Brushes.Orange);
                    }
                }
               
            }
            #region Context Menu
            
            ContextMenu context = new ContextMenu();
            MenuItem delete = new MenuItem();
            delete.Header = "Delete";
            MenuItem addMaterial = new MenuItem();
            addMaterial.Header = "Add Material";
            MenuItem applyPhisycs = new MenuItem();
            applyPhisycs.Header = "Apply Physics";
            MenuItem rigidBody= new MenuItem();
            rigidBody.Header = "Rigid Body";
            MenuItem solidBody = new MenuItem();
            solidBody.Header = "Solid Body";
            MenuItem elasticBody= new MenuItem();
            elasticBody.Header = "Elastic Body";
            MenuItem addForce = new MenuItem();
            addForce.Header = "Add Force";
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(rigidBody);

            
            delete.Click += (s, ev) => Delete(s, ev, environment, cubeInteractive);
           solidBody.Click += (s, ev) => Solid(s, ev, environment,cubeInteractive);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment,cubeInteractive);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment,cubeInteractive);
            addForce.Click +=(s,ev)=> Force(s,ev,environment,cubeInteractive,sphereInteractive);
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);
            context.IsOpen = true;
          
            #endregion

        }

        private void Force(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D cubeInteractive,UIElement3D objElement)
        {
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(1);
            //trebuie sa ecsiste doua corpuri pentru a aplica forta ( cubul va avea fi cel care se va ciocni <deci va produce o forta>
            //, iar sfera va fi afectata de ciocnire<va fi aplicata o forta>)
            if(!cubeBounds.IsEmpty&&!sphereBounds.IsEmpty)
            {
               
                rigid.StartForce(cubeBounds, sphereBounds,cubeInteractive,objElement);
            }
        }

      

        private void Elastic(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj)
        {
            
            
                _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10);
                  
                rigid.StartBouncing(obj,rigid.Mass);
            
        }

        private void SetMaterial(object s, RoutedEventArgs ev, Viewport3D environment,UIElement3D obj)
        {
            MessageBox.Show("Under development");
        }

        private void Solid(object s,RoutedEventArgs ev,Viewport3D environment,UIElement3D obj)
        {
            if (obj.Transform.Value.OffsetZ > 0)
            {
                _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10);
                rigid.Start(obj);
            }
         
        }

        private void Delete(object s, RoutedEventArgs ev, Viewport3D environment,UIElement3D obj)
        {
            environment.Children.Remove(obj);
        }

        private void SpherePressed(object sender, RoutedEventArgs e, Viewport3D environment)
        {
            if (sphere.Content is Model3DGroup model3Dgroup)
            {
                foreach (var model in model3Dgroup.Children)
                {
                    if (model is GeometryModel3D geometryModel)
                    {
                        geometryModel.Material = new DiffuseMaterial(Brushes.Orange);

                    }
                }
            }
            #region Context Menu

            ContextMenu context = new ContextMenu();
            MenuItem delete = new MenuItem();
            delete.Header = "Delete";
            MenuItem addMaterial = new MenuItem();
            addMaterial.Header = "Add Material";
            MenuItem applyPhisycs = new MenuItem();
            applyPhisycs.Header = "Apply Physics";
            MenuItem rigidBody = new MenuItem();
            rigidBody.Header = "Rigid Body";
            MenuItem solidBody = new MenuItem();
            solidBody.Header = "Solid Body";
            MenuItem elasticBody = new MenuItem();
            elasticBody.Header = "Elastic Body";
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            delete.Click += (s, ev) => Delete(s, ev, environment,sphereInteractive);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment,sphereInteractive);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, sphereInteractive);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, sphereInteractive);
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);
            context.IsOpen = true;
            #endregion
        
        }
        #endregion
    }
}
