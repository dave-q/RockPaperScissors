using Moq;
using RockPaperScissors.Interfaces;
using RockPaperScissors.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RockPaperScissors.Tests
{
    using GameRules = RockPaperScissors.GameRules.StandardGameRules;
    public class StandardRulesTests
    {
        [Fact]
        public void StandardRules_Rock_Beats_Scissors()
        {
            
            var mockPlayer1 = new Mock<IPlayer>().Object;
            var mockPlayer2 = new Mock<IPlayer>().Object;

            var result = PlayNewGame(GameRules.ROCK, GameRules.SCISSORS, mockPlayer1, mockPlayer2);

            Assert.True(result.HasWinner);
            Assert.Equal(mockPlayer1, result.Winner);

        }

        [Fact]
        public void StandardRules_Scissors_Beats_Paper()
        {
            var mockPlayer1 = new Mock<IPlayer>().Object;
            var mockPlayer2 = new Mock<IPlayer>().Object;

            var result = PlayNewGame(GameRules.SCISSORS, GameRules.PAPER, mockPlayer1, mockPlayer2);

            Assert.True(result.HasWinner);
            Assert.Equal(mockPlayer1, result.Winner);

        }

        [Fact]
        public void StandardRules_Paper_Beats_Rock()
        {
            var mockPlayer1 = new Mock<IPlayer>().Object;
            var mockPlayer2 = new Mock<IPlayer>().Object;

            var result = PlayNewGame(GameRules.PAPER, GameRules.ROCK, mockPlayer1, mockPlayer2);

            Assert.True(result.HasWinner);
            Assert.Equal(mockPlayer1, result.Winner);

        }

        [Fact]
        public void StandardRules_Rock_Draws_Rock()
        {
            var result = PlayNewGame(GameRules.ROCK, GameRules.ROCK);

            Assert.False(result.HasWinner);

            Assert.False(result.HasWinner);

        }

        [Fact]
        public void StandardRules_Paper_Draws_Paper()
        {
            var result = PlayNewGame(GameRules.PAPER, GameRules.PAPER);

            Assert.False(result.HasWinner);

        }

        [Fact]
        public void StandardRules_Sciccors_Draws_Scissors()
        {
        
            var result = PlayNewGame(GameRules.SCISSORS, GameRules.SCISSORS);

            Assert.False(result.HasWinner);

        }

        [Fact]
        public void StandardRules_WinningLineSetCorrect_Best_Of_5()
        {
            var gameRules = new GameRules();

            gameRules.StartGame(new Mock<IPlayer>().Object, new Mock<IPlayer>().Object, 5);

            Assert.Equal(3, gameRules.WinningLine);
        }

        [Fact]
        public void StandardRules_WinningLineSetCorrect_Best_Of_10()
        {
            var gameRules = new GameRules();

            gameRules.StartGame(new Mock<IPlayer>().Object, new Mock<IPlayer>().Object, 10);

            Assert.Equal(6, gameRules.WinningLine);
        }

        [Fact]
        public void StandardRules_Winner_Produced_When_Winning_Line_Met_Best_Of_5()
        {
            var gameRules = new GameRules();
            var player1 = new Mock<IPlayer>().Object;
            var player2 = new Mock<IPlayer>().Object;


            gameRules.StartGame(player1, player2, 5);
            int rounds = 0;
            var move1 = new Move(player1, "Rock");
            var move2 = new Move(player2, "Scissors");

            while(!gameRules.HasWinner())
            {
                gameRules.GetRoundWinner(move1, move2);
                rounds++;
            }

            Assert.Equal(3, rounds);
        }

        [Fact]
        public void StandardRules_Winner_Produced_When_Winning_Line_Met_Best_Of_10()
        {
            var gameRules = new GameRules();
            var player1 = new Mock<IPlayer>().Object;
            var player2 = new Mock<IPlayer>().Object;


            gameRules.StartGame(player1, player2, 10);
            int rounds = 0;
            var move1 = new Move(player1, "Rock");
            var move2 = new Move(player2, "Scissors");

            while (!gameRules.HasWinner())
            {
                gameRules.GetRoundWinner(move1, move2);
                rounds++;
            }

            Assert.Equal(6, rounds);
        }

        [Fact]
        public void StandardRules_Correct_Winner_Produced()
        {
            var gameRules = new GameRules();
            var player1 = new Mock<IPlayer>().Object;
            var player2 = new Mock<IPlayer>().Object;


            gameRules.StartGame(player1, player2, 10);
            var move1 = new Move(player1, "Rock");
            var move2 = new Move(player2, "Scissors");

            while (!gameRules.HasWinner())
            {
                gameRules.GetRoundWinner(move1, move2);
            }

            Assert.Equal(player1, gameRules.GetGameWinner().Winner);
        }
        private Result PlayNewGame(string player1Move, string player2Move, IPlayer player1 = null, IPlayer player2 = null)
        {
            var rules = new GameRules();
            var mockPlayer1 = player1 ?? new Mock<IPlayer>().Object;
            var mockPlayer2 = player2 ?? new Mock<IPlayer>().Object;
            rules.StartGame(mockPlayer1, mockPlayer2, 1);


            var move1 = new Move(mockPlayer1, player1Move);
            var move2 = new Move(mockPlayer2, player2Move);

            return rules.GetRoundWinner(move1, move2);
        }
    }
}
