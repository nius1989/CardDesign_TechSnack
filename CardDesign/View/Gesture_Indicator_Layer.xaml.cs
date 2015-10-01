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
    /// Interaction logic for Animation_Layer.xaml
    /// </summary>
    public partial class Gesture_Indicator_Layer : Canvas
    {
        Dictionary<int, Ellipse> _Followers = new Dictionary<int, Ellipse>();
        public Gesture_Indicator_Layer()
        {
            InitializeComponent();
            this.IsManipulationEnabled = false;
            this.IsHitTestVisible = false;
            this.Height = STATICS.SCREEN_HEIGHT;
            this.Width = STATICS.SCREEN_WIDTH;
        }

        public void Add(int id, Point point)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_Followers.Count < STATICS.MAX_TOUCH_POINT)
                {
                    Ellipse follower = new Ellipse();
                    follower.Width = follower.Height = 50;
                    follower.Fill = Brushes.Transparent;
                    follower.Stroke = Brushes.Goldenrod;
                    follower.StrokeThickness = 5;
                    follower.Opacity = 1;
                    follower.RenderTransform = new TranslateTransform(point.X - 25, point.Y - 25);
                    _Followers[id] = follower;
                    this.Children.Add(follower);
                }
            }));
        }

        public void Remove(int id)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_Followers.ContainsKey(id))
                {
                    Ellipse follower = _Followers[id];
                    this.Children.Remove(follower);
                    _Followers.Remove(id);
                }
            }));
        }
        public bool Contain(int id)
        {
            return _Followers.ContainsKey(id);
        }
        public void Move(int id, Point point)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (_Followers.ContainsKey(id))
                {
                    Ellipse follower = _Followers[id];
                    TranslateTransform transform = follower.RenderTransform as TranslateTransform;
                    transform.X = point.X - 25;
                    transform.Y = point.Y - 25;
                }
            }));
        }
        public void Clear()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (this.Children.Count > 0)
                {
                    this.Children.Clear();
                    _Followers = new Dictionary<int, Ellipse>();
                }
            }));
        }
    }
}
