using Checkers_Game.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Service
{
    public class GameLogic
    {
        private GameViewModel game;
        public GameLogic(GameViewModel gameViewModel) { game = gameViewModel; }
    }
}
