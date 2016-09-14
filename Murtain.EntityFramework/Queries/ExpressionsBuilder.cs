using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries
{
    public class ExpressionsBuilder<TEntity>
    {
        public ExpressionsBuilder()
        {
            Parameter = Expression.Parameter(typeof(TEntity), "t");
        }

        private ParameterExpression Parameter { get; set; }

        public ParameterExpression GetParameter()
        {
            return Parameter;
        }

        public Expression Create<T>(Expression<Func<TEntity, T>> property, Operator opt, object value)
        {
            return Parameter.Property(LambdaBuilder.GetMember(property)).Operation(opt, value);
        }

        public Expression<Func<TEntity, bool>> ToLambda(Expression expression)
        {
            if (expression == null)
                return null;
            return expression.ToLambda<Func<TEntity, bool>>(Parameter);
        }
    }
}
