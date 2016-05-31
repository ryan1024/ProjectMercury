using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Reflection;

namespace Mercury.Backoffice.Helpers
{
    public static class GridConstructionHelper
    {
        public const string OrderByAsc = "OrderBy";
        public const string OrderByDesc = "OrderByDescending";

        public const string ThenByAsc = "ThenBy";
        public const string ThenByDesc = "ThenByDescending";

        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, string direction)
        {
            //return ApplyOrder<T>(source, property, "OrderBy");
            return ApplyOrder<T>(source, property, direction);
        }
        //////////public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string property)
        //////////{
        //////////    return ApplyOrder<T>(source, property, "OrderByDescending");
        //////////}
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> source, string property, string direction)
        {
            //return ApplyOrder<T>(source, property, "ThenBy");
            return ApplyOrder<T>(source, property, direction);
        }
        //////////public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> source, string property)
        //////////{
        //////////    return ApplyOrder<T>(source, property, "ThenByDescending");
        //////////}
        static IOrderedQueryable<T> ApplyOrder<T>(IQueryable<T> source, string property, string methodName)
        {
            string[] props = property.Split('.');
            Type type = typeof(T);
            ParameterExpression arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props)
            {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            LambdaExpression lambda = Expression.Lambda(delegateType, expr, arg);

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }

        public static IOrderedQueryable<T> Paged<T>(this IQueryable<T> source, int? page, int? limit)
        {
            if (!page.HasValue || !limit.HasValue) { return (IOrderedQueryable<T>)source; }

            int start = (page.Value - 1) * limit.Value;
            source = source.Skip(start).Take(limit.Value).AsQueryable();

            return (IOrderedQueryable<T>)source;
        }
    }
}
