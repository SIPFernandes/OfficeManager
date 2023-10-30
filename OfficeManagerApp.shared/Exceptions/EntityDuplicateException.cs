namespace OfficeManager.Shared.Exceptions
{
    public class EntityDuplicateException : Exception
    {
        public EntityDuplicateException()
        {
        }

        public EntityDuplicateException(string message) : base("This " + message + " already exist.")
        {
        }

        public EntityDuplicateException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
