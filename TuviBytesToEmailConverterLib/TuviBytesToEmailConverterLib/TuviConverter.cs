using System;
using System.Numerics;

namespace TuviBytesToEmailConverterLib
{
    public class TuviConverter
    {
        public string ConvertBytesToEmailName(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            byte[] fiveBitsArray = ConvertEightToFiveBits(array);
            char[] symbolsArray = new char[fiveBitsArray.Length];
            for (int i = 0; i < symbolsArray.Length; i++)
            {
                symbolsArray[i] = ConvertByteToSymbol(fiveBitsArray[i]);
            }
            return new string(symbolsArray);
        }

        public byte[] ConvertStringToByteArray(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (name.Length > 64)
            {
                throw new ArgumentException("Email's name can not be longer than 64 symbols.", nameof(name));
            }

            char[] symbolsArray = name.ToCharArray();
            byte[] fiveBitsArray = new byte[symbolsArray.Length];

            for (int i = 0; i < fiveBitsArray.Length; i++)
            {
                fiveBitsArray[i] = ToBits(symbolsArray[i]);
            }

            return ConvertFiveToEightBits(fiveBitsArray);
        }

        private byte[] ConvertEightToFiveBits(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            int size = array.Length * 8 % 5 == 0 ? array.Length * 8 / 5 : array.Length * 8 / 5 + 1;
            int currentPosition = size - 1;
            byte[] result = new byte[size];
            BigInteger number = new BigInteger(array, true, true);
            while (number != 0 && currentPosition >= 0)
            {
                byte lastFiveBits = (byte)(number & 31);
                result[currentPosition] = lastFiveBits;
                number = number >> 5;
                currentPosition--;
            }

            if (result.Length > 64)
            {
                throw new ArgumentException("Initial array is too big to create coorect email's name (name can not be longer than 64 symbols).", nameof(array));
            }

            return result;
        }

        private byte[] ConvertFiveToEightBits(byte[] array)
        {
            if (array is null)
            {
                throw new ArgumentNullException(nameof(array));
            }

            if (array.Length > 64)
            {
                throw new ArgumentException("Email's name can not be longer than 64 symbols.", nameof(array));
            }

            BigInteger number = 0;
            int size = array.Length * 5 / 8;
            byte[] resultArray = new byte[size];
            for (int i = 0; i < array.Length; i++)
            {
                if (array[i] >= 32)
                {
                    throw new ArgumentOutOfRangeException(nameof(array),
                        $"Array at index {i} has wrong value. Allowed values are from 0 to 31.");
                }
                number = number << 5;
                number |= array[i];
            }

            byte[] tempArray = number.ToByteArray(true, true);
            if (tempArray.Length >= resultArray.Length)
            {
                return tempArray;
            }
            else
            {
                for (int i = 1; i <= tempArray.Length; i++)
                {
                    resultArray[^i] = tempArray[^i];
                }

                return resultArray;
            }
        }

        private char ConvertByteToSymbol(byte bits) => bits switch
        {
            0 => 'a',
            1 => 'b',
            2 => 'c',
            3 => 'd',
            4 => 'e',
            5 => 'f',
            6 => 'g',
            7 => 'h',
            8 => 'i',
            9 => 'j',
            10 => 'k',
            11 => 'l',
            12 => 'm',
            13 => 'n',
            14 => 'o',
            15 => 'p',
            16 => 'q',
            17 => 'r',
            18 => 's',
            19 => 't',
            20 => 'u',
            21 => 'v',
            22 => 'w',
            23 => 'x',
            24 => 'y',
            25 => 'z',
            26 => '2',
            27 => '3',
            28 => '4',
            29 => '5',
            30 => '6',
            31 => '7',
            _ => throw new ArgumentOutOfRangeException(nameof(bits)),
        };

        private byte ToBits(char symbol) => symbol switch
        {
            'a' => 0,
            'b' => 1,
            'c' => 2,
            'd' => 3,
            'e' => 4,
            'f' => 5,
            'g' => 6,
            'h' => 7,
            'i' => 8,
            'j' => 9,
            'k' => 10,
            'l' => 11,
            'm' => 12,
            'n' => 13,
            'o' => 14,
            'p' => 15,
            'q' => 16,
            'r' => 17,
            's' => 18,
            't' => 19,
            'u' => 20,
            'v' => 21,
            'w' => 22,
            'x' => 23,
            'y' => 24,
            'z' => 25,
            '2' => 26,
            '3' => 27,
            '4' => 28,
            '5' => 29,
            '6' => 30,
            '7' => 31,
            _ => throw new ArgumentOutOfRangeException(nameof(symbol)),
        };

    }
}
