using DataAccessLayer.Models;
using DataAccessLayer.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    internal static class SpecificationEvaluator <TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> InputQuery, ISpecifications<TEntity> Spec)
        {
            var query = InputQuery ;
            if ( Spec.Criteria != null )
            {

            }



            return query ;  
        }

    }
}
