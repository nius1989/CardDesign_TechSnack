using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CardDesign
{
    public class Card_List
    {
        static List<Card> cardList = new List<Card>();

        public static List<Card> CardList
        {
            get { return Card_List.cardList; }
            set { Card_List.cardList = value; }
            //hello there
        }


        public static void AddCard(Card card)
        {
            cardList.Add(card);
        }

    }
}
