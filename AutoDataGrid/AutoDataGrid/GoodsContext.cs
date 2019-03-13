namespace AutoDataGrid
{
	using System;
	using System.Data.Entity;
	using System.Linq;

	public class GoodsContext : DbContext
	{

		public GoodsContext()
			: base("name=GoodsContext")
		{
		}
		public DbSet<Good> Goods { get; set; }

	}


}