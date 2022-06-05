using FriendOrganizer.Model;
using FriendOrganizer.UI.Data.Lookups;
using FriendOrganizer.UI.Wrapper;
using Kitchen.Model;
using Kitchen.UI.Data;
using Kitchen.UI.Event;
using Kitchen.UI.View.Services;
using Kitchen.UI.Wrapper;
using Prism.Commands;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Kitchen.UI.ViewModel
{
    public class FriendDetailViewModel : DetailViewModelBase, IFriendDetailViewModel
    {
        private IClientRepository _clientRepository;
        private FriendWrapper _friend;
        private FriendPhoneNumberWrapper _selectedPhoneNumber;

        public FriendDetailViewModel(IClientRepository clientRepository,
          IEventAggregator eventAggregator,
          IMessageDialogService messageDialogService)
          : base(eventAggregator, messageDialogService)
        {
            _clientRepository = clientRepository; 

            eventAggregator.GetEvent<AfterCollectionSavedEvent>()
             .Subscribe(AfterCollectionSaved);

            AddPhoneNumberCommand = new DelegateCommand(OnAddPhoneNumberExecute);
            RemovePhoneNumberCommand = new DelegateCommand(OnRemovePhoneNumberExecute, OnRemovePhoneNumberCanExecute);

            ProgrammingLanguages = new ObservableCollection<LookupItem>();
            PhoneNumbers = new ObservableCollection<FriendPhoneNumberWrapper>();
        }

        private void AfterCollectionSaved(AfterCollectionSavedEventArgs obj)
        {
            throw new NotImplementedException();
        }

        public override async Task LoadAsync(int id)
        {
            var customer = id > 0
              ?  _clientRepository.GetById(id)
              : CreateNewCustomer();

            Id = id;

            InitializeFriend(customer);

            InitializeFriendPhoneNumbers(customer.PhoneNumbers); 
        }

        private void InitializeFriend(Customer customer)
        {
            Friend = new FriendWrapper(customer);
            Friend.PropertyChanged += (s, e) =>
            {
                if (!HasChanges)
                {
                    HasChanges = _clientRepository.HasChanges();
                }
                if (e.PropertyName == nameof(Friend.HasErrors))
                {
                    ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
                }
                if (e.PropertyName == nameof(Friend.FullName) )
                {
                    SetTitle();
                }

            };
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            if (Friend.Id == 0)
            {
                // Маленькая хитрость для запуска проверки
                Friend.FullName = "";
            }
            SetTitle();
        }

        private void SetTitle() => Title = Friend.FullName;

        private void InitializeFriendPhoneNumbers(ICollection<FriendPhoneNumber> phoneNumbers)
        {
            foreach (var wrapper in PhoneNumbers)
            {
                wrapper.PropertyChanged -= FriendPhoneNumberWrapper_PropertyChanged;
            }
            PhoneNumbers.Clear();
            foreach (var friendPhoneNumber in phoneNumbers)
            {
                var wrapper = new FriendPhoneNumberWrapper(friendPhoneNumber);
                PhoneNumbers.Add(wrapper);
                wrapper.PropertyChanged += FriendPhoneNumberWrapper_PropertyChanged;
            }
        }

        private void FriendPhoneNumberWrapper_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (!HasChanges)
            {
                HasChanges = _clientRepository.HasChanges();
            }
            if (e.PropertyName == nameof(FriendPhoneNumberWrapper.HasErrors))
            {
                ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        } 

        public FriendWrapper Friend
        {
            get { return _friend; }
            private set
            {
                _friend = value;
                OnPropertyChanged();
            }
        }

        public FriendPhoneNumberWrapper SelectedPhoneNumber
        {
            get { return _selectedPhoneNumber; }
            set
            {
                _selectedPhoneNumber = value;
                OnPropertyChanged();
                ((DelegateCommand)RemovePhoneNumberCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand AddPhoneNumberCommand { get; }

        public ICommand RemovePhoneNumberCommand { get; }

        public ObservableCollection<LookupItem> ProgrammingLanguages { get; }

        public ObservableCollection<FriendPhoneNumberWrapper> PhoneNumbers { get; }

        protected override async void OnSaveExecute()
        {
            _clientRepository.Save();
            HasChanges = _clientRepository.HasChanges();
            Id = Friend.Id;
            RaiseDetailSavedEvent(Friend.Id, Friend.FullName);
        }

        protected override bool OnSaveCanExecute()
        {
            return Friend != null
              && !Friend.HasErrors
              && PhoneNumbers.All(pn => !pn.HasErrors)
              && HasChanges;
        }

        protected override async void OnDeleteExecute()
        {
            if (_clientRepository.HasMeetings(Friend.Id))
            {
                MessageDialogService.ShowInfoDialog($"{Friend.FullName} can't be deleted, as this client is part of at least one meeting");
                return;
            }

            var result = MessageDialogService.ShowOkCancelDialog($"Do you really want to delete the client {Friend.FullName}?",
              "Question");
            if (result == MessageDialogResult.OK)
            {
                _clientRepository.Remove(Friend.Model);
                _clientRepository.Save();
                RaiseDetailDeletedEvent(Friend.Id);
            }
        }

        private void OnAddPhoneNumberExecute()
        {
            var newNumber = new FriendPhoneNumberWrapper(new FriendPhoneNumber());
            newNumber.PropertyChanged += FriendPhoneNumberWrapper_PropertyChanged;
            PhoneNumbers.Add(newNumber);
            Friend.Model.PhoneNumbers.Add(newNumber.Model);
            newNumber.Number = ""; // Trigger validation :-)
        }

        private void OnRemovePhoneNumberExecute()
        {
            SelectedPhoneNumber.PropertyChanged -= FriendPhoneNumberWrapper_PropertyChanged;
            _clientRepository.RemovePhoneNumber(SelectedPhoneNumber.Model);
            PhoneNumbers.Remove(SelectedPhoneNumber);
            SelectedPhoneNumber = null;
            HasChanges = _clientRepository.HasChanges();
            ((DelegateCommand)SaveCommand).RaiseCanExecuteChanged();
        }

        private bool OnRemovePhoneNumberCanExecute()
        {
            return SelectedPhoneNumber != null;
        }

        private Customer CreateNewCustomer()
        {
            var customer = new Customer();
            _clientRepository.Add(customer);
            return customer;
        } 
    }
}
