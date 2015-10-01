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
    /// Interaction logic for Group_Menu.xaml
    /// </summary>
    public partial class Menu_Container : Canvas
    {
        Menu_Layer menuLayer;
        public Menu_Container(Menu_Layer menuLayer)
        {
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"Resource\Image\menu_bg.png", UriKind.Relative));
            this.Background = ib;
            this.menuLayer = menuLayer;
            this.Height = STATICS.MENU_BAR_SIZE.Height;
            this.Width = STATICS.MENU_BAR_SIZE.Width;
            InitializeComponent();
        }

        
    }
}
