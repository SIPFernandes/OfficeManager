using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using OfficeManagerApp.Areas.Services.Interfaces;

namespace OfficeManagerApp.Shared
{
    public partial class CalendarHorizontal : ComponentBase
    {
        [Parameter]
        public EventCallback<DateTime> DateSelectedChanged { get; set; }

        [Parameter]
        public bool IsDateDisabled { get; set; }

        [Inject]
        private IDateSelectedService _dateSelectedService { get; set; }

        private DateTime _dateSelected;
        private IList<DateTime> _dates = new List<DateTime>();
        private string _monthDay;

        protected override void OnInitialized()
        {
            _dateSelected = _dateSelectedService.GetDate().Date;

            if(_dateSelected == DateTime.MinValue)
            {
                _dateSelected = DateTime.Today;
            }

            for (int i = 0; i < 5; i++)
            {
                _dates.Add(_dateSelected.AddDays(i));
            }

            _monthDay = _dates.First().ToString("MMMM");
        }

        private void selectDay(DateTime date)
        {
            if(date != _dateSelected && !IsDateDisabled)
            {
                _dateSelected = date;

                DateSelectedChanged.InvokeAsync(date);
            }
        }

        private void HandleKeyDown(KeyboardEventArgs e)
        {
            if(e.Key == "ArrowLeft")
            {
                GetDates(false);
            }
            else if(e.Key == "ArrowRight")
            {
                GetDates(true);
            }
        }

        private void GetDates(bool isNext)
        {
            var firstDay = DateTime.Today;

            if (isNext)
            {
                firstDay = _dates.Last();
            }
            else
            {
                var datesFirst = _dates.First().AddDays(-5);

                if (datesFirst > firstDay)
                {
                    firstDay = datesFirst;
                }
            }

            _dates.Clear();

            for (int i = 0; i < 5; i++)
            {
                _dates.Add(firstDay.AddDays(i));
            }

            _monthDay = _dates.First().ToString("MMMM");
        }
    }
}
