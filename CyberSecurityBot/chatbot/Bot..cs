using System;

namespace Chatbot
{
    public class Bot
    {
        private readonly string userName;

        public Bot(string name)
        {
            userName = string.IsNullOrWhiteSpace(name) ? "User" : name;
        }

        public void StartChat()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\nHello {userName}! Ask me anything about cybersecurity.");
            Console.ResetColor();

            while (true)
            {
                Console.Write("\n> ");
                string input = Console.ReadLine() ?? "";

                if (InputValidator.IsInvalid(input))
                {
                    Console.WriteLine("Please enter a valid question.");
                    continue;
                }

                if (input.ToLower() == "exit")
                {
                    Console.WriteLine("Goodbye! Stay safe online.");
                    break;
                }

                string response = Responses.GetResponse(input);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(response);
                Console.ResetColor();
            }
        }
    }
}