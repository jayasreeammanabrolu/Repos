using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    class Hand
    {
        public const int HandSize = 5;
        private Card[] cards;
        private int currentCardCt;

        public enum Ranking
        {
            RoyalFlush, StraightFlush, FourOfaKind, FullHouse, Flush, Straight, Threeofakind, TwoPair, OnePair, HighCard
        }

        public Hand()
        {
            currentCardCt = 0;
            cards = new Card[HandSize];
        }

        public void Dealt(Card card)
        {
            cards[currentCardCt++] = card;
        }

        public override string ToString()
        {
            //Implemented by Jayasree.A    ID :2182948
                       
            return string.Join(",", cards.Select(x => x.ToString()).ToArray());

        }

        public List<int> Sort(List<int> integerList) // This method arranges the ranks of cards in descending order
        {                                            // cards ranks in descending order Ace(rank=12 or 1) ,king(rank=11),Queen(rank=10),Jack(rank=9),Ten(rank=8),Nine(rank=7),Eight(rank=6),seven(rank=5),six(rank=4),five(rank=3),four(rank=2),three(rank=1),two(rank=0)
            int k = integerList.Count;
            for (int i = 0; i < k; ++i)
            {
                for (int j = i + 1; j < k; ++j)
                {
                    if (integerList[i] < integerList[j]) // Buuble sort logic for arranging cards in descending order.
                    {
                        int temp = integerList[i];
                        integerList[i] = integerList[j];
                        integerList[j] = temp;
                    }
                }
            }
            return integerList;
        }

        /* After arranging the list of cards in decending order this method is called to check whether the cards
         satisfy straight condition or not that is whether ranks of cards are consequetive numbers or not. For example 
         rank of cards in Ace, ten , nine, five,four is in decending order but does not satisfy straight condition*/

        public bool orderCheck(List<int> SortedCardInt) 
        {
            bool order = true;

            if (SortedCardInt[0] != 12)  // checks whether cards are straight or not if there is no ace card in the given set of cards.
            {
                for (int i = 0; i <= 3; i++)
                {
                    if (SortedCardInt[i] - SortedCardInt[i + 1] != 1 )
                    {
                        order = false;
                        break;
                    }
                    if (order == false)
                        break;

                }               
            }
            else if(SortedCardInt[1]!=11 && SortedCardInt[1]!=3) //checks cards are straight or not in presence of ace card.
            {                                                    // Ace,king,Queen,Ten,Nine or five,four,three,two,one represent straight conditions
                order = false;                                   // If the second card is not King(rank=11) or five(rank=3) then for sure cards won't satisfy straight condition.
            }
            
            
            else  // If the second card is either king or five then this condition checks whether remaining three cards are in straight order or not
            {
                for (int i = 0; i <= 2; i++)         
                {
                    if (SortedCardInt[i+1] - SortedCardInt[i + 2] != 1)
                    {
                        order = false;
                        break;
                    }
                    if (order == false)
                        break;
                }
            }

            return order;
        }
        
        public bool straight() // checks the ordering of the cards decides whether they satisfy straight condition or not.
        {
            bool straight = true;
            List<int> CardInt = new List<int>(); 

            foreach (Card c in cards)
            {
                CardInt.Add((int)c.GetRank()); // ranks of cards are stored in an integer list called CardInt
            }
            
            List<int> SortedCardInt = Sort(CardInt); // cards are first arranged in decending order by calling sort method
            straight = orderCheck(SortedCardInt);   // Then sequence of the cards whether their ranks are consequetive numbers are not checked by calling order check method.

            return straight;
        }

        public bool flush() // check whether the set of cards satisfy the flush condition or not. This method checks whether all the suits of cards are of same kind or not.
        {
            List<Card.Suit> suit1 = new List<Card.Suit>();
            bool order=true;

            foreach (Card c in cards)
            {
                suit1.Add(c.GetSuit());
            }

            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 5; j++)
                {
                    if (suit1[i] != suit1[j])
                    {
                        order = false;
                        break;
                    }
                        
                }
                if (order == false)
                    break;
            
            }

            return order;
        }

        public List<KeyValuePair<int, int>> returnCardsWithCount() // This method checks fourofakind,Fullhouse,Threeofakind,TwoPair,OnePair condition
        {
            List<KeyValuePair<int, int>> repeatCount = new List<KeyValuePair<int, int>>();
            List<int> CardInt = new List<int>();

            foreach (Card c in cards)
            {
                CardInt.Add((int)c.GetRank()); // ranks of cards are stored in an integer list called CardInt
            }

            List<int> SortedCardInt = Sort(CardInt); // Cards are arranged in descending order
            List<IGrouping<int,int>> groups = SortedCardInt.GroupBy(x => x).ToList(); // Grouping cards with similar ranks
            foreach (var group in groups)
            {
                repeatCount.Add(new KeyValuePair<int,int>(group.Key, group.ToList().Count)); // A card's rank and its count(number of times it is repeated ) is stored in repeatCount list
               // Console.WriteLine("{0}: {1}", group.Key, group.ToList().Count );
            }            
            
            return repeatCount;
        }

        public Ranking CalcRank() // This method calculates rank of each hand.
        {
            bool flush = this.flush();
            bool straight = this.straight();
            List<KeyValuePair<int, int>> cardsCount = this.returnCardsWithCount();
            if(straight && flush)
            {
                if (cardsCount[0].Key == 12 && cardsCount[1].Key == 11)
                {
                    return Ranking.RoyalFlush;
                }
                else
                {
                    return Ranking.StraightFlush;
                }
            }
            else if(flush)
            {
                return Ranking.Flush;
            }
            else if(straight)
            {
                return Ranking.Straight;
            }
            else
            {
                //if(cardsCount)
                List<int> cardsCounting = Sort(cardsCount.Select(x => x.Value).ToList());
                if(cardsCounting[0] == 4)
                {
                    return Ranking.FourOfaKind;
                }
                else if(cardsCounting[0] == 3 && cardsCounting[1] == 2)
                {
                    return Ranking.FullHouse;
                }
                else if(cardsCounting[0] == 3)
                {
                    return Ranking.Threeofakind;
                }
                else if(cardsCounting[0] == 2 && cardsCounting[1] == 2)
                {
                    return Ranking.TwoPair;
                }
                else if(cardsCounting[0] == 2)
                {
                    return Ranking.OnePair;
                }
                else
                {
                    return Ranking.HighCard; // returns high card if any of the above conditions are not satisfied.
                }
            }
        }
    }

   
}
