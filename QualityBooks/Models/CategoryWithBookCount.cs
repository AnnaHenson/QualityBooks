namespace QualityBooks.Models
{
    public class CategoryWithBookCount
    {
        public string CategoryName { get; }
        public int BookCount { get; }

        public CategoryWithBookCount(string categoryName, int bookCount)
        {
            CategoryName = categoryName;
            BookCount = bookCount;
        }
    }
}