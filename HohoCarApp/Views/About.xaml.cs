namespace HohoCarApp.Views
{
    public partial class About : ContentPage
    {
        public About()
        {
            InitializeComponent();
        }

        private async void OnBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}


