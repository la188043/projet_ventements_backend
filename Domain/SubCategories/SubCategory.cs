using System;
 using System.Data.SqlClient;
 using Domain;
 

 namespace Domains.SubCategories
{
    public class SubCategory:ISubCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int CategoryId { get; set; }

        public SubCategory()
        {
            
        }
        
        public SubCategory(int id, string title, int categoryId)
        {
            Id = id;
            Title = title;
            CategoryId = categoryId;
        }
        
        
        
    }
}