using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Previsao.Model
{
    public class Match
    {
        [JsonProperty]
        public List<Player> Players { get; set; }
        [JsonProperty]
        public List<Round> Rounds { get; set; }
        [JsonProperty]
        public bool Finished { get; set; }
        [JsonProperty]
        public int Id { get; set; }

        public int GetPlayerResult(Player p)
        {
            int index = 0;
            int result = 0;
            for (int i = 0; i < Players.Count; i++)
            {
                index = i;
                if (Players[i].Id == p.Id)
                    break;
            }
            foreach (Round r in Rounds)
            {
                result += r.Bets[index].Ok ? 10 + 2 * r.Bets[index].Value : 0;
            }
            return result;
        }

        public List<Player> GetResults()
        {
            foreach (Player p in Players)
            {
                p.Score = GetPlayerResult(p);
            }

            return Players.OrderByDescending(x => x.Score).ToList();
        }
    }
}
