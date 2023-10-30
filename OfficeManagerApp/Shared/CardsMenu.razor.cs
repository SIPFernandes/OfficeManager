using Microsoft.AspNetCore.Components;
using OfficeManagerApp.Data.Constants;
using OfficeManagerApp.Data.Constants.ConstsEN;

namespace OfficeManagerApp.Shared
{
    public partial class CardsMenu : ComponentBase
    {
        public class Menu
        {
            public string Title { get; set; }
            public string Image { get; set; }
            public string Url { get; set; }

        }

        public IList<Menu> MenuList = new List<Menu>() { 
            new Menu()
            {
                Title = @ConstEN.Locations,
                Image = @AssetsConst.IconsConst.Location2,
                Url = @AppConst.LocationUrlConst.LocationsPage
            },
            new Menu()
            {
                Title = @ConstEN.Companies,
                Image = @AssetsConst.IconsConst.Building,
                Url = @AppConst.CompanyUrlConst.CompaniesPage,
            },
            //new Menu() TODO: users page
            //{
            //    Title = @ConstEN.Users,
            //    Image = @AssetsConst.IconsConst.Users,
            //    Url = "",
            //},
            new Menu()
            {
                Title = @ConstEN.Bookings,
                Image = @AssetsConst.IconsConst.Calendar,
                Url = @AppConst.BookingUrlConst.BookingsPage,
            }
        };
    }
}
