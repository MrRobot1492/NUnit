using System.Linq;

namespace TestNinja.Mocking
{
    /// <summary>
    /// Using Interface of the auxiliar class
    /// </summary>
    public interface IBookingRepository
    {
        IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null);
    }

    /// <summary>
    /// Decopling external dependencies by using a Repo,
    /// Service or a Storage
    /// </summary>
    public class BookingRepository : IBookingRepository
    {
        //we can call this method with no excludedBookingId
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Status != "Cancelled");
            if (excludedBookingId.HasValue)
                bookings = bookings.Where(b => b.Id != excludedBookingId.Value);

            return bookings;
        }
    }
}