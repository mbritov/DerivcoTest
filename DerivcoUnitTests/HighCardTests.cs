using Microsoft.VisualStudio.TestTools.UnitTesting;
using DerivcoQuestion2;

namespace UnitTests
{
    [TestClass]
    public class HighCardTests
    {
        [TestMethod]
        public void ShouldWinHigherCard()
        {
            HighCardGame highCard = new HighCardGame();
            Card card1 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Two };
            Card card2 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Three };
            
            Assert.AreEqual(HighCardGame.Winner.Second, highCard.CheckWinner(card1, card2));
        }

        [TestMethod]
        public void ShouldWinSameCardWithTrumpSiut()
        {
            HighCardGame highCard = new HighCardGame();
            highCard.Trump = Card.CardSuit.Hearts;
            Card card1 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Two };
            Card card2 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Clover, Value = Card.CardValue.Three };

            Assert.AreEqual(HighCardGame.Winner.First, highCard.CheckWinner(card1, card2));
        }

        [TestMethod]
        public void ShouldWinWildCard()
        {
            HighCardGame highCard = new HighCardGame();
            highCard.Trump = Card.CardSuit.Hearts;
            Card card1 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Two };
            Card card2 = new Card() { IsWildCard = true, Suit = Card.CardSuit.Clover, Value = Card.CardValue.Three };

            Assert.AreEqual(HighCardGame.Winner.Second, highCard.CheckWinner(card1, card2));
        }

        [TestMethod]
        public void ShouldAllowDraw()
        {
            HighCardGame highCard = new HighCardGame();
            highCard.Trump = Card.CardSuit.Hearts;

            Card card1 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Two };
            Card card2 = new Card() { IsWildCard = false, Suit = Card.CardSuit.Hearts, Value = Card.CardValue.Two };

            var actual = highCard.CheckWinner(card1, card2);

            Assert.AreEqual(HighCardGame.Winner.Draw, actual);
        }
    }
}
