using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Extensions;
using MisGastos.UI.Movil.Consumidor.Utility;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MisGastos.UI.Movil.Consumidor.ViewModels
{
    public class RegistrarEgresoViewModel : BaseViewModel
    {
        private ObservableCollection<Cuenta> _cuentas;
        private ObservableCollection<Categoria> _categorias;
        private Movimiento _movimiento;
        private Cuenta _cuenta;
        private Categoria _categoria;
        private Balance _balance;

        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;
        ICategoriaManager _categoriaManager;
        IMovimientoManager _movimientoManager;
        IBalanceManager _balanceManager;

        public ObservableCollection<Cuenta> Cuentas
        {
            get => _cuentas;
            set => SetProperty(ref _cuentas, value);
        }

        public ObservableCollection<Categoria> Categorias
        {
            get => _categorias;
            set => SetProperty(ref _categorias, value);
        }

        public Movimiento Movimiento
        {
            get => _movimiento;
            set => SetProperty(ref _movimiento, value);
        }

        public Cuenta Cuenta
        {
            get => _cuenta;
            set => SetProperty(ref _cuenta, value);
        }

        public Categoria Categoria
        {
            get => _categoria;
            set => SetProperty(ref _categoria, value);
        }

        public Balance Balance
        {
            get => _balance;
            set => SetProperty(ref _balance, value);
        }


        public Command GuardarMovimientoCommnad { get; }
        public Command RegresarCommand { get; }
        public Command OnApperaringCommand { get; }
        public Command OnDisappearingCommand { get; }

        public RegistrarEgresoViewModel(FactoryManager factoryManager)
        {
            Title = "Registrar Egreso";
            _factoryManager = factoryManager;
            _cuentaManager = _factoryManager.CuentaManager();
            _categoriaManager = _factoryManager.CategoriaManager();
            _movimientoManager = _factoryManager.MovimientoManager();
            _balanceManager = _factoryManager.BalanceManager();
            GuardarMovimientoCommnad = new Command(OnGuardarMovimineto);
            RegresarCommand = new Command(OnRegresar);
            OnApperaringCommand = new Command(OnApperaring);
            OnDisappearingCommand = new Command(OnDisappearing);
            ActualizarDatos();
        }

        private void OnApperaring(object obj)
        {
            Shell.Current.Navigating += Current_Navigating;
        }

        private void OnDisappearing()
        {
            Shell.Current.Navigating -= Current_Navigating;
            ActualizarDatos();
        }

        private async void Current_Navigating(object sender,
                                ShellNavigatingEventArgs e)
        {
            if (e.CanCancel)
            {
                e.Cancel();
                await GoBack();
            }
        }

        private async Task GoBack()
        {
            Shell.Current.Navigating -= Current_Navigating;
            await Shell.Current.GoToAsync("..", true);
        }

        private async void OnRegresar(object obj)
        {
            await Shell.Current.Navigation.PopAsync();
            ActualizarDatos();
        }

        private void ActualizarDatos()
        {
            Cuenta = new Cuenta();
            Categoria = new Categoria();
            Movimiento = new Movimiento();
            Cuentas = _cuentaManager.ObtenerTodo.ToObservableCollection();
            Categorias = _categoriaManager.ObtenerTodo.Where(x => x.TipoCategoria == TipoCategoria.Gastos).OrderBy(x => x.TipoCategoria).ToObservableCollection();
        }

        private async void OnGuardarMovimineto(object obj)
        {
            Movimiento.IdCategoria = Categoria.Id;
            Movimiento.IdCuenta = Cuenta.Id;
            Movimiento.Fecha = DateTime.Now;
            Cuenta cuentaToEdit = _cuentaManager.SearchById(Cuenta.Id);
            cuentaToEdit.Balance -= Movimiento.Monto;
            Balance balanceToEdit = _balanceManager.ObtenerTodo.FirstOrDefault();
            balanceToEdit.BalanceGeneral -= Movimiento.Monto;
            balanceToEdit.Egresos -= Movimiento.Monto;


            _cuentaManager.Actualizar(cuentaToEdit);
            _balanceManager.Actualizar(balanceToEdit);
            _movimientoManager.Insertar(Movimiento);

            MessagingCenter.Send(this, MessageNames.CuentaChangedMessage, Cuenta);
            MessagingCenter.Send(this, MessageNames.MovimientoChangedMessage, Movimiento);
            MessagingCenter.Send(this, MessageNames.BalanceChangedMessage, Balance);

            await Shell.Current.Navigation.PopAsync();

            ActualizarDatos();
        }
    }
}
