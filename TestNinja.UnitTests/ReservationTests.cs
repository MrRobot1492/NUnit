using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ReservationTests
    {
        [Test]
        public void CanBeCancelledBy_UserIsAdmin_ReturnTrue()
        {
            //1.Arrange
            var reservation = new Reservation();

            //2.Act
            var result = reservation.CanBeCancelledBy(new User { IsAdmin = true });

            //3.Assert
            Assert.That(result);
        }

        [Test]
        public void CanBeCancelledBy_SameUserCancellingTheReservation_ReturnTrue()
        {
            //1.Arrange
            var user = new User { IsAdmin = false };
            var reservation = new Reservation { MadeBy = user };

            //2.Act
            var result = reservation.CanBeCancelledBy(user);

            //3.Assert
            Assert.That(result);
        }

        [Test]
        public void CanBeCancelledBy_AnotherUserCancellingTheReservation_ReturnFalse()
        {
            //1.Arrange
            var reservation = new Reservation { MadeBy = new User() };

            //2.Act
            var result = reservation.CanBeCancelledBy(new User());

            //3.Assert
            Assert.That(!result);
        }
    }
}