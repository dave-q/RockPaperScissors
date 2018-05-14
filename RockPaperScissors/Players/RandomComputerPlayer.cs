using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Players
{
    public class RandomComputerPlayer : IPlayer
    {
        public string PlayerName => "HAL";

        private Random _rand = new Random(DateTime.Now.Millisecond);

        public void Initialise()
        {
            //doesnt need anything here
        }

        public Move MakeMove(IGameRules gameRules)
        {
            var validOptions = gameRules.ValidOptions.ToList();
            var numberOfOptions = validOptions.Count;

            string moveToMake = validOptions[_rand.Next(0, numberOfOptions)];

            return new Move(this, moveToMake);
        }
    }
}
