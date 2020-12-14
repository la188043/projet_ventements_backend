using System;
using System.Linq;
using Domain.BaggedItems;
using Domain.Items;
using Domain.Users;
using NUnit.Framework;

namespace UnitTests.Domain
{
    [TestFixture]
    public class UserBagTests
    {
        public static BaggedItem CreateRandomBaggedItem()
        {
            var rnd = new Random();
            var randomNumber = rnd.Next(1, 100);
            return new BaggedItem
            {
                Id = randomNumber,
                Quantity = randomNumber,
                Size = randomNumber.ToString(),
                AddedAt = DateTime.Now,
                BagOwner = new User { Id = randomNumber },
                AddedItem = new Item { Id = randomNumber }
            };
        }
        
        [Test]
        public void Add_Items_AreContained()
        {
            // arrange
            var userBag = new UserBag();
            var baggedItem = CreateRandomBaggedItem();
            
            // act
            userBag.AddItem(baggedItem);
            
            // assert
            Assert.Contains(baggedItem, userBag.Items.ToList());
        }

        [Test]
        public void Add_MultipleItems()
        {
            // arrange           
            var userBag = new UserBag();
            var baggedItems = new[] { new BaggedItem {Id = 1}, new BaggedItem {Id = 2}, new BaggedItem {Id = 3} };
            
            // act
            userBag.AddItems(baggedItems);
            
            // assert
            Assert.AreEqual(baggedItems.Length, userBag.Items.Count);
        }
    }
}