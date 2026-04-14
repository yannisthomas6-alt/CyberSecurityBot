using System;
using System.IO;
using System.Threading;

namespace Utils
{
    public static class ConsoleHelper
    {
        public static void SetColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public static void DisplayLogo()
        {
            try
            {
                string logo = File.ReadAllText("Assets/logo.txt");
                Console.WriteLine(logo);
            }
            catch
            {
                Console.WriteLine("Logo file not found.");
            }
        }

        public static void TypingEffect(string text)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(20);
            }
            Console.WriteLine();
        }
    }
}