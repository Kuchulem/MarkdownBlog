using System;
using System.Runtime.CompilerServices;

namespace Kuchulem.MarkdownBlog.Libs.Extensions
{
    /// <summary>
    /// Extensions for object class objects
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Writes a debug line
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="callerMember"></param>
        /// <param name="message"></param>
        public static void WriteDebugLine(this object obj, [CallerMemberName] string callerMember = "", string message = "")
        {
            var suffix = string.IsNullOrEmpty(message) ? "" : $" : {message}";
            Console.WriteLine($"{obj.GetType().Name}.{callerMember}{suffix}");
        }
    }
}
