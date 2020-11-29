using System;
using Domain.Items;
using Domain.Shared;
using Domain.Users;

namespace Domain.BaggedItems
{
    public interface IBaggedItem : IEntity
    {
        DateTime AddedAt { get; set; }
        int Quantity { get; set; }
        string Size { get; set; }
        IUser BagOwner { get; set; }       
        IItem AddedItem { get; set; }
    }
}