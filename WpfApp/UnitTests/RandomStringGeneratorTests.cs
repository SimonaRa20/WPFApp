using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    public class RandomStringGeneratorTests
    {
        [Fact]
        public void RandomStringGenerator_GeneratesStringOfValidLength()
        {
            var generator = new RandomStringGenerator();
            string result = generator.Generate();

            Assert.InRange(result.Length, 5, 10);
        }

        [Fact]
        public void RandomStringGenerator_GeneratesRandomString()
        {
            var generator = new RandomStringGenerator();
            string result1 = generator.Generate();
            string result2 = generator.Generate();

            Assert.NotEqual(result1, result2);
        }
    }
}
