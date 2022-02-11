using System;

namespace DerivcoTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string test_string = "This is a test string";

            if (String.Compare(test_string, Encryptor.Decode(Encryptor.Encode(test_string))) == 0)
            {
                Console.WriteLine("Test succeeded");
                return;
            }

            Console.WriteLine("Test failed");
        }
    }
}
