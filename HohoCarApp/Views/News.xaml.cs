namespace HohoCarApp.Views
{
    public partial class News : ContentPage
    {
        public News()
        {
            InitializeComponent();
        }

        private async void OnBack(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}


