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
    /// Interaction logic for Linking_Gesture_Layer.xaml
    /// </summary>
    public partial class Linking_Gesture_Layer : Canvas
    {

        Dictionary<Gesture_Event_Linking, Line> cardLinks = new Dictionary<Gesture_Event_Linking, Line>();

        public Dictionary<Gesture_Event_Linking, Line> CardLinks
        {
            get { return cardLinks; }
            set { cardLinks = value; }
        }
        FileInfo[] fileInfo = new FileInfo[]{ 
            new FileInfo(@"Resource\Image\arrow1.png"),
        new FileInfo(@"Resource\Image\arrow2.png"),
        new FileInfo(@"Resource\Image\arrow3.png"),
        new FileInfo(@"Resource\Image\arrow4.png")};
        Dictionary<String,BitmapImage> arrowImage=new Dictionary<string,BitmapImage>();
        Dictionary<Line, Image[]> arrows = new Dictionary<Line, Image[]>();
        public Linking_Gesture_Layer()
        {
            InitializeComponent();
            this.IsManipulationEnabled = false;
            this.IsHitTestVisible = false;
            this.Background = null;
            arrowImage["Alex"] = new BitmapImage(new Uri(fileInfo[0].FullName));
            arrowImage["Ben"] = new BitmapImage(new Uri(fileInfo[1].FullName));
            arrowImage["Chris"] = new BitmapImage(new Uri(fileInfo[2].FullName));
            arrowImage["Danny"] = new BitmapImage(new Uri(fileInfo[3].FullName));
            this.Height = STATICS.SCREEN_HEIGHT;
            this.Width = STATICS.SCREEN_WIDTH;
        }

        public void Add(Gesture_Event_Linking link)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Gesture_Event_Linking temp = null;
                foreach (Gesture_Event_Linking gesture in cardLinks.Keys) {
                    if (gesture.Equals(link)) {
                        temp = gesture;
                    }
                }
                if (temp != null)
                {
                    Remove(temp);
                    return;
                }
                Line follower = new Line();
                follower.X1 = link.Card1.CurrentPosition.X;
                follower.Y1 = link.Card1.CurrentPosition.Y;

                follower.X2 = link.Points[0].CurrentPoint.Position.X;
                follower.Y2 = link.Points[0].CurrentPoint.Position.Y;
                follower.Stroke = new SolidColorBrush(link.Card1.HightlightColor);
                follower.StrokeThickness = 3;
                follower.Opacity = 1;
                
                cardLinks[link] = follower;
                Link_List.AddLink(link);
                Image image = new Image();
                image.Source = arrowImage[link.Card1.Owner];
                image.Width = 50;
                image.Height = 50;
                image.Stretch = Stretch.Fill;
                arrows[follower] = new Image[] { image };
                moveArrow(follower);
                this.Children.Add(follower);
                this.Children.Add(image);
                Canvas.SetZIndex(image, cardLinks.Count);

                DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION));
                follower.BeginAnimation(Canvas.OpacityProperty, animation);
                image.BeginAnimation(Canvas.OpacityProperty, animation);
                
            }));
        }
        private void moveArrow(Line line) {
            Image img = arrows[line][0];
            Matrix matrix = Matrix.Identity;
            matrix.RotateAt(Math.Atan2(line.Y2 - line.Y1, line.X2 - line.X1) * 180 / Math.PI, 25, 25);
            matrix.Translate((line.X1 + 2 * line.X2) / 3 - 25, (line.Y1 + 2 * line.Y2) / 3 - 25);
            img.RenderTransform = new MatrixTransform(matrix);
        }

        public void Remove(Gesture_Event_Linking link)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Contain(link))
                {
                    Line follower = cardLinks[link];
                    this.Children.Remove(arrows[follower][0]);
                    arrows.Remove(follower);
                    this.Children.Remove(follower);
                    cardLinks.Remove(link);
                    Link_List.RemoveLink(link);
                }
            }));
        }
        public bool Contain(Gesture_Event_Linking link)
        {
            Gesture_Event_Linking temp = null;
            foreach (Gesture_Event_Linking gesture in cardLinks.Keys)
            {
                if (gesture.Equals(link))
                {
                    temp = gesture;
                }
            }
            return temp!=null;
        }
        public void Move(Card card) { 
            Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (Gesture_Event_Linking gesture in cardLinks.Keys)
                {
                    if (gesture.Card1==card||gesture.Card2==card)
                    {
                        Move(gesture);
                    }
                }
            }));
        }
        public void Move(Gesture_Event_Linking link)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (cardLinks.ContainsKey(link))
                {
                    cardLinks[link].X1 = link.Card1.CurrentPosition.X;
                    cardLinks[link].Y1 = link.Card1.CurrentPosition.Y;
                    cardLinks[link].X2 = link.Points[0].CurrentPoint.Position.X;
                    cardLinks[link].Y2 = link.Points[0].CurrentPoint.Position.Y;
                    if (link.Card2 != null) {
                        cardLinks[link].X2 = link.Card2.CurrentPosition.X;
                        cardLinks[link].Y2 = link.Card2.CurrentPosition.Y;
                    }
                    moveArrow(cardLinks[link]);
                }
            }),System.Windows.Threading.DispatcherPriority.Background);
        }

    }
}
