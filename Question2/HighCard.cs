using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DerivcoQuestion2
{
    //1) Support ties when the face value of the cards are the same.
    //2) Allow for the ties to be resolved as a win by giving the different suits precedence.
    //3) Support for Multiple Decks.
    //4) Support the abilty to never end in a tie, by dealing a further card to each player.
    //5) Make one of the cards in the deck a wild card(beats all others ).
    //6) Now make the game work for a deck with 20 cards per suit

    public class Card
    {
        public enum CardSuit
        {
            Hearts,
            Tiles,
            Clover,
            Pikes
        }

        public enum CardValue
        {
            Two = 2,
            Three,
            Four,
            Five,
            Six,
            Seven,
            Eight,
            Nine,
            Ten,
            Jack,
            Queen,
            King,
            Ace
            //extra custom values
        }

        public CardSuit Suit { get; set; }
        public CardValue Value { get; set; }
        public bool IsWildCard { get; set; }

        public string ToString()
        {
            return Enum.GetName(typeof(CardValue), Value) + " " + Enum.GetName(typeof(CardSuit), Suit);
        }
    }

    public class Deck
    {
        private List<Card> _cards;
        private int _index = 0;

        private int SIZE = Enum.GetValues(typeof(Card.CardSuit)).Length * Enum.GetValues(typeof(Card.CardValue)).Length;
        private Random _rnd = new Random();

        public Deck()
        {
            _cards = new List<Card>();

            foreach(Card.CardSuit suit in Enum.GetValues(typeof(Card.CardSuit)))
            {
                foreach (Card.CardValue value in Enum.GetValues(typeof(Card.CardValue)))
                {
                    _cards.Add(new Card { Suit = suit, Value = value, IsWildCard = false});
                }
            }

            Shuffle();
            
            // make 1 card in a deck as a wildcart (beats all others)
            int i = _rnd.Next() % SIZE + 1;
            _cards[i].IsWildCard = true;
        }

        public Card GetRandomCard()
        {
            int index = _rnd.Next() % SIZE + 1;

            return _cards.ElementAt(index);
        }

        public Card GetNext()
        {
            return _cards[_index++];
        }

        private void Shuffle()
        {
            _index = 0;

            for (int shuffle = 0; shuffle < 10; shuffle++)
            { 
                for (int i = 0; i < SIZE; i++)
                {
                    int nextCardInd = _rnd.Next(0, SIZE);
                    var tmp = _cards[i];
                    _cards[i] = _cards[nextCardInd];
                    _cards[nextCardInd] = tmp;
                }
            }
        }
    }

    public class HighCardGame
    {
        public Card.CardSuit Trump { get; set; }

        private List<Deck> _decks;
        private int _countDeck;
        private Random _rnd = new Random();

        public enum Winner
        {
            First,
            Second,
            Draw
        }

        public HighCardGame(int countDeck = 1)
        {
            _decks = new List<Deck>();
            for (int i = 0; i < countDeck; i++)
            {
                _decks.Add(new Deck());
            }
            _countDeck = countDeck;

            // pick random suit as a trump
            int j = _rnd.Next(0, 4);
            Trump = (Card.CardSuit)Enum.GetValues(typeof(Card.CardSuit)).GetValue(j);
        }

        private Deck GetRandomDeck()
        {
            return _decks[_rnd.Next(0, _countDeck)];
        }

        public Winner CheckWinner(Card card1, Card card2)
        {
            if (card1.IsWildCard && !card2.IsWildCard) return Winner.First;
            if (!card1.IsWildCard && card2.IsWildCard) return Winner.Second;

            if (card1.Value == card2.Value)
            {
                if (card1.Suit == Trump && card2.Suit != Trump) return Winner.First;
                if (card1.Suit != Trump && card2.Suit == Trump) return Winner.Second;
                return Winner.Draw;
            }

            return (card1.Value > card2.Value) ? Winner.First : Winner.Second;
        }

        /// <summary>
        /// This method play the game
        /// </summary>
        /// <param name="allowDraw"></param>
        /// <returns>Returns 1 if first player won, 2 if second won, 0 when draw</returns>
        public int Play(bool allowDraw = false)
        {
            Console.WriteLine("Playing round: ");
            var card1 = GetRandomDeck().GetRandomCard();
            var card2 = GetRandomDeck().GetRandomCard();

            Winner res = CheckWinner(card1, card2);
            Console.WriteLine("Card1: {0}", card1.ToString());
            Console.WriteLine("Card2: {0}", card2.ToString());

            if (res == Winner.Draw)
            {
                if (allowDraw) return 0;
                Console.WriteLine("Draw! Go for next round: ");
                return Play(allowDraw);
            }
            Console.WriteLine("Winner: {0}", Enum.GetName(typeof(Winner), res));

            return res == Winner.First? 1: 2;
        }
    }
}
