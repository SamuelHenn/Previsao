using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Previsao
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        public void NewGame(object sender, EventArgs args)
        {
            Navigation.PushAsync(new View.Players());
        }

        public void Scores(object sender, EventArgs args)
        {
            Navigation.PushAsync(new View.Scores());
        }
    }
}
