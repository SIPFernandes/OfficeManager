namespace OfficeManagerApp.Areas.Services.Interfaces
{
    public interface IPageHistoryState
    {
        public void NavigateTo(string url);
        public void NavigateBack();
        public string GetBackPageUrl();
        public void Dispose();
    }
}
