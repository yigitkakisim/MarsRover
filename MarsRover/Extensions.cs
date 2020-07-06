using System;
using System.Linq;

namespace MarsRover
{
    public static class Extensions
    {
        public static char ToUpper(this char input)
        {
            return input.ToString().ToUpper().First<char>();
        }

        public static UInt32 ToUInt32(this string input)
        {
            return Convert.ToUInt32(input);
        }
    }
}
