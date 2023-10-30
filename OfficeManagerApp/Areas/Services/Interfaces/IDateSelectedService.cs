namespace OfficeManagerApp.Areas.Services.Interfaces
{
    public interface IDateSelectedService
    {
        public void SelectDate(DateTime date);
        public DateTime GetDate();
    }
}
