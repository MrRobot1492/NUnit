using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class FizzBuzzTests
    {
        [Test]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz()
        {
            var input = 15;
            var result = FizzBuzz.GetOutput(input);
            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleBy3_ReturnFizz()
        {
            var input = 9;
            var result = FizzBuzz.GetOutput(input);
            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_InputIsDivisibleBy5_ReturnBuzz()
        {
            var input = 10;
            var result = FizzBuzz.GetOutput(input);
            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_InputIsNotDivisibleBy3NorBy5_ReturnInput()
        {
            var input = 8;
            var result = FizzBuzz.GetOutput(input);
            Assert.That(result, Is.EqualTo(input.ToString()));
        }

        [Test]
        [TestCase(15, "FizzBuzz")]
        [TestCase(9, "Fizz")]
        [TestCase(10, "Buzz")]
        [TestCase(8, "8")]
        public void GetOutput_WhenCalled_ReturnExpectedResult(int input, string expectedResult)
        {
            var result = FizzBuzz.GetOutput(input);
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}
