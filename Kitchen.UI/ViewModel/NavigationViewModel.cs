using FriendOrganizer.UI.Data.Lookups;
using Kitchen.UI.Data;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Kitchen.UI.ViewModel
{
    public class NavigationViewModel : ViewModelBase, INavigationViewModel
    {
        private IClientRepository _clientRepository;
        private IEventAggregator _eventAggregator;
        private IMeetingLookupDataService _meetingLookupService;

        public NavigationViewModel(IClientRepository clientRepository,
          IMeetingLookupDataService meetingLookupService,
          IEventAggregator eventAggregator)
        {
            _clientRepository = clientRepository;
            _meetingLookupService = meetingLookupService;
            _eventAggregator = eventAggregator;
            Clients = new ObservableCollection<NavigationItemViewModel>();
            Measurements = new ObservableCollection<NavigationItemViewModel>();
            // _eventAggregator.GetEvent<AfterDetailSavedEvent>().Subscribe(AfterDetailSaved);
            // _eventAggregator.GetEvent<AfterDetailDeletedEvent>().Subscribe(AfterDetailDeleted);
        }

        public async Task LoadAsync()
        {
            var clients = _clientRepository.GetAll();
            Clients.Clear();
            foreach (var item in clients)
            {
                Clients.Add(new NavigationItemViewModel(item.Id, item.FullName,
                  nameof(FriendDetailViewModel),
                  _eventAggregator));
            }
            //clients = await _meetingLookupService.GetMeetingLookupAsync();
            //Measurements.Clear();
            //foreach (var item in clients)
            //{
            //    Measurements.Add(new NavigationItemViewModel(item.Id, item.DisplayMember,
            //      nameof(MeetingDetailViewModel),
            //      _eventAggregator));
            //}
        }

        public ObservableCollection<NavigationItemViewModel> Clients { get; }

        public ObservableCollection<NavigationItemViewModel> Measurements { get; }

        //private void AfterDetailDeleted(AfterDetailDeletedEventArgs args)
        //{
        //    switch (args.ViewModelName)
        //    {
        //        case nameof(FriendDetailViewModel):
        //            AfterDetailDeleted(Clients, args);
        //            break;
        //        case nameof(MeetingDetailViewModel):
        //            AfterDetailDeleted(measurements, args);
        //            break;
        //    }
        //}

        //private void AfterDetailDeleted(ObservableCollection<NavigationItemViewModel> items,
        //  AfterDetailDeletedEventArgs args)
        //{
        //    var item = items.SingleOrDefault(f => f.Id == args.Id);
        //    if (item != null)
        //    {
        //        items.Remove(item);
        //    }
        //}

        //private void AfterDetailSaved(AfterDetailSavedEventArgs args)
        //{
        //    switch (args.ViewModelName)
        //    {
        //        case nameof(FriendDetailViewModel):
        //            AfterDetailSaved(Clients, args);
        //            break;
        //        case nameof(MeetingDetailViewModel):
        //            AfterDetailSaved(measurements, args);
        //            break;
        //    }
        //}

        //private void AfterDetailSaved(ObservableCollection<NavigationItemViewModel> items,
        //  AfterDetailSavedEventArgs args)
        //{
        //    var lookupItem = items.SingleOrDefault(l => l.Id == args.Id);
        //    if (lookupItem == null)
        //    {
        //        items.Add(new NavigationItemViewModel(args.Id, args.DisplayMember,
        //          args.ViewModelName,
        //          _eventAggregator));
        //    }
        //    else
        //    {
        //        lookupItem.DisplayMember = args.DisplayMember;
        //    }
        //}
    }
}
