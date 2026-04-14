namespace Chatbot
{
    public static class Responses
    {
        public static string GetResponse(string input)
        {
            input = input.ToLower();

            if (input.Contains("how are you"))
                return "I'm doing well and ready to help you stay safe online.";

            if (input.Contains("purpose") || input.Contains("what's your purpose") || input.Contains("what is your purpose"))
                return "My purpose is to teach users about cybersecurity awareness.";

            if (input.Contains("what can i ask") || input.Contains("help"))
                return "You can ask me about password safety, phishing, and safe browsing.";

            if (input.Contains("password"))
                return "Use strong passwords with uppercase and lowercase letters, numbers, and symbols. Do not reuse passwords.";

            if (input.Contains("phishing"))
                return "Phishing is a scam where attackers trick you into giving personal information through fake emails, messages, or websites.";

            if (input.Contains("safe browsing") || input.Contains("browsing") || input.Contains("browse"))
                return "For safe browsing, only visit trusted websites, check for HTTPS, and avoid suspicious downloads and links.";

            return "I didn't quite understand that. Could you rephrase?";
        }
    }
}
