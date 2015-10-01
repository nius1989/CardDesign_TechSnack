using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CardDesign
{
    /// <summary>
    /// A class to initialize users
    /// Four users are created: Alex, Ben, Chris and Danny
    /// </summary>
    public class User_Controler
    {
        Dictionary<String, User> userList = new Dictionary<String, User>();

        public Dictionary<String, User> UserList
        {
            get { return userList; }
            set { userList = value; }
        }

        MainWindow mainWindow;
        public User_Controler(MainWindow mainWin) {
            this.mainWindow = mainWin;
            User user = new User();
            user.Name = "Alex";
            user.UserColor = STATICS.USER_COLOR["Alex"];
            userList.Add("Alex", user);
            user = new User();
            user.Name = "Ben";
            user.UserColor = STATICS.USER_COLOR["Ben"];
            userList.Add("Ben", user);
            user = new User();
            user.Name = "Chris";
            user.UserColor = STATICS.USER_COLOR["Chris"];
            userList.Add("Chris", user);
            user = new User();
            user.Name = "Danny";
            user.UserColor = STATICS.USER_COLOR["Danny"];
            userList.Add("Danny", user);
        }
        public void ReceiveCard(String userName, Card card)
        {
            if (userList.ContainsKey(userName))
            {
                card.HightlightColor = userList[userName].UserColor;
                userList[userName].ReceiveCard(card);
            }
        }
    }
}
