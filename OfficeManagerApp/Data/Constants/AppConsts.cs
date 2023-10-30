namespace OfficeManagerApp.Data.Constants
{
    public class AppConst
    {
        public static class ApiUrlConst
        {
            public class CompaniesApi
            {
#if DEBUG
#if ANDROID
                private static readonly string Base = "http://10.0.2.2";
#else
                private static readonly string Base = "http://localhost";
#endif
                public static readonly string ApiBaseUrl = $"{Base}:5112/";
#else
                public static readonly string ApiBaseUrl = "https://YOUR_APP_SERVICE.azurewebsites.net/";
#endif
                public const string CompanyUrl = "api/Company";
                public const string FacilityUrl = "api/Facility";
                public const string FacilityRoomUrl = "api/Facility/Room";
                public const string RoomFacilityUrl = "api/RoomFacility";
                public const string OfficeUrl = "api/Office";
                public const string OfficeCompanyUrl = "api/Office/Company";
                public const string RoomUrl = "api/Room";
                public const string RoomOfficeUrl = "api/Room/Office";
                public const string LocationUrl = "api/Location";
                public const string SeatUrl = "api/Seat";
                public const string SeatRoomUrl = "api/Seat/Room";
            }  
            
            public class BookingApi
            {
#if DEBUG
#if ANDROID
                private static readonly string Base = "http://10.0.2.2";
#else
                private static readonly string Base = "http://localhost";
#endif
                public static readonly string ApiBaseUrl = $"{Base}:5033/";
#else
                public static readonly string ApiBaseUrl = "https://YOUR_APP_SERVICE.azurewebsites.net/";
#endif

                public const string BookingUrl = "api/Booking";
                public const string SeatsAvailableUrl = "api/SeatsAvailable";
            }

            public class UserApi
            {
#if DEBUG
#if ANDROID
                private static readonly string Base = "http://10.0.2.2";
#else
                private static readonly string Base = "http://localhost";
#endif
                public static readonly string ApiBaseUrl = $"{Base}:5044/";
#else
                public static readonly string ApiBaseUrl = "https://YOUR_APP_SERVICE.azurewebsites.net/";
#endif

                public const string UserUrl = "api/User";
            }

        }

        public static class Identity
        {            
            /// <summary>
            /// The application (client) ID for the native app within Azure Active Directory
            /// </summary>
            public const string ApplicationId = "4a290e50-fa4f-45db-b8a3-eb7a0e687b41";
            /// <summary>
            /// The tenent ID
            /// </summary>
            public const string TenantId = "d3d402ef-42d8-4c14-a5dd-09fc49aaef4b";
            /// <summary>
            /// The list of scopes to request
            /// </summary>
            public static string[] CompanyApiScopes = new[]
            {
                "api://bea4b988-539f-4c65-b136-c79df6b27901/access_as_user"
            };

            public static string[] BookingApiScopes = new[]
            {
                "api://e2d8cb95-8d2d-430e-95ac-81b761e6887c/access_as_user"
            };            
        }

        public static class OtherPagesUrlConst
        {
            public const string LoginPage = "/login";
            public const string IndexPage = "/";
            public const string HomePage = "/home";
            public const string OtherMenuPage = "/other-menu";
            public const string DiscoverOfficesPage = "/discover-offices";
            public const string DiscoverRoomsPage = "/discover-rooms/{officeId:int}";
            public const string DiscoverRoomsPageFormat = "/discover-rooms/{0}";
        }

        public static class ForgotPassword
        {
            public const string FPEmail = "/fpemail";
            public const string FPInbox = "/fpinbox";
            public const string FPPassword = "/fppassword";
        }

        public static class LocationUrlConst
        {
            public const string LocationsPage = "/locations";
            public const string LocationIdPage = "/location/{locationId:int}";
            public const string LocationIdPageFormat = "/location/{0}";
        }

        public static class CompanyUrlConst
        {
            public const string CompaniesPage = "/companies";
            public const string CompanyIdPage = "/company/{companyId:int}";
            public const string CompanyIdPageFormat = "/company/{0}";
        }
        
        public static class OfficeUrlConst
        {
            public const string OfficesPage = "/offices/{companyId:int}";
            public const string OfficesPageFormat = "/offices/{0}";
            public const string OfficeIdPage = "/office/{companyId:int}/{officeId:int}";
            public const string OfficeIdPageFormat = "/office/{0}/{1}";
        }

        public static class RoomUrlConst
        {
            public const string RoomsPage = "/office/{officeId:int}/rooms";
            public const string RoomsPageFormat = "/office/{0}/rooms";
            public const string RoomDetailsPage = "/roomdetails/{roomId:int}";
            public const string RoomDetailsPageFormat = "/roomdetails/{0}";
            public const string RoomIdPage = "/office/{officeId:int}/{officename}/room/{roomId:int}";
            public const string RoomIdPageFormat = "/office/{0}/{1}/room/{2}";
        }

        public static class BookingUrlConst
        {
            public const string BookingsPage = "/bookings";
            public const string BookingIdPage = "/booking/{roomId:int}/{bookingId:int}";
            public const string BookingIdPageFormat = "/booking/{0}/{1}";
        }

    }
}
