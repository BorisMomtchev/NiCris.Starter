using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiCris.DataAccess.Interfaces
{
    public interface ICRUDRepository<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        int Insert(T model);
        int Update(T model);
        void Delete(T model);
    }
}
