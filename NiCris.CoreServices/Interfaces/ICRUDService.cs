using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NiCris.CoreServices.Interfaces
{
    public interface ICRUDService<T>
    {
        IList<T> GetAll();
        T GetById(int id);
        int Insert(T model);
        int Update(T model);
        void Delete(T model);
    }
}
