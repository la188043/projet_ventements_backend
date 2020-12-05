using System.Collections.Generic;
using Domain.Orders;

namespace Application.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<IOrder> GetByUserId(int userId);
        IOrder GetById(int orderId);
        IOrder Create(int userId);
        bool Delete(int orderId);
    }
}