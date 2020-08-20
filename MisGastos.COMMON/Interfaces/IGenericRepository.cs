using MisGastos.COMMON.Entidades;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MisGastos.COMMON.Interfaces
{
    public interface IGenericRepository<T> where T : BaseDTO
    {
        string Error { get; }
        T Create(T entidad);
        IEnumerable<T> Read { get; }
        T Update(T entidad);
        bool Delete(string id);
        IEnumerable<T> Query(Expression<Func<T, bool>> predicado);
        T SearchById(string id);
    }
}
