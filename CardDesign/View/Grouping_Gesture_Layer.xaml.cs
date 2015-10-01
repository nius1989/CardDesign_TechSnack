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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardDesign
{
    /// <summary>
    /// Interaction logic for Clustering_Gesture_Layer.xaml
    /// </summary>
    public partial class Grouping_Gesture_Layer : Canvas
    {
        Dictionary<Gesture_Event_Grouping, Line[]> groupLinks = new Dictionary<Gesture_Event_Grouping, Line[]>();

        private Dictionary<Gesture_Event_Grouping, Line[]> ClusterLinks
        {
            get { return groupLinks; }
            set { groupLinks = value; }
        }
        public Grouping_Gesture_Layer()
        {
            InitializeComponent();
            this.IsManipulationEnabled = false;
            this.IsHitTestVisible = false;
            this.Background = null;
        }
        public void Add(Gesture_Event_Grouping group)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                
                Line[] follower = new Line[4];
                for (int i = 0; i < 4; i++)
                {
                    follower[i] = new Line();
                    follower[i].X1 = group.Points[i].CurrentPoint.Position.X;
                    follower[i].Y1 = group.Points[i].CurrentPoint.Position.Y;
                    follower[i].X2 = group.Points[(i + 1) % 4].CurrentPoint.Position.X;
                    follower[i].Y2 = group.Points[(i + 1) % 4].CurrentPoint.Position.Y;
                    follower[i].Stroke = new SolidColorBrush(Colors.Goldenrod);
                    follower[i].StrokeThickness = 3;
                    this.Children.Add(follower[i]);
                    DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION));
                    follower[i].BeginAnimation(Canvas.OpacityProperty, animation);
                }
                groupLinks[group] = follower;
            }));
        }
        public void Move(Gesture_Event_Grouping cluster) {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Line[] follower = groupLinks[cluster];
                for (int i = 0; i < 4; i++)
                {
                    follower[i].X1 = cluster.Points[i].CurrentPoint.Position.X;
                    follower[i].Y1 = cluster.Points[i].CurrentPoint.Position.Y;
                    follower[i].X2 = cluster.Points[(i + 1) % 4].CurrentPoint.Position.X;
                    follower[i].Y2 = cluster.Points[(i + 1) % 4].CurrentPoint.Position.Y;
                }
            }));
        }
        public void Remove(Gesture_Event_Grouping cluster)
        {

            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (groupLinks.ContainsKey(cluster))
                {
                    Dictionary<Gesture_Event_Grouping, Line[]> local = groupLinks;
                    Line[] follower = local[cluster];
                    for (int i = 0; i < 4; i++)
                    {
                        DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION));
                        follower[i].BeginAnimation(Canvas.OpacityProperty, animation);
                        this.Children.Remove(follower[i]);
                    }
                    groupLinks.Remove(cluster);
                }
            }));
        }
    }
}
