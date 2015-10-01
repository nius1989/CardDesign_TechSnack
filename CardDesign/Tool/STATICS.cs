using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace CardDesign
{
    class STATICS
    {
        public static bool DEBUG_MODE = false;
        public static int SCREEN_WIDTH = 1920;
        public static int SCREEN_HEIGHT = 1080;
        public static Size DEAULT_CARD_SIZE = new Size(160,120);//default card size
        public static Size DEAULT_CARD_SIZE_WITH_BORDER = new Size(170,130);//default card size
        public static double POINT_REFRESH_RATE = 100;//refresh the data every "refreshRate" millionseconds
        public static int GESTURE_REFRESH_RATE = 80;
        public static int MAX_TOUCH_POINT = 12;//Maximum Touch Points Allowed
        public static double MIN_GESTURE_LIFE = 0;//detect gesture after xx millionseconds
        public static double MIN_DISTANCE_FOR_MOVE = 30;
        public static double MIN_LONG_PRESS_LIFE = 300;//Min time to active emphasizing gesture
        public static double MIN_SHOW_LIFE = 1000;//Min time to active showing gesture
        public static double START_CARD_BRIGHT = 0.3;
        public static int CARD_NUMBER = 32;
        public static double MAX_CARD_SCALE = 8;
        public static double MIN_CARD_SCALE = 0.67;
        public static double ANIMATION_DURATION = 0.5;
        public static double JOINT_INTEREST_DURATION = 60;//60 seconds
        public static Dictionary<string, Color> USER_COLOR = new Dictionary<string, Color>();
        public static bool ALEX_ACTIVE = true;
        public static bool BEN_ACTIVE = true;
        public static bool CHRIS_ACTIVE = false;
        public static bool DANNY_ACTIVE = false;
        public static int USER_NUMER = 4;
        public static int CLUSTER_BUTTON_NUM = 7;
        public static Size MENU_BAR_SIZE = new Size(600, 50);
        public static int BUTTON_PER_USER = 3;
    }
}
