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

        public static OutputDtoQueryCategory CreateOutputDtoQueryCategory(int i)
        {
            return new OutputDtoQueryCategory
            {
                Id = i,
                Title = i.ToString(),
                SubCategories = new[] {new OutputDtoQueryCategory.Category {Id = 1, Title = "1"},new OutputDtoQueryCategory.Category {Id = 2, Title = "2"}}
            };
        }
        
        [Test]
        public void Query_ReturnsParentCategoriesListWithSubcategoriesList()
        {
            // ARRANGE //
            var categoryRep = Substitute.For<ICategoryRepository>();
            categoryRep.Query().Returns(new[] {CreateParentCategory(1)});
            categoryRep.GetByCategoryId(Arg.Any<int>()).Returns(CreateListOfSubs(CreateParentCategory(1), 2));
            
            var categoryService = new CategoryService(categoryRep);

            var expected = new[] {CreateOutputDtoQueryCategory(1)};
            
            // ACT //
            var output = categoryService.Query();
            
            // ASSERT //
            Assert.AreEqual(expected, output);
        }
    }
}