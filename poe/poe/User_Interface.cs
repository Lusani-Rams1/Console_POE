using System;
using System.Collections;
using System.Linq;

namespace Poe
{
    public class User_Interface
    {
        private ArrayList responses = new ArrayList();
        private ArrayList ignoreWords = new ArrayList();
        private string userName;

        public User_Interface()
        {
            StoreIgnoreWords();
            StoreResponses();
            AI_Design();
        }

        private void AI_Design()
        {
            // Display welcome message
            DisplayMessage("Welcome to the Chatbot!", ConsoleColor.Red);
            DisplayMessage("==========================", ConsoleColor.White);

            Console.WriteLine("AI Chat -> What's your name? ");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("User-> ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                DisplayMessage("Invalid input. Please enter a valid name.", ConsoleColor.Yellow);
                AI_Design(); // Restart name prompt if input is invalid
                return;
            }

            DisplayMessage($"Hello, {userName}! Ask me anything. Type 'exit' to exit", ConsoleColor.Cyan);

            // Continuous loop for questions
            while (true)
            {
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            Console.WriteLine("Enter your question");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("User-> ");
            string question = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(question))
            {
                DisplayMessage("You must enter a valid question!", ConsoleColor.Red);
            }
            else
            {
                ProcessQuestion(question);
            }
        }

        private void ProcessQuestion(string question)
        {
            string[] words = question.Split(' ');

            // Remove ignored words
            var filteredWords = words.Where(word => !ignoreWords.Contains(word)).ToList();

            foreach (ArrayList responseData in responses)
            {
                string response = (string)responseData[0];
                ArrayList keywords = (ArrayList)responseData[1];

                if (filteredWords.Any(word => keywords.Contains(word)))
                {
                    Console.WriteLine(response);
                    DisplayMessage("===========================", ConsoleColor.White);
                    DisplayMessage("Ask another question!", ConsoleColor.Cyan);
                    Console.Write("User-> ");
                    DisplayMessage("===========================", ConsoleColor.White);
                    return;
                }

                // Check for specific keywords
                if(string.IsNullOrEmpty(question))
                    {
                DisplayMessage("You must enter a valid question!", ConsoleColor.Red);
                    return;
                }

                // Check for exit condition
                if (question.ToLower() == "exit")
                {
                    DisplayMessage("Goodbye!", ConsoleColor.Green);
                    Environment.Exit(0); // Exit the application
                }
            }

            DisplayMessage("I am sorry, I do not understand the question.", ConsoleColor.Red);
        }

        private void StoreResponses()
        {
            responses.Add(new ArrayList { "Strong passwords protect accounts from hackers.", new ArrayList { "passwords", "security" } });
            responses.Add(new ArrayList { "Cybersecurity defends computers and networks from digital threats.", new ArrayList { "cybersecurity", "computers", "networks", "threats" } });
            responses.Add(new ArrayList { "Phishing scams trick users into revealing sensitive information.", new ArrayList { "phishing", "fraud", "email", "scam" } });
            responses.Add(new ArrayList { "Two-factor authentication (2FA) provides extra security.", new ArrayList { "2fa", "authentication", "security", "verification" } });
            responses.Add(new ArrayList { "Public Wi-Fi is insecure and vulnerable to cyber attacks.", new ArrayList { "wifi", "public", "hackers", "data" } });
        }

        private void StoreIgnoreWords()
        {
            foreach (var word in new[] { "tell", "how", "me", "about", "what", "is", "the", "to", "a", "an", "and", "or", "in" ,"i" , "would" , "like"})
            {
                ignoreWords.Add(word);
            }
        }

        private void DisplayMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}