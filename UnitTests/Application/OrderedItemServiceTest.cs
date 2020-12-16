﻿using System.Collections.Generic;
using Application.Services.OrderedItems.Dto;
using Domain.Items;
using Domain.OrderedItems;
using Domain.Orders;
using NUnit.Framework;

namespace UnitTests.Application
{
    [TestFixture]
    public class OrderedItemServiceTest
    {
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

        public static OutputDtoQueryOrderedItem CreateOutputDtoQueryOrderedItem(int i)
        {
            return new OutputDtoQueryOrderedItem
            {
                Id = i,
                ItemOrder = new OutputDtoQueryOrderedItem.Order {Id = i},
                ItemOrdered = new OutputDtoQueryOrderedItem.Item
                {
                    Id = i,
                    Label = $"Item{i}",
                    Price = i,
                    ImageItem = i.ToString(),
                    DescriptionItem = i.ToString(),
                    Quantity = i,
                    Size = i.ToString()
                }
            };
        }

        public static IEnumerable<OutputDtoQueryOrderedItem> CreateListOfOutputDtoQueryOrderedItems(int sizeOfList)
        {
            IList<OutputDtoQueryOrderedItem> items = new List<OutputDtoQueryOrderedItem>();
            for (var i = 1; i <= sizeOfList; i++)
            {
                items.Add(CreateOutputDtoQueryOrderedItem(i));
            }

            return items;
        }

        public static InputDtoAddOrderedItem CreateInputDtoAddOrderedItem(int i)
        {
            return new InputDtoAddOrderedItem { Quantity = i, Size = i.ToString() };
        }

        public static InputDtoAddOrderedItems CreateInputDtoAddOrderedItems(int nbOfOrderedItems)
        {
            IList<InputDtoAddOrderedItems.OrderedItem> items = new List<InputDtoAddOrderedItems.OrderedItem>();
            for (var i = 1; i <= nbOfOrderedItems; i++)
            {
                items.Add(new InputDtoAddOrderedItems.OrderedItem
                {
                    ItemId = i,
                    Quantity = i,
                    Size = i.ToString()
                });
            }

            return new InputDtoAddOrderedItems {OrderedItems = items};
        }

        public static InputDtoUpdateOrderedItem CreateInputDtoUpdateOrderedItem(int i)
        {
            return new InputDtoUpdateOrderedItem {Quantity = i};
        }
    }
}