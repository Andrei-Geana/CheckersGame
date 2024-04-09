using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    public class GameStat
    {
        [JsonProperty("color")]
        private PieceColorEnum _color;
        [JsonProperty("number-of-wins")]
        private int _numberOfWins;
        [JsonProperty("max-score")]
        private int _maxScore;

        public GameStat() { }

        [JsonIgnore]
        public PieceColorEnum Color { get => _color; set => _color = value; }
        [JsonIgnore]
        public int NumberOfWins { get => _numberOfWins; set => _numberOfWins = value; }
        [JsonIgnore]
        public int MaxScore { get => _maxScore; set => _maxScore = value; }
    }
}
