using System.Collections.Generic;
using System.Linq;

namespace Application.Services.Categories.Dto
{
    public class OutputDtoQueryCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IEnumerable<Category> SubCategories { get; set; }

        public class Category
        {
            public int Id { get; set; }
            public string Title { get; set; }

            private bool Equals(Category other)
            {
                return Id == other.Id && Title == other.Title;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != this.GetType()) return false;
                return Equals((Category) obj);
            }
        }

        private bool Equals(OutputDtoQueryCategory other)
        {
            return Id == other.Id && Title == other.Title && ListEquals(SubCategories, other.SubCategories);
        }

        private bool ListEquals(IEnumerable<Category> subCategories, IEnumerable<Category> otherSubCategories)
        {
            var list = subCategories.ToList();
            var other = otherSubCategories.ToList();

            return list.Count == other.Count && list.All(cat => other.Contains(cat));
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoQueryCategory) obj);
        }
    }
}