using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RockPaperScissors.Utils
{
    public class GameNameAttribute : Attribute
    {
        public string GameName { get; private set; }

        public GameNameAttribute(string gameName)
        {
            GameName = gameName;
        }
    }
}
