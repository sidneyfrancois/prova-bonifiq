using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;
using System.Threading;

namespace ProvaPub.Models
{
    public abstract class PageList<T> where T : class
    { 
        public int TotalCount { get; set; } = 10;

        public List<T> ListPage(int page, TestDbContext ctx)
        {
            List<T> pageList = ctx.Set<T>()
                                .Skip((page - 1) * TotalCount)
                                .Take(TotalCount)
                                .ToList();

            if (pageList.Count == 0)
                throw new InvalidOperationException($"There is no {nameof(T)} in specific page");

            return pageList;
        }
    }
}

