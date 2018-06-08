using System;
using System.Collections.Generic;
using System.Text;

namespace Previsao.Model
{
    public class Match
    {
        public List<Player> Players { get; set; }
        public List<Round> Rounds { get; set; }
        public List<int> Results { get; set; }
    }
}
