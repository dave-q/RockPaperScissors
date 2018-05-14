using RockPaperScissors.Interfaces;
using RockPaperScissors.Players;
using RockPaperScissors.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.GameRules
{
    [GameName("Rock Paper Scissors")]
    public class StandardGameRules : IGameRules
    {
        public const string ROCK = "Rock";
        public const string PAPER = "Paper";
        public const string SCISSORS = "Scissors";

        private Dictionary<IPlayer, int> _scoreboard;

        private int _roundsNeededToWin;

        private string[] _validOptions = new string[] { ROCK, PAPER, SCISSORS };

        public IEnumerable<string> ValidOptions
        {
            get
            {
                return _validOptions;
            }
        }

        public int WinningLine
        {
            get
            {
                return _roundsNeededToWin;
            }
        }

        public string RulesName => "Rock, Paper, Scissors - classic";

        public Result GetGameWinner()
        {
            var winner = _scoreboard.Where(kvp => kvp.Value >= _roundsNeededToWin).Select(kvp => kvp.Key).FirstOrDefault();

            if (winner == null) return Result.NoWinner;
            return Result.Win(winner);
        }

        public Result GetRoundWinner(Move move1, Move move2)
        {
            if (!_validOptions.Contains(move1.ChosenOption))
            {
                throw new InvalidOperationException($"{move1.Player.PlayerName}, {move1.ChosenOption} is not a valid move");
            }
            if (!_validOptions.Contains(move2.ChosenOption))
            {
                throw new InvalidOperationException($"{move2.Player.PlayerName}, {move2.ChosenOption} is not a valid move");
            }
            if (move1.ChosenOption == move2.ChosenOption) return Result.NoWinner;
            Result result;
            switch (move1.ChosenOption)
            {
                case ROCK:
                    if (move2.ChosenOption == PAPER)
                    {
                        result = HandlePlayerWon(move2.Player);
                    }
                    else //must be scissors beacuse we know they aren't equal
                    {
                        result = HandlePlayerWon(move1.Player);
                    }
                    break;
                case PAPER:
                    if (move2.ChosenOption == ROCK)
                    {
                        result = HandlePlayerWon(move1.Player);
                    }
                    else //must be scissors beacuse we know they aren't equal
                    {
                        result = HandlePlayerWon(move2.Player);
                    }
                    break;
                case SCISSORS:
                    if (move2.ChosenOption == ROCK)
                    {
                        result = HandlePlayerWon(move2.Player);
                    }
                    else //must be Paper beacuse we know they aren't equal
                    {
                        result = HandlePlayerWon(move1.Player);
                    }
                    break;
                default:
                    throw new InvalidOperationException($"{move1.Player.PlayerName}, {move1.ChosenOption} is not a valid move");


            }

            return result;
        }

        public void StartGame(IPlayer player1, IPlayer player2, int bestOf)
        {
            _scoreboard = new Dictionary<IPlayer, int>
            {
                {player1,0 },
                {player2,0 }
            };
            _roundsNeededToWin = CalculateWinningLine(bestOf);

        }

        public bool HasWinner()
        {
            return GetGameWinner().HasWinner;
        }
        private int CalculateWinningLine(int bestOf)
        {
            return (int)Math.Floor((decimal)bestOf / 2) + 1;
        }

        public string WhatBeats(string move)
        {
            switch (move)
            {
                case ROCK:
                    return PAPER;
                case PAPER:
                    return SCISSORS;
                case SCISSORS:
                    return ROCK;
                default:
                    throw new NotImplementedException($"move: {move} is not valid");
            }
        }

        public string GetCurrentResults()
        {
            return string.Join(Environment.NewLine, _scoreboard.Select(kvp => $"{kvp.Key.PlayerName} - {kvp.Value}"));
        }

        private Result HandlePlayerWon(IPlayer winningPlayer)
        {
            _scoreboard[winningPlayer]++;
            return Result.Win(winningPlayer);
        }
    }
}
