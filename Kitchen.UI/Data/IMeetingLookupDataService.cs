using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data.Lookups
{
    public interface IMeetingLookupDataService
    {
        List<LookupItem> GetMeetingLookup();
    }
}
