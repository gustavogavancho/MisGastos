using MisGastos.COMMON.Entidades;
using System.Collections.Generic;

namespace MisGastos.COMMON.Interfaces
{
    public interface IGenericManager<T> where T : BaseDTO
    {
        string Error { get; }

        T Insertar(T entidad);

        IEnumerable<T> ObtenerTodo { get; }

        T Actualizar(T entidad);

        bool Eliminar(string id);

        T SearchById(string id);
    }
}
