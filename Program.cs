using System;
using Hors;
using Hors.Models;

namespace HorsConsole
{
    class Program
    {
        private static bool WithTokens = false;
        
        static void Main(string[] args)
        {
            var hors = new HorsTextParser();
            var currentDate = DateTime.Now;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("\nCurrent date is: " + currentDate.DayOfWeek + ", " + currentDate + "\n");
            Console.ResetColor();
            while(true)
            {
                var text = Console.ReadLine();
                if (text == "quit" || text == "exit")
                {
                    Console.WriteLine("\nLogging you out.\n");
                    break;
                }

                if (text == "tokens")
                {
                    WithTokens = !WithTokens;
                    Console.WriteLine("Tokens mode: " + (WithTokens ? "on" : "off") + "\n");
                    continue;
                }
                
                var result = hors.Parse(text, currentDate);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("  text: ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(WithTokens ? result.TextWithTokens : CapitalizeFirst(result.Text));
                foreach (var date in result.Dates)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("  date: ");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(
                        date.Type
                        + ", "
                    );
                    Console.Write((date.HasTime ? "HasTime, " : "NoTime, "));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(((date.Type == DateTimeTokenType.Fixed && date.HasTime)
                        ? date.DateFrom.ToString()
                        : (date.DateFrom + " - " + date.DateTo)));
                }
                Console.WriteLine();
                Console.ResetColor();
            }
        }
        
        public static string CapitalizeFirst(string s)
        {
            return s.Length == 0 ? "" : s.Substring(0, 1).ToUpper() + s.Substring(1);
        }
    }
}