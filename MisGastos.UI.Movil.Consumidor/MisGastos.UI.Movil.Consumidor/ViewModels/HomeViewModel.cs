using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Extensions;
using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        private ObservableCollection<Movimiento> _movimientos;

        FactoryManager _factoryManager;
        IMovimientoManager _movimientoManager;

        public Command RegistrarIngresoCommnad { get; } 
        public Command RegistrarEgresoCommand { get; }

        public ObservableCollection<Movimiento> Movimientos
        {
            get => _movimientos;
            set => SetProperty(ref _movimientos, value);
        }

        public HomeViewModel(FactoryManager factoryManager)
        {
            Title = "Inicio";
            _factoryManager = factoryManager;
            _movimientoManager = _factoryManager.MovimientoManager();
            RegistrarIngresoCommnad = new Command(OnRegistrarIngreso);
            RegistrarEgresoCommand = new Command(OnRegistrarEgreso);
            ActualizarDatos();

            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoChanged);
        }

        private void OnMovimientoChanged(RegistrarIngresoViewModel sender, Movimiento movimiento)
        {
            Movimientos = _movimientoManager.ObtenerTodo.ToObservableCollection();
        }

        private void ActualizarDatos()
        {
            Movimientos = _movimientoManager.ObtenerTodo.ToObservableCollection();
        }

        private async void OnRegistrarEgreso(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegistrarEgresoPage));
        }

        private async void OnRegistrarIngreso(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegistrarIngresoPage));
        }
    }
}
