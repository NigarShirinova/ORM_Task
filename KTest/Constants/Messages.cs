using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTest.Constants
{
    internal class Messages
    {
        public static void InvalidInputMessage(string message) { Console.WriteLine($"{message} is invalid, please try again"); }
        public static void InputMessage(string message) { Console.WriteLine($"Please input {message}"); }
        public static void SuccessMessage(string message) { Console.WriteLine($"{message} done successfully"); }
        public static void NotFoundMessage(string message) { Console.WriteLine($"{message} not found"); }
        public static void WantToChangeMessage(string message) { Console.WriteLine($"Do you want to change {message}"); }
        public static void ErrorOccuredMessage() { Console.WriteLine("Error Occured!"); }
    }
}
