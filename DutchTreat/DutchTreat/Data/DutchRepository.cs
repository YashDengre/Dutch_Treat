using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            _ctx = ctx;
            _logger = logger;
        }

        public Order GetOrderById(int id)
        {
            //return _ctx.Orders.Find(id);
            return _ctx.Orders.Include(i => i.Items)
              .ThenInclude(i => i.Product).Where(o => o.Id == id).FirstOrDefault();

        }

        public IEnumerable<Order> GetAllOrders(bool includeItem)
        {
            if(includeItem)
            {
                return _ctx.Orders.Include(i => i.Items)
                    .ThenInclude(i => i.Product).ToList();
            }
            else
            {
                return _ctx.Orders.ToList();
            }
            // Include used to for mitigate the lazy binding  and forcefully including the items
            //if there is and referencial cycle in the object then we will get the error
            //In order to handle this - we can remove the field it self from the class or we can make some changes in startup.cs
            //we have to include the newtonsoft json which is handled by microsoft frmm nuget and 
        }

        public IEnumerable<Product> GetProducts()
        {
            try
            {
                _logger.LogInformation("Get Products called.");
                return _ctx.Products.OrderBy(p => p.Title).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Get Products Failed: Reason: {ex}");
                return null;
            }
        }
        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            try
            {
                return _ctx.Products.Where(p => p.Category == category).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($" GetProductsByCategory Failed: Reason: {ex}");
                return null;
            }
        }
        public bool SaveAll()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError($" SaveAll Failed: Reason: {ex}");
                return false;
            }
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }

        public IEnumerable<Order> GetAllOrdersByUser(string user, bool includeItem)
        {

            if (includeItem)
            {
                return _ctx.Orders.Where(o=>o.User.UserName == user).Include(i => i.Items)
                    .ThenInclude(i => i.Product).ToList();
            }
            else
            {
                return _ctx.Orders.Where(o => o.User.UserName == user).ToList();
            }
        }

        public Order GetOrderByIdForUser(string user, int orderId)
        {
            //return _ctx.Orders.Find(id);
            return _ctx.Orders.Where(o => o.User.UserName == user).Include(i => i.Items)
              .ThenInclude(i => i.Product).Where(o => o.Id == orderId).FirstOrDefault();
        }
    }
}
