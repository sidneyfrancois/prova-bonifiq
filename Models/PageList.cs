namespace ProvaPub.Models
{
    public abstract class PageList
    {
		public int TotalCount { get; set; }
		public bool HasNext { get; set; }
    }
}
