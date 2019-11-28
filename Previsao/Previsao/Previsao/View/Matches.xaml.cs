using Previsao.Controller;
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
    public partial class Matches : ContentPage
    {
        public Matches()
        {
            InitializeComponent();

            ListMatches.ItemsSource = new MathController().GetMatches().OrderByDescending(x => x.Id);
        }

        private void ListMatches_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Navigation.PushAsync(new Game(null, (Match)e.SelectedItem));
        }
    }
}