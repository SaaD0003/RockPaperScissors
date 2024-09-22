using System;
using System.Text;

namespace RockPaperScissors
{
        public static class HelpDisplay
    {
        
        public static void DisplayHelp(string[] moves)
        {
            Console.Clear();
                 if (moves == null)
            {
                moves = ["Rock", "Paper", "Scissors", "Lizard", "Spock", "Water", "Fire", "Thunder", "Ice", "Poison", "Shadow", "Light", "Grass", "Metal", "Electric" ];
            }
            StringBuilder helpTable = new StringBuilder();
            int numMoves = moves.Length;

            helpTable.Append("|  User \\ PC |");
            for (int i = 0; i < numMoves; i++)
            {
                helpTable.Append($" {moves[i],-7} |");
            }
            helpTable.AppendLine();

            for (int i = 0; i < numMoves; i++)
            {
                helpTable.Append($"| {moves[i],-11}|");
                for (int j = 0; j < numMoves; j++)
                {
                    if (i == j)
                    {
                        helpTable.Append("  Draw   |");
                    }
                    else if (GameLogic.IsPlayerWinner(moves[i], moves[j]))
                    {
                        helpTable.Append("  Win    |");
                    }
                    else
                    {
                        helpTable.Append("  Lose   |");
                    }
                }
                helpTable.AppendLine();
            }

            Console.WriteLine("The table shows the game rule:");
            Console.WriteLine(helpTable.ToString());

            Console.WriteLine("\nPress Enter to return...");
            Console.ReadLine();
        }
    }
}