using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CardDesign
{
    public class User
    {
        String name = "";

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        List<Card> cards = new List<Card>();
        public List<Card> Cards
        {
            get { return cards; }
            set { cards = value; }
        }
        Color userColor = Colors.Yellow;

        public Color UserColor
        {
            get { return userColor; }
            set { userColor = value; }
        }

        public void ReceiveCard(Card card) {
            cards.Add(card);
            card.Owner = this.Name;
            card.HightlightColor = this.userColor;
        }
    }
}
