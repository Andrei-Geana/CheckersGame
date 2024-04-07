﻿using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers_Game.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PieceColorEnum
    {
        BLACK,
        WHITE,
        BLUE,
        GREEN
    }
}
