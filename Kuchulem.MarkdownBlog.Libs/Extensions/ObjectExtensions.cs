using System;
using System.Runtime.CompilerServices;

namespace Kuchulem.MarkDownBlog.Libs.Extensions
{
    public static class ObjectExtensions
    {
        public static void WriteDebugLine(this object obj, [CallerMemberName] string callerMember = "", string message = "")
        {
            var suffix = string.IsNullOrEmpty(message) ? "" : $" : {message}";
            Console.WriteLine($"{obj.GetType().Name}.{callerMember}{suffix}");
        }
    }
}
