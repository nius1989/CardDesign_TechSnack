using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CardDesign
{
    /// <summary>
    /// The class to initialize the sorting icons
    /// </summary>
    class Sorting_Icon_Controler
    {
        public Sorting_Icon_Controler(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        MainWindow mainWindow;

        public static String[] ButtontText = new String[]{
                "Physical","Mental","Social"
               };

        public static String[] ButtonTextBrif = new String[]{
                "Phy","Men","Soc"
               };


        public MainWindow MainWindow
        {
            get { return mainWindow; }
            set { mainWindow = value; }
        }

        public void InitializeSortButton()
        {
            Menu_Layer menuLayer = mainWindow.MenuLayer;
            int index = 0;

            if (STATICS.ALEX_ACTIVE)
            {
                for (char ch = 'A'; ch <= 'C'; ch++)
                {
                    String groupid = ch.ToString();
                    double x = (STATICS.SCREEN_WIDTH - STATICS.MENU_BAR_SIZE.Width) / 2;
                    //For Lab
                    //double y = STATICS.SCREEN_HEIGHT - STATICS.MENU_BAR_SIZE.Height;
                    double y = STATICS.SCREEN_HEIGHT/2;
                    Group_List.GroupButtons[groupid] = new Menu_Sort_Button(menuLayer, "", "" + ch, ButtontText[index], ButtonTextBrif[index]);
                    Group_List.GroupButtons[groupid].IsManipulationEnabled = true;
                    Group_List.GroupButtons[groupid].IsHitTestVisible = true;
                    //For Lab
                    //Matrix matrix = new Matrix(1, 0, 0, 1, 10 + x + index * Group_List.GroupButtons[groupid].Width, y);
                    Matrix matrix = new Matrix(1, 0, 0, 1, x + index * (STATICS.MENU_BAR_SIZE.Width-Group_List.GroupButtons[groupid].Width)/2, y);
                    Group_List.GroupButtons[groupid].RenderTransform = new MatrixTransform(matrix);
                    Group_List.GroupButtons[groupid].SetStartPosition(matrix.OffsetX + Group_List.GroupButtons[ch.ToString()].Width / 2,
                        matrix.OffsetY + Group_List.GroupButtons[ch.ToString()].Height / 2);
                    menuLayer.AddGroupButton(Group_List.GroupButtons[ch.ToString()]);
                    index++;
                }
            }
            //For Lab
            if (false && STATICS.BEN_ACTIVE)
            {
                for (char ch = 'D'; ch <= 'F'; ch++)
                {
                    String groupid = ch.ToString();
                    double x = (STATICS.SCREEN_WIDTH + STATICS.MENU_BAR_SIZE.Width) / 2;
                    double y = STATICS.MENU_BAR_SIZE.Height;
                    Matrix matrix = new Matrix(1, 0, 0, 1, x, y);
                    Group_List.GroupButtons[groupid] = new Menu_Sort_Button(menuLayer, "", "" + ch, ButtontText[index - STATICS.BUTTON_PER_USER], ButtonTextBrif[index - STATICS.BUTTON_PER_USER]);
                    Group_List.GroupButtons[groupid].IsManipulationEnabled = true;
                    Group_List.GroupButtons[groupid].IsHitTestVisible = true;
                    matrix = new Matrix(1, 0, 0, 1, 0, 0);
                    matrix.Rotate(180);
                    matrix.Translate(x - 10 - (index - STATICS.BUTTON_PER_USER) * Group_List.GroupButtons[groupid].Width, y);
                    Group_List.GroupButtons[groupid].SetStartPosition(matrix.OffsetX - Group_List.GroupButtons[ch.ToString()].Width / 2,
                        matrix.OffsetY - Group_List.GroupButtons[ch.ToString()].Height / 2);
                    Group_List.GroupButtons[groupid].RenderTransform = new MatrixTransform(matrix);
                    menuLayer.AddGroupButton(Group_List.GroupButtons[ch.ToString()]);
                    index++;
                }
            }
            //For Lab
            if (false && STATICS.CHRIS_ACTIVE)
            {
                for (char ch = 'G'; ch <= 'I'; ch++)
                {
                    String groupid = ch.ToString();
                    double x = STATICS.MENU_BAR_SIZE.Height;
                    double y = (STATICS.SCREEN_HEIGHT - STATICS.MENU_BAR_SIZE.Width) / 2;
                    Matrix matrix = new Matrix(1, 0, 0, 1, x, y);
                    Group_List.GroupButtons[groupid] = new Menu_Sort_Button(menuLayer, "", "" + ch, ButtontText[index - STATICS.BUTTON_PER_USER * 2], ButtonTextBrif[index - STATICS.BUTTON_PER_USER * 2]);
                    Group_List.GroupButtons[groupid].IsManipulationEnabled = true;
                    Group_List.GroupButtons[groupid].IsHitTestVisible = true;
                    matrix = new Matrix(1, 0, 0, 1, 0, 0);
                    matrix.Rotate(90);
                    matrix.Translate(x, y + 10 + (index - STATICS.BUTTON_PER_USER * 2) * Group_List.GroupButtons[groupid].Width);
                    Group_List.GroupButtons[groupid].SetStartPosition(matrix.OffsetX - Group_List.GroupButtons[ch.ToString()].Width / 2,
                        matrix.OffsetY + Group_List.GroupButtons[ch.ToString()].Height / 2);
                    Group_List.GroupButtons[groupid].RenderTransform = new MatrixTransform(matrix);
                    menuLayer.AddGroupButton(Group_List.GroupButtons[ch.ToString()]);
                    index++;
                }
            }
            //For Lab
            if (false && STATICS.DANNY_ACTIVE)
            {
                for (char ch = 'J'; ch <= 'L'; ch++)
                {
                    String groupid = ch.ToString();
                    double x = STATICS.SCREEN_WIDTH - STATICS.MENU_BAR_SIZE.Height;
                    double y = (STATICS.SCREEN_HEIGHT + STATICS.MENU_BAR_SIZE.Width) / 2;
                    Matrix matrix = new Matrix(1, 0, 0, 1, x, y);
                    Group_List.GroupButtons[groupid] = new Menu_Sort_Button(menuLayer, "", "" + ch, ButtontText[index - STATICS.BUTTON_PER_USER * 3], ButtonTextBrif[index - STATICS.BUTTON_PER_USER * 3]);
                    Group_List.GroupButtons[groupid].IsManipulationEnabled = true;
                    Group_List.GroupButtons[groupid].IsHitTestVisible = true;
                    matrix = new Matrix(1, 0, 0, 1, 0, 0);
                    matrix.Rotate(-90);
                    matrix.Translate(x, y - 10 - (index - STATICS.BUTTON_PER_USER * 3) * Group_List.GroupButtons[groupid].Width);
                    Group_List.GroupButtons[groupid].SetStartPosition(matrix.OffsetX + Group_List.GroupButtons[ch.ToString()].Width / 2,
                        matrix.OffsetY - Group_List.GroupButtons[ch.ToString()].Height / 2);
                    Group_List.GroupButtons[groupid].RenderTransform = new MatrixTransform(matrix);
                    menuLayer.AddGroupButton(Group_List.GroupButtons[ch.ToString()]);
                    index++;
                }
            }
        }
    }
}
