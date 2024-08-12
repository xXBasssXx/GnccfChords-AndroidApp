using GnccfChords.Models;
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

	public async void ViewDetails(object sender, SelectedItemChangedEventArgs e)
	{
		if(e.SelectedItem is Song selectedSong)
		{
            await Navigation.PushAsync(new SongDetailView(selectedSong.SongId));
        }    
	}
}