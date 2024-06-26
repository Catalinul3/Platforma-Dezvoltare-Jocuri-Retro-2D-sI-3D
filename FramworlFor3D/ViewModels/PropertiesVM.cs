using FramworkFor3D.Based_Operations;
using FramworkFor3D.helpers;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace FramworkFor3D.ViewModels
{
    public class PropertiesVM : BaseVM
    {
        private ModelVisual3D _model;
        public ModelVisual3D Model
        {
            get { return _model; }
            set
            {
                _model = value;
                NotifyPropertyChanged("Model");
            }
        }
        private string translateX, translateY, translateZ;
        private string scaleX, scaleY, scaleZ;
        private string rotateX, rotateY, rotateZ;
        public string TranslateX
        {
            get
            {
                return translateX;
            }
            set
            {
                translateX = value;
                NotifyPropertyChanged(nameof(TranslateX));
            }
        }
        public string TranslateY
        {
            get
            
                {
                    return translateY;
                }
                set
            {
                    translateY = value;
                NotifyPropertyChanged(nameof(TranslateY));
            }


        }
        public string TranslateZ
        {
            get

            {
                return translateZ;
            }
            set
            {
                translateZ = value;
                NotifyPropertyChanged(nameof(TranslateZ));
            }


        }
        public string ScaleX
        {
            get
            {
                return scaleX;
            }
            set
            {
                scaleX = value;
                NotifyPropertyChanged(nameof(ScaleX));
            }
        }
        public string ScaleY
        {
            get

            {
                return scaleY;
            }
            set
            {
                scaleY = value;
                NotifyPropertyChanged(nameof(ScaleY));
            }


        }
        public string ScaleZ
        {
            get

            {
                return scaleZ;
            }
            set
            {
                scaleZ = value;
                NotifyPropertyChanged(nameof(ScaleZ));
            }


        }
        public string RotateX
        {
            get
            {
                return rotateX;
            }
            set
            {
                rotateX = value;
                NotifyPropertyChanged(nameof(RotateX));
            }
        }
        public string RotateY
        {
            get

            {
                return rotateY;
            }
            set
            {
                rotateY = value;
                NotifyPropertyChanged(nameof(RotateY));
            }


        }
        public string RotateZ
        {
            get

            {
                return rotateZ;
            }
            set
            {
                rotateZ = value;
                NotifyPropertyChanged(nameof(RotateZ));
            }


        }
        public PropertiesVM()
        {

        }
        public PropertiesVM(ModelVisual3D model)
        {
            _model = model;
            if(model.Transform is Transform3DGroup)
            {
                Transform3DGroup group = (Transform3DGroup)model.Transform;
                if(group.Children.Count>0)
                {
                    if (group.Children[2] is TranslateTransform3D)
                    {
                        TranslateTransform3D translate = (TranslateTransform3D)group.Children[2];
                        translateX = translate.OffsetX.ToString();
                        translateY = translate.OffsetY.ToString();
                        translateZ = translate.OffsetZ.ToString();
                    }
                    if (group.Children[0] is ScaleTransform3D)
                    {
                        ScaleTransform3D scale = (ScaleTransform3D)group.Children[0];
                        scaleX = scale.ScaleX.ToString();
                        scaleY = scale.ScaleY.ToString();
                        scaleZ = scale.ScaleZ.ToString();
                    }
                    if (group.Children[1] is RotateTransform3D)
                    {
                        RotateTransform3D rotate = (RotateTransform3D)group.Children[1];
                        rotateX = rotate.CenterX.ToString();
                        rotateY = rotate.CenterY.ToString();
                        rotateZ = rotate.CenterZ.ToString();
                    }
                }
               
            }

        }
        
    }
}
