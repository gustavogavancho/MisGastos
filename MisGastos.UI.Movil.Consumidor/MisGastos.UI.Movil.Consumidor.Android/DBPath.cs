using System.IO;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DBPath))]
namespace MisGastos.UI.Movil.Consumidor.Droid
{
    public class DBPath : IDBPath
    {
        public string DBDir()
        {
            var path = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "Prestamo.bd");
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            return path;
        }
    }
}