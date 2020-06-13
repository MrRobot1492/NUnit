using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetThelastErrorProperty()
        {
            var logger = new ErrorLogger();

            logger.Log("a");

            //Testing void methods
            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            var logger = new ErrorLogger();

            //Testing throw exception methods
            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
        }

        [Test]
        //Testing only public API or public implementations
        public void Log_ValidError_RaiseErrorLoggerEvent()
        {
            var logger = new ErrorLogger();

            var id = Guid.Empty;

            //subscribe an event in order to monitoring the 'id' variable
            logger.ErrorLogged += (sender, args) => { id = args; };

            logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}