using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace TuviBase32EConverterLib
{
    public static class BigIntegerExtension
    {
        public static BigInteger ConcatBytes(this BigInteger number, byte[] array)
        {
            for (int i = 0; i < array.Length; i++)
            {
                number = number << 8;
                number |= array[i];
            }

            return number;
        }

        public static byte[] ToStraightByteArray(this BigInteger number)
        {
            BigInteger temp = number;
            List<byte> result = new List<byte>();
            while (temp > 0)
            {
                byte currentByte = (byte)(temp & 255);
                result.Add(currentByte);
                temp = temp >> 8;
            }

            result.Reverse();
            return result.ToArray();
        }
    }
}
