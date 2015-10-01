using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardDesign
{
    /// <summary>
    /// Interaction logic for My_Canvas.xaml
    /// </summary>
    public partial class Bottom_Layer : Canvas
    {          
        Dictionary<TouchDevice, Ellipse> _Followers = new Dictionary<TouchDevice, Ellipse>();
        public Bottom_Layer()
        {
            InitializeComponent();
            this.IsManipulationEnabled = false;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"Resource\Image\bg.png", UriKind.Relative));
            this.Background = ib;
            this.Height = STATICS.SCREEN_HEIGHT;
            this.Width = STATICS.SCREEN_WIDTH;
        }
    }
}
