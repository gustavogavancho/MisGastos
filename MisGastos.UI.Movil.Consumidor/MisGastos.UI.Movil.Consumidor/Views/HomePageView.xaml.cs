using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MisGastos.UI.Movil.Consumidor.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageView : ContentPage
    {
        FactoryManager _factoryManager;
        ICuentaManager _cuentaManager;
        public List<Cuenta> cuentas;
        public HomePageView()
        {
            InitializeComponent();
            _factoryManager = new FactoryManager("LiteDB");
            _cuentaManager = _factoryManager.CuentaManager();
            GetCuentas();
            this.BindingContext = cuentas;
            //InsertarCuenta();
        }

        public void InsertarCuenta()
        {
            var cuenta = new Cuenta()
            {
                Nombre = "Saldo Total"
            };

            _cuentaManager.Insertar(cuenta);
        }

        public void GetCuentas()
        {
            cuentas = _cuentaManager.ObtenerTodo.ToList();
        }
    }
}