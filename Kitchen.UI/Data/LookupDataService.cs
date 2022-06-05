using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Lookups;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kitchen.UI.Data.Lookups
{
    public class LookupDataService : Repository, IFriendLookupDataService, 
    IMeetingLookupDataService
  {

        public IEnumerable<LookupItem> GetFriendLookup() => customers
              .Select(f =>
              new LookupItem
              {
                  Id = f.Id,
                  DisplayMember = f.FullName
              })
              .ToList();

        public List<LookupItem> GetMeetingLookup()
        {
            throw new NotImplementedException();
        } 

        //    public async Task<IEnumerable<LookupItem>> GetProgrammingLanguageLookupAsync()
        //{
        //  using (var ctx = _contextCreator())
        //  {
        //    return await ctx.ProgrammingLanguages.AsNoTracking()
        //      .Select(f =>
        //      new LookupItem
        //      {
        //        Id = f.Id,
        //        DisplayMember = f.Name
        //      })
        //      .ToListAsync();
        //  }
        //}

        //public async Task<List<LookupItem>> GetMeetingLookupAsync()
        //{
        //  using (var ctx = _contextCreator())
        //  {
        //    var items = await ctx.Meetings.AsNoTracking()
        //      .Select(m =>
        //         new LookupItem
        //         {
        //           Id = m.Id,
        //           DisplayMember = m.Title
        //         })
        //      .ToListAsync();
        //    return items;
        //  }
        //}

    }
}
