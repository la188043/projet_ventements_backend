using Domain;
using Domain.Shared;

namespace Domains.SubCategories
{
    public interface ISubCategory:IEntity
    {
        public string Title { get; set; }
        public int CategoryId { get; set; }
        
        
     
    }
}