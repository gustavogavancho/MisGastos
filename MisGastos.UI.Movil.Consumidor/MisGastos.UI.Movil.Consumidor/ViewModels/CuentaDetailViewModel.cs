using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class CuentaDetailViewModel : BaseViewModel
    {
        private Cuenta _cuenta;
        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;

        public Cuenta Cuenta
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public Command GuardarCuentaCommand { get; }

        public CuentaDetailViewModel(FactoryManager factoryManager)
        {
            Title = "Detalle Cuenta";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            GuardarCuentaCommand = new Command(OnGuardarCuenta);
            Cuenta = new Cuenta();
        }

        private async void OnGuardarCuenta(object obj)
        {
            _cuentaManager.Insertar(Cuenta);

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

            await Shell.Current.Navigation.PopAsync();

        }
    }
}
