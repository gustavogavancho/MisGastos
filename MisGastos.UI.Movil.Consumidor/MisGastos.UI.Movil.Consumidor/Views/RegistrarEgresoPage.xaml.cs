using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrarEgresoPage : ContentPage
    {
        RegistrarEgresoViewModel _viewModel;

        public RegistrarEgresoPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ViewModelLocator.RegistrarEgresoViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((RegistrarEgresoViewModel)this.BindingContext)
                .OnApperaringCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((RegistrarEgresoViewModel)this.BindingContext)
                .OnDisappearingCommand.Execute(null);
        }
    }
}