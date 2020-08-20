using System;

namespace MisGastos.UI.Movil.Consumidor.Utility
{
    public class MasterNavigationItem
    {
        public MasterNavigationItem()
        {
            Target = typeof(MasterNavigationItem);

        }

        public string Title { get; set; }
        public string Icon { get; set; }
        public Type Target { get; set; }
    }
}
