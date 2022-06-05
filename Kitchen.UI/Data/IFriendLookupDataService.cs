using FriendOrganizer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Data.Lookups
{
    public interface IFriendLookupDataService
    {
        IEnumerable<LookupItem> GetFriendLookup();
    }
}