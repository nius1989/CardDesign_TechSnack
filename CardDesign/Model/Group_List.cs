using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

///<summary>
///Group List stores the cards belong to the same sorting group
///It uses a string key as an identifier, which corresponds to the menubutton name
///</summary>
namespace CardDesign
{
    public class Group_List
    {
        
        static Dictionary<String, List<Card>> cardGroups = new Dictionary<string, List<Card>>();
        static Dictionary<String, Menu_Sort_Button> groupButtons = new Dictionary<String, Menu_Sort_Button>();

        public static Dictionary<String, Menu_Sort_Button> GroupButtons
        {
            get { return Group_List.groupButtons; }
            set { Group_List.groupButtons = value; }
        }


        public static Dictionary<String, List<Card>> CardGroups
        {
            get { return Group_List.cardGroups; }
            set { Group_List.cardGroups = value; }
        }

        //Add a card to the group "key"
        public static void Add(String key, Card card)
        {
            if (!cardGroups.ContainsKey(key))
            {
                List<Card> list = new List<Card>();
                list.Add(card);
                cardGroups.Add(key, list);
            }
            else
            {
                cardGroups[key].Add(card);
            }
        }
        //delete the group "key"
        public static void Remove(String key)
        {
            if (cardGroups.ContainsKey(key))
            {
                foreach (Card c in cardGroups[key]) {
                    c.RemoveFromGroup(key);
                }
                cardGroups.Remove(key);

            }
        }
        //remove the card from the group "key"
        public static void RemoveCard(String key, Card card)
        {
            if (cardGroups.ContainsKey(key))
            {
                if (cardGroups[key].Contains(card))
                {
                    card.RemoveFromGroup(key);
                    cardGroups[key].Remove(card);
                    if (cardGroups[key].Count == 0)
                    {
                        Remove(key);
                    }
                }
            }
        }
        public static bool ContainCard(String key, Card card) {
            if (cardGroups.ContainsKey(key))
            {
                return cardGroups[key].Contains(card);
            }
            else {
                return false;
            }
        }
    }
}
