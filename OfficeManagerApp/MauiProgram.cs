using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Graph;
using OfficeManagerApp.Areas.Services.BookingService;
using OfficeManagerApp.Areas.Services.BookingService.SeatsAvailableService;
using OfficeManagerApp.Areas.Services.CompanyServices;
using OfficeManagerApp.Areas.Services.Implementations;
using OfficeManagerApp.Areas.Services.Interfaces;
using OfficeManagerApp.Areas.Services.UserService;
using OfficeManagerApp.Data.Constants;

namespace OfficeManagerApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddScoped<IJSInteropService, JSInteropService>();
            builder.Services.AddScoped<IPageHistoryState, PageHistoryState>();
            builder.Services.AddScoped<IModalDialogService, ModalDialogService>();
           
            builder.Services.AddBlazorWebViewDeveloperTools();

            builder.Services.AddScoped<IPlatformService, PlatformService>();            
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, ExternalAuthStateProvider>();

            builder.Services.AddScoped<IUserHttpService, UserHttpService>();
            builder.Services.AddScoped<IBookingHttpService, BookingHttpService>();
            builder.Services.AddScoped<ISeatsAvailableHttpService, SeatsAvailableHttpService>();
            builder.Services.AddScoped<IDateSelectedService, DateSelectedService>();
            builder.Services.AddScoped<ICompanyHttpService, CompanyHttpService>();

            builder.Services.AddHttpClient<IUserHttpService, UserHttpService>(
                nameof(UserHttpService), x =>
                {
                    x.BaseAddress = new Uri(AppConst.ApiUrlConst.UserApi.ApiBaseUrl);
                })
#if DEBUG                
                .ConfigureHttpMessageHandlerBuilder(x =>
                    x.PrimaryHandler = GetInsecureHandler())
#endif
                ;

            builder.Services.AddHttpClient<ISeatsAvailableHttpService, SeatsAvailableHttpService>(
                nameof(SeatsAvailableHttpService), x =>
                {
                    x.BaseAddress = new Uri(AppConst.ApiUrlConst.BookingApi.ApiBaseUrl);
                })
#if DEBUG                
                .ConfigureHttpMessageHandlerBuilder(x =>
                    x.PrimaryHandler = GetInsecureHandler())
#endif
                ;

            builder.Services.AddHttpClient<IBookingHttpService, BookingHttpService>(
                nameof(BookingHttpService), x =>
                {
                    x.BaseAddress = new Uri(AppConst.ApiUrlConst.BookingApi.ApiBaseUrl);
                })
#if DEBUG                
                .ConfigureHttpMessageHandlerBuilder(x =>
                    x.PrimaryHandler = GetInsecureHandler())
#endif
                ;

            builder.Services.AddHttpClient<ICompanyHttpService, CompanyHttpService>(
                nameof(CompanyHttpService), x =>
                {
                    x.BaseAddress = new Uri(AppConst.ApiUrlConst.CompaniesApi.ApiBaseUrl);
                })
#if DEBUG                
                .ConfigureHttpMessageHandlerBuilder(x =>
                    x.PrimaryHandler = GetInsecureHandler())
#endif
                ;

            return builder.Build();
        }

        public static HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new()
            {                
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
            };
            return handler;
        }
    }
}

