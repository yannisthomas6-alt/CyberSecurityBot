namespace Chatbot
{
    public static class InputValidator
    {
        public static bool IsInvalid(string input)
        {
            return string.IsNullOrWhiteSpace(input);
        }
    }
}
