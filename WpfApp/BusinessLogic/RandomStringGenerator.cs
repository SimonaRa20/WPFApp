using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class RandomStringGenerator
    {
        private static readonly char[] _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();
        private static readonly Random _random = new Random();

        public string Generate()
        {
            try
            {
                int length = _random.Next(5, 11);
                return new string(Enumerable.Repeat(_chars, length)
                    .Select(s => s[_random.Next(s.Length)]).ToArray());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating random string: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
