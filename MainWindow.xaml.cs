using System.Media;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CybersecurityAwarenessBot_Part2;

public partial class MainWindow : Window
{
    private readonly Random _random = new();
    private readonly Dictionary<string, List<string>> _keywordResponses;
    private string _lastTopic = string.Empty;
    private string _userName = string.Empty;
    private string _favouriteTopic = string.Empty;

    public MainWindow()
    {
        InitializeComponent();

        _keywordResponses = BuildKeywordResponses();
        AsciiLogoTextBlock.Text = BuildAsciiLogo();
        PlayVoiceGreeting();
        AddBotMessage("Hello! I am your Cybersecurity Awareness Bot. What is your name?");
    }

    private Dictionary<string, List<string>> BuildKeywordResponses()
    {
        return new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["password"] = new List<string>
            {
                "Use strong, unique passwords for each account. Avoid names, birthdays and simple words.",
                "A good password should be long and hard to guess. A passphrase like 'River!Cloud!Tiger!91' is stronger than a short password.",
                "Do not reuse the same password on many websites. If one site is hacked, other accounts can also be at risk."
            },
            ["phishing"] = new List<string>
            {
                "Be careful of emails or messages asking for personal information. Scammers often pretend to be trusted companies.",
                "Before clicking a link, check the sender, spelling, urgency and the website address.",
                "A phishing message often pressures you to act quickly, such as 'verify now' or 'your account will be closed'."
            },
            ["scam"] = new List<string>
            {
                "If an offer sounds too good to be true, it probably is. Always verify before sending money or personal details.",
                "Do not trust random messages promising prizes, jobs or investments without checking the source.",
                "Scammers use fear and excitement to make people act fast. Slow down and verify first."
            },
            ["privacy"] = new List<string>
            {
                "Protect your privacy by limiting what you share online, especially your address, school, phone number or ID details.",
                "Review privacy settings on social media so only trusted people can see your personal information.",
                "Be careful with apps that request unnecessary permissions like contacts, camera, microphone or location."
            },
            ["malware"] = new List<string>
            {
                "Malware is harmful software that can steal information or damage your device. Avoid downloading unknown files.",
                "Keep your operating system, browser and antivirus updated to reduce malware risks.",
                "Do not open attachments from unknown senders, especially files ending in .exe, .bat or suspicious zipped folders."
            },
            ["social engineering"] = new List<string>
            {
                "Social engineering is when attackers manipulate people instead of hacking systems directly.",
                "Be careful when someone asks for passwords, OTPs or personal details, even if they sound official.",
                "Attackers may pretend to be IT support, bank staff or delivery companies to gain your trust."
            }
        };
    }

    private string BuildAsciiLogo()
    {
        return @"
  ____      _               
 / ___|   _| |__   ___ _ __ 
| |  | | | | '_ \ / _ \ '__|
| |__| |_| | |_) |  __/ |   
 \____\__, |_.__/ \___|_|   
      |___/                 
  SECURITY AWARENESS BOT
