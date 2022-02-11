using System;
using System.Collections.Generic;

namespace DerivcoTest
{
    public class Encryptor
    {
        const int SIZE = 64;
        private static readonly char[] _transcode = new char[SIZE];

        static Encryptor()
        {
            for (int i = 0; i < SIZE; i++)
            {
                _transcode[i] = Convert.ToChar('A' + i);
                if (i > 25) _transcode[i] = Convert.ToChar(_transcode[i] + 6);
                if (i > 51) _transcode[i] = Convert.ToChar(_transcode[i] - 0x4b);
            }
            _transcode[SIZE - 3] = '+';
            _transcode[SIZE - 2] = '/';
            _transcode[SIZE - 1] = '=';
        }

        public static string Encode(string input)
        {
            int length = input.Length;
            int cb = (length / 3 + (Convert.ToBoolean(length % 3) ? 1 : 0)) * 4;

            char[] output = new char[cb];
            for (int i = 0; i < cb; i++)
            {
                output[i] = '=';
            }

            int index = 0;
            int reflex = 0;
            const int s = 0x3f;

            for (int j = 0; j < length; j++)
            {
                reflex <<= 8;
                reflex &= 0x00ffff00;
                reflex += input[j];

                int x = ((j % 3) + 1) * 2;
                int mask = s << x;
                while (mask >= s)
                {
                    int pivot = (reflex & mask) >> x;
                    output[index++] = _transcode[pivot];
                    int invert = ~mask;
                    reflex &= invert;
                    mask >>= 6;
                    x -= 6;
                }
            }

            switch (length % 3)
            {
                case 1:
                {
                    reflex <<= 4;
                    output[index++] = _transcode[reflex];
                    break;
                }
                case 2:
                {
                    reflex <<= 2;
                    output[index++] = _transcode[reflex];
                    break;
                }
            }

            return new string(output);
        }

        public static string Decode(string input)
        {
            int length = input.Length;
            //int cb = (length / 4 + ((Convert.ToBoolean(length % 4)) ? 1 : 0)) * 3 + 1;
            //char[] output = new char[cb];
            //int c = 0;

            var output = new List<char>();
            int bits = 0;
            int reflex = 0;
            for (int j = 0; j < length; j++)
            {
                reflex <<= 6;
                bits += 6;
                bool fTerminate = ('=' == input[j]);
                if (!fTerminate)
                {
                    reflex += IndexOf(input[j]);
                }
                else
                {
                    break;
                }

                while (bits >= 8)
                {
                    int mask = 0x000000ff << (bits % 8);
                    output.Add(Convert.ToChar((reflex & mask) >> (bits % 8)));
                    //output[c++] = Convert.ToChar((reflex & mask) >> (bits % 8));
                    int invert = ~mask;
                    reflex &= invert;
                    bits -= 8;
                }

                //if (fTerminate)
                //  break;
            }
            //Console.WriteLine("{0} --> {1}", input, new string(output.ToArray()));

            return new string(output.ToArray());
        }

        private static int IndexOf(char ch)
        {
            int index;
            for (index = 0; index < _transcode.Length; index++)
                if (ch == _transcode[index])
                    break;

            return index;
        }
    }
}
