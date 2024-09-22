using System;

namespace RockPaperScissors
{
    class Program
    {
        static bool continuePlaying = true; // This flag controls whether to keep looping through the game

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                // If command-line arguments are provided, run the game directly with a predefined set
                string[] predefinedSet = MoveSelector.CreateCustomMoveSet(args);
                if (predefinedSet != null)
                {
                    PlayGame(predefinedSet);
                }
                else
                {
                    Console.WriteLine("Invalid command-line arguments. Please use 'rock paper scissors' or similar moves.");
                }
            }
            else
            {
                StartGame(); // No command-line arguments, go to the menu
            }
        }


        // Central function that handles the main game flow
        public static void StartGame()
        {
            while (continuePlaying)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Rock, Paper, Scissors Game!");
                Console.WriteLine("Select an option:");
                Console.WriteLine("1: Use a predefined set of moves");
                Console.WriteLine("2: Create your own set of moves");
                Console.WriteLine("3: Use random moves");
                Console.WriteLine("0: Exit");
                Console.WriteLine("?: Help");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        string[] predefinedMoves = MoveSelector.SelectPredefinedMoveSet();
                        if (predefinedMoves.Length > 0) // If a valid set is returned
                            PlayGame(predefinedMoves);
                        break;
                    case "2":
                        string[] customMoves = MoveSelector.CreateCustomMoveSet();
                        PlayGame(customMoves);
                        break;
                    case "3":
                        string[] randomMoves = MoveSelector.GenerateRandomMoves();
                        PlayGame(randomMoves);
                        break;
                    case "0":
                        ExitGame();
                        break;
                    case "?":
                        HelpDisplay.DisplayHelp(null); // Display help with sample moves
                        break;
                    default:
                        Console.WriteLine("Invalid option! Please select a valid option.");
                        break;
                }
            }
        }

        // Play the game with the selected moves
        private static void PlayGame(string[] moves)
        {
            while (true)
            {
                Console.Clear();

                // Generate computer move and HMAC
                Random random = new Random();
                int computerMoveIndex = random.Next(moves.Length);
                string computerMove = moves[computerMoveIndex];

                // Generate random key and HMAC
                byte[] secretKey = HmacGenerator.GenerateRandomKey();
                string hmac = HmacGenerator.GenerateHmac(secretKey, computerMove);

                // Display the HMAC to the user
                Console.WriteLine($"HMAC: {hmac}");
                
                Console.WriteLine("Choose your move by typing a number or move name:");
                for (int i = 0; i < moves.Length; i++)
                {
                    Console.WriteLine($"{i + 1}: {moves[i]}");
                }
                Console.WriteLine("0: Exit to main menu");
                Console.WriteLine("?: Help");

                Console.Write("\nYour choice: ");
                string userInput = Console.ReadLine().ToLower();

                if (userInput == "0" || userInput == "exit")
                {
                    return; // Return to main menu by exiting this function
                }
                else if (userInput == "?" || userInput == "help")
                {
                    HelpDisplay.DisplayHelp(moves); // Call help if needed
                    continue; // Continue the game loop
                }

                int playerMoveIndex = MoveSelector.GetMoveIndex(userInput, moves);
                if (playerMoveIndex == -1)
                {
                    Console.WriteLine("Invalid choice, try again.");
                    continue;
                }

                string playerMove = moves[playerMoveIndex];

                Console.WriteLine($"\nYou chose: {playerMove}");
                Console.WriteLine($"The computer chose: {computerMove}\n");

                if (playerMove == computerMove)
                {
                    Console.WriteLine("It's a tie!");
                }
                else if (GameLogic.IsPlayerWinner(playerMove, computerMove))
                {
                    Console.WriteLine("You win!");
                }
                else
                {
                    Console.WriteLine("You lose!");
                }

                // Show the HMAC key after the result
                Console.WriteLine($"\nHMAC key: {BitConverter.ToString(secretKey).Replace("-", "")}");

                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadLine();
                return; // Return to the main menu after playing a game
            }
        }

        // Function to exit the game
        private static void ExitGame()
        {
            Console.WriteLine("Exiting the game. Goodbye!");
            continuePlaying = false; // This will stop the main game loop
        }
    }
}
