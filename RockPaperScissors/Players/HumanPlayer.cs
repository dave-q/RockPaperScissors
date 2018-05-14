using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Players
{
    public class HumanPlayer : IPlayer
    {
        public string PlayerName { get; private set; }

        public Action<string> OutputGameInfo { get; private set; }

        public Func<string> ReadInUserInput { get; private set; }

        public HumanPlayer(Action<string> output, Func<string> input)
        {
            OutputGameInfo = output;
            ReadInUserInput = input;
        }

        public void Initialise()
        {
            Console.WriteLine("Please enter your name...");
            PlayerName = Console.ReadLine();
        }

        public Move MakeMove(IGameRules gameRules)
        {
            var validMoves = gameRules.ValidOptions.ToList();

            OutputMoves(validMoves);

            int chosenMove;

            while (!int.TryParse(ReadInUserInput(), out chosenMove) || validMoves.ElementAtOrDefault(chosenMove -1) == null)
            {
                OutputGameInfo("You didn't enter a valid move, Try again....");
                OutputMoves(validMoves);
            }

            return new Move(this, validMoves[chosenMove-1]);
        }

        private void OutputMoves(List<string> validMoves)
        {
            OutputGameInfo("Please choose your move by entering the number next to it...");
            for (int i = 0; i < validMoves.Count; i++)
            {
                OutputGameInfo($"{i + 1}.\t{validMoves[i]}");
            }
        }
    }
}
