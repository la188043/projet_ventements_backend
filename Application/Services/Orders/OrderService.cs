using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<OutputDtoQueryOrder> GetByUserId(int userId)
        {
            return _orderRepository
                .GetByUserId(userId)
                .Select(orderFromDb =>
                {
                    var orderedItems = _orderedItemRepository.GetByOrderId(orderFromDb.Id);
                    var userOrder = new UserOrder();
                    userOrder.AddOrderedItems(orderedItems);

                    return new OutputDtoQueryOrder
                    {
                        Id = orderFromDb.Id,
                        IsPaid = orderFromDb.IsPaid,
                        OrderedAt = orderFromDb.orderedAt,
                        TotalPrice = userOrder.ComputeOrderPrice(),
                        Ordered = new OutputDtoQueryOrder.User
                        {
                            Id = orderFromDb.Orderer.Id,
                            Firstname = orderFromDb.Orderer.Firstname,
                            Lastname = orderFromDb.Orderer.Lastname,
                            Email = orderFromDb.Orderer.Email,
                        },
                        OrderedItems = userOrder.OrderedItems.Select(orderedItem => new OutputDtoQueryOrder.Item
                        {
                            Id = orderedItem.ItemOrdered.Id,
                            Label = orderedItem.ItemOrdered.Label,
                            Price = orderedItem.ItemOrdered.Price,
                            ImageItem = orderedItem.ItemOrdered.ImageItem,
                            DescriptionItem = orderedItem.ItemOrdered.DescriptionItem,
                            Quantity = orderedItem.Quantity
                        })
                    };
                });
        }

        public OutputDtoQueryOrder GetById(int orderId)
        {
            var orderFromDb = _orderRepository.GetById(orderId);
            var orderedItems = _orderedItemRepository.GetByOrderId(orderFromDb.Id);
            var userOrder = new UserOrder();
            userOrder.AddOrderedItems(orderedItems);

            return new OutputDtoQueryOrder
            {
                Id = orderFromDb.Id,
                IsPaid = orderFromDb.IsPaid,
                OrderedAt = orderFromDb.orderedAt,
                TotalPrice = userOrder.ComputeOrderPrice(),
                Ordered = new OutputDtoQueryOrder.User
                {
                    Id = orderFromDb.Orderer.Id,
                    Firstname = orderFromDb.Orderer.Firstname,
                    Lastname = orderFromDb.Orderer.Lastname,
                    Email = orderFromDb.Orderer.Email,
                },
                OrderedItems = userOrder.OrderedItems.Select(orderedItem => new OutputDtoQueryOrder.Item
                {
                    Id = orderedItem.ItemOrdered.Id,
                    Label = orderedItem.ItemOrdered.Label,
                    Price = orderedItem.ItemOrdered.Price,
                    ImageItem = orderedItem.ItemOrdered.ImageItem,
                    DescriptionItem = orderedItem.ItemOrdered.DescriptionItem,
                    Quantity = orderedItem.Quantity
                })
            };
        }

        public OutputDtoAddOrder Create(int userId)
        {
            var orderId = _orderRepository.Create(userId).Id;
            var orderFromDb = _orderRepository.GetById(orderId);

            return new OutputDtoAddOrder
            {
                Id = orderFromDb.Id,
                isPaid = orderFromDb.IsPaid,
                orderedAt = orderFromDb.orderedAt
            };
        }

        public bool Delete(int orderId)
        {
            return _orderRepository.Delete(orderId);
        }
    }
}