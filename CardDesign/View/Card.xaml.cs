using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CardDesign
{
    /// <summary>
    /// Interaction logic for Card.xaml
    /// A class of the card
    /// </summary>
    public partial class Card : Canvas
    {

        Card_Controler cardControler;
        int uID = 0;
        int zIndex = 0;
        String owner = "";
        String cardText = "";
        Rectangle background = new Rectangle();
        Rectangle hightlightMask = new Rectangle();
        Image image = new Image();
        TextBlock topText = new TextBlock();
        TextBlock bottomText = new TextBlock();
        TextBlock sortingGroupText = new TextBlock();
        Linking_Icon linkingIcon;
        Copy_Icon copyIcon;
        String imgFile;
        Color backgroundColor;
        Color hightlightColor = Colors.Gold;
        double brightness = STATICS.START_CARD_BRIGHT;
        DoubleAnimation highlightAnim;
        Point defaultPostion = new Point(0, 0);
        Point previousPostion = new Point(0, 0);
        Point currentPosition = new Point(0, 0);
        double defaultRotation = 0;
        double currentScale = 1;
        double currentRotation = 0;
        int touchPointNum = 0;

        bool isJointInterested = false;
        List<String> sortingGroups = new List<string>();

        public Card_Controler CardControler
        {
            get { return cardControler; }
            set { cardControler = value; }
        }
        public double CurrentScale
        {
            get { return currentScale; }
            set { currentScale = value; }
        }
        public String Owner
        {
            get { return owner; }
            set { owner = value; }
        }
        public int ZIndex
        {
            get { return zIndex; }
            set { zIndex = value; }
        }
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set { backgroundColor = value; }
        }
        public Color HightlightColor
        {
            get { return hightlightColor; }
            set { hightlightColor = value; }
        }
        public int UID
        {
            get { return uID; }
            set { uID = value; }
        }
        public String ImgFile
        {
            get { return imgFile; }
            set { imgFile = value; }
        }
        public Point CurrentPosition
        {
            get { return currentPosition; }
            set { currentPosition = value; }
        }

        public bool IsJointInterested
        {
            get { return isJointInterested; }
            set { isJointInterested = value; }
        }

        public double Brightness
        {
            get { return brightness; }
            set { brightness = value; }
        }
        public String CardText
        {
            get { return cardText; }
            set { cardText = value; }
        }
        public List<String> SortingGroups
        {
            get { return sortingGroups; }
            set { sortingGroups = value; }
        }
        public double CurrentRotation
        {
            get { return currentRotation; }
            set { currentRotation = value; }
        }
        public Point PreviousPostion
        {
            get { return previousPostion; }
            set { previousPostion = value; }
        }
        public Card(Card_Controler cardControl)
        {
            InitializeComponent();
            this.IsManipulationEnabled = true;
            this.cardControler = cardControl;

            this.Width = STATICS.DEAULT_CARD_SIZE_WITH_BORDER.Width;
            this.Height = STATICS.DEAULT_CARD_SIZE_WITH_BORDER.Height;
        }
        public void InitializeCard(String imgFile, String cardTxt, Color maskColor, Point defaultPosi, double defaultDegree, double defaultScale, int zidx)
        {
            SetCardImage(imgFile);

            this.backgroundColor = maskColor;
            background.Width = STATICS.DEAULT_CARD_SIZE.Width;
            background.Height = STATICS.DEAULT_CARD_SIZE.Height;
            background.Opacity = 1;
            background.StrokeThickness = 0;
            background.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1,
                       -background.Width / 2,
                       -background.Height / 2));
            background.Opacity = 0.9;
            Rectangle shine = new Rectangle();
            shine.Width = STATICS.DEAULT_CARD_SIZE.Width;
            shine.Height = STATICS.DEAULT_CARD_SIZE.Height;
            shine.Opacity = 0.3;
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = new BitmapImage(new Uri(@"Resource\Image\card_bg.png", UriKind.Relative));
            shine.Fill = ib;
            shine.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1,
                       -background.Width / 2,
                       -background.Height / 2));

            ResetBrightness();

            this.cardText = cardTxt;
            topText.Text = this.cardText;
            bottomText.Text = this.cardText;

            topText.Foreground = new SolidColorBrush(Colors.White);
            topText.FontSize = 16;
            topText.Width = background.Width;
            topText.TextWrapping = TextWrapping.WrapWithOverflow;
            topText.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1, -background.Width / 2 + 7, -background.Height / 2 + 3));

            sortingGroupText.Foreground = new SolidColorBrush(Colors.Black);
            sortingGroupText.FontSize = 14;
            sortingGroupText.FontFamily = new FontFamily("Quartz MS");
            sortingGroupText.TextWrapping = TextWrapping.WrapWithOverflow;
            sortingGroupText.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1, -background.Width / 2 + 7, -background.Height / 2 - 14));

            Matrix matrix = Matrix.Identity;
            matrix.Translate(-background.Width / 2, -background.Height / 2 + 5);
            matrix.Rotate(180);
            matrix.Translate(-5, 9);
            bottomText.RenderTransform = new MatrixTransform(matrix);
            bottomText.Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            bottomText.FontSize = 16;
            bottomText.Width = background.Width;
            bottomText.TextWrapping = TextWrapping.WrapWithOverflow;

            hightlightMask.Width = this.Width;
            hightlightMask.Height = this.Height;
            hightlightMask.Fill = Brushes.Transparent;
            this.hightlightMask.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1,
                       -background.Width / 2,
                       -background.Height / 2));


            linkingIcon = new Linking_Icon(this);
            linkingIcon.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1,
                -linkingIcon.Width / 2,
                -(background.Height - linkingIcon.Height / 2)));
            linkingIcon.Opacity = 0;

            copyIcon = new Copy_Icon(this);
            copyIcon.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1,
                -copyIcon.Width / 2,
                -copyIcon.Height / 2));
            copyIcon.Opacity = 0;

            this.Container.Children.Add(hightlightMask);
            this.Container.Children.Add(background);
            this.Container.Children.Add(shine);
            this.Container.Children.Add(image);
            this.Container.Children.Add(topText);
            this.Container.Children.Add(bottomText);
            this.Container.Children.Add(sortingGroupText);

            defaultPostion = defaultPosi;
            defaultRotation = defaultDegree;
            positCard(defaultPostion);
            rotateCard(defaultRotation);
            scaleCard(defaultScale);
            this.zIndex = zidx;
        }
        public bool Contain(Point p)
        {
            return Calculator.CalDistance(currentPosition, p) < STATICS.DEAULT_CARD_SIZE.Width / 2;
        }
        public void ChangeBrightness(double nsize, double osize)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                double rate = 5000.0;
                double opacitychange = (nsize - osize) / rate;
                double newOpacity = brightness + opacitychange;
                if (newOpacity > 1)
                {
                    newOpacity = 1;
                }
                else if (newOpacity < STATICS.START_CARD_BRIGHT)//if dehight
                {
                    isJointInterested = false;
                    newOpacity = STATICS.START_CARD_BRIGHT;
                }
                brightness = newOpacity;
                updateMaskColor();
            }));
        }
        public void ResetBrightness()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                updateMaskColor();
            }));
        }
        public void SetCardImage(String file)
        {
            imgFile = file;
            Dispatcher.BeginInvoke(new Action(() =>
            {
                BitmapImage img = new BitmapImage(new Uri(imgFile, UriKind.RelativeOrAbsolute));
                image.Height = 60;
                image.Width = image.Height * img.Width / img.Height;
                image.Source = img;
                //image.Opacity = STATICS.START_CARD_BRIGHT; 
                double offsetV = (STATICS.DEAULT_CARD_SIZE.Height - image.Height) / 2;
                double offsetH = (STATICS.DEAULT_CARD_SIZE.Width - image.Width) / 2;
                image.RenderTransform = new MatrixTransform(new Matrix(1, 0, 0, 1, -STATICS.DEAULT_CARD_SIZE.Width / 2, -STATICS.DEAULT_CARD_SIZE.Height / 2));
            }));
        }
        public void MoveCard(double x, double y, double duration)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Matrix fromMatrix = (this.RenderTransform as MatrixTransform).Matrix;
                Matrix toMatrix = new Matrix();
                toMatrix.Rotate(currentRotation);
                toMatrix.Scale(currentScale, currentScale);
                toMatrix.Translate(currentPosition.X + x, currentPosition.Y + y);
                if (duration > 0)
                {
                    LinearMatrixAnimation anim = new LinearMatrixAnimation(fromMatrix, toMatrix, TimeSpan.FromSeconds(duration));
                    MatrixTransform trans = new MatrixTransform();
                    this.RenderTransform.BeginAnimation(MatrixTransform.MatrixProperty, anim);
                }
                else
                {
                    this.RenderTransform = new MatrixTransform(toMatrix);
                }
                this.currentPosition = new Point(this.currentPosition.X + x, this.currentPosition.Y + y);
            }));
        }
        public void MoveCardTo(double x, double y, double duration)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                Matrix fromMatrix = (this.RenderTransform as MatrixTransform).Matrix;
                Matrix toMatrix = new Matrix();
                toMatrix.Rotate(currentRotation);
                toMatrix.Scale(currentScale, currentScale);
                toMatrix.Translate(x, y);
                if (duration > 0)
                {
                    LinearMatrixAnimation anim = new LinearMatrixAnimation(fromMatrix, toMatrix, TimeSpan.FromSeconds(duration));
                    MatrixTransform trans = new MatrixTransform();
                    this.RenderTransform.BeginAnimation(MatrixTransform.MatrixProperty, anim);
                }
                else
                {
                    this.RenderTransform = new MatrixTransform(toMatrix);
                }
                this.currentPosition = new Point(x, y);
            }));
        }
        private void positCard(Point point)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                currentPosition.X += point.X;
                currentPosition.Y += point.Y;
                MatrixTransform xform = this.RenderTransform as MatrixTransform;
                Matrix matrix = xform.Matrix;
                matrix.Translate(point.X, point.Y);
                this.RenderTransform = new MatrixTransform(matrix);
            }), System.Windows.Threading.DispatcherPriority.Normal);
        }
        private void rotateCard(double angle)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MatrixTransform xform = this.RenderTransform as MatrixTransform;
                Matrix matrix = xform.Matrix;
                matrix.RotateAt(angle,
                    matrix.OffsetX,
                matrix.OffsetY);
                currentRotation += angle;
                this.RenderTransform = new MatrixTransform(matrix);
            }));
        }
        private void scaleCard(double scale) {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                MatrixTransform xform = this.RenderTransform as MatrixTransform;
                Matrix matrix = xform.Matrix;
                matrix.ScaleAt(scale,scale,
                    matrix.OffsetX,
                matrix.OffsetY);
                currentScale = scale;
                this.RenderTransform = new MatrixTransform(matrix);
            }));
        }
        public void Hightlight(Color color)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                this.hightlightMask.Fill = new SolidColorBrush(color); ;
                DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
                this.hightlightMask.BeginAnimation(Canvas.OpacityProperty, animation);
            }));
        }
        public void Hightlight()
        {
            Dispatcher.BeginInvoke(new Action(() =>
               {
                   if (!isJointInterested)
                   {
                       this.hightlightMask.Fill = new SolidColorBrush(hightlightColor);
                       DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
                       this.hightlightMask.BeginAnimation(Canvas.OpacityProperty, animation);

                   }
               }));
        }

        public void Dehightlight()
        {
            Dispatcher.BeginInvoke(new Action(() =>
               {
                   if (!isJointInterested)
                   {
                       DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
                       this.hightlightMask.BeginAnimation(Canvas.OpacityProperty, animation);
                   }
               }));
        }

        public void HightlightJointInterest()
        {
            Dispatcher.BeginInvoke(new Action(() =>
           {
               this.hightlightMask.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 215, 0));
               highlightAnim = new DoubleAnimation(0, 0.8, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 5));
               highlightAnim.RepeatBehavior = RepeatBehavior.Forever;
               this.hightlightMask.BeginAnimation(Canvas.OpacityProperty, highlightAnim);
               isJointInterested = true;
           }));
        }
        public void DehightJointInterest()
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                //this.hightlightMask.Fill = new SolidColorBrush(hightlightColor);
                DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
                this.hightlightMask.BeginAnimation(Canvas.OpacityProperty, animation);
            }));
        }
        public void SortToGroup(String groupID)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (!sortingGroups.Contains(groupID))
                {
                    sortingGroups.Add(groupID);
                    String str = "";
                    foreach (String s in sortingGroups)
                    {
                        str += Group_List.GroupButtons[s].GroupTextBrief+" ";
                    }
                    sortingGroupText.Text = str;
                }
            }));
        }
        public void RemoveFromGroup(String groupNum)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                if (sortingGroups.Contains(groupNum))
                {
                    sortingGroups.Remove(groupNum);
                    String str = "";
                    foreach (String s in sortingGroups)
                    {
                        str += Group_List.GroupButtons[s].GroupTextBrief + " ";
                    }
                    sortingGroupText.Text = str;
                }
            }));
        }
        private void updateMaskColor()
        {
            background.Fill = new SolidColorBrush(Color.FromArgb(255,
               (byte)(backgroundColor.R * (brightness)),
               (byte)(backgroundColor.G * (brightness)),
               (byte)(backgroundColor.B * (brightness))));
        }
        protected override void OnTouchDown(TouchEventArgs e)
        {
            this.CaptureTouch(e.TouchDevice);
            TouchPoint point = e.GetTouchPoint(cardControler.MainWindow.CardLayer);
            cardControler.MainWindow.TouchControler.TouchDown(this, this.GetType(), e.TouchDevice.Id, point);
            cardControler.MainWindow.ControlWindow.UpdateTextInfo(cardControler.MainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            Hightlight();
            previousPostion = this.currentPosition;
            //touchPointNum++;
            if (touchPointNum == 3)
            {
                if (!this.Container.Children.Contains(linkingIcon))
                {
                    this.Container.Children.Add(linkingIcon);
                }

                if (!this.Container.Children.Contains(copyIcon))
                {
                    this.Container.Children.Add(copyIcon);
                }
                DoubleAnimation animation = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
                linkingIcon.BeginAnimation(Canvas.OpacityProperty, animation);
                copyIcon.BeginAnimation(Canvas.OpacityProperty, animation);
            }
            base.OnTouchDown(e);
        }
        protected override void OnTouchMove(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(cardControler.MainWindow.CardLayer);
            cardControler.MainWindow.TouchControler.TouchMove(this, this.GetType(), e.TouchDevice, point);
            if (STATICS.DEBUG_MODE)
            {
                cardControler.MainWindow.ControlWindow.UpdateTextInfo(cardControler.MainWindow.TouchControler.ToString(), 1);
            }
            e.Handled = true;
            base.OnTouchMove(e);
        }

        protected override void OnTouchUp(TouchEventArgs e)
        {
            TouchPoint point = e.GetTouchPoint(cardControler.MainWindow.CardLayer);
            cardControler.MainWindow.TouchControler.TouchUp(e.TouchDevice, point);
            cardControler.MainWindow.ControlWindow.UpdateTextInfo(cardControler.MainWindow.TouchControler.ToString(), 1);
            e.Handled = true;
            Dehightlight();
            //touchPointNum--;
            if (touchPointNum != 3)
            {
                DoubleAnimation animation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(STATICS.ANIMATION_DURATION * 0.3));
                linkingIcon.BeginAnimation(Canvas.OpacityProperty, animation);
                copyIcon.BeginAnimation(Canvas.OpacityProperty, animation);
                if (this.Container.Children.Contains(linkingIcon))
                {
                    this.Container.Children.Remove(linkingIcon);
                }
                if (this.Container.Children.Contains(copyIcon))
                {
                    this.Container.Children.Remove(copyIcon);
                }
            }
            base.OnTouchUp(e);
        }
        public override string ToString()
        {
            String result = "";
            Dispatcher.BeginInvoke(new Action(() =>
            {
                result = this.uID + " " + this.Owner + " " + this.topText;
            }), System.Windows.Threading.DispatcherPriority.Send);
            return base.ToString();
        }
    }
}