using System;
using Domain.Items;
using Domain.Users;

namespace Domain.BaggedItems
{
    public class BaggedItem : IBaggedItem
    {
        public int Id { get; set; }
        public DateTime AddedAt { get; set; }
        public int Quantity { get; set; }
        public string Size { get; set; }
        public IUser BagOwner { get; set; }
        public IItem AddedItem { get; set; }

        public BaggedItem()
        {
        }

        public override bool Equals(object obj)
        {
            if (obj is BaggedItem item)
            {
                return Id == item.Id;
            }

            return false;
        }
    }
}