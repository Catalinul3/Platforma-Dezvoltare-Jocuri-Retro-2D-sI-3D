
using BulletSharp;
using FramworkFor3D._3DObjects;
using FramworkFor3D._3DPhysics;
using FramworkFor3D.helpers;
using FramworlFor3D.helpers;
using FramworlFor3D.ViewModels;
using RetroEngine.ViewModels;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Media3D;

using System.Windows.Threading;


namespace FramworkFor3D.Commands
{
    public class GameObjectCommands : BaseVM
    {
        #region Variables
        private readonly MainVM _mainVM;
        private List<UIElement3D> _objects;
        public List<UIElement3D> objects
        {
            get
            {
                return _objects;
            }
            set
            {
                _objects = value;

            }
        }
        private RelayCommand _add3DCube;
        private RelayCommand _add3DSphere;
        private RelayCommand _add3DOther;
        private Irregular3DObject obj;



        Cube3D cube;
        Sphere3D sphere;
        UIElement3D cubeInteractive, sphereInteractive, objInteractive;
        DispatcherTimer timer;
        Rect3D cubeBounds, sphereBounds, objBounds;
        bool collider = false;
        bool isPressed = false;
        DiffuseMaterial defaultMaterial = new DiffuseMaterial(Brushes.Gray);
        ObjectType type;
        #endregion

        public GameObjectCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
            _objects = new List<UIElement3D>();


        }


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
                UIElement3D newCubeInteractive = cubeInteractive;
                environment.Children.Add(newCubeInteractive);

                TranslateTransform3D center = new TranslateTransform3D(1, 2.3, 1.1);
                cube.Transform = center;
                cubeBounds = Collider.UpdateBounds(cube, center);


