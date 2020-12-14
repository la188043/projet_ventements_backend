using System;
using System.Collections;
using System.Collections.Generic;
using Domain.Categories;
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
                orderedItems.Add(new OrderedItem
                {
                    Id = i,
                    Quantity = i,
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
                        Price = i,
                        ImageItem = i.ToString(),
                        DescriptionItem = i.ToString(),
                        Category = new Category {Id = 1}
                    }
                });
            }

            return orderedItems;
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
    }
}