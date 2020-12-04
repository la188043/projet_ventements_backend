using System;
using System.Collections.Generic;
using Application.Repositories;
using Application.Services.Orders.Dto;
using Domain.Orders;

namespace Application.Services.Orders
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderedItemRepository _orderedItemRepository;

        public OrderService(IOrderRepository orderRepository, IOrderedItemRepository orderedItemRepository)
        {
            _orderRepository = orderRepository;
            _orderedItemRepository = orderedItemRepository;
        }

        public IEnumerable<OutputQueryOrder> GetByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public OutputAddOrder Create(int userId)
        {
            var orderFromDb = _orderRepository.Create(userId);

            // todo
            throw new NotImplementedException();
        }
    }
}