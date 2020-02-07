namespace Ploeh.Samples.BookingApi
{
    public class ReservationProcessor
    {
        private readonly IReservationsRepository reservationsRepository;

        public ReservationProcessor(IReservationsRepository reservationsRepository)
        {            
            this.reservationsRepository = reservationsRepository;   
        }
        
        public bool IsFeasible(ReservationDate requestedDate,
                                int quantity)
        {            
            var min = requestedDate.Value.Date;
            var max = requestedDate.Value.Date.AddDays(1);
            var reservedSeats = reservationsRepository.GetTotalReservations(min, max);

            var capacity = reservationsRepository.GetCapacity();

            if (quantity + reservedSeats > capacity)
                return false;

            return true;
        }            
    }
}