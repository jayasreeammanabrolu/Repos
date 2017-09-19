using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    class Deck
    {
        public const int CardCt = 52;
       
        private Card[] cards;
        private int topCardIndex;

        public Deck()
        {
            topCardIndex = 0;
            cards = new Card[CardCt];

            int i = 0;
            for (Card.Rank r = Card.Rank.Two; r <= Card.Rank.Ace; r++)
            {
                for (Card.Suit s = Card.Suit.Club; s <= Card.Suit.Spade; s++)
                {
                    cards[i++] = new Card(r, s);
                }
            }
        }

        private Card GetCard(int i)
        {
            return cards[i];
        }

        public Card Deal()
        {
            return cards[topCardIndex++];
        }

        public override string ToString()
        {
            //Implemented by Jayasree.A    ID :2182948 
            
            String deckToString = "";
            int i = 0;
            foreach (Card card in cards) // For adding each card to the string deckToString.
            {
                deckToString += card.ToString();
                i++;
                if (i % 4 == 0)  // seperates the cards with different ranks.
                    deckToString += ";";
            }
            return deckToString;   // Returns deck of cards.          
                      
        }

                
        public void Shuffle()
        {
          //Implemented by Jayasree.A    ID :2182948
 
            Random rand = new Random(); //Generates a random number 
            for (int i = 0; i < CardCt; i++) // This loop randomly shuffles the card.
            {
                Card temp = cards[i];
                int r = rand.Next(i, 51);
                cards[i] = cards[r];
                cards[r] = temp;
           
            }
         

        }
    }
}
