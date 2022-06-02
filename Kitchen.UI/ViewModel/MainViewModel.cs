using Kitchen.Model;
using Kitchen.UI.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kitchen.UI.ViewModel
{ 
    public class MainViewModel:ViewModelBase
    {
        private Order _selectedOrder;
        public Order SelectedOrder
        {
            get { return _selectedOrder; }
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Order> Orders { get; set; }
         

        private IOrderRepository _orderDataService; 
        public MainViewModel(IOrderRepository OrderDataService)
        {
            Orders = new ObservableCollection<Order>();
            _orderDataService = OrderDataService;
        }


        public void Load()
        {

            var orders = _orderDataService.GetAll();
            Orders.Clear();//чтобы не загружать дважды 

            foreach (var item in orders)
            {
                Orders.Add(item);
            }

        }
    }
}
