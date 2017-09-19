using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    class Card
    {
         public enum Rank 
         {
             Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace 
         };

        public enum Suit 
        { 
            Club, Diamond, Heart, Spade 
        };
        
        private Suit suit;
        private Rank rank;

        public Card(Rank rank, Suit suit)
        {
            this.rank = rank;
            this.suit = suit;
        }

        public Rank GetRank()
        {
            return rank;
        }

        public Suit GetSuit()
        {
            return suit;
        }

        public override string ToString()
        {
            //Implemented by Jayasree.A    ID :2182948 
            String cardToString = rank.ToString() + "-" + suit.ToString();
            return cardToString;     // Returns a card along with the Rank and Suit.      

        }
    }
}
