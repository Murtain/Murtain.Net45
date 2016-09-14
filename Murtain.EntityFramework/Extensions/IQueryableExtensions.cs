using Murtain.EntityFramework.Queries;
using Murtain.EntityFramework.Queries.Criterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace System.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, Expression<Func<T, bool>> predicate, bool condition = false, bool isOr = false) where T : class
        {
            if (predicate == null || condition)
            {
                return source;
            }
            if (LambdaBuilder.GetValue(predicate) == null)
            {
                return source;
            }
            return source.Where(predicate);
        }
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, ICriteria<T> criteria) where T : class
        {
            if (criteria == null)
            {
                return source;
            }
            var predicate = criteria.GetPredicate();
            if (predicate == null)
            {
                return source;
            }
            return source.Where(predicate);
        }
        public static IQueryable<T> Filter<T>(this IQueryable<T> source, ICriteria criteria) where T : class
        {
            if (criteria == null)
            {
                return source;
            }
            var predicate = criteria.GetPredicate();
            if (string.IsNullOrWhiteSpace(predicate))
            {
                return source;
            }
            return source.Where(predicate, criteria.GetValues());
        }
        public static IQueryable<T> FilterInt<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> propertyExpression, int? min, int? max) where T : class
        {
            return source.Filter(new IntSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }
        public static IQueryable<T> FilterDouble<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> propertyExpression, double? min, double? max) where T : class
        {
            return source.Filter(new DoubleSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }
        public static IQueryable<T> FilterDecimal<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> propertyExpression, decimal? min, decimal? max) where T : class
        {
            return source.Filter(new DecimalSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }
        public static IQueryable<T> FilterDate<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> propertyExpression, DateTime? min, DateTime? max) where T : class
        {
            return source.Filter(new DateSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }
        public static IQueryable<T> FilterDateTime<T, TProperty>(this IQueryable<T> source, Expression<Func<T, TProperty>> propertyExpression, DateTime? min, DateTime? max) where T : class
        {
            return source.Filter(new DateTimeSegmentCriteria<T, TProperty>(propertyExpression, min, max));
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, string sort) where T : class
        {
            return source.OrderBy(string.IsNullOrEmpty(propertyName) ? "ID" : propertyName + " " + sort);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool isDesc) where T : class
        {
            return source.OrderBy(string.IsNullOrEmpty(propertyName) ? "ID" : propertyName + " " + (isDesc ? "desc" : "asc"));
        }
        public static IQueryable<T> Paging<T>(this IQueryable<T> source, int pageIndex, int pageSize, out int total) where T : class
        {
            total = source.Count();
            return source.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
        }
        public static IQueryable<T> Paging<T>(this IQueryable<T> source, int pageIndex, int pageSize) where T : class
        {
            return source.Skip<T>(pageSize * (pageIndex - 1)).Take<T>(pageSize);
        }
    }
}
