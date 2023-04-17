using Castle.Core.Resource;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : PageListService<Product>
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public List<Product> ListProducts(int page)
		{
			return ListPage(page, _ctx);
		}

	}
}
