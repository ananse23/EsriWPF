using System.Collections.Generic;

namespace SOPFIM.DataLayer
{
    public interface IRepository<T>
    {
        List<T> QueryData(string whereClause);
        void Save(List<T> listToSave);
    }
}