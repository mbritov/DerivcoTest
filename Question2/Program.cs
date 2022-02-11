using System;

namespace DerivcoQuestion2
{
    class Program
    {
        static void Main(string[] args)
        {
            HighCardGame card = new HighCardGame();
            if (card.Play() == 1)
            {
                Console.WriteLine("win");
            }
            else
            {
                Console.WriteLine("lose");
            }
        }
    }
}
