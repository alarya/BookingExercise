using Ploeh.Samples.BookingApi;
using System;

namespace BookingApi.Tests
{
    public class MockReservationsRepository : IReservationsRepository
    {
        public void AddReservation(Reservation rendition)
        {

        }

        public int GetCapacity()
        {
            return 10;
        }

        public int GetTotalReservations(DateTime min, DateTime max)
        {
            return 5;
        }
    }
}
