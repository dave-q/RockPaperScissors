using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Players
{
    public class Move
    {
        public IPlayer Player { get; private set; }
        public string ChosenOption { get; private set; }

        public Move(IPlayer player, string chosenOption)
        {
            if (string.IsNullOrWhiteSpace(chosenOption))
            {
                throw new ArgumentException("chosenOption cannot be empty", nameof(chosenOption));
            }

            Player = player ?? throw new ArgumentNullException(nameof(player));
            ChosenOption = chosenOption;
        }
    }
}
