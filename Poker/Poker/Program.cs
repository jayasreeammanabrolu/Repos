using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment7
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p = new Program();
            Deck deck = new Deck();

          //  Console.WriteLine("Deck of cards Intially");
          //  Console.WriteLine("{0}", deck);
            Console.WriteLine("");

          //  Console.WriteLine("Deck of cards after shuffling");
            deck.Shuffle();
          //  Console.WriteLine("{0}", deck);
            Console.WriteLine("");

            Hand hand1 = new Hand();
            Hand hand2 = new Hand();
            Hand hand3 = new Hand();
            Hand hand4 = new Hand();

            for (int i = 0; i < Hand.HandSize; i++)
            {
                hand1.Dealt(deck.Deal());
                hand2.Dealt(deck.Deal());
                hand3.Dealt(deck.Deal());
                hand4.Dealt(deck.Deal());
            }
            Console.WriteLine("Hand of cards with each player");
            Console.WriteLine("hand1 => {0}", hand1);
            Console.WriteLine("Hand1 Ranking :" + hand1.CalcRank());
            Console.WriteLine("hand2 => {0}", hand2);
            Console.WriteLine("Hand2 Ranking :"+ hand2.CalcRank());
            Console.WriteLine("hand3 => {0}", hand3);
            Console.WriteLine("Hand3 Ranking :"+hand3.CalcRank());
            Console.WriteLine("hand4 => {0}", hand4);
            Console.WriteLine("Hand4 Ranking :"+hand4.CalcRank());
            Console.WriteLine(" ");

           //test Royal Flush
            Hand testRoyalFlushhand = new Hand();
            testRoyalFlushhand.Dealt(new Card(Card.Rank.Ace, Card.Suit.Club));
            testRoyalFlushhand.Dealt(new Card(Card.Rank.King, Card.Suit.Club));
            testRoyalFlushhand.Dealt(new Card(Card.Rank.Queen, Card.Suit.Club));
            testRoyalFlushhand.Dealt(new Card(Card.Rank.Jack, Card.Suit.Club));
            testRoyalFlushhand.Dealt(new Card(Card.Rank.Ten, Card.Suit.Club));   
            Console.WriteLine("testRoyalFlushhand => {0}", testRoyalFlushhand);
            Console.WriteLine(testRoyalFlushhand.CalcRank());

            //test Straight Flush
            Console.WriteLine(" ");
           Hand testStraightFlushhand = new Hand();
           testStraightFlushhand.Dealt(new Card(Card.Rank.Nine, Card.Suit.Heart));
           testStraightFlushhand.Dealt(new Card(Card.Rank.Eight, Card.Suit.Heart));
           testStraightFlushhand.Dealt(new Card(Card.Rank.Seven, Card.Suit.Heart));
           testStraightFlushhand.Dealt(new Card(Card.Rank.Six, Card.Suit.Heart));
           testStraightFlushhand.Dealt(new Card(Card.Rank.Five, Card.Suit.Heart));
           Console.WriteLine("testStraightFlushhand => {0}", testStraightFlushhand);
           Console.WriteLine(testStraightFlushhand.CalcRank());

           //testfourofakind
           Console.WriteLine(" ");
           Hand testfourofakind = new Hand();
           testfourofakind.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           testfourofakind.Dealt(new Card(Card.Rank.Ace, Card.Suit.Club));
           testfourofakind.Dealt(new Card(Card.Rank.Ace, Card.Suit.Diamond));
           testfourofakind.Dealt(new Card(Card.Rank.Ace, Card.Suit.Spade));
           testfourofakind.Dealt(new Card(Card.Rank.Ten, Card.Suit.Heart));
           Console.WriteLine("testfourofakind => {0}", testfourofakind);
           Console.WriteLine(testfourofakind.CalcRank());


           //test Full House
           Console.WriteLine(" ");
           Hand testfullHouse = new Hand();
           testfullHouse.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           testfullHouse.Dealt(new Card(Card.Rank.Ace, Card.Suit.Club));
           testfullHouse.Dealt(new Card(Card.Rank.Ace, Card.Suit.Diamond));
           testfullHouse.Dealt(new Card(Card.Rank.King, Card.Suit.Club));
           testfullHouse.Dealt(new Card(Card.Rank.King, Card.Suit.Heart));
           Console.WriteLine("testfullHouse => {0}", testfullHouse);
           Console.WriteLine(testfullHouse.CalcRank());


           //test Flush
           Console.WriteLine(" ");
           Hand testflush = new Hand();
           testflush.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           testflush.Dealt(new Card(Card.Rank.Ten, Card.Suit.Heart));
           testflush.Dealt(new Card(Card.Rank.Eight, Card.Suit.Heart));
           testflush.Dealt(new Card(Card.Rank.Five, Card.Suit.Heart));
           testflush.Dealt(new Card(Card.Rank.Two, Card.Suit.Heart));
           Console.WriteLine("testflush => {0}", testflush);
           Console.WriteLine(testflush.CalcRank());

           //test Straight
           Console.WriteLine(" ");
           Hand testStraight = new Hand();
           testStraight.Dealt(new Card(Card.Rank.Five, Card.Suit.Heart));
           testStraight.Dealt(new Card(Card.Rank.Four, Card.Suit.Club));
           testStraight.Dealt(new Card(Card.Rank.Three, Card.Suit.Diamond));
           testStraight.Dealt(new Card(Card.Rank.Two, Card.Suit.Spade));
           testStraight.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           Console.WriteLine("testStraight => {0}", testStraight);
           Console.WriteLine(testStraight.CalcRank());

           //test Three of a kind
           Console.WriteLine(" ");
           Hand testThreeofaKind = new Hand();
           testThreeofaKind.Dealt(new Card(Card.Rank.Ten, Card.Suit.Heart));
           testThreeofaKind.Dealt(new Card(Card.Rank.Ten, Card.Suit.Club));
           testThreeofaKind.Dealt(new Card(Card.Rank.Ten, Card.Suit.Diamond));
           testThreeofaKind.Dealt(new Card(Card.Rank.Five, Card.Suit.Heart));
           testThreeofaKind.Dealt(new Card(Card.Rank.Three, Card.Suit.Spade));
           Console.WriteLine("testThreeofaKind => {0}", testThreeofaKind);
           Console.WriteLine(testThreeofaKind.CalcRank());

           // test two pair
           Console.WriteLine(" ");
           Hand testTwoPair = new Hand();
           testTwoPair.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           testTwoPair.Dealt(new Card(Card.Rank.Ace, Card.Suit.Club));
           testTwoPair.Dealt(new Card(Card.Rank.King, Card.Suit.Heart));
           testTwoPair.Dealt(new Card(Card.Rank.King, Card.Suit.Club));
           testTwoPair.Dealt(new Card(Card.Rank.Five, Card.Suit.Club));
           Console.WriteLine("testTwoPair => {0}", testTwoPair);
           Console.WriteLine(testTwoPair.CalcRank());

           //test one pair
           Console.WriteLine(" ");
           Hand testOnePair = new Hand();
           testOnePair.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           testOnePair.Dealt(new Card(Card.Rank.Ace, Card.Suit.Club));
           testOnePair.Dealt(new Card(Card.Rank.Six, Card.Suit.Heart));
           testOnePair.Dealt(new Card(Card.Rank.Four, Card.Suit.Club));
           testOnePair.Dealt(new Card(Card.Rank.Two, Card.Suit.Diamond));
           Console.WriteLine("testOnePair => {0}", testOnePair);
           Console.WriteLine(testOnePair.CalcRank());

           
           //test High card
           Console.WriteLine(" ");
           Hand testHighCard = new Hand();
           testHighCard.Dealt(new Card(Card.Rank.Ace, Card.Suit.Heart));
           testHighCard.Dealt(new Card(Card.Rank.Nine, Card.Suit.Club));
           testHighCard.Dealt(new Card(Card.Rank.Six, Card.Suit.Diamond));
           testHighCard.Dealt(new Card(Card.Rank.Four, Card.Suit.Spade));
           testHighCard.Dealt(new Card(Card.Rank.Two, Card.Suit.Heart));
           Console.WriteLine("testHighCard => {0}", testHighCard);
           Console.WriteLine(testHighCard.CalcRank());    


            Console.ReadLine();

        }

     
    }
}
