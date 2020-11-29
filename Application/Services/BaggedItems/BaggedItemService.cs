using System.Linq;
using System.Reflection.Emit;
using Application.Repositories;
using Application.Services.BaggedItems.Dto;
using Domain.BaggedItems;

namespace Application.Services.BaggedItems
{
    public class BaggedItemService : IBaggedItemService
    {
        private readonly IBaggedItemRepository _baggedItemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IItemRepository _itemRepository;

        public BaggedItemService(IBaggedItemRepository baggedItemRepository, IUserRepository userRepository,
            IItemRepository itemRepository)
        {
            _baggedItemRepository = baggedItemRepository;
            _userRepository = userRepository;
            _itemRepository = itemRepository;
        }

        public OutputDtoQueryUserBaggedItem GetByUserId(int userId)
        {
            var baggedItems = _baggedItemRepository.GetByUserId(userId);
            var userBag = new UserBag();

            userBag.AddItems(baggedItems);

            var bagOwner = _userRepository.GetById(userId);

            var dtoBaggedItems = userBag.Items
                .Select(baggedItem => new OutputDtoQueryUserBaggedItem.BaggedItem
                {
                    Id = baggedItem.Id,
                    AddedAt = baggedItem.AddedAt,
                    Quantity = baggedItem.Quantity,
                    Size = baggedItem.Size,

                    BagItem = new OutputDtoQueryUserBaggedItem.BaggedItem.Item
                    {
                        Id = baggedItem.AddedItem.Id,
                        Label = baggedItem.AddedItem.Label,
                        Price = baggedItem.AddedItem.Price,
                        ImageItem = baggedItem.AddedItem.ImageItem,
                        DescriptionItem = baggedItem.AddedItem.DescriptionItem
                    }
                });

            return new OutputDtoQueryUserBaggedItem
            {
                BagOwner = new OutputDtoQueryUserBaggedItem.User
                {
                    Id = bagOwner.Id,
                    Firstname = bagOwner.Firstname,
                    Lastname = bagOwner.Lastname
                },
                TotalPrice = userBag.ComputeTotalPrice(),
                Items = dtoBaggedItems
            };
        }

        public OutputDtoAddBaggedItem AddToBag(int userId, int itemId, InputDtoAddItemToBag inputDtoAddItemToBag)
        {
            var baggedItemFromDto = new BaggedItem
            {
                Quantity = inputDtoAddItemToBag.Quantity,
                Size = inputDtoAddItemToBag.Size
            };

            var baggedItemFromDb = _baggedItemRepository.AddToBag(userId, itemId, baggedItemFromDto);
            var item = _itemRepository.GetById(itemId);
            
            return new OutputDtoAddBaggedItem
            {
                Id = baggedItemFromDb.Id,
                AddedAt = baggedItemFromDb.AddedAt,
                Quantity = baggedItemFromDb.Quantity,
                Size = baggedItemFromDb.Size,
                BagItem = new OutputDtoAddBaggedItem.Item
                {
                    Id = item.Id,
                    Label = item.Label,
                    Price = item.Price,
                    ImageItem = item.ImageItem,
                    DescriptionItem = item.DescriptionItem
                }
            };
        }

        public int EmptyBag(int userId)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteItem(int baggedItemId)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateQuantity(int baggedItemId, InputDtoUpdateBaggedItem inputDtoUpdateBaggedItem)
        {
            throw new System.NotImplementedException();
        }
    }
}