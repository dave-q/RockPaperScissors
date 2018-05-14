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
    public class TacticalComputerPlayerTests
    {
        [Fact]
        public void TacticalComputerPlayer_Choses_Move_That_Beats_Last_Move()
        {
            var mockGameRules = new Mock<IGameRules>();
            mockGameRules.Setup(g => g.ValidOptions).Returns(() => new[] { "option1", "option2", "option3" });
            mockGameRules.Setup(g => g.WhatBeats("option1")).Returns(() => "option2");
            mockGameRules.Setup(g => g.WhatBeats("option2")).Returns(() => "option3");
            mockGameRules.Setup(g => g.WhatBeats("option3")).Returns(() => "option1");

            var computerPlayer = new TacticalComputerPlayer();
            var gameRules = mockGameRules.Object;

            var firstMove = computerPlayer.MakeMove(gameRules);

            switch (firstMove.ChosenOption)
            {
                case "option1":
                    Assert.Equal("option2", computerPlayer.MakeMove(gameRules).ChosenOption);
                    break;
                case "option2":
                    Assert.Equal("option3", computerPlayer.MakeMove(gameRules).ChosenOption);
                    break;
                case "option3":
                    Assert.Equal("option1", computerPlayer.MakeMove(gameRules).ChosenOption);
                    break;
                default:
                    Assert.True(false, "Uknown move made");
                    break;
            }
        }
    }
}
