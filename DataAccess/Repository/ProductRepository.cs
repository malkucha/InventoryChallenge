using DataAccess.Behaviours;
using DataAccess.Context;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : RepositoryBase<ProductEntity, InventoryDbContext>, IProductRepository
    {
        public ProductRepository(InventoryDbContext context): base(context)
        {            
        }
    }
}
