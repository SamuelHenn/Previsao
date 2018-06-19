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
    public partial class Menu : MasterDetailPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        public void NewGame(object sender, EventArgs args)
        {
            Detail.Navigation.PushAsync(new View.Players());
            IsPresented = false;
        }

        public void Scores(object sender, EventArgs args)
        {
            Detail.Navigation.PushAsync(new View.Scores());
            IsPresented = false;
        }
    }
}