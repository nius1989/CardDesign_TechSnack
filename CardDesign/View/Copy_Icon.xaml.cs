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
    /// Interaction logic for Copy_Icon.xaml
    /// </summary>
    public partial class Copy_Icon : Canvas
    {
        Card card;

        public Card Card
        {
            get { return card; }
            set { card = value; }
        }
        public Copy_Icon(Card card)
        {
            InitializeComponent();
            this.card = card;
            ImageBrush brush = new ImageBrush();
            BitmapImage img = new BitmapImage(new Uri(@"Resource\Image\copy_icon.png", UriKind.RelativeOrAbsolute));
            brush.ImageSource = img;
            rect.Fill = brush;
        }

        protected override void OnTouchDown(TouchEventArgs e)
        {
            this.CaptureTouch(e.TouchDevice);
            TouchPoint point = e.GetTouchPoint(card.CardControler.MainWindow.CardLayer);
            card.CardControler.MainWindow.TouchControler.TouchDown(this, this.GetType(), e.TouchDevice.Id, point);
            Matrix mtx = (this.RenderTransform as MatrixTransform).Matrix;
            mtx.ScaleAt(1.5, 1.5, this.Width / 2, this.Height / 2);
            this.RenderTransform = new MatrixTransform(mtx);
            card.CardControler.MainWindow.ControlWindow.UpdateTextInfo(card.CardControler.MainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            base.OnTouchMove(e);
        }

        protected override void OnTouchMove(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(card.CardControler.MainWindow.CardLayer);
            card.CardControler.MainWindow.TouchControler.TouchMove(this, this.GetType(), e.TouchDevice, point);
            if (STATICS.DEBUG_MODE)
            {
                card.CardControler.MainWindow.ControlWindow.UpdateTextInfo(card.CardControler.MainWindow.TouchControler.ToString(), 1);
            }
            e.Handled = true;
            base.OnTouchMove(e);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(card.CardControler.MainWindow.CardLayer);
            card.CardControler.MainWindow.TouchControler.TouchUp(e.TouchDevice, point);
            card.CardControler.MainWindow.ControlWindow.UpdateTextInfo(card.CardControler.MainWindow.TouchControler.ToString(), 1);
            Matrix mtx = (this.RenderTransform as MatrixTransform).Matrix;
            mtx.ScaleAt(1.0 / 1.5, 1.0 / 1.5, this.Width / 2, this.Height/2);
            this.RenderTransform = new MatrixTransform(mtx);
            e.Handled = true;
            base.OnTouchUp(e);
        }
    }
}
