using HohoCarApp.ViewModel;

namespace HohoCarApp.Views
{
    public partial class Register : ContentPage
    {
        private readonly RegisterViewModel _viewModel;

        public Register()
        {
            InitializeComponent();
            _viewModel = new RegisterViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}
