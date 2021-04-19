using DutchTreat.Data.Entities;
using System.Collections.Generic;

namespace DutchTreat.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetProducts();
        IEnumerable<Product> GetProductsByCategory(string category);
        IEnumerable<Order> GetAllOrders(bool includeItem);
        IEnumerable<Order> GetAllOrdersByUser(string user, bool includeItem);

        Order GetOrderById(int id);
        bool SaveAll();
        void AddEntity(object model);
        Order GetOrderByIdForUser(string user, int orderId);
    }
}