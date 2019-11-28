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

            PlayerName.Completed += delegate
            {
                AddPlayer(this, null);
            };

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
                PlayerName.Focus();
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
                // For test
                /*players.Add(new Player { Id = Guid.NewGuid(), Name = "Kuki" });
                players.Add(new Player { Id = Guid.NewGuid(), Name = "Muka" });
                players.Add(new Player { Id = Guid.NewGuid(), Name = "Bronca" });
                players.Add(new Player { Id = Guid.NewGuid(), Name = "Pacheco" });*/

                if (players is null || players.Count == 0)
                    throw new Exception();

                var gamePage = new View.Game(players);
                NavigationPage.SetHasBackButton(gamePage, false);
                Navigation.PushAsync(gamePage);
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