namespace Application.Services.Categories.Dto
{
    public class OutputDtoAddCategory
    {
        public int Id { get; set; }
        public string Title { get; set; }

        private bool Equals(OutputDtoAddCategory other)
        {
            return Id == other.Id && Title == other.Title;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((OutputDtoAddCategory) obj);
        }
    }
}