using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace CardDesign
{
    /// <summary>
    /// A controler to highight sorting groups
    /// </summary>
    class Sorting_Group_Controler
    {
        Gesture_Controler gestureControler;
        /// <summary>
        /// Store and manage the sorting groups
        /// </summary>
        /// <param name="gestureControler">The one created in the MainWindow</param>
        public Sorting_Group_Controler(Gesture_Controler gestureControler) {
            this.gestureControler = gestureControler;           
        }
        public bool ContainCard(String key, Card c)
        {
            return Group_List.CardGroups.ContainsKey(key)&&
                Group_List.CardGroups[key].Contains(c);
        }
        public void GroupCard(String key, Card c) {
            Group_List.Add(key, c);
            c.SortToGroup(key);
        }
        public void RemoveCard(String key, Card c) {
            Group_List.RemoveCard(key, c);
            c.RemoveFromGroup(key);
        }
        public void HighlightCards(String key) {
            List<Card> cards = Group_List.CardGroups[key];
            foreach (Card c in cards) {
                c.Hightlight();
            }
        }
        public void DehighlightCards(String key)
        {
            List<Card> cards = Group_List.CardGroups[key];
            foreach (Card c in cards)
            {
                c.Dehightlight();
            }
        }
    }
}
