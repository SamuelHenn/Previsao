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

            match = new Match { Players = players, Rounds = new List<Round>(), Results = new List<int>() };

            // For test
            for (int i = 1; i <= 3; i++)
            {
                Round r = new Round { Players = match.Players, Bet = new List<int>() };

                foreach (var x in match.Players)
                {
                    r.Bet.Add((int)Math.Round(3.0m));
                }

                match.Rounds.Add(r);
            }
            // End test

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
                        new RowDefinition { Height = GridLength.Auto },
                    },
                    ColumnDefinitions = {
                        new ColumnDefinition { Width = GridLength.Star },
                    }
                };
                for (int c = 0; c < match.Players.Count; c++)
                {
                    grid.Children.Add(new Label()
                    {
                        Text = match.Players.ElementAt(c).Name,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center,
                        BackgroundColor = Color.LightGray
                    }, c, 0);
                }
                for (int l = 1; l <= match.Rounds.Count; l++)
                {
                    for (int c = 0; c < match.Players.Count; c++)
                    {
                        Label bet = new Label()
                        {
                            Text = match.Rounds.ElementAt(l - 1).Bet.ElementAt(c).ToString(),
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            HorizontalTextAlignment = TextAlignment.Center,
                            BackgroundColor = Color.LightBlue
                        };

                        StackLayout cell = new StackLayout() { Orientation = StackOrientation.Horizontal };
                        if (l == match.Rounds.Count)
                        {
                            //Button error = new Button() { Text = "X", WidthRequest = 15 };
                            //Button acert = new Button() { Text = "V", WidthRequest = 15 };

                            //cell.Children.Add(error);
                            cell.Children.Add(bet);
                            //cell.Children.Add(acert);
                        }
                        else
                        {
                            cell.Children.Add(bet);
                        }

                        grid.Children.Add(cell, c, l);
                    }
                }
                GameContent.Children.Add(grid);
            }
            catch (Exception e)
            {
                GameContent.Children.Add(new Label() { Text = e.Message });
            }

            /*
            // Print players
            StackLayout players = new StackLayout() { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.LightGray, Spacing = 0.0 };
            foreach (Player p in match.Players)
            {
                players.Children.Add(new Label()
                {
                    Text = p.Name,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HorizontalTextAlignment = TextAlignment.Center
                });
                players.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 1.0 });
            }
            GameContent.Children.Add(players);
            GameContent.Children.Add(new BoxView() { Color = Color.Black, HeightRequest = 1.0 });

            // Print rounds
            foreach (Round r in match.Rounds)
            {
                StackLayout round = new StackLayout() { Orientation = StackOrientation.Horizontal, BackgroundColor = Color.White, Spacing = 0.0 };
                for (int i = 0; i < match.Players.Count; i++)
                {
                    round.Children.Add(new Label()
                    {
                        Text = r.Bet.ElementAt(i).ToString(),
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        HorizontalTextAlignment = TextAlignment.Center
                    });
                    round.Children.Add(new BoxView() { Color = Color.Black, WidthRequest = 1.0 });
                }
                GameContent.Children.Add(round);
                GameContent.Children.Add(new BoxView() { Color = Color.Black, HeightRequest = 1.0 });
            }
            */
        }
    }
}