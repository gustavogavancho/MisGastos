using MisGastos.BIZ;
using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Enumeraciones;
using MisGastos.COMMON.Interfaces;
using System.Linq;

namespace MisGastos.UI.Movil.Consumidor.Helpers
{
    public class SeedData
    {
        static FactoryManager _factoryManager = App.FactoryManager;
        static ICuentaManager _cuentaManager = _factoryManager.CuentaManager();
        static ICategoriaManager _categoriaManager = _factoryManager.CategoriaManager();
        static IMovimientoManager _movimientoManager = _factoryManager.MovimientoManager();
        static IBalanceManager _balanceManager = _factoryManager.BalanceManager();

        public static void VaciarCuenta()
        {
            var cuentaEliminar = _cuentaManager.ObtenerTodo;
            foreach (var item in cuentaEliminar)
            {
                _cuentaManager.Eliminar(item.Id);
            }
        }

        public static void SeedCuenta()
        {
            if (_cuentaManager.ObtenerTodo.Count() <= 0)
            {
                Cuenta[] Cuentas = new Cuenta[]
                {
                    new Cuenta
                    {
                        Nombre = "Efectivo",
                        ImageUrl = "icon_efectivo.png",
                        Balance = 0.0M,
                    },

                    new Cuenta
                    {
                        Nombre = "Tarjeta Débito",
                        ImageUrl = "icon_tarjetasueldo",
                        Balance = 0.0M,
                    },

                };

                foreach (Cuenta cuenta in Cuentas)
                {
                    _cuentaManager.Insertar(cuenta);
                }
            }
        }

        public static void VaciarCategoria()
        {
            var categoriaEliminar = _categoriaManager.ObtenerTodo;
            foreach (var item in categoriaEliminar)
            {
                _categoriaManager.Eliminar(item.Id);
            }
        }

        public static void SeedCategoria()
        {
            if (_categoriaManager.ObtenerTodo.Count() <= 0)
            {
                Categoria[] Categorias = new Categoria[]
                {
                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Cuentas",
                        ImageUrl = "icon_bills.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Carro",
                        ImageUrl = "icon_cars.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Ropa",
                        ImageUrl = "icon_clothes.png"
                    },
                    
                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Comunicaciones",
                        ImageUrl = "icon_comunicaciones.png"
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Restaurantes",
                        ImageUrl = "icon_restaurantes.png"
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Entretenimiento",
                        ImageUrl = "icon_entretenimiento.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Comida",
                        ImageUrl = "icon_comida.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Regalos",
                        ImageUrl = "icon_regalos.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Salud",
                        ImageUrl = "icon_salud.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Casa",
                        ImageUrl = "icon_casa.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Mascotas",
                        ImageUrl = "icon_mascotas.png"
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Deportes",
                        ImageUrl = "icon_deportes.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Taxi",
                        ImageUrl = "icon_taxi.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Limpieza",
                        ImageUrl = "icon_limpieza.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Gastos,
                        Nombre = "Transporte",
                        ImageUrl = "icon_transporte.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Ingresos,
                        Nombre = "Depositos",
                        ImageUrl = "icon_depositos.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Ingresos,
                        Nombre = "Sueldo",
                        ImageUrl = "icon_sueldo.png",
                    },

                    new Categoria
                    {
                        TipoCategoria = TipoCategoria.Ingresos,
                        Nombre = "Ahorros",
                        ImageUrl = "icon_ahorros.png",
                    },
                };

                foreach (Categoria categoria in Categorias)
                {
                    _categoriaManager.Insertar(categoria);
                }
            }
        }

        public static void VaciarMovimiento()
        {
            var movimientoAEliminar = _movimientoManager.ObtenerTodo;
            foreach (var item in movimientoAEliminar)
            {
                _movimientoManager.Eliminar(item.Id);
            }
        }

        public static void SeedBalance()
        {
            if (_balanceManager.ObtenerTodo.Count() <= 0)
            {
                Balance balance = new Balance { BalanceGeneral = 0.0M };
                _balanceManager.Insertar(balance);
            }
        }
        
        public static void VaciarBalance()
        {
            var balanceEliminar = _balanceManager.ObtenerTodo;
            foreach (var item in balanceEliminar)
            {
                _balanceManager.Eliminar(item.Id);
            }
        }
    }
}
