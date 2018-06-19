using Previsao.Controller;
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
    public partial class Scores : ContentPage
    {
        public Scores()
        {
            InitializeComponent();

            ListScores.ItemsSource = new ScoreController().GetScores().OrderBy(x => x.Score);
        }
    }
}