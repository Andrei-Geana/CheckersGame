using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class GameSettings
    {
        [JsonProperty("multi-jump")]
        private bool _multiJump;
        [JsonProperty("show-possible-moves")]
        private bool _showPossibleMoves;

        [JsonIgnore]
        public bool MultiJump { get => _multiJump; set => _multiJump = value; }
        [JsonIgnore]
        public bool ShowPossibleMoves { get => _showPossibleMoves; set => _showPossibleMoves = value; }
    }
}
