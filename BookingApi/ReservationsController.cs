using System;
using System.Net;
using System.Web.Http;

namespace Ploeh.Samples.BookingApi
{
    public class ReservationsController : ApiController
    {
        IReservationsRepository reservationsRepository;
        ReservationProcessor reservationProcessor;

        public ReservationsController(IReservationsRepository reservationsRepository,
                                        ReservationProcessor reservationProcessor)
        {
            this.reservationsRepository = reservationsRepository;
            this.reservationProcessor = reservationProcessor;
        }

        public IHttpActionResult Post(ReservationRendition rendition)
        {
            if (!ReservationDate.IsValid(rendition.Date))
                return this.BadRequest("Invalid date.");
                        
            var requestedDate = new ReservationDate(rendition.Date);
            if (!reservationProcessor.IsFeasible(requestedDate, rendition.Quantity))
                return this.StatusCode(HttpStatusCode.Forbidden);

            reservationsRepository.AddReservation(new Reservation
            {
                Date = requestedDate.Value,
                Name = rendition.Name,
                Email = rendition.Email,
                Quantity = rendition.Quantity
            });

            return this.Ok();
        }
    }
}