namespace OfficeManagerApp.Areas.Helpers
{
    public static class FuncsHelper<T>
    {
        public static Func<T, bool> AndAlso(Func<T, bool> predicate1, Func<T, bool> predicate2)
        {
            return arg => predicate1(arg) && predicate2(arg);
        }
    }
}
