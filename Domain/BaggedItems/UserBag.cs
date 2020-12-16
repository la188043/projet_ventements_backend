using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Exceptions;
using Domain.Users;

namespace Domain.BaggedItems
{
    public class UserBag : IUserBag
    {
        public IUser BagOwner { get; set; }
        public IList<IBaggedItem> Items { get; }

        public UserBag()
        {
            Items = new List<IBaggedItem>();
        }

        public void AddItem(IBaggedItem item)
        {
            if (Items.Contains(item)) throw new DuplicateItemException();
            Items.Add(item);
        }

        public void AddItems(IEnumerable<IBaggedItem> items)
        {
            foreach (var item in items)
            {
                try
                {
                    AddItem(item);
                }
                catch (DuplicateItemException e) {}
            }
        }

        public float ComputeTotalPrice()
        {
            return Items.Select(item => item.AddedItem.Price * item.Quantity).Sum();
        }
    }
}