﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Previsao.Model
{
    public class Match
    {
        public List<Player> Players { get; set; }
        public List<Round> Rounds { get; set; }
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
    }
}
