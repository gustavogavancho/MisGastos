using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    [QueryProperty(nameof(CuentaId), nameof(CuentaId))]
    public class CuentaDetailViewModel : BaseViewModel
    {
        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;

        private Cuenta _cuenta = new Cuenta();
        private string _cuentaId;

        public Cuenta Cuenta
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public string CuentaId
        {
            get => _cuentaId;
            set => SetProperty(ref _cuentaId, value);
        }

        public Command GuardarCuentaCommand { get; }
        public Command EliminarCuentaCommand { get; }

        public CuentaDetailViewModel(FactoryManager factoryManager)
        {
            Title = "Detalle Cuenta";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            GuardarCuentaCommand = new Command(OnGuardarCuenta);
            EliminarCuentaCommand = new Command(OnEliminarCuenta);
        }

        private async void OnEliminarCuenta(object obj)
        {
            _cuentaManager.Eliminar(CuentaId);

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnGuardarCuenta(object obj)
        {
            _cuentaManager.Insertar(Cuenta);

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);

            await Shell.Current.Navigation.PopAsync();
        }
    }
}
