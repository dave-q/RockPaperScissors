using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Players
{
    public class Result
    {
        private static Result _noWinner = new Result();
        public bool HasWinner { get; private set; }
        public IPlayer Winner { get; private set; }

        private Result(IPlayer winner = null)
        {
            HasWinner = winner != null;
            Winner = winner;
        }

        public static Result NoWinner => _noWinner;

        public static Result Win(IPlayer winner)
        {
            return new Result(winner);
        }
    }
}
