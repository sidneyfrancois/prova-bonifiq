namespace ProvaPub.Models
{
    public abstract class PageList
    {
        public int TotalCount { get; set; } = 10;
        public bool HasNext { get; set; } = false;
    }
}
