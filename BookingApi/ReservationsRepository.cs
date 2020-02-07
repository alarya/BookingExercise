using System;
using System.Linq;

namespace Ploeh.Samples.BookingApi
{
    public class ReservationsRepository : IReservationsRepository
    {
        public void AddReservation(Reservation reservation)
        {
            using (var ctx = new ReservationsContext())
            {
                ctx.Reservations.Add(reservation);
                ctx.SaveChanges();
            }
        }

        public int GetCapacity()
        {
            return 10;
        }

        public int GetTotalReservations(DateTime min, DateTime max)
        {
            using (var ctx = new ReservationsContext())
            {
                var reservedSeats = (from r in ctx.Reservations
                                     where min <= r.Date && r.Date < max
                                     select r.Quantity)
                                    .DefaultIfEmpty(0)
                                    .Sum();

                return reservedSeats;
            }
        }
    }
}