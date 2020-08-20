using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using MisGastos.COMMON.Interfaces;
using MisGastos.UI.Movil.Consumidor.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DBPath))]
namespace MisGastos.UI.Movil.Consumidor.iOS
{
    public class DBPath : IDBPath
    {
        public string DBDir()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, "Prestamo.db");
        }
    }
}