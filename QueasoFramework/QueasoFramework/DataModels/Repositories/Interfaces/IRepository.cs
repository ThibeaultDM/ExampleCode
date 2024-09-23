using System;
using System.Linq;

namespace QueasoFramework.DataModels.Repositories.Interfaces
{
    public interface IRepository<T> : IDisposable where T : DataObjectBase
    {
        #region CRUD

        void Insert(T record);

        void Update(T record);

        void Delete(T record);

        void DeleteById(Guid id);

        IQueryable<T> GetAll();

        T GetById(Guid id);

        #endregion CRUD

        #region Save

        void Save();

        #endregion Save
    }
}