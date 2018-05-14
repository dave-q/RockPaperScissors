using RockPaperScissors.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IPlayer
    {
        string PlayerName { get; }
        Move MakeMove(IGameRules gameRules);

        void Initialise();
    }
}
