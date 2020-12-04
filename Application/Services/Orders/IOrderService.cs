using System.Collections.Generic;
using Application.Services.Orders.Dto;

namespace Application.Services.Orders
{
    public interface IOrderService
    {
        IEnumerable<OutputQueryOrder> GetByUserId(int userId);
        OutputQueryOrder GetById(int orderId);
        OutputAddOrder Create(int userId);
    }
}