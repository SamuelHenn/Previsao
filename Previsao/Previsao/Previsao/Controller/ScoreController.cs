using Previsao.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Linq;

namespace Previsao.Controller
{
    public class ScoreController
    {
        public List<Player> GetScores()
        {
            if (App.Current.Properties.ContainsKey("Scores"))
            {
                string obj = App.Current.Properties["Scores"].ToString();

                if (!string.IsNullOrEmpty(obj))
                    return JsonConvert.DeserializeObject<List<Player>>(obj);
            }

            return new List<Player>();
        }

        public void SaveScores(List<Player> players)
        {
            List<Player> currentScores = GetScores();

            foreach (var p in players)
            {
                if (currentScores.Where(x => x.Name == p.Name).Count() > 0)
                {
                    Player player = currentScores.Where(x => x.Name == p.Name).FirstOrDefault();
                    if (p.Score > player.Score)
                        player.Score = p.Score;
                }
                else
                {
                    currentScores.Add(p);
                }
            }

            if (App.Current.Properties.ContainsKey("Scores"))
            {
                App.Current.Properties.Remove("Scores");
            }
            App.Current.Properties.Add("Scores", JsonConvert.SerializeObject(currentScores));
        }
    }
}
