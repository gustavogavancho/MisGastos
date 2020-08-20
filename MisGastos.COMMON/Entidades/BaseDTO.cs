using System;

namespace MisGastos.COMMON.Entidades
{
    public abstract class BaseDTO : IDisposable
    {
        private bool _isDiposed;

        public string Id { get; set; }
        public DateTime FechaHoraCreacion { get; set; }

        public void Dispose()
        {
            if (!_isDiposed)
            {
                this._isDiposed = true;
                GC.SuppressFinalize(this);
            }
        }
    }
}
