using MisGastos.COMMON.Entidades;
using MisGastos.COMMON.Interfaces;
using System.Collections.Generic;

namespace MisGastos.BIZ
{
    public class GenericManager<T> : IGenericManager<T> where T : BaseDTO
    {
        internal readonly IGenericRepository<T> _repositorio;

        public GenericManager(IGenericRepository<T> repositorio)
        {
            _repositorio = repositorio;
        }

        public string Error
        {
            get
            {
                return _repositorio.Error;
            }
        }

        public IEnumerable<T> ObtenerTodo
        {
            get
            {
                return _repositorio.Read;
            }
        }

        public T Actualizar(T entidad)
        {
            return _repositorio.Update(entidad);
        }

        public bool Eliminar(string id)
        {
            return _repositorio.Delete(id);
        }

        public T Insertar(T entidad)
        {
            return _repositorio.Create(entidad);
        }

        public T SearchById(string id)
        {
            return _repositorio.SearchById(id);
        }
    }
}
