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
    public partial class Game : ContentPage
    {
        private Match match;

        public Game(List<Player> players)
        {
            InitializeComponent();

            match = new Match { Players = players, Rounds = new List<Round>() };

            // For test
            Random rand = new Random();
            for (int i = 1; i <= 3; i++)
            {
                Round r = new Round { Players = match.Players, Bets = new List<Bet>() };

                foreach (var x in match.Players)
                {
                    r.Bets.Add(new Bet { Value = rand.Next(3), Ok = rand.NextDouble() >= 0.5 });
                }

                match.Rounds.Add(r);
            }
            // End test

            RefreshGame();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            RefreshGame();
        }

        private void RefreshGame()
        {
            try
            {
                GameContent.Children.Clear();
                Grid grid = new Grid()
                {
                    VerticalOptions = LayoutOptions.Start,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    RowDefinitions = {
                        new RowDefinition { Height = GridLength.Auto }
                    },
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = GridLength.Auto },
                        new ColumnDefinition { Width = GridLength.Star }
                    }
                };
                grid.Children.Add(new Label()
                {
                    Text = "Rod.",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.Gray
                }, 0, 0);
                for (int c = 1; c <= match.Players.Count; c++)
                {
                    grid.Children.Add(new Label()
                    {
                        Text = match.Players.ElementAt(c - 1).Name,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.LightGray
                    }, c, 0);
                }
                for (int l = 1; l <= match.Rounds.Count; l++)
                {
                    grid.Children.Add(new Label()
                    {
                        Text = l.ToString(),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.Gray
                    }, 0, l);
                    for (int c = 0; c < match.Players.Count; c++)
                    {
                        Bet b = match.Rounds.ElementAt(l - 1).Bets.ElementAt(c);
                        Label bet = new Label()
                        {
                            Text = b.Value.ToString(),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HorizontalTextAlignment = TextAlignment.Center,
                            BackgroundColor = b.Ok ? Color.LightGreen : Color.LightSalmon
                        };

                        StackLayout cell = new StackLayout() { Orientation = StackOrientation.Horizontal };
                        if (l == match.Rounds.Count)
                        {
                            TapGestureRecognizer tap = new TapGestureRecognizer();
                            tap.Tapped += delegate
                            {
                                b.Ok = !b.Ok;
                                RefreshGame();
                            };
                            cell.GestureRecognizers.Add(tap);
                            cell.Children.Add(bet);
                        }
                        else
                        {
                            cell.Children.Add(bet);
                        }

                        grid.Children.Add(cell, c + 1, l);
                    }
                }
                grid.Children.Add(new Label()
                {
                    Text = "Tot.",
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center,
                    BackgroundColor = Color.Gray
                }, 0, match.Rounds.Count + 1);
                for (int c = 1; c <= match.Players.Count; c++)
                {
                    grid.Children.Add(new Label()
                    {
                        Text = match.GetPlayerResult(match.Players.ElementAt(c - 1)).ToString(),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.LightGray
                    }, c, match.Rounds.Count + 1);
                }
                GameContent.Children.Add(grid);

                if (match.Rounds.Count < 10)
                {
                    Button newRound = new Button()
                    {
                        Text = "Nova rodada",
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = 10
                    };
                    newRound.Clicked += delegate
                    {
                        Navigation.PushAsync(new NewRound(match));
                    };
                    GameContent.Children.Add(newRound);
                }
                else
                {
                    Button endGame = new Button()
                    {
                        Text = "Finalizar partida",
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = 10
                    };
                    endGame.Clicked += delegate
                    {
                        List<Player> results = match.GetResults();
                        string message = string.Empty;
                        foreach (var p in results)
                        {
                            message += p.Name + ": " + p.Score + "\n";
                        }

                        DisplayAlert("Parabéns " + results.First().Name, message, "Ok");
                    };
                    GameContent.Children.Add(endGame);
                }
            }
            catch (Exception e)
            {
                GameContent.Children.Add(new Label() { Text = e.Message });
            }
        }
    }
}