using Eco.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DObjects
{
    public static class InteractiveHelper
    {public static UIElement3D ConvertToUI(ModelVisual3D model)
        {
            ModelUIElement3D modelUIElement = new ModelUIElement3D();
            modelUIElement.Model = model.Content;
            return modelUIElement;
        }
        public static ModelVisual3D ConvertToModel(UIElement3D uIElement3D)
        { ModelUIElement3D modelUi = new ModelUIElement3D();
            modelUi = (ModelUIElement3D)uIElement3D;
            ModelVisual3D modelVisual3DmodelUIElement = new ModelVisual3D();
            modelVisual3DmodelUIElement.Content = modelUi.Model ;
            return modelVisual3DmodelUIElement;
        }
        public static Cube3D convertToCube(ModelVisual3D model)
        {
            Cube3D cube = new Cube3D(model);
           
            return cube;
        }
        public static Sphere3D convertToSphere(ModelVisual3D model)
        {
                       Sphere3D sphere = new Sphere3D(model);
            return sphere;
        }
        public static Irregular3DObject convertToObject(ModelVisual3D model)
        {
            Irregular3DObject obj = new Irregular3DObject(model);
            return obj;
        }
       

     
    }
}
