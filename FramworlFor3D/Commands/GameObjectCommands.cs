


using FramworkFor3D._3DObjects;
using FramworkFor3D._3DPhysics;
using FramworkFor3D._3DSounds;
using FramworkFor3D.Animations;
using FramworkFor3D.helpers;
using FramworkFor3D.View;
using FramworkFor3D.ViewModels;
using FramworlFor3D.helpers;
using FramworlFor3D.ViewModels;

using System;
using System.Collections.Generic;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

using System.Windows.Media.Imaging;
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
        private RelayCommands3D _add3DCube;
        private RelayCommands3D _add3DSphere;
        private RelayCommands3D _add3DOther;
        private Irregular3DObject obj;



        Cube3D cube;
        Sphere3D sphere;
        UIElement3D cubeInteractive, sphereInteractive, objInteractive;
        DispatcherTimer timer;
        Rect3D cubeBounds, sphereBounds, objBounds;
        SoundManager SoundManager;

        private Transform3DGroup _cubeTransform, _sphereTransform, _objTransform;

        bool addSound = false;
        public Rect3D CubeBounds
        {
            get
            {
                return cubeBounds;
            }
            set
            {
                cubeBounds = value;
            }
        }
        public Rect3D SphereBounds
        {
            get
            {
                return sphereBounds;
            }
            set
            {
                sphereBounds = value;
            }
        }
        public Rect3D ObjBounds
        {
            get
            {
                return objBounds;
            }
            set
            {
                objBounds = value;
            }
        }
        bool collider = false;
        bool isPressed = false;
        DiffuseMaterial defaultMaterial = new DiffuseMaterial(Brushes.Gray);
        ObjectType type;
        #endregion

        public GameObjectCommands(MainVM mainVM)
        {
            _mainVM = mainVM;
            _objects = new List<UIElement3D>();
            SoundManager = new SoundManager();


        }


        #region Create Models 
        public RelayCommands3D add3DCube
        {
            get
            {
                if (_add3DCube == null)
                    _add3DCube = new RelayCommands3D(addCube);
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
                Transform3DGroup transfGroup = new Transform3DGroup();
                ScaleTransform3D scale = new ScaleTransform3D(0.4, 1, 1);
                transfGroup.Children.Add(scale);
                RotateTransform3D rotate=new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), 50));
               // cube.Rotate(15, new Vector3D(1, 0, 0));
                transfGroup.Children.Add(rotate);
                TranslateTransform3D center = new TranslateTransform3D(2, 1.5, 0.0);

              
                transfGroup.Children.Add(center);
              
                _cubeTransform = transfGroup;
                cube.Transform = transfGroup;
                cubeBounds = Collider.UpdateBounds(cube, center);


                cube.position = new Vector3D(center.OffsetX, center.OffsetY, center.OffsetZ);
                newCubeInteractive.MouseRightButtonDown += (s, e) => CubePressed(s, e, environment);
                newCubeInteractive.Transform = center;
                objects.Add(newCubeInteractive);
            }
            MessageBox.Show("Scene has " + objects.Count + " objects");
        }
        public RelayCommands3D add3DSphere
        {
            get
            {
                if (_add3DSphere == null)
                    _add3DSphere = new RelayCommands3D(addSphere);
                return _add3DSphere;
            }
        }
        private void addSphere(object parameter)
        {
            if (parameter is Viewport3D environment)
            {

                sphere = new Sphere3D();
                sphereInteractive = InteractiveHelper.ConvertToUI(sphere);
                Transform3DGroup transformGroup=new Transform3DGroup();
                environment.Children.Add(sphereInteractive);
                ScaleTransform3D scale = new ScaleTransform3D(1, 0.4, 0.4);
                transformGroup.Children.Add(scale);
                sphere.Rotate(90, new Vector3D(1, 0, 0));
                transformGroup.Children.Add(sphere.Transform);
                TranslateTransform3D center = new TranslateTransform3D(1, 2.4, 1.3);
                transformGroup.Children.Add(center);
                sphereBounds = Collider.UpdateBounds(sphere, center);
                sphere.Transform = center;
                _sphereTransform=transformGroup;
                sphereInteractive.MouseRightButtonDown += (s, e) => SpherePressed(s, e, environment);
                sphereInteractive.Transform = center;
                objects.Add(sphereInteractive);
            }
            MessageBox.Show("Scene has " + objects.Count + " objects");
        }
        public RelayCommands3D add3DOther
        {
            get
            {
                if (_add3DOther == null)
                    _add3DOther = new RelayCommands3D(addOther);
                return _add3DOther;
            }
        }
        private void addOther(object parameter)
        {
            if (parameter is Viewport3D environment)
            {
                string fileName = FileHelpers.LoadObjectDialog("Select a 3d model");

                if (fileName != null)
                {
                    obj = new Irregular3DObject(fileName);


                    Transform3DGroup transformGroup = new Transform3DGroup();
                   objInteractive = InteractiveHelper.ConvertToUI(obj);
                    ScaleTransform3D scale = new ScaleTransform3D(1, 1, 1);
                    transformGroup.Children.Add(scale);
                    obj.Rotate(90, new Vector3D(1, 0, 0));
              
                    transformGroup.Children.Add(obj.Transform);
                   
                    TranslateTransform3D center = new TranslateTransform3D(2, 2.4, 1);

                    transformGroup.Children.Add(center);
                    objInteractive.Transform= transformGroup;

                    _objTransform= transformGroup;
                    
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
            MenuItem sound = new MenuItem();
            sound.Header = "Add Sound";
        
            MenuItem properties = new MenuItem();
            properties.Header = "Properties";
         
            collider.Items.Add(colliderModify);
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(collider);


            delete.Click += (s, ev) => Delete(s, ev, environment, clickedObject,type);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, clickedObject, ObjectType.IRREGULAR);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, clickedObject, ObjectType.IRREGULAR);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, clickedObject, ObjectType.IRREGULAR);
            if (sphereInteractive != null)
            {
                addForce.Click += (s, ev) => Force(s, ev, environment, clickedObject, sphereInteractive);
            }
            if (cubeInteractive != null)
            {
                addForce.Click += (s, ev) => Force(s, ev, environment, clickedObject, cubeInteractive);
            }
            colliderModify.Click += (s, ev) => ModifyCollider(s, ev, environment, clickedObject);
            sound.Click += (s, ev) => Sound(s, ev, clickedObject);
          
            properties.Click += (s, ev) => Properties(s, ev, clickedObject);
         
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);
            context.Items.Add(sound);
           
            
            context.Items.Add(properties);


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
            MenuItem sound = new MenuItem();
            sound.Header = "Add Sound";
          
            MenuItem properties = new MenuItem();
            properties.Header = "Properties";
          

            collider.Items.Add(colliderModify);
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(collider);


            delete.Click += (s, ev) => Delete(s, ev, environment, clickedCube,type);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, clickedCube, type);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, clickedCube, type);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, clickedCube, type);
            if (sphereInteractive != null)
            {
                addForce.Click += (s, ev) => Force(s, ev, environment, clickedCube, sphereInteractive);
            }
            if(objInteractive!=null)
            {
                addForce.Click += (s, ev) => Force(s, ev, environment, clickedCube, objInteractive);
            }
            colliderModify.Click += (s, ev) => ModifyCollider(s, ev, environment, clickedCube);
            sound.Click +=(s,ev)=>Sound(s,ev,clickedCube);
            
            properties.Click += (s, ev) => Properties(s, ev, clickedCube);
        
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);
            context.Items.Add(sound);
          
            context.Items.Add(properties);
           


            context.IsOpen = true;

            #endregion

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
            MenuItem sound = new MenuItem();
            sound.Header = "Add Sound";
        
            MenuItem properties = new MenuItem();
            properties.Header = "Properties";
          

            collider.Items.Add(colliderModify);
            rigidBody.Items.Add(solidBody);
            rigidBody.Items.Add(elasticBody);
            applyPhisycs.Items.Add(rigidBody);
            applyPhisycs.Items.Add(addForce);
            applyPhisycs.Items.Add(collider);


            delete.Click += (s, ev) => Delete(s, ev, environment, clickedSphere, type);
            rigidBody.Click += (s, ev) => Solid(s, ev, environment, clickedSphere, ObjectType.SPHERE);
            elasticBody.Click += (s, ev) => Elastic(s, ev, environment, clickedSphere, ObjectType.SPHERE);
            addMaterial.Click += (s, ev) => SetMaterial(s, ev, environment, clickedSphere, ObjectType.SPHERE);
            if (cubeInteractive != null)
            { addForce.Click += (s, ev) => Force(s, ev, environment, clickedSphere, cubeInteractive); }
            if (objInteractive != null) { addForce.Click += (s, ev) => Force(s, ev, environment, clickedSphere, objInteractive); }
            colliderModify.Click += (s, ev) => ModifyCollider(s, ev, environment, clickedSphere);
            sound.Click += (s, ev) => Sound(s, ev, clickedSphere);
          
            properties.Click += (s, ev) => Properties(s, ev, clickedSphere);
           
            context.Items.Add(delete);
            context.Items.Add(addMaterial);
            context.Items.Add(applyPhisycs);
            context.Items.Add(sound);
        
            context.Items.Add(properties);
             



            context.IsOpen = true;
            #endregion

        }

        private void Properties(object s, RoutedEventArgs ev, UIElement3D obj)
        {ModelVisual3D model=InteractiveHelper.ConvertToModel(obj);
            isPressed = false;
            if (!isPressed)
            {
                if (model.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var m in model3Dgroup.Children)
                    {
                        if (m is GeometryModel3D geometryModel)
                        {
                            geometryModel.Material = defaultMaterial;
                        }
                    }

                }
            }
            if (type==ObjectType.CUBE)
            {
                model.Transform = _cubeTransform;
            }
          if(type== ObjectType.SPHERE)
            {
                model.Transform = _sphereTransform;
            }
          if(type==ObjectType.IRREGULAR)
            {
                model.Transform = _objTransform;
            }
            
         
          

            PropertiesVM pageVM = new PropertiesVM(model);
            var propertiesWindow = new Properties


            { Title = type.ToString(),
                
              
        };
            propertiesWindow.DataContext = pageVM;
            propertiesWindow.Show();
         

        }

        private void Animate(object s, RoutedEventArgs ev, UIElement3D clickedCube)
        {
            ModelVisual3D model = InteractiveHelper.ConvertToModel(clickedCube);
            Animations.Animation animate = new Animation();
            animate.StartAnimation(model);
        }

        private void Sound(object s, RoutedEventArgs ev, UIElement3D clickedCube)
        {
            addSound = true;
            string[] audio = FileHelpers.LoadSoundDialog("Add audio on object");
            Audio clip = new Audio();
            clip.LoadSound("sound1", audio[0]);
            SoundManager.AddSound(clip);
            clip.LoadSound("sound2", audio[1]);
            SoundManager.AddSound(clip);
            clip.LoadSound("sound3", audio[2]);
            SoundManager.AddSound(clip);





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

        private void Force(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D objectWithForce, UIElement3D objAffectByForce)
        {
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10,this);
            ModelVisual3D objectModel = InteractiveHelper.ConvertToModel(objectWithForce);
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
                rigid.StartForce(cubeBounds, sphereBounds, objectWithForce, objAffectByForce, type, ObjectType.SPHERE);
            }
            if (cubeBounds.Location != new Point3D(0, 0, 0) && sphereBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.SPHERE)
            {
                rigid.StartForce(sphereBounds, cubeBounds, objectWithForce, objAffectByForce, type, ObjectType.CUBE);
            }
            if (sphereBounds.Location != new Point3D(0, 0, 0) && objBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.SPHERE)
            {
                rigid.StartForce(sphereBounds, objBounds, objectWithForce, objAffectByForce, type, ObjectType.IRREGULAR);
            }
            if (sphereBounds.Location != new Point3D(0, 0, 0) && objBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.IRREGULAR)
            {
                rigid.StartForce(objBounds, sphereBounds, objectWithForce, objAffectByForce, type, ObjectType.SPHERE);
            }
            if (objBounds.Location != new Point3D(0, 0, 0) && cubeBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.IRREGULAR)
            {
                rigid.StartForce(objBounds, cubeBounds, objectWithForce, objAffectByForce, type, ObjectType.CUBE);
            }
            if (objBounds.Location != new Point3D(0, 0, 0) && cubeBounds.Location != new Point3D(0, 0, 0) && !collider && type == ObjectType.CUBE)
            {
                rigid.StartForce(cubeBounds, objBounds, objectWithForce, objAffectByForce, type, ObjectType.IRREGULAR);
            }
            if (collider)
            {
                rigid.StartForceWithUserCollider(cubeBounds, sphereBounds, objectWithForce, objAffectByForce,type,ObjectType.IRREGULAR);
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
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10,this);

            rigid.StartBouncing(obj, rigid.Mass, type);
            if (type == ObjectType.CUBE)
            {
                cubeBounds = rigid.Bound;
            }
            if (type == ObjectType.SPHERE)
            {
                sphereBounds = rigid.Bound;
            }
            else
            {
                objBounds = rigid.Bound;
            }

        }

        private void SetMaterial(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D objs, ObjectType type)
        {
            string fileName = FileHelpers.LoadImageDialog("Select material of model");

            if (fileName != null)
            {
              ModelVisual3D model= InteractiveHelper.ConvertToModel(objs);
             
             
                if (model.Content is Model3DGroup model3Dgroup)
                {
                    foreach (var model3D in model3Dgroup.Children)
                    {
                        if (model3D is GeometryModel3D geometryModel)
                        {

                            ImageBrush image = new ImageBrush();
                            image.ImageSource = new BitmapImage(new Uri(fileName));
                            DiffuseMaterial newMaterial = new DiffuseMaterial(image);
                            geometryModel.Material = newMaterial;
                           
                        }
                    }
                }
            }
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
            if (addSound)
            {
              
              //  collision.LoadSound("collision", audio[1]);
                SoundManager.PlaySound("sound3");
              
                //SoundManager.AddSound(collision);
               
                
                
               
            }
            _3DPhysics.RigidBody rigid = new _3DPhysics.RigidBody(10, this);
            if (obj.Transform.Value.OffsetZ > 0)
            {
                rigid.Start(obj, type);
                
            }
         

        }

        private void Delete(object s, RoutedEventArgs ev, Viewport3D environment, UIElement3D obj,ObjectType objectType)
        {
            isPressed = false;
            if(objectType==ObjectType.CUBE)
            {
                cubeBounds.Location=new Point3D(0,0,0);
            }
            if(objectType==ObjectType.SPHERE)
            {
                sphereBounds.Location=new Point3D(0,0,0);
            }
            if(objectType==ObjectType.IRREGULAR)
            {
                objBounds.Location=new Point3D(0,0,0);
            }
            environment.Children.Remove(obj);
            objects.Remove(obj);
            MessageBox.Show("Object deleted ");
            SoundManager.stop("sound3");
        }
        
        #endregion
    }
}
