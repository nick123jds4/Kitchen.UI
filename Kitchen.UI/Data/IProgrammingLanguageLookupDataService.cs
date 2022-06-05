using FriendOrganizer.Model;
using System.Collections.Generic;

namespace FriendOrganizer.UI.Data.Lookups
{
    public interface IProgrammingLanguageLookupDataService
    {
        IEnumerable<LookupItem> GetProgrammingLanguageLookup();
    }
}