using Newtonsoft.Json;
using Previsao.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Previsao.Controller
{
    public class MathController
    {
        public List<Match> GetMatches()
        {
            if (App.Current.Properties.ContainsKey("Matches"))
            {
                string obj = App.Current.Properties["Matches"].ToString();

                if (!string.IsNullOrEmpty(obj))
                    return JsonConvert.DeserializeObject<List<Match>>(obj);
            }

            return new List<Match>();
        }

        public void SaveMatch(Match match)
        {
            List<Match> currentMatches = GetMatches();

            Match m = currentMatches.Where(x => x.Id == match.Id).FirstOrDefault();

            if (m != null)
            {
                m = match;
            }
            else
            {
                currentMatches.Add(match);
            }

            if (App.Current.Properties.ContainsKey("Matches"))
            {
                App.Current.Properties.Remove("Matches");
            }
            App.Current.Properties.Add("Matches", JsonConvert.SerializeObject(currentMatches));
        }

        public Match GetMatch(int id)
        {
            return GetMatches().Where(x => x.Id == id).FirstOrDefault();
        }

        public int GetNextId()
        {
            return GetMatches().Count + 1;
        }
    }
}
