using System;
//using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for Card_Layer.xaml
    /// </summary>
    public partial class Card_Layer : Canvas
    {
        MainWindow mainWindow;

        public MainWindow MainWindow
        {
            get { return mainWindow; }
            set { mainWindow = value; }
        }
        public Card_Layer(MainWindow mainWindow)
        {
            InitializeComponent();
            this.IsManipulationEnabled = false;
            this.mainWindow = mainWindow;
            this.Height = STATICS.SCREEN_HEIGHT;
            this.Width = STATICS.SCREEN_WIDTH;
        }
        public void AddCard(Card card)
        {
            this.Children.Add(card);
        }
        protected override void OnTouchDown(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(this);
            mainWindow.TouchControler.TouchDown(this, this.GetType(), e.TouchDevice.Id, point);
            mainWindow.ControlWindow.UpdateTextInfo(mainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            base.OnTouchDown(e);
        }

        protected override void OnTouchMove(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(this);
            mainWindow.TouchControler.TouchMove(this, this.GetType(), e.TouchDevice, point);
            if(STATICS.DEBUG_MODE)
                mainWindow.ControlWindow.UpdateTextInfo(mainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            base.OnTouchMove(e);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(this);
            mainWindow.TouchControler.TouchUp(e.TouchDevice, point);
            mainWindow.ControlWindow.UpdateTextInfo(mainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            base.OnTouchUp(e);
        }
        protected override void OnManipulationStarting(ManipulationStartingEventArgs e)
        {
            e.ManipulationContainer = this;
            // Adjust Z-order
            Card element = e.Source as Card;
            if (element.ZIndex < this.Children.Count - 1)
            {
                foreach (FrameworkElement child in this.Children)
                {
                    Card c = child as Card;
                    if (element.ZIndex < c.ZIndex)
                    {
                        Canvas.SetZIndex(c, --c.ZIndex);
                    }
                }
                Canvas.SetZIndex(element, this.Children.Count - 1);
                element.ZIndex = this.Children.Count - 1;
            }
            e.Handled = true;
            base.OnManipulationStarting(e);
        }
        protected override void OnManipulationDelta(ManipulationDeltaEventArgs e)
        {
            Card element = e.Source as Card;
            ManipulationDelta delta = e.DeltaManipulation;
            Point center = e.ManipulationOrigin;
            Matrix matrix = new Matrix();
            if (STATICS.MIN_CARD_SCALE < element.CurrentScale * delta.Scale.X && element.CurrentScale * delta.Scale.X < STATICS.MAX_CARD_SCALE)
            {
                element.CurrentScale = element.CurrentScale * delta.Scale.X;
                matrix.Scale(element.CurrentScale, element.CurrentScale);
            }
            else
            {
                matrix.Scale(element.CurrentScale, element.CurrentScale);
            }
            element.CurrentPosition = new Point(element.CurrentPosition.X + delta.Translation.X, element.CurrentPosition.Y + delta.Translation.Y);
            element.CurrentRotation += delta.Rotation;
            
            matrix.Rotate(element.CurrentRotation);
            matrix.Translate(element.CurrentPosition.X, element.CurrentPosition.Y);

            element.RenderTransform = new MatrixTransform(matrix);

            mainWindow.LinkingGestureLayer.Move(element);
            e.Handled = true;
            base.OnManipulationDelta(e);
        }
    }
}
