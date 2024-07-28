using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Specifications
{
    public interface ISpecifications<T>  where T : BaseEntity //Genaric Of < T >  //Spec For query Which Run Aganiest Dbset <T>
    {
        // Property Signature for each and every Spec
        // Lambda Expression taking one parameter and returning a bool// Func Or Predict
        Expression<Func<T, bool>> Criteria { get; set; }

        List<Expression<Func<T, object>>> Includes { get; set; }

    }
}
