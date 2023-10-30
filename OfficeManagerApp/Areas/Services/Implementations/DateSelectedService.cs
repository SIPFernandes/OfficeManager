using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    public class DateSelectedService : IDateSelectedService
    {
        private DateTime dateSelected;

        public DateTime GetDate()
        {
            return dateSelected;
        }

        public void SelectDate(DateTime date)
        {
            dateSelected = date;
        }
    }
}
