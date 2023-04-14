using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
	public class ProductService
	{
		TestDbContext _ctx;

		public ProductService(TestDbContext ctx)
		{
			_ctx = ctx;
		}

		public List<Product> ListProducts(int page)
		{
			var productPageList = new PageList<Product>();

			productPageList.ResultPageList = _ctx.Products.Skip((page - 1) * productPageList.TotalCount).Take(productPageList.TotalCount).ToList();

			return productPageList.ResultPageList;
		}

	}
}
