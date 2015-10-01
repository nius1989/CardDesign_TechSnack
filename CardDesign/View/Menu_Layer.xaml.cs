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
    /// Interaction logic for Menu_Layer.xaml
    /// </summary>
    public partial class Menu_Layer : Canvas
    {
        MainWindow mainWindow;
        Menu_Container[] menuBars = new Menu_Container[4];

        public Menu_Container[] MenuBars
        {
            get { return menuBars; }
            set { menuBars = value; }
        }

        public MainWindow MainWindow
        {
            get { return mainWindow; }
            set { mainWindow = value; }
        }
        public Menu_Layer(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.Height = STATICS.SCREEN_HEIGHT;
            this.Width = STATICS.SCREEN_WIDTH;
            this.IsManipulationEnabled = false;
            this.Background = null;
            //if (STATICS.ALEX_ACTIVE)
            //{
            //    menuBars[0] = new Menu_Container(this);
            //    double x = (STATICS.SCREEN_WIDTH - menuBars[0].Width) / 2;
            //    double y = STATICS.SCREEN_HEIGHT - menuBars[0].Height;

            //    Matrix matrix = new Matrix(1, 0, 0, 1, x, y);
            //    menuBars[0].RenderTransform = new MatrixTransform(matrix);
            //    menuBars[0].IsManipulationEnabled = false;
            //    menuBars[0].IsHitTestVisible = false;
            //    this.Children.Add(menuBars[0]);
            //}
            //if (STATICS.BEN_ACTIVE)
            //{
            //    menuBars[1] = new Menu_Container(this);
            //    double x = (STATICS.SCREEN_WIDTH + menuBars[1].Width) / 2;
            //    double y = menuBars[0].Height;
            //    Matrix matrix = new Matrix(1, 0, 0, 1, 0, 0);
            //    matrix.Rotate(180);
            //    matrix.Translate(x, y);
            //    menuBars[1].RenderTransform = new MatrixTransform(matrix);
            //    menuBars[1].IsManipulationEnabled = false;
            //    menuBars[1].IsHitTestVisible = false;
            //    this.Children.Add(menuBars[1]);
            //}
            if (STATICS.CHRIS_ACTIVE)
            {
                menuBars[2] = new Menu_Container(this);
                double x = menuBars[2].Height;
                double y = (STATICS.SCREEN_HEIGHT - menuBars[0].Width) / 2;
                Matrix matrix = new Matrix(1, 0, 0, 1, 0, 0);
                matrix.Rotate(90);
                matrix.Translate(x, y);
                menuBars[2].RenderTransform = new MatrixTransform(matrix);
                menuBars[2].IsManipulationEnabled = false;
                menuBars[2].IsHitTestVisible = false;
                this.Children.Add(menuBars[2]);
            }
            if (STATICS.DANNY_ACTIVE)
            {
                menuBars[3] = new Menu_Container(this);
                double x = STATICS.SCREEN_WIDTH - menuBars[3].Height;
                double y = (STATICS.SCREEN_HEIGHT + menuBars[3].Width) / 2;
                Matrix matrix = new Matrix(1, 0, 0, 1, 0, 0);
                matrix.Rotate(-90);
                matrix.Translate(x, y);
                menuBars[3].RenderTransform = new MatrixTransform(matrix);
                menuBars[3].IsManipulationEnabled = false;
                menuBars[3].IsHitTestVisible = false;
                this.Children.Add(menuBars[3]);
            }
        }
        public void AddGroupButton(Menu_Sort_Button button) {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.Children.Add(button);
            }));
        }
        public bool IsButtonInOriginPos(Menu_Sort_Button button)
        {
            double w = STATICS.MENU_BAR_SIZE.Width;
            double h = STATICS.MENU_BAR_SIZE.Height;
            double x0 = (STATICS.SCREEN_WIDTH - w) / 2;
            double y0 = STATICS.SCREEN_HEIGHT - h;
            double x1 = (STATICS.SCREEN_WIDTH - w) / 2;
            double y1 = 0;
            double x2 = 0;
            double y2 = (STATICS.SCREEN_HEIGHT - w) / 2;
            double x3 = STATICS.SCREEN_WIDTH - h;
            double y3 = (STATICS.SCREEN_HEIGHT - w) / 2;
            Rect rec0 = new Rect(x0, y0, w, h);
            Rect rec1 = new Rect(x1, y1, w, h);
            Rect rec2 = new Rect(x2, y2, h, w);
            Rect rec3 = new Rect(x3, y3, h, w);
            if (rec0.Contains(button.CurrentPosition) || rec1.Contains(button.CurrentPosition) || rec2.Contains(button.CurrentPosition) || rec3.Contains(button.CurrentPosition))
            {
                return true;
            }
            else
                return false;
        }
    }
}