                cube.position = new Vector3D(center.OffsetX, center.OffsetY, center.OffsetZ);
                newCubeInteractive.MouseRightButtonDown += (s, e) => CubePressed(s, e, environment);
                newCubeInteractive.Transform = center;
                objects.Add(newCubeInteractive);
            }
            MessageBox.Show("Scene has " + objects.Count + " objects");
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
                TranslateTransform3D center = new TranslateTransform3D(1, 2.4, 1.3);
                sphereBounds = Collider.UpdateBounds(sphere, center);
                sphere.Transform = center;
                sphereInteractive.MouseRightButtonDown += (s, e) => SpherePressed(s, e, environment);
                sphereInteractive.Transform = center;
                objects.Add(sphereInteractive);
            }
            MessageBox.Show("Scene has " + objects.Count + " objects");
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


                    Transform3DGroup transformGroup = new Transform3DGroup();
                    objInteractive = InteractiveHelper.ConvertToUI(obj);

                    obj.Rotate(90, new Vector3D(1, 0, 0));
                    transformGroup.Children.Add(obj.Transform);
                    TranslateTransform3D center = new TranslateTransform3D(3, 2.4, 1.3);

                    transformGroup.Children.Add(center);



                    objInteractive.Transform = transformGroup;
                    objBounds = Collider.UpdateBounds(obj, center);

                    objInteractive.MouseRightButtonDown += (s, e) => ObjectPressed(s, e, environment);

                    environment.Children.Add(objInteractive);
                    objects.Add(objInteractive);
                }

                MessageBox.Show("Scene has " + objects.Count + " objects");

            }
        }
        #endregion

        #region Base Events
        private void ObjectPressed(object sender, MouseEventArgs e, Viewport3D environment)
        {
            Point mouse = e.GetPosition(environment);
            var hitTestResult = VisualTreeHelper.HitTest(environment, mouse);
            var clickedObject = hitTestResult.VisualHit as UIElement3D;

            if (hitTestResult != null && hitTestResult.VisualHit is UIElement3D hit)
            {
                var hitModel = InteractiveHelper.ConvertToModel(hit);
                if (hitModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = new DiffuseMaterial(Brushes.Orange);
                        }
                    }

                }
            }
            type = ObjectType.IRREGULAR;
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
            MenuItem addForce = new MenuItem();
            addForce.Header = "Add Force";
            MenuItem collider = new MenuItem();
            collider.Header = "Collider";
            MenuItem colliderModify = new MenuItem();
            colliderModify.Header = "Modify Collider";
            collider.Items.Add(colliderModify);
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(collider);


            delete.Click += (s, ev) => Delete(s, ev, environment, clickedObject);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, clickedObject, ObjectType.IRREGULAR);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, clickedObject, ObjectType.IRREGULAR);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, clickedObject, ObjectType.IRREGULAR);
            addForce.Click += (s, ev) => Force(s, ev, environment, clickedObject, cubeInteractive);
            colliderModify.Click += (s, ev) => ModifyCollider(s, ev, environment, clickedObject);
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);


            context.IsOpen = true;
            #endregion
        }

        private void CubePressed(object sender, MouseEventArgs e, Viewport3D environment)
        {
            isPressed = true;
            Point mouse = e.GetPosition(environment);
            var hitTestResult = VisualTreeHelper.HitTest(environment, mouse);
            var clickedCube = hitTestResult.VisualHit as UIElement3D;

            if (hitTestResult != null && hitTestResult.VisualHit is UIElement3D hit)
            {
                var hitModel = InteractiveHelper.ConvertToModel(hit);
                if (hitModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = new DiffuseMaterial(Brushes.Orange);
                        }
                    }

                }
            }
            #region Context Menu
            type = ObjectType.CUBE;
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
            MenuItem addForce = new MenuItem();
            addForce.Header = "Add Force";
            MenuItem collider = new MenuItem();
            collider.Header = "Collider";
            MenuItem colliderModify = new MenuItem();
            colliderModify.Header = "Modify Collider";
            collider.Items.Add(colliderModify);
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(collider);


            delete.Click += (s, ev) => Delete(s, ev, environment, clickedCube);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, clickedCube, type);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, clickedCube, type);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, clickedCube, type);
            addForce.Click += (s, ev) => Force(s, ev, environment, clickedCube, sphereInteractive);
            colliderModify.Click += (s, ev) => ModifyCollider(s, ev, environment, clickedCube);
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);


            context.IsOpen = true;

            #endregion

        }

        private void ModifyCollider(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj)
        {
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            cubeBounds = ObjectCollider.getCurrentObjectCollider(objectModel);
            MessageBox.Show("X = " + cubeBounds.SizeX + " y = " + cubeBounds.Y + " Z = " + cubeBounds.Z);
            cubeBounds = ObjectCollider.modifyObjectBounds(objectModel, 1);
            MessageBox.Show("X = " + cubeBounds.SizeX + " y = " + cubeBounds.Y + " Z = " + cubeBounds.Z);
            isPressed = false;
            collider = true;
            if (!isPressed)
            {
                if (objectModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = defaultMaterial;
                        }
                    }

                }
            }

        }

        private void Force(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj2, UIElement3D objElement)
        {
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10);
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj2);
            //trebuie sa ecsiste doua corpuri pentru a aplica forta ( cubul va avea fi cel care se va ciocni <deci va produce o forta>
            //, iar sfera va fi afectata de ciocnire<va fi aplicata o forta>)
            isPressed = false;
            if (!isPressed)
            {
                if (objectModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = defaultMaterial;
                        }
                    }

                }
            }
            if (cubeBounds.Location != new Point3D(0, 0, 0) && sphereBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.CUBE)
            {
                rigid.StartForce(cubeBounds, sphereBounds, obj2, objElement, type);
            }
            if (cubeBounds.Location!= new Point3D(0, 0, 0) && sphereBounds.Location!=new Point3D(0, 0, 0) && !collider && type == ObjectType.SPHERE)
            {
                rigid.StartForce(sphereBounds, cubeBounds, obj2, objElement, type);
            }
            if (sphereBounds.Location!=new Point3D(0, 0, 0) && objBounds.Location!=new Point3D(0, 0, 0) && !collider&& type == ObjectType.SPHERE)
            {
                rigid.StartForce(sphereBounds, objBounds, obj2, objElement, type);
            }
            if (sphereBounds.Location != new Point3D(0,0,0) && objBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.IRREGULAR)
            {
                rigid.StartForce(objBounds, sphereBounds, obj2, objElement, type);
            }
            if (objBounds.Location != new Point3D(0, 0, 0) && cubeBounds.Location != new Point3D(0, 0, 0) && !collider&&type==ObjectType.IRREGULAR)
            {
                rigid.StartForce(objBounds, cubeBounds, obj2, objElement,type);
            }
            if (objBounds.Location != new Point3D(0, 0, 0) && cubeBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.CUBE)
            {
                rigid.StartForce(cubeBounds, objBounds, obj2, objElement, type);
            }
            if (collider)
            {
                rigid.StartForceWithUserCollider(cubeBounds, sphereBounds, obj2, objElement);
            }

        }

        private void Elastic(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj, ObjectType type)
        {

            isPressed = false;
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            if (!isPressed)
            {
                if (objectModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = defaultMaterial;
                        }
                    }

                }
            }
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10);

            rigid.StartBouncing(obj, rigid.Mass, type);
            if(type==ObjectType.CUBE)
            {
                cubeBounds = rigid.Bound;
            }
            if(type==ObjectType.SPHERE)
            {
                sphereBounds= rigid.Bound;
            }
            else
            {
                objBounds= rigid.Bound;
            }

        }

        private void SetMaterial(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj, ObjectType type)
        {
            MessageBox.Show("Under development");
        }

        private void Solid(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj, ObjectType type)
        {
            isPressed = false;
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(obj);
            if (!isPressed)
            {
                if (objectModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = defaultMaterial;
                        }
                    }

                }
            }
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10);
            if (obj.Transform.Value.OffsetZ > 0)
            {



                rigid.Start(obj, type);



            }


        }

        private void Delete(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj)
        {
            isPressed = false;
            environment.Children.Remove(obj);
            objects.Remove(obj);
            MessageBox.Show("Object deleted ");
        }

        private void SpherePressed(object sender, MouseEventArgs e, Viewport3D environment)
        {
            Point mouse = e.GetPosition(environment);
            var hitTestResult = VisualTreeHelper.HitTest(environment, mouse);
            var clickedSphere = hitTestResult.VisualHit as UIElement3D;

            if (hitTestResult != null && hitTestResult.VisualHit is UIElement3D hit)
            {
                var hitModel = InteractiveHelper.ConvertToModel(hit);
                if (hitModel.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model in model3Dgroup.Children)
                    {
                        if (model is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = new DiffuseMaterial(Brushes.Orange);
                        }
                    }

                }
            }
            type = ObjectType.SPHERE;
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
            MenuItem addForce = new MenuItem();
            addForce.Header = "Add Force";
            MenuItem collider = new MenuItem();
            collider.Header = "Collider";
            MenuItem colliderModify = new MenuItem();
            colliderModify.Header = "Modify Collider";
            collider.Items.Add(colliderModify);
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(collider);


            delete.Click += (s, ev) => Delete(s, ev, environment, clickedSphere);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, clickedSphere, ObjectType.SPHERE);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, clickedSphere, ObjectType.SPHERE);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, clickedSphere, ObjectType.SPHERE);
            if (cubeInteractive != null)
            { addForce.Click += (s, ev) => Force(s, ev, environment, clickedSphere, cubeInteractive); }
            if(objInteractive!= null) { addForce.Click += (s, ev) => Force(s, ev, environment, clickedSphere, objInteractive); }
            colliderModify.Click += (s, ev) => ModifyCollider(s, ev, environment, clickedSphere);
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);


            context.IsOpen = true;
            #endregion

        }
        #endregion
    }
}
