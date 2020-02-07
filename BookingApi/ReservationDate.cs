using System;

namespace Ploeh.Samples.BookingApi
{
    public class ReservationDate
    {
        private readonly string value;

        public ReservationDate(string value)
        {
            if (!IsValid(value))
                throw new ArgumentException("Invalid date value");

            this.value = value;
        }

        private DateTimeOffset date;

        public DateTimeOffset Value
        {
            get
            {
                if(date == null)                    
                    DateTimeOffset.TryParse(value, out date);

                return date;
            }
        }

        public static bool IsValid(string date)
        {
            DateTimeOffset requestedDate;
            if (!DateTimeOffset.TryParse(date, out requestedDate))
                return false;

            return true;        
        }
    }
}