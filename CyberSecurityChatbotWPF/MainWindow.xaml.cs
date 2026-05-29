using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberSecurityChatbotWPF
{
    public partial class MainWindow : Window
    {
        // Create chatbot object
        private ChatBot CyberForce;

        public MainWindow()
        {
            InitializeComponent();

            // Create chatbot
            CyberForce = new();
        }

        // Voice greeting when app opens
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Optional voice greeting removed - no implementation provided.
        }

        // START CHATBOT button
        private void proceed(object sender, RoutedEventArgs e)
        {
            home_grid.Visibility =
                Visibility.Hidden;

            username_grid.Visibility =
                Visibility.Visible;
        }

        // SUBMIT NAME button
        private void submit_name(object sender, RoutedEventArgs e)
        {
            string username =
                usernames_input.Text.Trim();

            // Validation
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show(
                    "Name cannot be empty.");

                return;
            }

            if (username.Length < 3)
            {
                MessageBox.Show(
                    "Name must contain at least 3 letters.");

                return;
            }

            if (!username.All(Char.IsLetter))
            {
                MessageBox.Show(
                    "Name must contain letters only.");

                return;
            }

            // Save username
            CyberForce.SetUserName(username);

            // Change screen
            username_grid.Visibility =
                Visibility.Hidden;

            chat_grid.Visibility =
                Visibility.Visible;

            // Welcome message
            AppendBotMessage(
                $"Hello {username}! Welcome to CyberForce AI.\n\n" +
                $"How can I help you stay safe online today?");
        }

        // SEND button
        private async void send(
            object sender,
            RoutedEventArgs e)
        {
            await SendMessage();
        }

        // ENTER key
        private async void UserInputBox_KeyDown(
            object sender,
            KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await SendMessage();
            }
        }

        // Main chatbot messaging
        private async Task SendMessage()
        {
            string userInput =
                question.Text.Trim();

            // Empty validation
            if (string.IsNullOrWhiteSpace(userInput))
            {
                return;
            }

            // Add user message
            AppendUserMessage(userInput);

            // Clear textbox
            question.Clear();

            // Typing effect
            AppendBotMessage("Typing...");

            await Task.Delay(1000);

            // Remove typing message
            chats.Items.RemoveAt(
                chats.Items.Count - 1);

            // Get chatbot response
            string response =
                CyberForce.ProcessInput(userInput);

            // Add bot response
            AppendBotMessage(response);

            // Auto scroll
            chats.ScrollIntoView(
                chats.Items[chats.Items.Count - 1]);
        }

        // User chat bubble
        private void AppendUserMessage(string message)
        {
            Border border = new()
            {
                Background = Brushes.Cyan,
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12),
                Margin = new Thickness(120, 5, 10, 5),
                HorizontalAlignment = HorizontalAlignment.Right
            };

            TextBlock text = new()
            {
                Text = "YOU: " + message,
                Foreground = Brushes.Black,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 16,
                MaxWidth = 450
            };

            border.Child = text;

            ListViewItem item = new()
            {
                Content = border,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0)
            };

            chats.Items.Add(item);
        }

        // Bot chat bubble
        private void AppendBotMessage(string message)
        {
            Border border = new()
            {
                Background = Brushes.DarkSlateBlue,
                CornerRadius = new CornerRadius(12),
                Padding = new Thickness(12),
                Margin = new Thickness(10, 5, 120, 5),
                HorizontalAlignment = HorizontalAlignment.Left
            };

            TextBlock text = new()
            {
                Text = "CYBERFORCE AI: " + message,
                Foreground = Brushes.White,
                TextWrapping = TextWrapping.Wrap,
                FontSize = 16,
                MaxWidth = 450
            };

            border.Child = text;

            ListViewItem item = new()
            {
                Content = border,
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0)
            };

            chats.Items.Add(item);
        }
    }
}
namespace CyberSecurityChatbotWPF
{
    public class ResponseManager
    {
        // Random object for random responses
        private static Random random =
            new Random();

