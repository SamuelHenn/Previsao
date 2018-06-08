using Previsao.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Previsao.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Players : ContentPage
    {
        private List<Player> players = new List<Player>();

        public Players()
        {
            InitializeComponent();
            RefreshList();
        }

        public void AddPlayer(object sender, EventArgs args)
        {
            try
            {
                string name = PlayerName.Text.Trim();

                if (string.IsNullOrEmpty(name))
                    throw new Exception();

                PlayerName.Text = string.Empty;
                players.Add(new Player { Id = Guid.NewGuid(), Name = name });
                RefreshList();
            }
            catch
            {
                DisplayAlert("Atenção", "Preencha um nome!", "Ok");
            }
        }

        public void StartGame(object sender, EventArgs args)
        {
            try
            {
                // Para testar
                players.Add(new Player { Name = "Kuki" });
                players.Add(new Player { Name = "Muka" });
                players.Add(new Player { Name = "Bronca" });
                players.Add(new Player { Name = "Pacheco" });

                if (players is null || players.Count == 0)
                    throw new Exception();

                Navigation.PushAsync(new View.Game(players));
            }
            catch
            {
                DisplayAlert("Atenção", "Adiciona os jogadores!", "Ok");
            }
        }

        private void RefreshList()
        {
            ListPlayers.ItemsSource = new List<Player>();
            ListPlayers.ItemsSource = players;
        }
    }
}