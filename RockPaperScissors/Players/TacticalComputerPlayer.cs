using RockPaperScissors.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Players
{
    public class TacticalComputerPlayer : IPlayer
    {
        public string PlayerName => "Deep Thought";

        private string _lastMove;

        private Random _rand = new Random(DateTime.Now.Millisecond);

        public void Initialise()
        {
        }

        public Move MakeMove(IGameRules gameRules)
        {
            Move move;
            if (string.IsNullOrWhiteSpace(_lastMove))
            {
                move = GetRandomMove(gameRules);
            }
            else
            {
                move = new Move(this,gameRules.WhatBeats(_lastMove));
            }

            _lastMove = move.ChosenOption;
            return move;

        }

        private Move GetRandomMove(IGameRules gameRules)
        {
            var validOptions = gameRules.ValidOptions.ToList();
            var numberOfOptions = validOptions.Count;

            string moveToMake = validOptions[_rand.Next(0, numberOfOptions)];

            return new Move(this, moveToMake);
        }
    }
}
