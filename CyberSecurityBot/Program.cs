using System;
using System.IO;
using System.Media;
using Chatbot;
using Utils;

class Program
{
    static void Main()
    {
        ConsoleHelper.SetColor(ConsoleColor.Cyan);

        try
        {
            string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "assets", "greeting.wav");
            SoundPlayer player = new SoundPlayer(audioPath);
            player.PlaySync();
        }
        catch
        {
            Console.WriteLine("Voice file not found.");
        }

        ConsoleHelper.DisplayLogo();

        ConsoleHelper.SetColor(ConsoleColor.Green);
        Console.Write("Enter your name: ");
        string name = Console.ReadLine() ?? "User";

        Bot bot = new Bot(name);
        bot.StartChat();
    }
}