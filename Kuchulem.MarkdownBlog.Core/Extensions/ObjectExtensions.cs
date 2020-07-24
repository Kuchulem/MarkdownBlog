using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Kuchulem.MarkdownBlog.Client.Extensions
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
