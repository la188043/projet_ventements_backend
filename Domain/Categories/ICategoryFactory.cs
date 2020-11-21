namespace Domain.Categories
{
    public interface ICategoryFactory
    {
        ICategory CreateFromTitle(string title);
        
    }
}