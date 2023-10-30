namespace NotificationsServiceApi.Areas.Services
{
    public interface INotificationService
    {
        public void TreatMessageReceived(object? obj, string msg);
    }
}
