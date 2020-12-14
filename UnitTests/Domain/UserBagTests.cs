using System;
using System.Collections.Generic;
using System.Linq;
using Domain.BaggedItems;
using Domain.Exceptions;
using Domain.Items;
using Domain.Users;
using NUnit.Framework;

namespace UnitTests.Domain
{
    [TestFixture]
    public class UserBagTests
    {
        public static IList<IBaggedItem> CreateListOfBaggedItems()
        {
            IList<IBaggedItem> items = new List<IBaggedItem>();
            for (var i = 1; i < 11; i++)
            {
                items.Add(new BaggedItem
                {
                    Id = i,
                    Quantity = i,
                    Size = i.ToString(),
                    AddedAt = DateTime.Now,
                    BagOwner = new User { Id = i },
                    AddedItem = new Item { Id = i }
                });
            }

            return items;
        }
        
        [Test]
        public void Add_Items_AreContained()
        {
            // arrange
            var userBag = new UserBag();
            var baggedItems = CreateListOfBaggedItems();
            
            // act
            userBag.AddItems(baggedItems);
            
            // assert
            Assert.AreEqual(baggedItems, userBag.Items);
        }

        [Test]
        public void Add_DuplicateItem_ThrowsDuplicateItemException()
        {
            // arrange
            var userBag = new UserBag();
            var baggedItem = new BaggedItem {Id = 1};
            
            // act
            userBag.AddItem(baggedItem);
            
            // assert
            Assert.Throws<DuplicateItemException>(() => userBag.AddItem(baggedItem));
        }

        [Test]
        public void Add_DuplicateItems_ContinueWithoutAdding()
        {
            // arrange
            var userBag = new UserBag();
            var baggedItems = new[] {new BaggedItem {Id = 1}, new BaggedItem {Id = 1}};

            // act
            userBag.AddItems(baggedItems);

            // assert
            Assert.AreEqual(1, userBag.Items.Count);
        }

        [Test]
        [TestCase(new[] {19.99f, 29.99f}, new[] {1, 1},49.98f)]
        [TestCase(new[] {10.0f, 5.0f, 20.0f, 99.0f}, new[] {1, 2, 3, 1}, 179)]
        [TestCase(new float[] {}, new int[] {}, 0)]
        public void Add_Items_ComputeTotalPrice(float[] prices, int[] quantities, float expected)
        {
            // arrange
            var userBag = new UserBag();
            var baggedItems = prices.Select((price, i) => new BaggedItem
            {
                Id = i,
                Quantity = quantities[i],
                AddedItem = new Item {Price = price}
            });
            userBag.AddItems(baggedItems);

            // act
            var totalPrice = userBag.ComputeTotalPrice();
            
            // assert
            Assert.AreEqual(totalPrice, expected);
        }
    }
}