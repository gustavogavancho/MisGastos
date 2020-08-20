using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.ViewModels;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CuentaDetailPage : ContentPage
    {
        CuentaDetailViewModel _viewModel;

        public CuentaDetailPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = ViewModelLocator.CuentaDetailViewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ((CuentaDetailViewModel)this.BindingContext)
                    .OnApperaringCommand.Execute(null);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ((CuentaDetailViewModel)this.BindingContext)
                    .OnDisappearingCommand.Execute(null);
        }
    }
}