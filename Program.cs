using System;
using System.Collections.Generic;
using System.IO;

/**

Author: Yanzhi Wang
Purpose: The Program class is the main class for the Mad Libs game. It prompts the user to play the game and asks for their name.
It then reads a file containing Mad Libs story templates and prompts the user to choose a story. Once a story is chosen,
the program prompts the user to input various parts of speech (nouns, verbs, adjectives, etc.) to fill in the blanks of the story.
The final Mad Libs story is printed to the console.
Restrictions: The MadLibsTemplate.txt file must be located at the specified file path ("C:\Users\Wiz\Desktop\IGME 201\MadLibs\bin\Debug\MadLibsTemplate.txt").
The user input for story selection must be an integer between 1 and the total number of stories in the MadLibsTemplate.txt file.
Known errors: None
*/

namespace MadLibs
{
    class Program
    {
        // This program allows the user to play a game called "Mad Libs" where the user
        // inputs various words based on prompts in a story template to create a unique story.
        static void Main(string[] args)
        {
            Console.WriteLine("Do you want to play Mad Libs? (yes/no)");
            string playGame = Console.ReadLine();

            // Check if user wants to play the game
            while (playGame.ToLower() != "yes" && playGame.ToLower() != "no")
            {
                Console.WriteLine("Invalid input. Please enter either 'yes' or 'no'.");
                playGame = Console.ReadLine();
            }

            // If user wants to play, prompt for name and display story options
            if (playGame.ToLower() == "yes")
            {
                Console.WriteLine("Enter your name: ");
                string name = Console.ReadLine();

                // Read story templates from file and display number of available stories
                string[] lines = File.ReadAllLines("C:\\Users\\Wiz\\Desktop\\IGME 201\\MadLibs\\bin\\Debug\\MadLibsTemplate.txt");
                int numOfStories = lines.Length;

                Console.WriteLine("Hi " + name + ", there are " + numOfStories + " stories to choose from.");
                Console.WriteLine("Please enter a story number (1-" + numOfStories + "): ");

                int storyNum = int.Parse(Console.ReadLine());

                // Check if user's input for story number is valid
                while (storyNum < 1 || storyNum > numOfStories)
                {
                    Console.WriteLine("Invalid story number. Please enter a number between 1 and " + numOfStories + ".");
                    storyNum = int.Parse(Console.ReadLine());
                }

                // Retrieve the chosen story template and split it into individual words
                string chosenStory = lines[storyNum - 1];
                string[] words = chosenStory.Split(' ');
                string resultString = "";

                // Create dictionary to store user's input for each prompt
                Dictionary<string, string> inputs = new Dictionary<string, string>();

                // Iterate through each word in the story template
                foreach (string word in words)
                {
                    if (word == "\n")
                    {
                        // Add new line to result string if current word is a line break
                        resultString += "\n";
                    }
                    else if (word[0] == '{')
                    {
                        // If current word is a prompt, retrieve user's input or prompt for input
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

                        // Add user's input to result string
                        resultString += userInput + " ";
                    }
                    else
                    {
                        // Add current word to result string if it is not a prompt or line break
                        resultString += word + " ";
                    }
                }

                // Display completed story to the user
                Console.WriteLine("\n" + resultString);
            }
            else
            {
                // If user does not want to play, exit the program
                Console.WriteLine("Goodbye.");
            }
        }
    }
}
