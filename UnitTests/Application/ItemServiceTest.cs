using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Items;
using Application.Services.Items.Dto;
using Domain.Categories;
using Domain.Items;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class ItemServiceTest
    {
        public static IItem CreateItem(int i)
        {
            return new Item
            {
                Id = i,
                Label = $"Item{i}",
                Price = i,
                ImageItem = i.ToString(),
                DescriptionItem = i.ToString(),
                Category = new Category {Id = i, Title = i.ToString()}
            };
        }

        public static ICategory CreateCategory(int i)
        {
            return new Category {Id = i, Title = i.ToString()};
        }

        public static IEnumerable<IItem> CreateListOfItems(int sizeOfList)
        {
            IList<IItem> items = new List<IItem>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                items.Add(CreateItem(i));
            }

            return items;
        }

        public static OutputDtoQueryItem CreateOutputDtoQueryItem(int i)
        {
            return new OutputDtoQueryItem
            {
                Id = i,
                Label = $"Item{i}",
                Price = i,
                ImageItem = i.ToString(),
                DescriptionItem = i.ToString(),
                ItemCategory = new OutputDtoQueryItem.Category {Id = i, Title = i.ToString()}
            };
        }

        public static IEnumerable<OutputDtoQueryItem> CreateListOfOutputDtoQueryItems(int size)
        {
            IList<OutputDtoQueryItem> items = new List<OutputDtoQueryItem>();
            for (var i = 1; i <= size; i++)
            {
                items.Add(CreateOutputDtoQueryItem(i));
            }

            return items;
        }

        public static InputDtoAddItem CreateInputDtoAddItem(int i)
        {
            return new InputDtoAddItem
            {
                Label = $"Item{i}",
                Price = i,
                ImageItem = i.ToString(),
                DescriptionItem = i.ToString()
            };
        }

        public static InputDtoUpdateItem CreateInputDtoUpdateItem(int i)
        {
            return new InputDtoUpdateItem
            {
                Label = $"Item{i}",
                Price = i,
                ImageItem = i.ToString(),
                DescriptionItem = i.ToString()
            };
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(7)]
        [TestCase(13)]
        public void Query_ReturnsListOfAllItems(int numberOfItems)
        {
            // ARRANGE //
            var itemRep = Substitute.For<IItemRepository>();
            var categoryRep = Substitute.For<ICategoryRepository>();

            itemRep.Query().Returns(CreateListOfItems(numberOfItems));

            var itemService = new ItemService(itemRep, categoryRep);
            var expected = CreateListOfOutputDtoQueryItems(numberOfItems);

            // ACT //
            var output = itemService.Query();

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void GetById_SingleNumber_ReturnsSingleOutputDtoQueryItem()
        {
            // ARRANGE //
            var itemRep = Substitute.For<IItemRepository>();
            var categoryRep = Substitute.For<ICategoryRepository>();

            itemRep.GetById(1).Returns(CreateItem(1));
            
            var itemService = new ItemService(itemRep, categoryRep);
            var expected = CreateOutputDtoQueryItem(1);

            // ACT //
            var output = itemService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(4)]
        [TestCase(14)]
        public void GetByCategoryId_SingleNumber_ReturnsListOfOutputDtoQueryItem(int numberOfItems)
        {
            // ARRANGE //
            var itemRep = Substitute.For<IItemRepository>();
            var categoryRep = Substitute.For<ICategoryRepository>();

            itemRep.GetByCategoryId(1).Returns(CreateListOfItems(numberOfItems));
            
            var itemService = new ItemService(itemRep, categoryRep);
            var expected = CreateListOfOutputDtoQueryItems(numberOfItems);

            // ACT //
            var output = itemService.GetByCategoryId(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Create_CategoryIdAndInputAddItem_ReturnsSingleOutputDtoQueryItem()
        {
            // ARRANGE //
            var itemRep = Substitute.For<IItemRepository>();
            var categoryRep = Substitute.For<ICategoryRepository>();

            itemRep.Create(1, Arg.Any<IItem>()).Returns(CreateItem(1));
            categoryRep.GetById(1).Returns(CreateCategory(1));
            
            var itemService = new ItemService(itemRep, categoryRep);
            var expected = CreateOutputDtoQueryItem(1);

            // ACT //
            var output = itemService.Create(1, CreateInputDtoAddItem(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Update_ItemIdAndInputDtoUpdateItem_ReturnsIsUpdated(bool isModifiedFromRepo)
        {
            // ARRANGE //
            var itemRep = Substitute.For<IItemRepository>();
            var categoryRep = Substitute.For<ICategoryRepository>();

            itemRep.Update(1, Arg.Any<IItem>()).Returns(isModifiedFromRepo);
            
            var itemService = new ItemService(itemRep, categoryRep);
            
            // ACT //
            var output = itemService.Update(1, CreateInputDtoUpdateItem(1));

            // ASSERT // 
            Assert.AreEqual(isModifiedFromRepo, output);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Delete_ItemId_ReturnsIsDeleted(bool isDeletedFromRepo)
        {
            // ARRANGE //
            var itemRep = Substitute.For<IItemRepository>();
            var categoryRep = Substitute.For<ICategoryRepository>();

            itemRep.Delete(1).Returns(isDeletedFromRepo);
            
            var itemService = new ItemService(itemRep, categoryRep);
            
            // ACT //
            var output = itemService.Delete(1);
            
            // ARRANGE //
            Assert.AreEqual(isDeletedFromRepo, output);
        }
    }
}