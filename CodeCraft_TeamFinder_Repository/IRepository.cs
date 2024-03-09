using CodeCraft_TeamFinder_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Repository
{
    public interface IRepository<T> where T : BaseClass
    {
        Task<T> Get(string id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(string fieldName, string fieldValue);
        Task<bool> Create(T t);
        Task<bool> Update(T t);
        Task<bool> Delete(string id);
    }
}
