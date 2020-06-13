using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;
namespace TestNinja.UnitTests
{
    [TestFixture]
    public class MathTests
    {
        #region Initializers
        [SetUp]
        public void SetUp()
        {
            _math = new Math();
        }
        #endregion

        #region PublicMethods
        [Test]
        [Ignore("Is Not required for this sprint")]
        public void Add_WhenCalled_ReturnSumOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2, 1, 2)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 1, 1)]
        [TestCase(-1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        public void GetOddNumbers_LimitGreaterThanZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            //TOO SPECIFIC
            //Assert.That(result,Is.Not.Empty);
            //Assert.That(result.Count(), Is.EqualTo(3));
            //Assert.That(result.Count(), Does.Contain(1));
            //Assert.That(result.Count(), Does.Contain(3));
            //Assert.That(result.Count(), Does.Contain(5));
            Assert.That(result.Count(), Is.EquivalentTo(new[] { 1, 3, 5 }));
            Assert.That(result.Count(), Is.Ordered);
            Assert.That(result.Count(), Is.Unique);
        }
        #endregion

        #region Private variables
        Math _math;
        #endregion
    }
}