using System.Collections.Generic;
using Application.Repositories;
using Domain.BaggedItems;
using Infrastructure.SqlServer.Factories;

namespace Infrastructure.SqlServer.BaggedItems
{
    public class BaggedItemRepository : IBaggedItemRepository
    {
        private readonly IInstanceFromReader<IBaggedItem> _factory = new BaggedItemFactory();
        
        public IEnumerable<IBaggedItem> QueryUserBag(int userId)
        {
            throw new System.NotImplementedException();
        }

        public IBaggedItem AddToBag(int userId, int itemId, IBaggedItem baggedItem)
        {
            throw new System.NotImplementedException();
        }

        public int EmptyBag(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteItem(int baggedItemId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateQuantity(int baggedItemId, IBaggedItem baggedItem)
        {
            throw new System.NotImplementedException();
        }
    }
}