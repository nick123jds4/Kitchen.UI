using Kitchen.Model;
using System.Collections.Generic;

namespace Kitchen.UI.Data
{
    public class OrderRepository : IOrderRepository
    {
        public IEnumerable<Order> GetAll()
        {
            yield return new Order() { FirstName = "Thomas", LastName = "Huber" };
            yield return new Order() { FirstName = "Andy", LastName = "Spencer" };
        }
    }
}
