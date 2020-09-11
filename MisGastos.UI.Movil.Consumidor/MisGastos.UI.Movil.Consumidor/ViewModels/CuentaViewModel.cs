using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Extensions;
using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.Views;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CuentaViewModel : BaseViewModel
    {
        ICuentaManager _cuentaManager;

        private ObservableCollection<Cuenta> _cuentas;

        public ObservableCollection<Cuenta> Cuentas
        {
            get => _cuentas;
            set => SetProperty(ref _cuentas, value);
        }

        public Command AgregarCuentaCommand { get; }
        public Command CuentaSelectedCommand { get; }

        public CuentaViewModel(FactoryManager factoryManager)
        {
            Title = "Cuenta";
            _cuentaManager = factoryManager.CuentaManager();
            AgregarCuentaCommand = new Command(OnAgregarCuenta);
            CuentaSelectedCommand = new Command<Cuenta>(OnCuentaSelected);
            ActualizarDatos();

            MessagingCenter.Subscribe<CuentaDetailViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaChanged);
            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaUpdated);
            MessagingCenter.Subscribe<RegistrarEgresoViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaEgresoUpdated);

            //Config events
            MessagingCenter.Subscribe<ConfiguracionesViewModel, Cuenta>
                (this, MessageNames.CuentaChangedMessage, OnCuentaConfigChanged);

        }

        private void OnCuentaConfigChanged(ConfiguracionesViewModel sender, Cuenta cuenta)
        {
            ActualizarDatos();
        }

        private void OnCuentaEgresoUpdated(RegistrarEgresoViewModel sender, Cuenta cuenta)
        {
            ActualizarDatos();
        }

        private void OnCuentaUpdated(RegistrarIngresoViewModel sender, Cuenta cuenta)
        {
            ActualizarDatos();
        }
        private void OnCuentaChanged(CuentaDetailViewModel sender, Cuenta cuenta)
        {
            ActualizarDatos();
        }

        private async void OnCuentaSelected(Cuenta cuenta)
        {
            await Shell.Current.GoToAsync($"{nameof(CuentaDetailPage)}?{nameof(CuentaDetailViewModel.CuentaId)}={cuenta.Id}");
        }

        private void ActualizarDatos()
        {
            Cuentas = new ObservableCollection<Cuenta>(_cuentaManager.ObtenerTodo.ToObservableCollection());
        }

        private async void OnAgregarCuenta(object obj)
        {
            await Shell.Current.GoToAsync(nameof(CuentaDetailPage));
        }
    }
}
