namespace OfficeManagerApp.Areas.Helpers
{
    public static class FilterBaseHelper
    {
        public static void AddFilter<T>(ref Func<T, bool> tempFilter, Func<T, bool> filter)
        {
            if (tempFilter == null)
            {
                tempFilter = filter;
            }
            else
            {
                tempFilter = FuncsHelper<T>.AndAlso(tempFilter, filter);
            }
        }
    }
}