";
    }

    private void PlayVoiceGreeting()
    {
        try
        {
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");
            if (System.IO.File.Exists(path))
            {
                using SoundPlayer player = new(path);
                player.Play();
            }
        }
        catch
        {
            // The app must not crash if the audio file cannot play.
        }
    }

    private void SendButton_Click(object sender, RoutedEventArgs e) => ProcessUserInput();

    private void UserInputTextBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
        {
            ProcessUserInput();
        }
    }

    private void ClearButton_Click(object sender, RoutedEventArgs e)
    {
        ChatPanel.Children.Clear();
        AddBotMessage("Chat cleared. Ask me about password safety, phishing, scams, privacy, malware or social engineering.");
    }

    private void QuickTopic_Click(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Content is string topic)
        {
            UserInputTextBox.Text = $"Tell me about {topic.ToLower()}.";
            ProcessUserInput();
        }
    }

    private void ProcessUserInput()
    {
        string input = UserInputTextBox.Text.Trim();
        if (string.IsNullOrWhiteSpace(input))
        {
            AddBotMessage("Please type a question or topic so I can help you.");
            return;
        }

        AddUserMessage(input);
        UserInputTextBox.Clear();

        string response = GenerateResponse(input);
        AddBotMessage(response);
        UpdateMemoryPanel();
    }

    private string GenerateResponse(string input)
    {
        string lowerInput = input.ToLower();
        string sentimentPrefix = DetectSentiment(lowerInput);

        string memoryResponse = HandleMemory(input, lowerInput);
        if (!string.IsNullOrEmpty(memoryResponse))
        {
            return sentimentPrefix + memoryResponse;
        }

        if (IsFollowUp(lowerInput))
        {
            if (!string.IsNullOrEmpty(_lastTopic) && _keywordResponses.ContainsKey(_lastTopic))
            {
                return sentimentPrefix + $"Here is more about {_lastTopic}: " + GetRandomResponse(_lastTopic);
            }

            return sentimentPrefix + "I can explain more, but please first tell me the cybersecurity topic you want help with.";
        }

        foreach (string keyword in _keywordResponses.Keys)
        {
            if (lowerInput.Contains(keyword))
            {
                _lastTopic = keyword;
                return sentimentPrefix + GetRandomResponse(keyword);
            }
        }

        if (lowerInput.Contains("help") || lowerInput.Contains("topics"))
        {
            return "You can ask me about password safety, phishing, scams, privacy, malware or social engineering.";
        }

        return "I'm not sure I understand. Can you try rephrasing? You can ask me about password safety, phishing, scams, privacy, malware or social engineering.";
    }

    private string HandleMemory(string originalInput, string lowerInput)
    {
        Match nameMatch = Regex.Match(originalInput, @"\b(my name is|i am|i'm)\s+([A-Za-z]+)", RegexOptions.IgnoreCase);
        if (nameMatch.Success)
        {
            _userName = nameMatch.Groups[2].Value;
            return $"Nice to meet you, {_userName}. I will remember your name.";
        }

        Match topicMatch = Regex.Match(originalInput, @"\b(interested in|favourite topic is|favorite topic is|i like)\s+([A-Za-z\s]+)", RegexOptions.IgnoreCase);
        if (topicMatch.Success)
        {
            string topic = topicMatch.Groups[2].Value.Trim().ToLower();
            foreach (string keyword in _keywordResponses.Keys)
            {
                if (topic.Contains(keyword))
                {
                    _favouriteTopic = keyword;
                    _lastTopic = keyword;
                    return $"Great! I will remember that you are interested in {_favouriteTopic}. " + GetRandomResponse(keyword);
                }
            }
        }

        if (lowerInput.Contains("remember about me") || lowerInput.Contains("what do you remember"))
        {
            if (string.IsNullOrEmpty(_userName) && string.IsNullOrEmpty(_favouriteTopic))
            {
                return "I do not have any saved details yet. You can say 'My name is ...' or 'I am interested in privacy'.";
            }

            List<string> details = new();
            if (!string.IsNullOrEmpty(_userName)) details.Add($"your name is {_userName}");
            if (!string.IsNullOrEmpty(_favouriteTopic)) details.Add($"you are interested in {_favouriteTopic}");
            return "I remember that " + string.Join(" and ", details) + ".";
        }

        return string.Empty;
    }

    private string DetectSentiment(string lowerInput)
    {
        if (lowerInput.Contains("worried") || lowerInput.Contains("scared") || lowerInput.Contains("afraid") || lowerInput.Contains("anxious"))
        {
            return "It is completely understandable to feel worried. I will help you stay safe. ";
        }

        if (lowerInput.Contains("frustrated") || lowerInput.Contains("annoyed") || lowerInput.Contains("angry"))
        {
            return "I understand that this can be frustrating. Let us take it step by step. ";
        }

        if (lowerInput.Contains("curious") || lowerInput.Contains("interested"))
        {
            return "Great question. Being curious is a good way to learn cybersecurity. ";
        }

        return string.Empty;
    }

    private bool IsFollowUp(string lowerInput)
    {
        return lowerInput.Contains("tell me more") ||
               lowerInput.Contains("explain more") ||
               lowerInput.Contains("another tip") ||
               lowerInput.Contains("more details") ||
               lowerInput.Contains("give me more");
    }

    private string GetRandomResponse(string topic)
    {
        List<string> responses = _keywordResponses[topic];
        string response = responses[_random.Next(responses.Count)];

        if (!string.IsNullOrEmpty(_userName))
        {
            response = $"{_userName}, {char.ToLower(response[0])}{response[1..]}";
        }

        return response;
    }

    private void AddUserMessage(string message)
    {
        AddMessageBubble("You", message, "#2563EB", HorizontalAlignment.Right);
    }

    private void AddBotMessage(string message)
    {
        AddMessageBubble("Bot", message, "#334155", HorizontalAlignment.Left);
    }

    private void AddMessageBubble(string sender, string message, string colour, HorizontalAlignment alignment)
    {
        Border bubble = new()
        {
            Background = (Brush)new BrushConverter().ConvertFromString(colour),
            CornerRadius = new CornerRadius(14),
            Padding = new Thickness(12),
            Margin = new Thickness(0, 6, 0, 6),
            MaxWidth = 570,
            HorizontalAlignment = alignment
        };

        StackPanel stack = new();
        stack.Children.Add(new TextBlock
        {
            Text = sender,
            Foreground = Brushes.White,
            FontWeight = FontWeights.Bold,
            Margin = new Thickness(0, 0, 0, 4)
        });
        stack.Children.Add(new TextBlock
        {
            Text = message,
            Foreground = Brushes.White,
            TextWrapping = TextWrapping.Wrap,
            FontSize = 14
        });

        bubble.Child = stack;
        ChatPanel.Children.Add(bubble);
    }

    private void UpdateMemoryPanel()
    {
        List<string> memory = new();
        if (!string.IsNullOrEmpty(_userName)) memory.Add($"Name: {_userName}");
        if (!string.IsNullOrEmpty(_favouriteTopic)) memory.Add($"Favourite topic: {_favouriteTopic}");
        if (!string.IsNullOrEmpty(_lastTopic)) memory.Add($"Last topic: {_lastTopic}");

        MemoryTextBlock.Text = memory.Count == 0 ? "No user details saved yet." : string.Join("\n", memory);
    }
}
