using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    
    public static class MoveSelector
    {    
       
        // Predefined move sets
        private static Dictionary<int, string[]> predefinedMoveSets = new Dictionary<int, string[]>()
        {
            { 3, new string[] { "Rock", "Paper", "Scissors" } },
            { 5, new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock" } },
            { 7, new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire" } },
            { 9, new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice" } },
            { 11, new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow" } },
            { 13, new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass" } },
            { 15, new string[] { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass", "Metal", "Electric" } }
        };

        public static string[] SelectPredefinedMoveSet()
        {
            // Display available options
            Console.WriteLine("Select a set of moves (choose an odd number of moves):");
            foreach (var set in predefinedMoveSets)
            {
                Console.WriteLine($"{set.Key}: {string.Join(", ", set.Value)}");
            }
            Console.WriteLine("0: Exit");
            Console.WriteLine("?: Help");

            while (true)
            {
                Console.Write("Enter the number of the set you want to use: ");
                string input = Console.ReadLine();
                if (input == "0" || input == "exit")
                {
                    return Array.Empty<string>(); // Exit the game
                }
                else if (input == "?" || input == "help")
                {
                    HelpDisplay.DisplayHelp(predefinedMoveSets[15]); // Provide help for default 3 moves
                    continue;
                }
                else if (int.TryParse(input, out int selectedSet) && predefinedMoveSets.ContainsKey(selectedSet))
                {
                    return predefinedMoveSets[selectedSet];
                }
                Console.WriteLine("Invalid choice! Please select a valid set number.");
            }
        }
public static string[] CreateCustomMoveSet()
{
    string[] allMoves = { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass", "Metal", "Electric" };
    List<string> selectedMoves = new List<string>();

    Console.WriteLine("Select an odd number of moves from the following options (at least 3):");

    // Display all moves
    for (int i = 0; i < allMoves.Length; i += 5)
    {
        for (int j = i; j < i + 5 && j < allMoves.Length; j++)
        {
            Console.Write($"{j + 1}: {allMoves[j]} \t");
        }
        Console.WriteLine(); // Add a new line after each group of 5
    }
    Console.WriteLine("0: Exit");
    Console.WriteLine("?: Help");

    while (true)
    {
        Console.Write("Enter the numbers or names of your selected moves (comma or space-separated): ");
        string input = Console.ReadLine().ToLower();

        // Split input by comma or space
        string[] moveEntries = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        // Handle exit condition
        if (moveEntries.Length == 1 && moveEntries[0] == "0")
        {
            // Exit the move selection and return to the main menu
            Program.StartGame();
            return null; // Indicating exit, no further action
        }
        // Handle help condition
        else if (moveEntries.Length == 1 && moveEntries[0] == "?")
        {
            HelpDisplay.DisplayHelp(allMoves); // Display help for all moves
            continue; // Return to the move selection
        }

        // Process user-selected moves
        foreach (var entry in moveEntries)
        {
            if (int.TryParse(entry, out int index) && index >= 1 && index <= allMoves.Length)
            {
                string selectedMove = allMoves[index - 1];
                if (!selectedMoves.Contains(selectedMove))
                {
                    selectedMoves.Add(selectedMove);
                }
            }
            else if (allMoves.Contains(entry, StringComparer.OrdinalIgnoreCase))
            {
                string selectedMove = allMoves.First(m => m.Equals(entry, StringComparison.OrdinalIgnoreCase));
                if (!selectedMoves.Contains(selectedMove))
                {
                    selectedMoves.Add(selectedMove);
                }
            }
            else
            {
                Console.WriteLine($"Invalid move: {entry}");
                break; // If there's an invalid move, break and ask for input again
            }
        }

        // Ensure the user selected an odd number of moves (at least 3)
        if (selectedMoves.Count >= 3 && selectedMoves.Count % 2 == 1)
        {
            break; // Move on to the next step of the game
        }
        else
        {
            Console.WriteLine("You must select an odd number of moves (at least 3).");
            selectedMoves.Clear(); // Reset and start over
        }
    }

    return selectedMoves.ToArray(); // Return the selected moves
}

        public static string[] GenerateRandomMoves()
        {
            string[] baseMoves = { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass", "Metal", "Electric" };
            Random random = new Random();
            int numMoves = random.Next(3, 16); // Random odd number between 3 and 15

            // Shuffle the moves and select a random subset
            return baseMoves.OrderBy(x => random.Next()).Take(numMoves).ToArray();
        }

        public static int GetMoveIndex(string input, string[] moves)
        {
            if (int.TryParse(input, out int moveIndex))
            {
                if (moveIndex >= 1 && moveIndex <= moves.Length)
                {
                    return moveIndex - 1; // Convert to 0-based index
                }
            }
            else
            {
                int index = Array.FindIndex(moves, m => m.Equals(input, StringComparison.OrdinalIgnoreCase));
                if (index != -1)
                {
                    return index;
                }
            }
            return -1; // Invalid move
        }
    }
}