        // Cybersecurity response database
        private static Dictionary<string, List<string>>
            responses =
            new Dictionary<string, List<string>>()
        {
            // PASSWORD RESPONSES
            {
                "password",
                new()
                {
                    "Strong passwords should contain uppercase letters, lowercase letters, numbers, and symbols.",

                    "Avoid using personal information like birthdays or names in your passwords.",

                    "Using a different password for every account greatly improves security.",

                    "Password managers help generate and safely store secure passwords.",

                    "Enable two-factor authentication together with strong passwords for maximum protection."
                }
            },

            // PHISHING RESPONSES
            {
                "phishing",
                new List<string>()
                {
                    "Phishing scams often pretend to be trusted companies to steal personal information.",

                    "Never click suspicious links or download unknown email attachments.",

                    "Always verify the sender's email address before responding to messages requesting sensitive information.",

                    "Many phishing attacks create urgency to pressure users into making mistakes.",

                    "Be cautious of fake login pages asking for passwords or banking information."
                }
            },

            // MALWARE RESPONSES
            {
                "malware",
                new List<string>()
                {
                    "Malware is harmful software designed to damage or access systems illegally.",

                    "Keep your antivirus software updated to protect against malware threats.",

                    "Avoid downloading pirated software because it may contain malware.",

                    "Ransomware is a dangerous type of malware that locks files until payment is made.",

                    "Always scan downloaded files before opening them."
                }
            },

            // PRIVACY RESPONSES
            {
                "privacy",
                new List<string>()
                {
                    "Review your social media privacy settings regularly.",

                    "Avoid oversharing personal information online.",

                    "Enable privacy settings on apps and devices to protect your information.",

                    "Be careful when using public Wi-Fi because attackers may intercept your data.",

                    "Using VPN services can help improve online privacy."
                }
            },

            // SCAM RESPONSES
            {
                "scam",
                new List<string>()
                {
                    "Online scams often promise rewards or prizes to trick users.",

                    "Never send money to unknown individuals online.",

                    "Scammers frequently create fake urgency to pressure victims.",

                    "Verify websites and organisations before sharing personal information.",

                    "If an offer seems to good to be true, it probably is."
                }
            },

            // SAFE BROWSING RESPONSES
            {
                "safe browsing",
                new List<string>()
                {
                    "Always look for HTTPS when browsing websites.",

                    "Avoid clicking pop-up advertisements from unknown websites.",

                    "Keep your browser updated for improved security.",

                    "Use trusted websites when entering personal information online.",

                    "Be careful when downloading files from unfamiliar sources."
                }
            },

            // TWO FACTOR AUTHENTICATION
            {
                "2fa",
                new List<string>()
                {
                    "Two-factor authentication adds an extra layer of protection to your accounts.",

                    "Even if hackers steal your password, 2FA can help stop unauthorised access.",

                    "Authenticator apps are generally safer than SMS verification codes.",

                    "Enable 2FA on email, banking, and social media accounts.",

                    "2FA significantly improves online account security."
                }
            }
        };

        // Main response method
        public static string GetResponse(
            string message,
            ref string currentTopic)
        {
            message = message.ToLower();

            // Greetings
            if (message.Contains("hello") ||
                message.Contains("hi") ||
                message.Contains("hey"))
            {
                return "Hello! 👋 I'm CyberForce AI. How can I help you stay safe online today?";
            }

            // Help command
            if (message.Contains("help"))
            {
                return
                    "You can ask me about:\n\n" +
                    "• Password safety\n" +
                    "• Phishing scams\n" +
                    "• Malware\n" +
                    "• Privacy protection\n" +
                    "• Safe browsing\n" +
                    "• Two-factor authentication\n" +
                    "• Online scams";
            }

            // Loop through all cybersecurity topics
            foreach (var topic in responses)
            {
                // Detect keyword
                if (message.Contains(topic.Key))
                {
                    // Save topic
                    currentTopic = topic.Key;

                    // Get random response
                    int index =
                        random.Next(topic.Value.Count);

                    return topic.Value[index];
                }
            }

            // Unknown message response
            return
                "I'm not fully sure what you mean.\n\n" +
                "Try asking me about cybersecurity topics like:\n" +
                "• phishing\n" +
                "• passwords\n" +
                "• malware\n" +
                "• scams\n" +
                "• privacy";
        }
    }
}

// stray duplicate removed
namespace CyberSecurityChatbotWPF
{
    public static class MemoryManager
    {
        private static string _favoriteTopic;

        public static string FavoriteTopic
        {
            get => _favoriteTopic;
            set => _favoriteTopic = value;
        }

        static MemoryManager()
        {
            _favoriteTopic = "";
        }
    }
}
namespace CyberSecurityChatbotWPF
{
    public class SentimentAnalyzer
    {
        public static string DetectEmotion(
            string message)
        {
            if (message.Contains("worried") ||
                message.Contains("scared") ||
                message.Contains("nervous"))
            {
                return "worried";
            }

            if (message.Contains("frustrated") ||
                message.Contains("angry") ||
                message.Contains("confused"))
            {
                return "frustrated";
            }

            return "";
        }
    }
}
