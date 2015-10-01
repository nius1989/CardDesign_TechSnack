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
    /// Interaction logic for NameClustering_Gesture_Layer.xaml
    /// </summary>
    public partial class Sorting_Gesture_Layer : Canvas
    {
        Dictionary<String, Line[]> groupLinks = new Dictionary<String, Line[]>();

        public Dictionary<String, Line[]> GroupLinks
        {
            get {
                return groupLinks;
            }
            set { groupLinks = value; }
        }
        MainWindow mainWindow;
        public Sorting_Gesture_Layer(MainWindow mainwin)
        {
            this.mainWindow = mainwin;
            InitializeComponent();
            this.IsHitTestVisible = false;
            this.IsManipulationEnabled = false;
        }
        public void ShowLines(String key)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Group_List.CardGroups.ContainsKey(key))
                {
                    Point center = Group_List.GroupButtons[key].CurrentPosition;
                    Card[] cards = Group_List.CardGroups[key].ToArray();
                    if (cards != null && cards.Length > 0)
                    {
                        Line[] lines = new Line[cards.Length];
                        for (int i = 0; i < cards.Length; i++)
                        {
                            lines[i] = new Line();
                            lines[i].X1 = cards[i].CurrentPosition.X;
                            lines[i].Y1 = cards[i].CurrentPosition.Y;
                            lines[i].X2 = center.X;
                            lines[i].Y2 = center.Y;
                            lines[i].Stroke = new SolidColorBrush(Colors.LightBlue);
                            lines[i].StrokeThickness = 3;
                            lines[i].Opacity = 0.8;
                            this.Children.Add(lines[i]);
                        }
                        groupLinks[key] = lines;
                    }
                }
            }));
        }
        public void RemoveLines(String key)
        {
            Dispatcher.BeginInvoke(new Action(() =>
               {
                   if (groupLinks.ContainsKey(key))
                   {
                       Line[] lines = groupLinks[key];
                       for (int i = 0; i < lines.Length; i++)
                       {
                           this.Children.Remove(lines[i]);
                       }
                       groupLinks.Remove(key);
                   }
               }));
        }
        public void RemoveAll() {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                foreach (String key in groupLinks.Keys)
                {
                    RemoveLines(key);
                }
            }));
        }

        public void Repaint()
        {
            String[] keys = groupLinks.Keys.ToArray();
            foreach (String key in keys) {
                RemoveLines(key);
                ShowLines(key);
            }
        }
    }
}
