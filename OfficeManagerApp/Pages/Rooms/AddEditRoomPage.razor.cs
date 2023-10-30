using Microsoft.AspNetCore.Components;
using OfficeManager.Shared.Entities;
using OfficeManager.Shared.Helper;
using OfficeManager.Shared.Request_Model;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Helpers;
using OfficeManagerApp.Shared;
using static OfficeManagerApp.Shared.RoomPlanning;

namespace OfficeManagerApp.Pages.Rooms
{
    public partial class AddEditRoomPage : ComponentBase
    {
        [CascadingParameter]
        public Loading LoadingComponent { get; set; }

        [Parameter]
        public int OfficeId { get; set; }

        [Parameter]
        public string OfficeName { get; set; }

        [Parameter]
        public int RoomId { get; set; }

        [Inject]
        private ICompanyHttpService CompanyHttpService { get; set; }

        [Inject]
        private NavigationManager _navigationManager { get; set; }

        private RoomRequestModel roomRequestModel;
        private IList<string> _allRoomTypes = RoomTypeHelper.RoomTypes;
        private string msg = null;
        private List<string> messages { get; set; }
        private IList<Facility> _allFacilities;
        private IList<string> imageSources = new List<string>();
        private static int rows = 7;
        private static int columns = 12;
        private SeatMatrix[,] matrix = new SeatMatrix[rows, columns];

        protected async override Task OnInitializedAsync()
        {
            LoadingComponent.OpenLoadingComponent();

            if (RoomId != 0)
            {
                var room = await CompanyHttpService.OfficeHttpService
                    .RoomHttpService.Get(RoomId);

                roomRequestModel = new RoomRequestModel()
                {
                    Name = room.Name,
                    Type = room.Type,
                    Description = room.Description,
                    Images = room.Images.Select(x => x.File).ToList(),
                    OpeningHour = room.OpeningHour,
                    ClosingHour = room.ClosingHour,
                    Status = Status.Available.ToString(),
                    OfficeId = room.OfficeId,
                    RoomFacilities = room.RoomFacilities.Select(x => new RoomFacilityRequestModel()
                    {
                        Id = x.Id,
                        RoomId = x.RoomId,
                        FacilityId = x.FacilityId
                    }).ToList(),
                    Seats = room.Seats.Select(x => new SeatRequestModel()
                    {
                        Id = x.Id,
                        RoomId = x.RoomId,
                        Name = x.Name,
                        CoordinateX = x.CoordinateX,
                        CoordinateY = x.CoordinateY
                    }).ToList()
                };

                foreach (var seat in roomRequestModel.Seats)
                {
                    matrix[seat.CoordinateX, seat.CoordinateY] = new SeatMatrix
                    {
                        SeatName = seat.Name,
                        SeatId = seat.Id
                    };
                }
            }
            else
            {
                roomRequestModel = new RoomRequestModel()
                {
                    Status = Status.Available.ToString(),
                    OfficeId = OfficeId,
                    RoomFacilities = new List<RoomFacilityRequestModel>(),
                    Images = new List<string>(),
                    Seats = new List<SeatRequestModel>()
                };
            }

            imageSources = roomRequestModel.Images;
            _allFacilities = await CompanyHttpService.OfficeHttpService
                .RoomHttpService.FacilityHttpService.GetAll();

            LoadingComponent.CloseLoadingComponent();
        }

        protected async Task CreateOrEditRoom()
        {
            LoadingComponent.OpenLoadingComponent();

            msg = null;
            messages = new List<string>();

            try
            {
                if (RoomId == 0)
                {
                    AddSeats();

                    await CompanyHttpService.OfficeHttpService
                        .RoomHttpService.Insert(roomRequestModel);
                }
                else
                {
                    roomRequestModel.Seats.Clear();

                    AddSeats();

                    await CompanyHttpService.OfficeHttpService
                        .RoomHttpService.Update(roomRequestModel, RoomId);
                }
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            LoadingComponent.CloseLoadingComponent();

            if (msg != null)
            {
                messages = msg.Split(',').ToList();
            }
            else
            {
                Cancel();
            }
        }
        protected void Cancel()
        {
            _navigationManager?.NavigateTo(string.Format(AppConst.RoomUrlConst.RoomsPageFormat, OfficeId));
        }

        protected async Task DeleteRoom()
        {
            LoadingComponent.OpenLoadingComponent();

            await CompanyHttpService.OfficeHttpService
                .RoomHttpService.Delete(RoomId);

            _navigationManager?.NavigateTo(string.Format(AppConst.RoomUrlConst.RoomsPageFormat, OfficeId));
        }

        private void InsertOrRemoveRoomFacility(Facility facility)
        {
            if (roomRequestModel.RoomFacilities.Any(x => x.FacilityId == facility.Id))
            {
                var f = roomRequestModel.RoomFacilities.Single(x => x.FacilityId == facility.Id);

                roomRequestModel.RoomFacilities.Remove(f);
            }
            else
            {
                roomRequestModel.RoomFacilities.Add(new RoomFacilityRequestModel()
                {
                    FacilityId = facility.Id
                });
            }
        }

        private void OnUploadImageChanged(IList<string> images)
        {
            roomRequestModel.Images = images;
        }

        private void AddSeats()
        {
            for(int i = 0; i < rows; i++)
            {
                for(int j = 0; j < columns; j++)
                {
                    if (matrix[i, j] != null && !string.IsNullOrEmpty(matrix[i, j].SeatName))
                    {
                        roomRequestModel.Seats.Add(new SeatRequestModel()
                        {
                            Id = matrix[i, j].SeatId,
                            Name = matrix[i, j].SeatName,
                            CoordinateX = i,
                            CoordinateY = j,
                        });
                    }
                }
            }
        }
    }
}