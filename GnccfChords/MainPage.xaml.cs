using GnccfChords.Views;

namespace GnccfChords
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void StartPage(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SongView());
        }
    }

}
