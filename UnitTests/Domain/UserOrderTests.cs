using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Categories;
using Domain.Exceptions;
using Domain.Items;
using Domain.OrderedItems;
using Domain.Orders;
using Domain.Users;
using NUnit.Framework;

namespace UnitTests.Domain
{
    [TestFixture]
    public class UserOrderTests
    {
        public IList<IOrderedItem> CreateListOfOrdrItems()
        {
            IList<IOrderedItem> orderedItems = new List<IOrderedItem>();
            for (var i = 1; i < 11; i++)
            {
                orderedItems.Add(CreateOrderedItem(i));
            }

            return orderedItems;
        }

        public static IOrderedItem CreateOrderedItem(int i)
        {
            return CreateOrderedItem(i, i, i);
        }

        public static IOrderedItem CreateOrderedItem(int i, float price, int quantity)
        {
            return new OrderedItem
            {
                Id = i,
                Quantity = quantity,
                Size = i.ToString(),
                Order = new Order
                {
                    Id = i,
                    IsPaid = false,
                    orderedAt = DateTime.Now,
                    Orderer = new User {Id = i}
                },
                ItemOrdered = new Item
                {
                    Id = i,
                    Label = $"Item{i}",
                    Price = price,
                    ImageItem = i.ToString(),
                    DescriptionItem = i.ToString(),
                    Category = new Category {Id = i}
                }
            };
        }

        [Test]
        public void Add_OrderedItems_AreContained()
        {
            // arrange
            var userOrder = new UserOrder();
            var orderedItems = CreateListOfOrdrItems();

            // act
            userOrder.AddOrderedItems(orderedItems);

            // assert
            Assert.AreEqual(orderedItems, userOrder.OrderedItems);
        }

        [Test]
        public void Add_DuplicateOrderedItem_ThrowsDuplicateItemException()
        {
            // arrange
            var userOrder = new UserOrder();
            var orderedItem = CreateOrderedItem(1);

            // act
            userOrder.AddOrderedItem(orderedItem);

            // assert
            Assert.Throws<DuplicateItemException>(() => userOrder.AddOrderedItem(orderedItem));
        }

        [Test]
        public void Add_DuplicateItems_ContinueWithoutAdding()
        {
            // arrange
            var userOrder = new UserOrder();
            var orderedItemsWithDuplicate = new[] {CreateOrderedItem(1), CreateOrderedItem(1)};

            // act
            userOrder.AddOrderedItems(orderedItemsWithDuplicate);

            // assert
            Assert.AreEqual(1, userOrder.OrderedItems.Count);
        }

        [Test]
        [TestCase(new float[] {}, new int[] {}, 0)]
        [TestCase(new[] {10.0f, 15.99f, 99.99f}, new[] {2, 3, 1}, 167.96f)]
        [TestCase(new[] {10.0f, 5.0f, 20.0f, 99.0f}, new[] {1, 2, 3, 1}, 179)]
        [TestCase(new[] {19.99f, 29.99f}, new[] {1, 1},49.98f)]
        public void Add_OrderedItems_ComputeTotalPrice(float[] prices, int[] quantities, float expected)
        {
            // arrange
            var userOrder = new UserOrder();
            var orderedItems = 
                prices.Select((price, i) => CreateOrderedItem(i, price, quantities[i]));
            userOrder.AddOrderedItems(orderedItems);

            // act
            var totalPrice = userOrder.ComputeOrderPrice();

            // assert
            Assert.IsTrue(Math.Round(expected, 2).Equals(Math.Round(totalPrice, 2)));
        }
    }
}