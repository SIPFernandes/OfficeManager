using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Areas.Services.Implementations
{
    public class PageHistoryState : IPageHistoryState, IDisposable
    {
        private readonly NavigationManager _navigationManager;
        private readonly List<string> _history;

        public PageHistoryState(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
            _history = new List<string>
            {
                _navigationManager.Uri
            };

            _navigationManager.LocationChanged += OnLocationChanged;
        }

        public void NavigateTo(string url)
        {
            _navigationManager.NavigateTo(url);
        }

        public bool CanNavigateBack => _history.Count >= 2;

        public void NavigateBack()
        {
            if (!CanNavigateBack) return;

            var backPageUrl = _history[^2];

            _history.RemoveRange(_history.Count - 2, 2);

            _navigationManager.NavigateTo(backPageUrl);
        }

        public string GetBackPageUrl()
        {
            return _history[^2];
        }

        private void OnLocationChanged(object sender, LocationChangedEventArgs e)
        {
            if(e.Location != _navigationManager.BaseUri)
            {
                if (!_history.Contains(e.Location))
                {
                    _history.Add(e.Location);
                }
                else
                {
                    int index = _history.IndexOf(e.Location);

                    _history.RemoveRange(index + 1, _history.Count - index - 1);
                }
            }
        }

        public void Dispose()
        {
            _navigationManager.LocationChanged -= OnLocationChanged;
        }
    }
}