using System;
using System.Collections.Generic;
using Application.Repositories;
using Application.Services.Reviews;
using Application.Services.Reviews.Dto;
using Domain.Categories;
using Domain.Items;
using Domain.reviews;
using Domain.Users;
using NSubstitute;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class ReviewServiceTest
    {
        public static IReview CreateReview(int i)
        {
            return new Review
            {
                Id = i,
                Stars = i,
                Title = i.ToString(),
                DescriptionReview = i.ToString(),
                Reviewer = new User
                {
                    Id = i,
                    Firstname = i.ToString(),
                    Lastname = i.ToString(),
                    Email = $"{i}@gmail.com",
                    Birthdate = DateTime.Now,
                    EncryptedPassword = i.ToString(),
                    Administrator = false,
                    Gender = 'M'
                },
                ItemReviewed = new Item
                {
                    Id = i,
                    Label = $"Item{i}",
                    Price = i,
                    ImageItem = i.ToString(),
                    DescriptionItem = i.ToString(),
                    Category = new Category {Id = i, Title = i.ToString()}
                }
            };
        }

        public static IEnumerable<IReview> CreateListOfReviews(int sizeOfList)
        {
            
            IList<IReview> reviews = new List<IReview>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                reviews.Add(CreateReview(i));
            }

            return reviews;
        }
        
        public static OutputDtoQueryReview CreateOutputDtoQueryReview(int i)
        {
            return new OutputDtoQueryReview
            {
                Id = i,
                Stars = i,
                Title = i.ToString(),
                DescriptionReview = i.ToString(),
                Reviewer = new OutputDtoQueryReview.User
                {
                    Id = i,
                    Firstname = i.ToString(),
                    Lastname = i.ToString(),
                },
                ItemReviewed = new OutputDtoQueryReview.Item
                {
                    Id = i,
                    Label = $"Item{i}",
                }
            };
        }

        public static IEnumerable<OutputDtoQueryReview> CreateListOfOutputDtoQueryReviews(int sizeOfList)
        {
            IList<OutputDtoQueryReview> reviews = new List<OutputDtoQueryReview>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                reviews.Add(CreateOutputDtoQueryReview(i));
            }

            return reviews;
        }

        public static InputDtoAddReview CreateInputDtoAddReview(int i)
        {
            return new InputDtoAddReview
            {
                Title = i.ToString(),
                Stars = i,
                DescriptionReview = i.ToString()
            };
        }

        public static InputDtoUpdateReview CreateInputDtoUpdateReview(int i)
        {
            return new InputDtoUpdateReview
            {
                Stars = i,
                DescriptionReview = i.ToString()
            };
        }

        [Test]
        [TestCase(0)]
        [TestCase(3)]
        [TestCase(14)]
        [TestCase(36)]
        public void Query_ReturnsListOfOutputDtoQueryReview(int nbOfReviews)
        {
            // ARRANGE //
            var reviewRep = Substitute.For<IReviewRepository>();
            reviewRep.Query().Returns(CreateListOfReviews(nbOfReviews));

            var reviewService = new ReviewService(reviewRep);
            var expected = CreateListOfOutputDtoQueryReviews(nbOfReviews);

            // ACT //
            var output = reviewService.Query();

            // ASSERT //
            Assert.AreEqual(expected, output);
        }
        
        [Test]
        public void GetById_SingleNumber_ReturnsSingleOutputDtoQueryReview()
        {
            // ARRANGE //
            var reviewRep = Substitute.For<IReviewRepository>();
            reviewRep.GetById(1).Returns(CreateReview(1));
            
            var reviewService = new ReviewService(reviewRep);
            var expected = CreateOutputDtoQueryReview(1);

            // ACT //
            var output = reviewService.GetById(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(0)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(16)]
        public void GetByItemId_SingleNumber_ReturnsListOfOutputDtoQueryReview(int nbOfReviews)
        {
            // ARRANGE //
            var reviewRep = Substitute.For<IReviewRepository>();
            reviewRep.GetByItemId(1).Returns(CreateListOfReviews(nbOfReviews));

            var reviewService = new ReviewService(reviewRep);
            var expected = CreateListOfOutputDtoQueryReviews(nbOfReviews);
            
            // ACT //
            var output = reviewService.GetByItemId(1);

            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        public void Create_UserIdAndItemIdAndInputDtoAddReview_ReturnsSingleOutputDtoQueryReview()
        {
            
            // ARRANGE //
            var reviewRep = Substitute.For<IReviewRepository>();
            reviewRep.GetById(1).Returns(CreateReview(1));
            reviewRep.Create(1, 1, Arg.Any<IReview>()).Returns(CreateReview(1));

            var reviewService = new ReviewService(reviewRep);
            var expected = CreateOutputDtoQueryReview(1);
            
            // ACT //
            var output = reviewService.Create(1, 1, CreateInputDtoAddReview(1));
            
            // ASSERT //
            Assert.AreEqual(expected, output);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Delete_SingleNumber_ReturnsIsDeleted(bool isDeletedFromRepo)
        {
            // ARRANGE //
            var reviewRep = Substitute.For<IReviewRepository>();
            reviewRep.Delete(1).Returns(isDeletedFromRepo);

            var reviewService = new ReviewService(reviewRep);
            
            // ACT //
            var output = reviewService.Delete(1);

            // ASSERT //
            Assert.AreEqual(isDeletedFromRepo, output);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void Update_ReviewIdAndInputDtoUpdateReview_ReturnsIsUpdated(bool isUpdatedFromRepo)
        {
            
            // ARRANGE //
            var reviewRep = Substitute.For<IReviewRepository>();
            reviewRep.Update(1, Arg.Any<IReview>()).Returns(isUpdatedFromRepo);

            var reviewService = new ReviewService(reviewRep);
            
            // ACT //
            var output = reviewService.Update(1, CreateInputDtoUpdateReview(1));

            // ASSERT //
            Assert.AreEqual(isUpdatedFromRepo, output);
        }
    }
}