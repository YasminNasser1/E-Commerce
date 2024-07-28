using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Specifications
{
    internal class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        //Atomatic Prop --->> Compiler Will Generate Packing Feiled Hidden Privete Atrribute Get And Set With Criteria and include

        public Expression<Func<T, bool>> Criteria { get; set; }

        public List<Expression<Func<T, object>>> Includes { get; set ; }


        public BaseSpecifications(Expression<Func<T, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
            //Includes = new List<Expression<Func<T, object>>>();
        }

        // Now We will Generate the method Wich Build The Query [SpecEvaluature --> Evaluate The Specification To Get Query --> Will Used With Genaric Repostry] 
    }
}
