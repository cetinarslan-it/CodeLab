using System;
using System.Collections.Generic;

namespace api.Repositories;

public interface IRepository<T> : IDisposable
{
    IEnumerable<T> GetAll();
    T? GetById(int id);
    void Insert(T item);
    void Delete(int id);
    void Update(T item);
    void Save();
}