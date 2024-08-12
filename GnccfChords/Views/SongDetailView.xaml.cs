using GnccfChords.ViewModel;

namespace GnccfChords.Views;

public partial class SongDetailView : ContentPage
{
	private Guid _songId;
	public SongDetailView(Guid songId)
	{
		InitializeComponent();
		_songId = songId;

		this.BindingContext = new SongDetailViewModel(_songId);
	}
}