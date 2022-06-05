using FriendOrganizer.Model;
using Kitchen.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.UI.Data.Repositories
{
    public class MeetingRepository : Repository, IGenericRepository<Meeting>, IMeetingRepository
    {

        public Meeting GetById(int id)
        {
            return meetings.Single(m => m.Id == id);
        }

        public List<Customer> GetAllFriends()
        {
            return customers;
        }

        public async Task ReloadFriendAsync(int friendId)
        {
            //var dbEntityEntry = Context.ChangeTracker.Entries<Friend>()
            //  .SingleOrDefault(db => db.Entity.Id == friendId);
            //if(dbEntityEntry!=null)
            //{
            //  await dbEntityEntry.ReloadAsync();
            //}
        }

        public IEnumerable<Meeting> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task SaveAsync()
        {
            throw new System.NotImplementedException();
        }

        public bool HasChanges()
        {
            return false;
        }

        public void Add(Meeting model)
        {
            meetings.Add(model);
        }

        public void Remove(Meeting model)
        {
            throw new System.NotImplementedException();
        }
    }
}
