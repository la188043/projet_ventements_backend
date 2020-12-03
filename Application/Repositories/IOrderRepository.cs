using Domain.Orders;

namespace Application.Repositories
{
    public interface IOrderRepository
    {
        IOrder Create(int userId);
    }
}