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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Control_Window controlWindow;
        public Control_Window ControlWindow
        {
            get { return controlWindow; }
            set { controlWindow = value; }
        }
        Touch_Controler touchControler;

        public Touch_Controler TouchControler
        {
            get { return touchControler; }
            set { touchControler = value; }
        }
        Gesture_Controler gestureControler;

        public Gesture_Controler GestureControler
        {
            get { return gestureControler; }
            set { gestureControler = value; }
        }
        Card_Controler cardControler;

        public Card_Controler CardControler
        {
            get { return cardControler; }
            set { cardControler = value; }
        }
        User_Controler userControler;
        public User_Controler UserControler
        {
            get { return userControler; }
            set { userControler = value; }
        }

        Joint_Interest_Controler jointInterestControler;

        public Joint_Interest_Controler JointInterestControler
        {
            get { return jointInterestControler; }
            set { jointInterestControler = value; }
        }


        Card_Layer cardLayer;
        public Card_Layer CardLayer
        {
            get { return cardLayer; }
            set { cardLayer = value; }
        }
        Bottom_Layer bottomLayer;

        public Bottom_Layer BottomLayer
        {
            get { return bottomLayer; }
            set { bottomLayer = value; }
        }
        Sorting_Icon_Controler iconControler;

        internal Sorting_Icon_Controler IconControler
        {
            get { return iconControler; }
            set { iconControler = value; }
        }
        Gesture_Indicator_Layer gestureIndicatorLayer;

        public Gesture_Indicator_Layer GestureIndicatorLayer
        {
            get { return gestureIndicatorLayer; }
            set { gestureIndicatorLayer = value; }
        }
        Linking_Gesture_Layer linkingGestureLayer;

        public Linking_Gesture_Layer LinkingGestureLayer
        {
            get { return linkingGestureLayer; }
            set { linkingGestureLayer = value; }
        }
        Menu_Layer menuLayer;
        private Grouping_Gesture_Layer groupingGestureLayer;

        public Grouping_Gesture_Layer GroupingGestureLayer
        {
            get { return groupingGestureLayer; }
            set { groupingGestureLayer = value; }
        }

        public Menu_Layer MenuLayer
        {
            get { return menuLayer; }
            set { menuLayer = value; }
        }
        Sorting_Gesture_Layer sortingGestureLayer;

        public Sorting_Gesture_Layer SortingGestureLayer
        {
            get { return sortingGestureLayer; }
            set { sortingGestureLayer = value; }
        }

        String layoutFile = "";

        public String LayoutFile
        {
            get { return layoutFile; }
            set { layoutFile = value; }
        }
        public MainWindow()
        {
            InitializeComponent();

            if (System.Windows.Forms.Screen.AllScreens.Length >= 2)
            {

                STATICS.SCREEN_WIDTH = System.Windows.Forms.Screen.AllScreens[1].Bounds.Width;
                STATICS.SCREEN_HEIGHT = System.Windows.Forms.Screen.AllScreens[1].Bounds.Height;
                STATICS.DEAULT_CARD_SIZE = new Size(0.08333 * STATICS.SCREEN_WIDTH, 0.11111 * STATICS.SCREEN_HEIGHT);
                STATICS.DEAULT_CARD_SIZE_WITH_BORDER = new Size(0.08333 * STATICS.SCREEN_WIDTH + 10, 0.11111 * STATICS.SCREEN_HEIGHT + 10); 
                System.Drawing.Rectangle screenBounds = System.Windows.Forms.Screen.AllScreens[1].Bounds;
                this.Left = screenBounds.Left;
                this.Top = screenBounds.Top;                
            }
            else
            {
                STATICS.SCREEN_WIDTH = (int)SystemParameters.PrimaryScreenWidth;
                STATICS.SCREEN_HEIGHT = (int)SystemParameters.PrimaryScreenHeight;
                STATICS.DEAULT_CARD_SIZE = new Size(0.08333 * STATICS.SCREEN_WIDTH, 0.11111 * STATICS.SCREEN_HEIGHT);
                STATICS.DEAULT_CARD_SIZE_WITH_BORDER = new Size(0.08333 * STATICS.SCREEN_WIDTH + 10, 0.11111 * STATICS.SCREEN_HEIGHT + 10);
                this.Width = STATICS.SCREEN_WIDTH;
                this.Height = STATICS.SCREEN_HEIGHT;
                this.WindowState = System.Windows.WindowState.Normal;
                this.Left = 0;
            }
            Stylus.SetIsPressAndHoldEnabled(this, false);
            Stylus.SetIsTapFeedbackEnabled(this, false);
            Stylus.SetIsFlicksEnabled(this, false);
            Stylus.SetIsTouchFeedbackEnabled(this, false);
            Timeline.DesiredFrameRateProperty.OverrideMetadata(typeof(Timeline), new FrameworkPropertyMetadata { DefaultValue = 28 });
            controlWindow = new Control_Window(this);
            controlWindow.Show();

            this.Loaded += Window_Loaded;
            //InitializeControlers();
        }
        public void InitializeControlers()
        {
            STATICS.USER_COLOR["Alex"] = Color.FromArgb(255, 255, 102, 0);
            STATICS.USER_COLOR["Ben"] = Color.FromArgb(255, 0, 255, 0);
            STATICS.USER_COLOR["Chris"] = Color.FromArgb(255, 255, 255, 255);
            STATICS.USER_COLOR["Danny"] = Color.FromArgb(255, 128, 0, 128);


            userControler = new User_Controler(this);
            cardLayer = new Card_Layer(this);
            cardControler = new Card_Controler(this);
            cardControler.LoadCardLayout(layoutFile);
            touchControler = new Touch_Controler(this);
            menuLayer = new Menu_Layer(this);
            iconControler = new Sorting_Icon_Controler(this);
            iconControler.InitializeSortButton();
            gestureIndicatorLayer = new Gesture_Indicator_Layer();
            bottomLayer = new Bottom_Layer();

            linkingGestureLayer = new Linking_Gesture_Layer();
            groupingGestureLayer = new Grouping_Gesture_Layer();
            gestureControler = new Gesture_Controler(this);
            gestureControler.start();
            //For Lab
            //jointInterestControler = new Joint_Interest_Controler(this);//test
            //jointInterestControler.Start();
            sortingGestureLayer = new Sorting_Gesture_Layer(this);

            MainContainer.Children.Add(bottomLayer);
            MainContainer.Children.Add(linkingGestureLayer);
            MainContainer.Children.Add(sortingGestureLayer);
            MainContainer.Children.Add(groupingGestureLayer);
            MainContainer.Children.Add(cardLayer);
            MainContainer.Children.Add(gestureIndicatorLayer);
            MainContainer.Children.Add(menuLayer);
        }
        public void DeinitControlers() {

            MainContainer.Children.Remove(bottomLayer);
            MainContainer.Children.Remove(linkingGestureLayer);
            MainContainer.Children.Remove(sortingGestureLayer);
            MainContainer.Children.Remove(groupingGestureLayer);
            MainContainer.Children.Remove(cardLayer);
            MainContainer.Children.Remove(gestureIndicatorLayer);
            MainContainer.Children.Remove(menuLayer);
            sortingGestureLayer = null;
            gestureControler.quit();
            gestureControler = null;
            groupingGestureLayer = null;
            linkingGestureLayer = null;
            bottomLayer = null;
            gestureIndicatorLayer = null;
            iconControler = null;
            menuLayer = null;
            touchControler = null;
            cardControler = null;
            cardLayer = null;
            userControler.UserList.Clear();
            userControler = null;

            Card_List.CardList.Clear();            
            Gesture_List.GestureList.Clear();
            Group_List.CardGroups.Clear();
            Group_List.GroupButtons.Clear();
            Link_List.CardLinks.Clear();
            Point_List.TouchPointList.Clear();
            
            
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        protected override void OnMouseEnter(MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.None;
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(MouseEventArgs e)
        {
            Mouse.OverrideCursor = Cursors.Arrow;
            base.OnMouseLeave(e);
        }
    }
}
