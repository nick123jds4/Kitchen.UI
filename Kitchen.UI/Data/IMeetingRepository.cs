using System.Threading.Tasks;
using FriendOrganizer.Model;
using System.Collections.Generic;
using Kitchen.Model;

namespace Kitchen.UI.Data.Repositories
{
  public interface IMeetingRepository : IGenericRepository<Meeting>
  {
    List<Customer> GetAllFriends();
    Task ReloadFriendAsync(int friendId);
  }
}