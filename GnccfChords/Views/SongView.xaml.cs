using GnccfChords.ViewModel;

namespace GnccfChords.Views;

public partial class SongView : ContentPage
{
	public SongView()
	{
		InitializeComponent();
		this.BindingContext = new SongsViewModel();
	}

	private async void AddChordsPage(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddChordsView());
	}
}