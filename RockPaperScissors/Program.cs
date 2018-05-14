using RockPaperScissors.Interfaces;
using RockPaperScissors.Players;
using RockPaperScissors.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors
{
    class Program
    {

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                StartGame();
                Console.Write("Type 'exit' to close the game or any other key to start again");
            }
            while (Console.ReadLine().ToLower() != "exit");
        }

        private static void StartGame()
        {
            IPlayer player1 = ChoosePlayerType(1);
            Console.Clear();
            player1.Initialise();
            Console.Clear();
            IPlayer player2 = ChoosePlayerType(2);
            Console.Clear();
            player2.Initialise();
            Console.Clear();
            IGameRules gameRules = GetGameRules(player1, player2);

            Console.WriteLine("Hit any key to begin");
            Console.ReadKey();
            Console.Clear();

            int round = 1;
            while (!gameRules.HasWinner())
            {
                Console.WriteLine($"-----------ROUND {round}------------");
                var player1Move = player1.MakeMove(gameRules);
                Console.WriteLine($"{player1.PlayerName} chose: {player1Move.ChosenOption}");
                var player2Move = player2.MakeMove(gameRules);
                Console.WriteLine($"{player2.PlayerName} chose: {player2Move.ChosenOption}");
                var result = gameRules.GetRoundWinner(player1Move, player2Move);
                if (result.HasWinner)
                {
                    Console.WriteLine($"{result.Winner.PlayerName} is the winner in round {round}");
                }
                else
                {
                    Console.WriteLine($"Round {round} was a draw");
                }
                Console.WriteLine();
                Console.WriteLine("-----CURRENT SCOREBOARD------");
                Console.WriteLine(gameRules.GetCurrentResults());
                Console.WriteLine();
                Console.WriteLine("Hit any key to start the next round");
                Console.ReadKey();
                Console.Clear();
                round++;
            }

            Console.WriteLine($"{gameRules.GetGameWinner().Winner.PlayerName} is the winner!!");
        }

        private static IPlayer ChoosePlayerType(int playerNumber)
        {
            IPlayer player = null;

            PrintPlayerChoices(playerNumber);
            int choice = GetUserPlayerChoice(playerNumber);

            while (player == null)
            {
                switch (choice)
                {
                    case 1:
                        player = new HumanPlayer(Console.WriteLine, Console.ReadLine);
                        break;
                    case 2:
                        player = new RandomComputerPlayer();
                        break;
                    case 3:
                        player = new TacticalComputerPlayer();
                        break;
                    default:
                        Console.WriteLine("Please Enter a valid choice");
                        PrintPlayerChoices(playerNumber);
                        choice = GetUserPlayerChoice(playerNumber);
                        break;
                }
            }
            return player;
        }

        private static int GetUserPlayerChoice(int playerNumber)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Please Enter a valid choice");
                PrintPlayerChoices(playerNumber);
            }

            return choice;
        }

        private static void PrintPlayerChoices(int playerNumber)
        {
            Console.WriteLine($"What kind of player do you want 'player {playerNumber}' to be");

            Console.WriteLine("1.\tHuman Player");
            Console.WriteLine("2.\tRandom Computer Player");
            Console.WriteLine("3.\tTactical Computer Player");
        }

        private static IGameRules GetGameRules(IPlayer player1, IPlayer player2)
        {
            //this could be app config or something like that, but for now lets just ask
            var gameTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => typeof(IGameRules).IsAssignableFrom(t)).Where(t => !t.IsInterface).ToList(); ;

            if (gameTypes.Any())
            {
                IGameRules rules;
                if (gameTypes.Count == 1)
                {
                    rules = (IGameRules)Activator.CreateInstance(gameTypes.First());
                }
                else
                {
                    PrintOutGameOptions(gameTypes);
                    int choice;
                    while(!int.TryParse(Console.ReadLine(), out choice) || gameTypes.ElementAtOrDefault(choice - 1) == null)
                    {
                        Console.WriteLine("That wasnt a valid choice, try again");
                        PrintOutGameOptions(gameTypes);
                    }

                    rules = (IGameRules)Activator.CreateInstance(gameTypes[choice - 1]);
                }

                int rounds;
                do
                {
                    Console.WriteLine("What should we play to best of...?"); ;
                } while (!int.TryParse(Console.ReadLine(), out rounds));

                Console.WriteLine();
                Console.WriteLine($"Player 1: {player1.PlayerName}");
                Console.WriteLine($"Player 2: {player2.PlayerName}");

                rules.StartGame(player1, player2, rounds);

                Console.WriteLine($"You are about to start playing a game of {rules.RulesName}. Enjoy!");


                return rules;
            }
            else
            {
                Console.WriteLine("There are no game types available :(");
                Console.ReadLine();
                Environment.Exit(-1);
                return null;
            }
        }

        private static void PrintOutGameOptions(List<Type> gameTypes)
        {
            Console.WriteLine("Choose which rules you want to play. Enter the number next to the rule set you want to play with...");
            for (int i = 0; i < gameTypes.Count; i++)
            {
                var gameType = gameTypes[i];
                var gameTypeNameAtt = gameType.GetCustomAttribute<GameNameAttribute>();
                var gameTypeName = gameTypeNameAtt?.GameName ?? gameType.Name;
                Console.WriteLine($"{i+1}.\t{gameTypeName}");
            }
        }
    }
}
