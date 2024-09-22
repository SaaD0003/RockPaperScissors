using System;
using System.Collections.Generic;
using System.Linq;

namespace RockPaperScissors
{
    
    public static class MoveSelector
    {    
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
                    return Array.Empty<string>(); 
                }
                else if (input == "?" || input == "help")
                {
                    HelpDisplay.DisplayHelp(predefinedMoveSets[15]); 
                    continue;
                }
                else if (int.TryParse(input, out int selectedSet) && predefinedMoveSets.ContainsKey(selectedSet))
                {
                    return predefinedMoveSets[selectedSet];
                }
                Console.WriteLine("Invalid choice! Please select a valid set number.");
            }
        }
public static string[] CreateCustomMoveSet(string[] args = null)
{
    string[] allMoves = { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass", "Metal", "Electric" };
    List<string> selectedMoves = new List<string>();

    if (args != null && args.Length > 0)
    {
        foreach (var arg in args)
        {
            if (int.TryParse(arg, out int index) && index >= 1 && index <= allMoves.Length)
            {
                string selectedMove = allMoves[index - 1];
                if (!selectedMoves.Contains(selectedMove))
                {
                    selectedMoves.Add(selectedMove);
                }
            }
            else if (allMoves.Contains(arg, StringComparer.OrdinalIgnoreCase))
            {
                string selectedMove = allMoves.First(m => m.Equals(arg, StringComparison.OrdinalIgnoreCase));
                if (!selectedMoves.Contains(selectedMove))
                {
                    selectedMoves.Add(selectedMove);
                }
            }
            else
            {
                Console.WriteLine($"Invalid move from argument: {arg}");
            }
        }

        if (selectedMoves.Count >= 3 && selectedMoves.Count % 2 == 1)
        {
            return selectedMoves.ToArray(); 
        }
        else
        {
            Console.WriteLine("You must select an odd number of moves (at least 3) from command-line arguments.");
            return null; 
        }
    }

    Console.WriteLine("Select an odd number of moves from the following options (at least 3):");

    for (int i = 0; i < allMoves.Length; i += 5)
    {
        for (int j = i; j < i + 5 && j < allMoves.Length; j++)
        {
            Console.Write($"{j + 1}: {allMoves[j]} \t");
        }
        Console.WriteLine(); 
    }
    Console.WriteLine("0: Exit");
    Console.WriteLine("?: Help");

    while (true)
    {
        Console.Write("Enter the numbers or names of your at least 3 selected moves (comma or space-separated): ");


        string input = Console.ReadLine().ToLower();
        

        string[] moveEntries = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);

        if (moveEntries.Length == 1 && moveEntries[0] == "0")
        {
            Program.StartGame();
            return null; 
        }

        else if (moveEntries.Length == 1 && moveEntries[0] == "?")
        {
            HelpDisplay.DisplayHelp(null); 
            continue; 
        }

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
                break; 
            }
        }

        if (selectedMoves.Count >= 3 && selectedMoves.Count % 2 == 1)
        {
            break; 
        }
        else
        {
            Console.WriteLine("You must select an odd number of moves (at least 3).");
            selectedMoves.Clear(); 
        }
    }

    return selectedMoves.ToArray(); 
}

        public static string[] GenerateRandomMoves()
        {
            string[] baseMoves = { "Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass", "Metal", "Electric" };
            Random random = new Random();
            int numMoves = random.Next(3, 16); 

            return baseMoves.OrderBy(x => random.Next()).Take(numMoves).ToArray();
        }

        public static int GetMoveIndex(string input, string[] moves)
        {
            if (int.TryParse(input, out int moveIndex))
            {
                if (moveIndex >= 1 && moveIndex <= moves.Length)
                {
                    return moveIndex - 1; 
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
            return -1; 
        }
    }
}
