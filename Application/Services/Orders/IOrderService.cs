using System.Collections.Generic;
using Application.Services.Orders.Dto;

namespace Application.Services.Orders
{
    public interface IOrderService
    {
        IEnumerable<OutputDtoQueryOrder> GetByUserId(int userId);
        OutputDtoQueryOrder GetById(int orderId);
        OutputDtoAddOrder Create(int userId);
        bool Delete(int orderId);
    }
}