using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithTypesAndBrandesSpecification : BaseSpecification<Transaction>
    {
        public ProductsWithTypesAndBrandesSpecification()
        {
            AddInclude(x=> x.Account);
            AddInclude( x=> x.TransactionCat);
        }

        //public ProductsWithTypesAndBrandesSpecification(int id) 
        //    : base(x=> x.Id == id)
        //{
        //    AddInclude(x=> x.ProductType);
        //    AddInclude( x=> x.ProductBrand);
        //}
    }
}