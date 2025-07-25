using DataAccess.Context;
using DataAccess.Repository;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Behaviours
{
    public interface IProductRepository: IRepository<ProductEntity>
    {
    }

}
