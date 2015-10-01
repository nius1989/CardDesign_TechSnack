using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardDesign
{
    /// <summary>
    /// Interaction logic for Cluster_Button.xaml
    /// </summary>
    public partial class Menu_Sort_Button : Canvas
    {

        FileInfo[] fileInfo = new FileInfo[]{ 
            new FileInfo(@"Resource\Image\orange_button.png"),
        new FileInfo(@"Resource\Image\green_button.png"),
        new FileInfo(@"Resource\Image\white_button.png"),
        new FileInfo(@"Resource\Image\purple_button.png"),
        new FileInfo(@"Resource\Image\blue_button.png")};
        //User owner;
        String groupID;
        Point currentPosition = new Point();
        Menu_Layer menuLayer;
        String groupText;
        String groupTextBrief;

        public String GroupText
        {
            get { return groupText; }
            set { groupText = value; }
        }

        public String GroupTextBrief
        {
            get { return groupTextBrief; }
            set { groupTextBrief = value; }
        }

        public Point CurrentPosition
        {
            get { return currentPosition; }
        }
        public String GroupID
        {
            get { return groupID; }
            set { groupID = value; }
        }

        public Menu_Sort_Button(Menu_Layer menuLayer, String owner, String id, String text, String textbrief)
        {
            InitializeComponent();
            ImageBrush brush = new ImageBrush();
            switch (owner)
            {
                case "Alex":
                    brush.ImageSource = new BitmapImage(new Uri(fileInfo[0].FullName, UriKind.RelativeOrAbsolute));
                    break;
                case "Ben":
                    brush.ImageSource = new BitmapImage(new Uri(fileInfo[1].FullName, UriKind.RelativeOrAbsolute));
                    break;
                case "Chris":
                    brush.ImageSource = new BitmapImage(new Uri(fileInfo[2].FullName, UriKind.RelativeOrAbsolute));
                    break;
                case "Danny":
                    brush.ImageSource = new BitmapImage(new Uri(fileInfo[3].FullName, UriKind.RelativeOrAbsolute));
                    break;
                default:
                    brush.ImageSource = new BitmapImage(new Uri(fileInfo[4].FullName, UriKind.RelativeOrAbsolute));
                    break;
            }
            //rect.Fill = brush;
            //this.owner = null;
            this.groupID = id;
            this.ButtonText1.Text = text;
            this.ButtonText2.Text = text;
            this.groupText = text;
            this.groupTextBrief = textbrief;
            this.menuLayer = menuLayer;
        }
        public void SetStartPosition(double x, double y) {
            currentPosition = new Point(x, y);
        }
        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this.Parent as IInputElement;
        }
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            Menu_Sort_Button element = e.Source as Menu_Sort_Button;
            //For Lab
            //ManipulationDelta delta = e.DeltaManipulation;
            //Point center = e.ManipulationOrigin;
            //MatrixTransform mxTrans = element.RenderTransform as MatrixTransform;
            //Matrix matrix = mxTrans.Matrix;
            //currentPosition.X += delta.Translation.X;
            //currentPosition.Y += delta.Translation.Y;
            ////matrix.RotateAt(delta.Rotation, currentPosition.X + this.Width / 2, currentPosition.Y + this.Height / 2);
            //matrix.Translate(delta.Translation.X,delta.Translation.Y);
            //element.RenderTransform = new MatrixTransform(matrix);
            
            e.Handled = true;
            base.OnManipulationDelta(e);
        }
        protected override void OnTouchDown(TouchEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(1, 0.5, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
            this.BeginAnimation(Canvas.OpacityProperty, animation);
            this.CaptureTouch(e.TouchDevice);
            TouchPoint point = e.GetTouchPoint(menuLayer);
            menuLayer.MainWindow.TouchControler.TouchDown(this, this.GetType(), e.TouchDevice.Id, point);
            menuLayer.MainWindow.ControlWindow.UpdateTextInfo(menuLayer.MainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            base.OnTouchDown(e);
        }
        protected override void OnTouchMove(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(menuLayer);
            menuLayer.MainWindow.TouchControler.TouchMove(this, this.GetType(), e.TouchDevice, point);
            e.Handled = true;
            base.OnTouchMove(e);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            DoubleAnimation animation = new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
            this.BeginAnimation(Canvas.OpacityProperty, animation);
            TouchPoint point = e.GetTouchPoint(menuLayer);
            menuLayer.MainWindow.TouchControler.TouchUp(e.TouchDevice, point);
            menuLayer.MainWindow.ControlWindow.UpdateTextInfo(menuLayer.MainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            base.OnTouchUp(e);
        }
    }
}
