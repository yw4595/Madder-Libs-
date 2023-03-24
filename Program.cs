using System;
using System.Collections.Generic;
using System.IO;

namespace MadLibs
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to play Mad Libs? (yes/no)");
            string playGame = Console.ReadLine();

            while (playGame.ToLower() != "yes" && playGame.ToLower() != "no")
            {
                Console.WriteLine("Invalid input. Please enter either 'yes' or 'no'.");
                playGame = Console.ReadLine();
            }

            if (playGame.ToLower() == "yes")
            {
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();

                string[] lines = File.ReadAllLines("C:\\Users\\Wiz\\Desktop\\IGME 201\\MadLibs\\bin\\Debug\\MadLibsTemplate.txt");
                int numOfStories = lines.Length;

                Console.WriteLine("Hi " + name + ", there are " + numOfStories + " stories to choose from.");
                Console.WriteLine("Please enter a story number (1-" + numOfStories + "): ");

                int storyNum = int.Parse(Console.ReadLine());
                while (storyNum < 1 || storyNum > numOfStories)
                {
                    Console.WriteLine("Invalid story number. Please enter a number between 1 and " + numOfStories + ".");
                    storyNum = int.Parse(Console.ReadLine());
                }

                string chosenStory = lines[storyNum - 1];
                string[] words = chosenStory.Split(' ');
                string resultString = "";

                Dictionary<string, string> inputs = new Dictionary<string, string>();

                foreach (string word in words)
                {
                    if (word == "\n")
                    {
                        resultString += "\n";
                    }
                    else if (word[0] == '{')
                    {
                        string prompt = word.Replace("_", " ");
                        prompt = prompt.Replace("{", "");
                        prompt = prompt.Replace("}", "");

                        string userInput;
                        if (inputs.ContainsKey(prompt))
                        {
                            userInput = inputs[prompt];
                        }
                        else
                        {
                            Console.Write(prompt + ": ");
                            userInput = Console.ReadLine();
                            inputs[prompt] = userInput;
                        }

                        resultString += userInput + " ";
                    }
                    else
                    {
                        resultString += word + " ";
                    }
                }
                Console.WriteLine("\n" + resultString);
            }
            else
            {
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
