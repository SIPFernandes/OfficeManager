using OfficeManagerApp.Data.Constants.ConstsEN;

namespace OfficeManagerApp.Data.Helpers
{
    public class RoomTypeHelper
    {
        public static IList<string> RoomTypes = new List<string>() 
        { 
            ConstEN.WorkingRoom, 
            ConstEN.MeetingRoom 
        };
    }
}
