using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Previsao.Model
{
    public class Round
    {
        [JsonIgnore]
        public List<Player> Players { get; set; }
        [JsonProperty]
        public List<Bet> Bets { get; set; }
    }
}
