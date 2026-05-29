using CyberSecurityChatbotWPF;
using System;
using System.Collections.Generic;

namespace CyberSecurityChatbotWPF

{
    public class ChatBot
    {
        // Store username
        private string userName = "";

        // Save last discussed topic
        private string currentTopic = "";

        // Random object
        Random random = new Random();

        // Save user name
        public void SetUserName(string name)
        {
            userName = name;
        }

        // Main chatbot processing
        public string ProcessInput(string message)
        {
            message = message.ToLower();

            // Detect user sentiment
            string emotion =
                SentimentAnalyzer.DetectEmotion(message);

            // Emotional support responses
            if (emotion == "worried")
            {
                return
                    $"I understand your concern {userName}. " +
                    $"Cybersecurity threats can feel overwhelming, " +
                    $"but learning safe habits greatly reduces risks.\n\n" +
                    $"One important tip is to avoid clicking suspicious links " +
                    $"or downloading unknown files.";
            }

            if (emotion == "frustrated")
            {
                return
                    $"Don't worry {userName}, cybersecurity can seem confusing at first.\n\n" +
                    $"Take things one step at a time and focus on basic protection methods " +
                    $"like strong passwords and safe browsing.";
            }

            // Save favourite topic
            if (message.Contains("interested in"))
            {
                string topic =
                    message.Replace("interested in", "").Trim();
                // Persist favourite topic for current user
                MemoryManager.AddOrUpdateUser(userName, topic);

                return $"Great! I will remember that you like {topic}.";
            }

            // Recall favourite topic
            if (message.Contains("favorite topic") ||
                message.Contains("favourite topic"))
            {
                string favorite = MemoryManager.GetFavoriteTopic(userName);

                if (string.IsNullOrWhiteSpace(favorite))
                {
                    return
                        $"I don't know your favourite topic yet {userName}. " +
                        $"You can tell me by saying:\n" +
                        $"'I am interested in phishing'";
                }

                return
                    $"Your favourite cybersecurity topic is:\n" +
                    $"{favorite}\n\n" +
                    $"As someone interested in this topic, " +
                    $"you should regularly stay updated about new threats.";
            }

            // Follow-up questions
            if (message.Contains("tell me more") ||
                message.Contains("more details") ||
                message.Contains("explain more") ||
                message.Contains("another tip"))
            {
                return ContinueConversation();
            }

            // Quiz
            if (message.Contains("quiz"))
            {
                return
                    $"Quiz Time 🧠\n\n" +
                    $"Question:\n" +
                    $"What should you do if you receive a suspicious email?\n\n" +
                    $"A) Click the link immediately\n" +
                    $"B) Ignore warning signs\n" +
                    $"C) Verify the sender before clicking";
            }

            // Password checker
            if (message.Contains("check password"))
            {
                return
                    $"A strong password should:\n\n" +
                    $"✔ Be at least 8 characters\n" +
                    $"✔ Include symbols and numbers\n" +
                    $"✔ Avoid personal information\n" +
                    $"✔ Use uppercase and lowercase letters";
            }

            // Get cybersecurity response
            string response =
                ResponseManager.GetResponse(
                    message,
                    ref currentTopic);

            return response;
        }

        // Continue conversation naturally
        private string ContinueConversation()
        {
            switch (currentTopic)
            {
                case "phishing":

                    return
                        $"Phishing attacks often create urgency " +
                        $"to pressure victims into acting quickly.\n\n" +
                        $"Always check:\n" +
                        $"✔ sender email addresses\n" +
                        $"✔ suspicious links\n" +
                        $"✔ spelling mistakes\n" +
                        $"✔ urgent requests";

                case "password":

                    return
                        $"Password managers are useful because " +
                        $"they create and store secure passwords safely.\n\n" +
                        $"Never reuse the same password across multiple accounts.";

                case "privacy":

                    return
                        $"Protecting your privacy online includes:\n\n" +
                        $"✔ limiting personal information shared online\n" +
                        $"✔ reviewing app permissions\n" +
                        $"✔ enabling two-factor authentication";

                case "malware":

                    return
                        $"Malware can spread through:\n\n" +
                        $"✔ infected downloads\n" +
                        $"✔ unsafe websites\n" +
                        $"✔ email attachments\n" +
                        $"✔ pirated software";

                default:

                    return
                        $"Could you tell me which cybersecurity topic " +
                        $"you want more information about?\n\n" +
                        $"Examples:\n" +
                        $"• phishing\n" +
                        $"• malware\n" +
                        $"• privacy\n" +
                        $"• passwords";
            }
        }
    }
}