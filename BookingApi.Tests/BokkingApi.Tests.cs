using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ploeh.Samples.BookingApi;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Routing;

namespace BookingApi.Tests
{
    [TestClass]
    public class BookingApi_Tests
    {
        IReservationsRepository reservationsRepository;
        ReservationProcessor reservationProcessor;
        ReservationsController reservationController;

        [TestInitialize]
        public void Setup()
        {
            reservationsRepository = new MockReservationsRepository();
            reservationProcessor = new ReservationProcessor(reservationsRepository);
            reservationController = new ReservationsController(reservationsRepository,
                                                                reservationProcessor);

            reservationController.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/reservation"),
            };
            reservationController.Configuration = new HttpConfiguration();
            reservationController.Configuration.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });
            reservationController.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "reservation" } });
        }

        [TestMethod]
        public void succesful_reservation()
        {
            var rendition = new ReservationRendition
            {
                Quantity = 3,
                Date = "02-07-2020"
            };
            var response = reservationController
                                .Post(rendition)
                                .ExecuteAsync(new CancellationToken())
                                .Result;

            Assert.AreEqual(response.StatusCode,
                            HttpStatusCode.OK);
        }        

        [TestMethod]
        public void reservation_cannot_be_fulfilled_because_of_capacity()
        {
            var rendition = new ReservationRendition
            {
                Quantity = 6,
                Date = "02-07-2020"
            };
            var response = reservationController
                                .Post(rendition)
                                .ExecuteAsync(new CancellationToken())
                                .Result;

            Assert.AreEqual(response.StatusCode,
                            HttpStatusCode.Forbidden);
        }

        [TestMethod]
        public void reservation_cannot_be_fulfilled_because_of_invalid_requested_date()
        {
            var rendition = new ReservationRendition
            {
                Quantity = 6,
                Date = "02-07-20sd"
            };
            var response = reservationController
                                .Post(rendition)
                                .ExecuteAsync(new CancellationToken())
                                .Result;

            Assert.AreEqual(response.StatusCode,
                            HttpStatusCode.BadRequest);
        }
    }
}
