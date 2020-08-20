using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MisGastos.UI.Movil.Consumidor.Entensions
{
    public static class LinqExtensions
    {
        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> _LinqResult)
        {
            return new ObservableCollection<T>(_LinqResult);
        }
    }
}
