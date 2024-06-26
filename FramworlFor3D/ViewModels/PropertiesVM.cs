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
        private double translateX, translateY, translateZ;
        private double scaleX, scaleY, scaleZ;
        private double rotateX, rotateY, rotateZ;
        public double TranslateX
        {
            get
            {
                return translateX;
            }
            set
            {
                translateX = value;
            }
        }
        public double TranslateY
        {
            get
            
                {
                    return translateY;
                }
                set
            {
                    translateY = value;
                }


        }
        public double TranslateZ
        {
            get

            {
                return translateZ;
            }
            set
            {
                translateZ = value;
            }


        }
        public double ScaleX
        {
            get
            {
                return scaleX;
            }
            set
            {
                scaleX = value;
            }
        }
        public double ScaleY
        {
            get

            {
                return scaleY;
            }
            set
            {
                scaleY = value;
            }


        }
        public double ScaleZ
        {
            get

            {
                return scaleZ;
            }
            set
            {
                scaleZ = value;
            }


        }
        public double RotateX
        {
            get
            {
                return rotateX;
            }
            set
            {
                rotateX = value;
            }
        }
        public double RotateY
        {
            get

            {
                return rotateY;
            }
            set
            {
                rotateY = value;
            }


        }
        public double RotateZ
        {
            get

            {
                return rotateZ;
            }
            set
            {
                rotateZ = value;
            }


        }
        public PropertiesVM(ModelVisual3D model)
        {
            _model = model;

        }
        
    }
}
