namespace QualityBooks.Models
{
    public class CategoryWithBookCount
    {
        public string CategoryName { get; }
        public int BookCount { get; }
        public int CategoryId { get; }

        public CategoryWithBookCount(int categoryId, string categoryName, int bookCount)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            BookCount = bookCount;
        }
    }
}