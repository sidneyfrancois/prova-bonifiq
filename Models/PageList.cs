namespace ProvaPub.Models
{
    public class PageList<T>
    {
		public List<T> ResultPageList { get; set; } = new List<T>();
        public int TotalCount { get; set; } = 10;
        public bool HasNext { get; set; } = true;
    }
}
