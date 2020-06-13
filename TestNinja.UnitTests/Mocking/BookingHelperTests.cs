using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Rhino.Mocks.Exceptions;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperOverlappingBookingExistsTests
    {
        #region private members

        private Mock<IBookingRepository> _bookingRepo;
        private Booking _existingBooking;

        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }

        #endregion

        #region PublicMembers

        [SetUp]
        public void Setup()
        {
            //1.Arrange
            var reference = Guid.NewGuid().ToString();
            _bookingRepo = new Mock<IBookingRepository>();
            _existingBooking = new Booking
            {
                Id = 2,
                ArrivalDate = ArriveOn(2017, 1, 15),
                DepartureDate = DepartOn(2017, 1, 20),
                Reference = reference
            };

            _bookingRepo.Setup(r => r.GetActiveBookings(1))
                .Returns(
                    new List<Booking>
                    {
                        //Existing Booking
                        _existingBooking
                    }.AsQueryable());
        }

        [Test]
        public void BookingStartsAndFinishesBeforeAnExistingBooking_ReturnEmptyString()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate, days: 2),
                    DepartureDate = Before(_existingBooking.ArrivalDate)
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingStartsBeforeAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.ArrivalDate)
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsBeforeAndFinishesAfterAnExistingBooking_ReturnExistingBookingReference()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = Before(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate)
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesInTheMiddleOfAnExistingBooking_ReturnExistingBookingReference()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = Before(_existingBooking.DepartureDate)
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsInTheMiddleOfAnExistingBookingButFinishesAfter_ReturnExistingBookingReference()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate)
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.EqualTo(_existingBooking.Reference));
        }

        [Test]
        public void BookingStartsAndFinishesAfterAnExistingBooking_ReturnEmptyString()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.DepartureDate),
                    DepartureDate = After(_existingBooking.DepartureDate, days: 2)
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingsOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            //2.Act
            //New Booking
            //The contract parameters ont the delegate call in the setup method 
            //must be the same as on the result call to check the Assertion
            var result = BookingHelper.OverlappingBookingsExist(new Booking
                {
                    Id = 1,
                    ArrivalDate = After(_existingBooking.ArrivalDate),
                    DepartureDate = After(_existingBooking.DepartureDate),
                    Status = "Cancelled"
                }
                , _bookingRepo.Object);

            //3.Assert
            Assert.That(result, Is.Empty);
        }

        #endregion
    }
}