using GnccfChords.ViewModel;

namespace GnccfChords.Views;

public partial class AddChordsView : ContentPage
{
	public AddChordsView()
	{
		InitializeComponent();
		this.BindingContext = new AddChordsViewModel(Navigation);
	}
}