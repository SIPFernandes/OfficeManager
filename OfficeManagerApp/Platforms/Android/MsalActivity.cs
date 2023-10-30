using Android.App;
using Android.Content;
using Microsoft.Identity.Client;
using OfficeManagerApp.Data.Constants;

namespace OfficeManagerApp.Platforms.Android
{
    [Activity(Exported = true)]
    [IntentFilter(new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryBrowsable, Intent.CategoryDefault },
        DataHost = "auth",
        DataScheme = "msal" + AppConst.Identity.ApplicationId)]
    public class MsalActivity : BrowserTabActivity
    {
    }
}