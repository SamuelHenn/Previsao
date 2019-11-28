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
    public partial class NewRound : ContentPage
    {
        Match match = null;
        Round round = null;
        int roundNumber = 0;
        int firstPosition = 0;

        public NewRound(Match _match)
        {
            InitializeComponent();

            match = _match;
            round = new Round { Players = _match.Players, Bets = new List<Bet>() };
            roundNumber = match.Rounds.Count + 1;
            firstPosition = _match.Players.Max(x => x.Score);

            string firstPlayers = string.Empty;

            foreach (var x in _match.Players)
            {
                round.Bets.Add(new Bet { Value = 0, Ok = true });

                if (x.Score == firstPosition)
                    firstPlayers += x.Name + "\n";
            }


            ShowPlayers();
        }

        private void ShowPlayers()
        {
            Content.Children.Clear();

            Label title = new Label()
            {
                Text = roundNumber + "º rodada",
                FontSize = 25,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Content.Children.Add(title);

            for (int i = 0; i < round.Bets.Count; i++)
            {
                StackLayout line = new StackLayout() { Orientation = StackOrientation.Horizontal, Spacing = 0.0 };
                Label playerName = new Label()
                {
                    Text = "(" + match.Players[i].Score + ") " + match.Players[i].Name,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Start,
                    VerticalOptions = LayoutOptions.Center
                };
                if (roundNumber >= 6)
                {
                    playerName.TextColor = match.Players[i].Score == firstPosition ? Color.Red : Color.Black;
                    playerName.FontAttributes = match.Players[i].Score == firstPosition ? FontAttributes.Bold : FontAttributes.None;
                }
                line.Children.Add(playerName);
                Label bet = new Label()
                {
                    Text = round.Bets[i].Value.ToString(),
                    WidthRequest = 25,
                    VerticalOptions = LayoutOptions.Center,
                    HorizontalTextAlignment = TextAlignment.Center
                };
                Button less = new Button() { Text = "-", WidthRequest = 50, CommandParameter = i };
                less.Clicked += delegate
                {
                    int v = int.Parse(bet.Text);
                    v--;
                    round.Bets[(int)less.CommandParameter].Value = v > 0 ? v : 0;
                    ShowPlayers();
                };
                Button more = new Button() { Text = "+", WidthRequest = 50, CommandParameter = i };
                more.Clicked += delegate
                {
                    int v = int.Parse(bet.Text);
                    v++;
                    round.Bets[(int)more.CommandParameter].Value = v;
                    ShowPlayers();
                };
                line.Children.Add(less);
                line.Children.Add(bet);
                line.Children.Add(more);

                Content.Children.Add(line);
                Content.Children.Add(new BoxView() { Color = Color.Black, HeightRequest = 1.0 });
            }

            int sum = round.Bets.Sum(x => x.Value);
            Label total = new Label()
            {
                Text = "Total: " + sum,
                FontAttributes = FontAttributes.Bold,
                HorizontalOptions = LayoutOptions.End,
                FontSize = 20,
                TextColor = roundNumber == sum ? Color.Red : Color.Green
            };
            Content.Children.Add(total);
            Button back = new Button() { Text = "Começar rodada", HorizontalOptions = LayoutOptions.End };
            back.Clicked += delegate
            {
                if (roundNumber == sum)
                {
                    DisplayAlert("Atenção", "Número de vitórias não pode ser igual ao número da rodada!", "Ok");
                    return;
                }

                match.Rounds.Add(round);
                Navigation.PopAsync();
            };
            Content.Children.Add(back);
        }
    }
}