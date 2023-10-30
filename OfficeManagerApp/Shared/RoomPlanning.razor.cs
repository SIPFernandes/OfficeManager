using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Data.Constants.ConstsEN;

namespace OfficeManagerApp.Shared
{
    public partial class RoomPlanning : ComponentBase
    {
        [Parameter]
        public SeatMatrix[,] Matrix { get; set; }

        [Parameter]
        public bool IsBooking { get; set; }

        [Parameter]
        public EventCallback<int> SeatSelectedIdChanged { get; set; }

        private int _rows;
        private int _columns;
        private string _iconSelected;
        private int _selectedX = -1;
        private int _selectedY = -1;

        protected override void OnInitialized()
        {
            _rows = Matrix.GetLength(0);

            _columns = Matrix.GetLength(1);
        }

        private void SelectIcon(string icon)
        {
            _iconSelected = icon;

            StateHasChanged();
        }

        private void ChangeRoomPlanning(int row, int column)
        {
            if (IsBooking)
            {
                if (Matrix[row, column] is not null)
                {
                    if (Matrix[row, column].SeatName == ConstEN.Chair)
                    {
                        Matrix[_selectedX, _selectedY].SeatName = ConstEN.Chair;

                        Matrix[row, column].SeatName = ConstEN.Selected;

                        _selectedX = row;

                        _selectedY = column;
                    }

                    SeatSelectedIdChanged.InvokeAsync(Matrix[_selectedX, _selectedY].SeatId);
                }
            }
            else
            {
                if(Matrix[row, column] is null)
                {
                    Matrix[row, column] = new SeatMatrix();
                }

                Matrix[row, column].SeatName = Matrix[row, column].SeatName == _iconSelected
                ? string.Empty
                : string.IsNullOrEmpty(Matrix[row, column].SeatName)
                    ? _iconSelected
                    : Matrix[row, column].SeatName;
            }
        }

        private void SetSelectedSeat(int x, int y)
        {
            _selectedX = x;
            _selectedY = y;
        }
        public class SeatMatrix
        {
            public string SeatName { get; set; }
            public int SeatId { get; set; }
            public string UserName { get; set; }
        }
    }
}
