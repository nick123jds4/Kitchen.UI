using Autofac.Features.Indexed;
using Kitchen.Model;
using Kitchen.UI.Data;
using Kitchen.UI.Event;
using Kitchen.UI.View.Services;
using Prism.Events;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.UI.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private IEventAggregator _eventAggregator;
        private Customer _selectedOrder;
        private IClientRepository _clientRepository;
        private IDetailViewModel _selectedDetailViewModel;
        private IIndex<string, IDetailViewModel> _detailViewModelCreator;

        #region Properies
        public Customer SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        public IDetailViewModel SelectedDetailViewModel
        {
            get { return _selectedDetailViewModel; }
            set
            {
                _selectedDetailViewModel = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Customer> Clients { get; set; }
        public INavigationViewModel NavigationViewModel { get; }
        public ObservableCollection<IDetailViewModel> DetailViewModels { get; }
        #endregion

        public MainViewModel(IClientRepository clientRepository,
            INavigationViewModel navigationViewModel,
            IEventAggregator eventAggregator,
            IMessageDialogService messageDialogService,
            IIndex<string, IDetailViewModel> detailViewModelCreator)
        {
            _clientRepository = clientRepository;
            _eventAggregator = eventAggregator;
            _detailViewModelCreator = detailViewModelCreator;
            Clients = new ObservableCollection<Customer>();
            DetailViewModels = new ObservableCollection<IDetailViewModel>();

            _eventAggregator.GetEvent<OpenDetailViewEvent>()
             .Subscribe(OnOpenDetailView);
            _eventAggregator.GetEvent<AfterDetailClosedEvent>()
        .Subscribe(AfterDetailClosed);


            NavigationViewModel = navigationViewModel;
        }

        private async void OnOpenDetailView(OpenDetailViewEventArgs args)
        {
            IDetailViewModel detailViewModel = DetailViewModels
              .SingleOrDefault(vm => vm.Id == args.Id
              && vm.GetType().Name == args.ViewModelName);

            if (detailViewModel == null)
            {
                detailViewModel = _detailViewModelCreator[args.ViewModelName];
                await detailViewModel.LoadAsync(args.Id);
                DetailViewModels.Add(detailViewModel);
            }

            SelectedDetailViewModel = detailViewModel;
        }

        public async Task LoadAsync()
        {
            await NavigationViewModel.LoadAsync();
        }

        private void AfterDetailClosed(AfterDetailClosedEventArgs args)
        {
            RemoveDetailViewModel(args.Id, args.ViewModelName);
        }

        private void RemoveDetailViewModel(int id, string viewModelName)
        {
            var detailViewModel = DetailViewModels
                   .SingleOrDefault(vm => vm.Id == id
                   && vm.GetType().Name == viewModelName);
            if (detailViewModel != null)
            {
                DetailViewModels.Remove(detailViewModel);
            }
        }
    }
}
