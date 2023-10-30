namespace NotificationsServiceApi.Data.Consts
{
    public class ReservationServiceConst
    {           
        public const string ReservationService = "ReservationService";

        public class Actions
        {
            public class Delete
            {
                public const string DeleteReservation = "DeleteReservation";
                public const string Subject = "Reservation Canceled";
                public const string msg = "{0} canceled your reservation {1}.";
            }                
        }                            
    }
}
