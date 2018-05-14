using RockPaperScissors.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Interfaces
{
    public interface IGameRules
    {
        string RulesName { get; }
        IEnumerable<string> ValidOptions { get; }

        int WinningLine { get; }

        Result GetRoundWinner(Move move1, Move move2);

        void StartGame(IPlayer player1, IPlayer player2, int bestOf);

        Result GetGameWinner();

        bool HasWinner();

        string WhatBeats(string move);

        string GetCurrentResults();
    }
}
