﻿using MisGastos.UI.Movil.Consumidor.ViewModels;

namespace MisGastos.UI.Movil.Consumidor.Utility
{
    public static class ViewModelLocator
    {
        public static CuentaViewModel CuentaViewModel { get; set; } =
            new CuentaViewModel(App.FactoryManager);

        public static CuentaDetailViewModel CuentaDetailViewModel { get; set; } =
            new CuentaDetailViewModel(App.FactoryManager);
    }
}
