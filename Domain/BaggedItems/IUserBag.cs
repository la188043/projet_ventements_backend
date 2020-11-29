using System.Collections.Generic;
using Domain.Items;
using Domain.Users;

namespace Domain.BaggedItems
{
    public interface IUserBag
    {
        IUser BagOwner { get; set; }
        void AddItem(IBaggedItem item);
        void AddItems(IEnumerable<IBaggedItem> items);
        float ComputeTotalPrice();
    }
}