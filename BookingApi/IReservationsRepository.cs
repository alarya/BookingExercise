using System;

namespace Ploeh.Samples.BookingApi
{
    public interface IReservationsRepository
    {
        void AddReservation(Reservation rendition);

        int GetTotalReservations(DateTime min, DateTime max);

        int GetCapacity();
    }
}
