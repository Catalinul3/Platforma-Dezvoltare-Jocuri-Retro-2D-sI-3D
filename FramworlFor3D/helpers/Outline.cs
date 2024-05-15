using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Effects;

namespace FramworkFor3D.helpers
{
    public class Outline:ShaderEffect
    {
        public static readonly DependencyProperty Instance = ShaderEffect.RegisterPixelShaderSamplerProperty("Input",typeof(Outline),0);

        public Outline()
        {
            PixelShader pixelShader= new PixelShader();
            pixelShader.UriSource = new Uri("Outline.ps", UriKind.Relative);
            this.PixelShader = pixelShader;
            this.UpdateShaderValue(Instance);
        }
        public System.Windows.Media.Brush Input
        {
            get { return (Brush)this.GetValue(Instance); }
            set { this.SetValue(Instance, value);}
        }
    }
}
