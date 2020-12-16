using System;
using System.Collections.Generic;
using System.Linq;
using Application.Services.Addresses.Dto;
using Application.Services.Orders.Dto;
using Domain.Orders;
using Domain.Users;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class OrderServiceTest
    {
        public static IOrder CreateOrder(int i)
        {
            return new Order
            {
                Id = i,
                IsPaid = false,
                orderedAt = DateTime.Now,
                Orderer = new User
                {
                    Id = i,
                    Firstname = i.ToString(),
                    Lastname = i.ToString(),
                    Email = $"{i}@gmail.com",
                    Birthdate = DateTime.Now,
                    EncryptedPassword = i.ToString(),
                    Administrator = false,
                    Gender = 'M',
                }
            };
        }

        public static IEnumerable<IOrder> CreateListOfOrders(int sizeOfList)
        {
            IList<IOrder> orders = new List<IOrder>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                orders.Add(CreateOrder(i));
            }

            return orders;
        }

        public static OutputDtoAddOrder CreateOutputDtoAddOrder(int i)
        {
            return new OutputDtoAddOrder
            {
                Id = i,
                isPaid = false,
                orderedAt = DateTime.Now
            };
        }

        public static OutputDtoQueryOrder.Item CreateOutputDtoQueryOrderItem(int i)
        {
            return new OutputDtoQueryOrder.Item
            {
                Id = i,
                ItemId = i,
                Label = $"Item{i}",
                Price = i,
                ImageItem = i.ToString(),
                DescriptionItem = i.ToString(),
                Quantity = i,
                Size = i.ToString()
            };
        }

        public static OutputDtoQueryOrder CreateOutputDtoQueryOrder(int i, int nbOfOrderedItems)
        {
            IList<OutputDtoQueryOrder.Item> items = new List<OutputDtoQueryOrder.Item>();
            for (var j = 1; j <= nbOfOrderedItems; j++)
                items.Add(CreateOutputDtoQueryOrderItem(j));

            var totalPrice = items.Select(item => item.Price * item.Quantity).Sum();
            
            return new OutputDtoQueryOrder
            {
                Id = i,
                IsPaid = false,
                OrderedAt = DateTime.Now,
                TotalPrice = totalPrice,
                Ordered = new OutputDtoQueryOrder.User
                {
                    Id = i,
                    Firstname = i.ToString(),
                    Lastname = i.ToString(),
                    Email = $"{i}@gmail.com"
                },
                OrderedItems = items
            };
        }

        public static IEnumerable<OutputDtoQueryOrder> CreateListOfOutputDtoQueryOrder(int sizeOfList,
            int nbOfOrderedItems)
        {
            IList<OutputDtoQueryOrder> orders = new List<OutputDtoQueryOrder>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                orders.Add(CreateOutputDtoQueryOrder(i, nbOfOrderedItems));
            }

            return orders;
        }
    }
}