using System.Collections.Generic;

namespace ModelViewPresenter.WindowsForms.Shared
{
    public interface IRepository<TModel, in TKey>
    {
        void Add(TModel model);
        void Remove(TModel model);
        TModel GetById(TKey id);
        IEnumerable<TModel> GetAll();
    }
}
