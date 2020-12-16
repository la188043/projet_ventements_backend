using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Categories;
using Application.Services.Categories.Dto;
using Domain.Categories;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class CategoryServiceTest
    {
        public static ICategory CreateParentCategory(int i)
        {
            return new Category
            {
                Id = i,
                Title = i.ToString(),
                ParentCategory = null
            };
        }

        public static ICategory CreateSubcategory(ICategory parent, int i)
        {
            return new Category
            {
                Id = i,
                Title = i.ToString(),
                ParentCategory = parent
            };
        }

        public static IEnumerable<ICategory> CreateListOfCategories(int offset, int sizeOfList)
        {
            IList<ICategory> categories = new List<ICategory>();
            for (var i = offset; i < offset + sizeOfList; i++)
            {
                categories.Add(CreateParentCategory(i));
            }

            return categories;
        }

        public static IEnumerable<ICategory> CreateListOfSubs(ICategory parent, int sizeOfList)
        {
            IList<ICategory> categories = new List<ICategory>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                categories.Add(CreateSubcategory(parent, i));
            }

            return categories;
        }
        
        // DTOs
        public static InputDtoAddCategory CreateInputDtoAddCategory(int i)
        {
            return new InputDtoAddCategory {Title = i.ToString()};
        }

        public static OutputDtoAddCategory CreateOupOutputDtoAddCategory(int i)
        {
            return new OutputDtoAddCategory
            {
                Id = i,
                Title = i.ToString()
            };
        }

        public static OutputDtoQueryCategory CreateOutputDtoQueryCategory(int i, int sizeOfSubs)
        {
            var subs = new OutputDtoQueryCategory.Category[sizeOfSubs];
            for (var j = 1; j <= sizeOfSubs; j++)
            {
                subs[j - 1] = new OutputDtoQueryCategory.Category {Id = j, Title = j.ToString()};
            }
            
            return new OutputDtoQueryCategory
            {
                Id = i,
                Title = i.ToString(),
                SubCategories = subs
            };
        }

        public static IEnumerable<OutputDtoQueryCategory> CreateListOfOutputDtoQueryCategories(int sizeOfList)
        {
            IList<OutputDtoQueryCategory> categories = new List<OutputDtoQueryCategory>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                categories.Add(CreateOutputDtoQueryCategory(i, 0));
            }

            return categories;
        }
        
        [Test]
        public void Query_ReturnsParentCategoriesListWithSubcategoriesList()
        {
            // ARRANGE //
            var categoryRep = Substitute.For<ICategoryRepository>();
            categoryRep.Query().Returns(new[] {CreateParentCategory(1)});
            categoryRep.GetByCategoryId(Arg.Any<int>()).Returns(CreateListOfSubs(CreateParentCategory(1), 2));
            
            var categoryService = new CategoryService(categoryRep);

            var expected = new[] {CreateOutputDtoQueryCategory(1, 2)};
            
            // ACT //
            var output = categoryService.Query();
            
            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(4)]
        [TestCase(0)]
        public void GetById_ReturnsSingleParentCategoryWithSubcategoriesList(int numberOfSubs)
        {
            // ARRANGE //
            var categoryRep = Substitute.For<ICategoryRepository>();
            categoryRep.GetById(1).Returns(CreateParentCategory(1));
            categoryRep.GetByCategoryId(1).Returns(CreateListOfSubs(CreateParentCategory(1), numberOfSubs));
            
            var categoryService = new CategoryService(categoryRep);

            var expected = CreateOutputDtoQueryCategory(1, numberOfSubs);
            
            // ACT //
            var output = categoryService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(6)]
        public void GetByCategoryId_SingleNumber_ReturnsSubcategoriesOfParent(int nbOfSubs)
        {
            // ARRANGE //
            var categoryRep = Substitute.For<ICategoryRepository>();
            categoryRep.GetByCategoryId(1).Returns(CreateListOfSubs(CreateParentCategory(1), nbOfSubs));
            
            var categoryService = new CategoryService(categoryRep);
            var expected = CreateListOfOutputDtoQueryCategories(nbOfSubs);

            // ACT //
            var output = categoryService.GetByCategoryId(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void CreateCategory_SingleInputAddCategory_ReturnsSingleOutputDtoAddCategory()
        {
            // ARRANGE //
            var categoryRep = Substitute.For<ICategoryRepository>();
            categoryRep.CreateCategory(Arg.Any<ICategory>()).Returns(CreateParentCategory(1));

            var categoryService = new CategoryService(categoryRep);
            var expected = CreateOupOutputDtoAddCategory(1);

            // ACT //
            var output = categoryService.CreateCategory(CreateInputDtoAddCategory(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void CreateSubCategory_SingleInputAddCategory_ReturnsSingleOutputDtoAddCategory()
        {
            // ARRANGE //
            var categoryRep = Substitute.For<ICategoryRepository>();
            categoryRep.CreateSubCategory(1, Arg.Any<ICategory>()).Returns(CreateParentCategory(1));
            
            var categoryService = new CategoryService(categoryRep);
            var expected = CreateOupOutputDtoAddCategory(1);

            // ACT //
            var output = categoryService.CreateSubCategory(1, CreateInputDtoAddCategory(1));

            // ASSERT //
            Assert.AreEqual(expected, output);
        }
    }
}