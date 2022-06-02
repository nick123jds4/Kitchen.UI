using Kitchen.Model;
using System.Collections.Generic;

namespace Kitchen.UI.Data
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
    }
}