using System;
using System.Text;

namespace RockPaperScissors
{
     public static class GameLogic
    {
        public static bool IsPlayerWinner(string playerMove, string computerMove)
        {
            // Define winning moves
            Dictionary<string, List<string>> winningMoves = new Dictionary<string, List<string>>()
            {
                { "Rock", new List<string> { "Scissors", "Lizard", "Fire", "Thunder", "Ice" } },
                { "Paper", new List<string> { "Rock", "Water", "Spock", "Shadow", "Grass" } },
                { "Scissors", new List<string> { "Paper", "Lizard", "Water", "Poison", "Metal" } },
                { "Lizard", new List<string> { "Spock", "Paper", "Fire", "Grass", "Poison" } },
                { "Spock", new List<string> { "Rock", "Scissors", "Water", "Shadow", "Electric" } },
                { "Water", new List<string> { "Fire", "Rock", "Scissors", "Ice", "Shadow" } },
                { "Fire", new List<string> { "Paper", "Lizard", "Spock", "Metal", "Electric" } },
                { "Thunder", new List<string> { "Water", "Ice", "Lizard", "Grass", "Fire" } },
                { "Ice", new List<string> { "Grass", "Thunder", "Shadow", "Lizard", "Water" } },
                { "Poison", new List<string> { "Grass", "Metal", "Spock", "Paper", "Water" } },
                { "Shadow", new List<string> { "Spock", "Fire", "Rock", "Poison", "Thunder" } },
                { "Light", new List<string> { "Shadow", "Lizard", "Fire", "Grass", "Electric" } },
                { "Grass", new List<string> { "Water", "Rock", "Paper", "Lizard", "Spock" } },
                { "Metal", new List<string> { "Grass", "Fire", "Thunder", "Poison", "Ice" } },
                { "Electric", new List<string> { "Water", "Metal", "Fire", "Rock", "Grass" } }
            };

            if (winningMoves.ContainsKey(playerMove))
            {
                return winningMoves[playerMove].Contains(computerMove);
            }

            return false;
        }
    }
}
