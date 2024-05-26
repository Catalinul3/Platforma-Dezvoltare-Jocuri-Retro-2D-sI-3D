using Eco.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace FramworkFor3D._3DObjects
{
    public static class Cube3DInteractive
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
       
    }
}
