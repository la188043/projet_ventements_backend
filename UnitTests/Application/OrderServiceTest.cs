using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Orders;
using Application.Services.Orders.Dto;
using Domain.Items;
using Domain.OrderedItems;
using Domain.Orders;
using Domain.Users;
using NSubstitute;
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
                OrderedAt = DateTime.Now,
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

        public static IOrderedItem CreateOrderedItem(int i)
        {
            return new OrderedItem
            {
                Id = i,
                Quantity = i,
                Size = i.ToString(),
                ItemOrdered = new Item
                {
                    Id = i,
                    Label = $"Item{i}",
                    Price = i,
                    ImageItem = i.ToString(),
                    DescriptionItem = i.ToString(),
                },
                Order = new Order
                {
                    Id = i
                }
            };
        }

        public static IEnumerable<IOrderedItem> CreateListOfOrderedItems(int sizeOfList)
        {
            IList<IOrderedItem> items = new List<IOrderedItem>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                items.Add(CreateOrderedItem(i));
            }

            return items;
        }

        public static OutputDtoAddOrder CreateOutputDtoAddOrder(int i)
        {
            return new OutputDtoAddOrder
            {
                Id = i,
                IsPaid = false,
                OrderedAt = DateTime.Now
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

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 0)]
        [TestCase(6, 3)]
        [TestCase(13, 19)]
        public void GetByUserId_SingleNumber_ReturnsListOfOutputDtoQueryOrder(int nbOfOrders, int nbOfOrderedItems)
        {
            // ARRANGE //
            var orderRep = Substitute.For<IOrderRepository>();
            var orderedItemRep = Substitute.For<IOrderedItemRepository>();

            orderedItemRep
                .GetByOrderId(Arg.Any<int>())
                .Returns(CreateListOfOrderedItems(nbOfOrderedItems));

            orderRep.GetByUserId(Arg.Any<int>())
                .Returns(CreateListOfOrders(nbOfOrders));
            
            var orderService = new OrderService(orderRep, orderedItemRep);
            var expected = 
                CreateListOfOutputDtoQueryOrder(nbOfOrders, nbOfOrderedItems);

            // ACT //
            var output = orderService.GetByUserId(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(7)]
        [TestCase(15)]
        public void GetById_SingleNumber_ReturnsOuputDtoQuery(int nbOfOrderedItems)
        {
            // ARRANGE //
            var orderRep = Substitute.For<IOrderRepository>();
            var orderedItemRep = Substitute.For<IOrderedItemRepository>();

            orderedItemRep
                .GetByOrderId(Arg.Any<int>())
                .Returns(CreateListOfOrderedItems(nbOfOrderedItems));

            orderRep
                .GetById(Arg.Any<int>())
                .Returns(CreateOrder(1));
            
            var orderService = new OrderService(orderRep, orderedItemRep);
            var expected = CreateOutputDtoQueryOrder(1, nbOfOrderedItems);

            // ACT //
            var output = orderService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Create_SingleNumber_ReturnsOutputDtoAddOrder()
        {
            // ARRANGE //
            var orderRep = Substitute.For<IOrderRepository>();
            var orderedItemRep = Substitute.For<IOrderedItemRepository>();

            orderRep
                .GetById(Arg.Any<int>())
                .Returns(args => CreateOrder((int) args[0]));

            orderRep
                .Create(Arg.Any<int>())
                .Returns(CreateOrder(1));
            
            var orderService = new OrderService(orderRep, orderedItemRep);
            var expected = CreateOutputDtoAddOrder(1);

            // ACT //
            var output = orderService.Create(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Delete_SingleNumber_ReturnsIsDeleted(bool isDeletedFromRepo)
        {
            // ARRANGE //
            var orderRep = Substitute.For<IOrderRepository>();
            var orderedItemRep = Substitute.For<IOrderedItemRepository>();

            orderRep.Delete(Arg.Any<int>()).Returns(isDeletedFromRepo);
            
            var orderService = new OrderService(orderRep, orderedItemRep);

            // ACT //
            var output = orderService.Delete(1);

            // ASSERT //
            Assert.AreEqual(isDeletedFromRepo, output);
        }
    }
}