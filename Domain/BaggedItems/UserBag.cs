using System.Collections.Generic;
using System.Linq;
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
            Items.Add(item);
        }

        public void AddItems(IEnumerable<IBaggedItem> items)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }

        public float ComputeTotalPrice()
        {
            return Items.Select(item => item.AddedItem.Price).Sum();
        }
    }
}