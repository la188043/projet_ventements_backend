using System.Collections.Generic;
using Application.Services.Orders.Dto;

namespace Application.Services.Orders
{
    public interface IOrderService
    {
        IEnumerable<OutputQueryOrder> GetByUserId(int userId);
        OutputAddOrder Create(int userId);
    }
}