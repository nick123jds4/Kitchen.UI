using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kitchen.UI.Data.Repositories
{
  public interface IGenericRepository<T>
  {
    T GetById(int id);
    IEnumerable<T> GetAll();
    Task SaveAsync();
    bool HasChanges();
    void Add(T model);
    void Remove(T model);
  }
}