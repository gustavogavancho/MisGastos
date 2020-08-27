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
        private ObservableCollection<Movimiento> _movimientos;
        private decimal _balance;
        private ObservableCollection<MovimientoModel> _movimientoModels;

        FactoryManager _factoryManager;
        IMovimientoManager _movimientoManager;
        ICuentaManager _cuentaManager;
        ICategoriaManager _categoriaManager;
        IBalanceManager _balanceManager;

        public Command RegistrarIngresoCommnad { get; } 
        public Command RegistrarEgresoCommand { get; }
        public Command MovimientoSelectedCommand { get; }

        public ObservableCollection<Movimiento> Movimientos
        {
            get => _movimientos;
            set => SetProperty(ref _movimientos, value);
        }

        public decimal Balance
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
            _factoryManager = factoryManager;
            _movimientoManager = _factoryManager.MovimientoManager();
            _balanceManager = factoryManager.BalanceManager();
            _categoriaManager = factoryManager.CategoriaManager();
            _cuentaManager = factoryManager.CuentaManager();
            RegistrarIngresoCommnad = new Command(OnRegistrarIngreso);
            RegistrarEgresoCommand = new Command(OnRegistrarEgreso);
            MovimientoSelectedCommand = new Command(OnMovimientoSelected);
            ActualizarDatos();

            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoChanged);
            MessagingCenter.Subscribe<RegistrarIngresoViewModel, Balance>
                (this, MessageNames.BalanceChangedMessage, OnBalanceChanged);

            MessagingCenter.Subscribe<RegistrarEgresoViewModel, Movimiento>
                (this, MessageNames.MovimientoChangedMessage, OnMovimientoEgresoChanged);
            MessagingCenter.Subscribe<RegistrarEgresoViewModel, Balance>
                (this, MessageNames.BalanceChangedMessage, OnBalanceEgresoChanged);
        }

        private async void OnMovimientoSelected(object obj)
        {
            MovimientoModel check = (MovimientoModel)obj;
            var categoria = _categoriaManager.SearchById(check.Movimiento.IdCategoria);
            if (categoria.TipoCategoria == TipoCategoria.Gastos)
            {
                await Shell.Current.GoToAsync(nameof(RegistrarEgresoPage));
            }
            else if (categoria.TipoCategoria == TipoCategoria.Ingresos)
            {
                await Shell.Current.GoToAsync(nameof(RegistrarIngresoPage));
            }
        }

        private void OnBalanceEgresoChanged(RegistrarEgresoViewModel arg1, Balance arg2)
        {
            Balance = _balanceManager.ObtenerTodo.FirstOrDefault().BalanceGeneral;
        }

        private void OnMovimientoEgresoChanged(RegistrarEgresoViewModel arg1, Movimiento arg2)
        {
            ActualizarDatos();
        }

        private void OnBalanceChanged(RegistrarIngresoViewModel arg1, Balance arg2)
        {
            Balance = _balanceManager.ObtenerTodo.FirstOrDefault().BalanceGeneral;
        }

        private void OnMovimientoChanged(RegistrarIngresoViewModel sender, Movimiento movimiento)
        {
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            Movimientos = _movimientoManager.ObtenerTodo.ToObservableCollection();
            Balance = _balanceManager.ObtenerTodo.FirstOrDefault().BalanceGeneral;

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
