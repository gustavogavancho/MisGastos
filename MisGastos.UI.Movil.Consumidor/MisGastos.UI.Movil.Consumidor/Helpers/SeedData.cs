using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System.Linq;

namespace MisGastos.UI.Movil.Consumidor.Helpers
{
    public class SeedData
    {
        static FactoryManager _factoryManager = App.FactoryManager;
        static ICuentaManager _cuentaManager = _factoryManager.CuentaManager();

        public static void SeedCuenta()
        {
            if (_cuentaManager.ObtenerTodo.Count() <= 0)
            {
                Cuenta[] Cuentas = new Cuenta[]
                {
                    new Cuenta
                    {
                        Nombre = "Efectivo",
                    },

                    new Cuenta
                    {
                        Nombre = "Tarjeta Debidto",
                    },

                };

                foreach (Cuenta cuenta in Cuentas)
                {
                    _cuentaManager.Insertar(cuenta);
                }
            }
        }
    }
}
