using System.Runtime.CompilerServices;

namespace RabbitMQClient.Shared.Areas.Helpers
{
    public static class MethodHelper
    {
        public static string? GetAsyncMethodName([CallerMemberName] string? name = null) => name;       
    }
}
