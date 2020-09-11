using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Extensions;
using MisGastos.UI.Movil.Consumidor.Models;
using MisGastos.UI.Movil.Consumidor.Utility;
using MisGastos.UI.Movil.Consumidor.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        IMovimientoManager _movimientoManager;
        ICuentaManager _cuentaManager;
        ICategoriaManager _categoriaManager;
        IBalanceManager _balanceManager;

        private ObservableCollection<Movimiento> _movimientos;
        private Balance _balance;
        private ObservableCollection<MovimientoModel> _movimientoModels;

        public Command RegistrarIngresoCommnad { get; } 
        public Command RegistrarEgresoCommand { get; }
        public Command MovimientoSelectedCommand { get; }

        public ObservableCollection<Movimiento> Movimientos
        {
            get => _movimientos;
            set => SetProperty(ref _movimientos, value);
        }

        public Balance Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }

        public ObservableCollection<MovimientoModel> MovimientoModels
        {
            get => _movimientoModels;
            set => SetProperty(ref _movimientoModels, value);
        }

        public HomeViewModel(FactoryManager factoryManager)
        {
            Title = "Inicio";
            _movimientoManager = factoryManager.MovimientoManager();
            _balanceManager = factoryManager.BalanceManager();
            _categoriaManager = factoryManager.CategoriaManager();
            _cuentaManager = factoryManager.CuentaManager();
            RegistrarIngresoCommnad = new Command(OnRegistrarIngreso);
            RegistrarEgresoCommand = new Command(OnRegistrarEgreso);
            MovimientoSelectedCommand = new Command<MovimientoModel>(OnMovimientoSelected);
            ActualizarDatos();

            //Ingreso events
            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoChanged);
            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Balance>
                (this, MessageNames.BalanceChangedMessage, OnBalanceChanged);

            //Egreso events
            MessagingCenter.Subscribe<RegistrarEgresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoEgresoChanged);
            MessagingCenter.Subscribe<RegistrarEgresoViewModel, Balance>
                (this, MessageNames.BalanceChangedMessage, OnBalanceEgresoChanged);

            //Config events
            MessagingCenter.Subscribe<ConfiguracionesViewModel, Balance>
                (this, MessageNames.BalanceChangedMessage, OnBalanceConfigChanged);
        }


        private async void OnMovimientoSelected(MovimientoModel movimientoModel)
        {
            Categoria categoria = _categoriaManager.SearchById(movimientoModel.Movimiento.IdCategoria);
            if (categoria.TipoCategoria == TipoCategoria.Ingresos)
            {
                await Shell.Current.GoToAsync($"{nameof(RegistrarIngresoPage)}?{nameof(RegistrarIngresoViewModel.MovimientoId)}={movimientoModel.Movimiento.Id}");
            }
            else if (categoria.TipoCategoria == TipoCategoria.Gastos)
            {
                await Shell.Current.GoToAsync($"{nameof(RegistrarEgresoPage)}?{nameof(RegistrarEgresoViewModel.MovimientoId)}={movimientoModel.Movimiento.Id}");
            }
        }

        private void OnBalanceConfigChanged(ConfiguracionesViewModel arg1, Balance balance)
        {
            ActualizarDatos();
        }

        private void OnBalanceEgresoChanged(RegistrarEgresoViewModel sender, Balance balance)
        {
            ActualizarDatos();
        }

        private void OnMovimientoEgresoChanged(RegistrarEgresoViewModel sender, Movimiento balance)
        {
            ActualizarDatos();
        }

        private void OnBalanceChanged(RegistrarIngresoViewModel sender, Balance balance)
        {
            ActualizarDatos();
        }

        private void OnMovimientoChanged(RegistrarIngresoViewModel sender, Movimiento movimiento)
        {
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            Movimientos = _movimientoManager.ObtenerTodo.ToObservableCollection();
            Balance = _balanceManager.ObtenerTodo.FirstOrDefault();

            MovimientoModels = new ObservableCollection<MovimientoModel>();
            foreach (var item in _movimientoManager.ObtenerTodo.ToObservableCollection())
            {
                Cuenta cuenta = _cuentaManager.SearchById(item.IdCuenta);
                Categoria categoria = _categoriaManager.SearchById(item.IdCategoria);

                MovimientoModels.Add(new MovimientoModel
                {
                    Movimiento = item,            
                    NombreCuenta = cuenta.Nombre,
                    TipoCategoria = categoria.TipoCategoria.ToString(),
                    ImageUrl = (categoria.TipoCategoria == TipoCategoria.Ingresos) ? "icon_ingreso.png" : "icon_egreso.png",
                    Background = (categoria.TipoCategoria == TipoCategoria.Ingresos) ? "LightBlue" : "White",
                    DescripcionMovimiento = item.Descripcion,
                });
            }
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
