using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries
{
    public class LambdaBuilder
    {
        public static MemberInfo GetMember(Expression expression)
        {
            var memberExpression = GetMemberExpression(expression);
            if (memberExpression == null)
                return null;
            return memberExpression.Member;
        }
        public static MemberExpression GetMemberExpression(Expression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetMemberExpression(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetMemberExpression(((UnaryExpression)expression).Operand);
                case ExpressionType.MemberAccess:
                    return (MemberExpression)expression;
            }
            return null;
        }
        public static string GetName(Expression expression)
        {
            var memberExpression = GetMemberExpression(expression);
            return GetMemberName(memberExpression);
        }
        private static string GetMemberName(MemberExpression memberExpression)
        {
            if (memberExpression == null)
                return string.Empty;
            string result = memberExpression.ToString();
            return result.Substring(result.IndexOf(".") + 1);
        }
        public static List<string> GetNames<T>(Expression<Func<T, object[]>> expression)
        {
            var result = new List<string>();
            if (expression == null)
                return result;
            var arrayExpression = expression.Body as NewArrayExpression;
            if (arrayExpression == null)
                return result;
            foreach (var each in arrayExpression.Expressions)
                AddName(result, each);
            return result;
        }
        private static void AddName(List<string> result, Expression expression)
        {
            var name = GetName(expression);
            if (string.IsNullOrWhiteSpace(name))
                return;
            result.Add(name);
        }
        public static object GetValue(Expression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetValue(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetValue(((UnaryExpression)expression).Operand);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    return GetValue(((BinaryExpression)expression).Right);
                case ExpressionType.Call:
                    return GetValue(((MethodCallExpression)expression).Arguments.FirstOrDefault());
                case ExpressionType.MemberAccess:
                    return GetMemberValue((MemberExpression)expression);
                case ExpressionType.Constant:
                    return GetConstantExpressionValue(expression);
            }
            return null;
        }
        private static object GetMemberValue(MemberExpression expression)
        {
            if (expression == null)
                return null;
            var field = expression.Member as FieldInfo;
            if (field != null)
            {
                var constValue = GetConstantExpressionValue(expression.Expression);
                return field.GetValue(constValue);
            }
            var property = expression.Member as PropertyInfo;
            if (property == null)
                return null;
            var value = GetMemberValue(expression.Expression as MemberExpression);
            if (value == null)
            {
                return null;
            }
            return property.GetValue(value);
        }
        private static object GetConstantExpressionValue(Expression expression)
        {
            var constantExpression = (ConstantExpression)expression;
            return constantExpression.Value;
        }
        public static ParameterExpression GetParameter(Expression expression)
        {
            if (expression == null)
                return null;
            switch (expression.NodeType)
            {
                case ExpressionType.Lambda:
                    return GetParameter(((LambdaExpression)expression).Body);
                case ExpressionType.Convert:
                    return GetParameter(((UnaryExpression)expression).Operand);
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                    return GetParameter(((BinaryExpression)expression).Left);
                case ExpressionType.MemberAccess:
                    return GetParameter(((MemberExpression)expression).Expression);
                case ExpressionType.Call:
                    return GetParameter(((MethodCallExpression)expression).Object);
                case ExpressionType.Parameter:
                    return (ParameterExpression)expression;
            }
            return null;
        }
        public static TAttribute GetAttribute<TAttribute>(Expression expression) where TAttribute : Attribute
        {
            var memberInfo = GetMember(expression);
            return memberInfo.GetCustomAttribute<TAttribute>();
        }
        public static TAttribute GetAttribute<TEntity, TProperty, TAttribute>(Expression<Func<TEntity, TProperty>> propertyExpression) where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(propertyExpression);
        }
        public static TAttribute GetAttribute<TProperty, TAttribute>(Expression<Func<TProperty>> propertyExpression) where TAttribute : Attribute
        {
            return GetAttribute<TAttribute>(propertyExpression);
        }
        public static IEnumerable<TAttribute> GetAttributes<TEntity, TProperty, TAttribute>(Expression<Func<TEntity, TProperty>> propertyExpression) where TAttribute : Attribute
        {
            var memberInfo = GetMember(propertyExpression);
            return memberInfo.GetCustomAttributes<TAttribute>();
        }
        public static ConstantExpression Constant(Expression expression, object value)
        {
            var memberExpression = expression as MemberExpression;
            if (memberExpression == null)
                return Expression.Constant(value);
            return Expression.Constant(value, memberExpression.Type);
        }
        public static int GetCriteriaCount(LambdaExpression expression)
        {
            if (expression == null)
                return 0;
            var result = expression.ToString().Replace("AndAlso", "|").Replace("OrElse", "|");
            return result.Split('|').Count();
        }
        public static Expression<Func<T, bool>> Equal<T>(string propertyName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                    .Equal(value)
                    .ToLambda<Func<T, bool>>(parameter);
        }
        private static ParameterExpression CreateParameter<T>()
        {
            return Expression.Parameter(typeof(T), "t");
        }
        public static Expression<Func<T, bool>> NotEqual<T>(string propertyName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                    .NotEqual(value)
                    .ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> Greater<T>(string propertyName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                    .Greater(value)
                    .ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> Less<T>(string propertyName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                    .Less(value)
                    .ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> GreaterEqual<T>(string propertyName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                    .GreaterEqual(value)
                    .ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> LessEqual<T>(string propertyName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                    .LessEqual(value)
                    .ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> Contains<T>(string propertyName, object value)
        {
            return Call<T>(propertyName, "Contains", value);
        }
        private static Expression<Func<T, bool>> Call<T>(string propertyName, string methodName, object value)
        {
            var parameter = CreateParameter<T>();
            return parameter.Property(propertyName)
                .Call(methodName, value)
                .ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> Starts<T>(string propertyName, string value)
        {
            var parameter = CreateParameter<T>();
            var property = parameter.Property(propertyName);
            var call = Expression.Call(property, property.Type.GetMethod("StartsWith", new Type[] { typeof(string) }),
                Expression.Constant(value));
            return call.ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> Ends<T>(string propertyName, string value)
        {
            var parameter = CreateParameter<T>();
            var property = parameter.Property(propertyName);
            var call = Expression.Call(property, property.Type.GetMethod("EndsWith", new Type[] { typeof(string) }),
                Expression.Constant(value));
            return call.ToLambda<Func<T, bool>>(parameter);
        }
        public static Expression<Func<T, bool>> ParsePredicate<T>(string propertyName, object value, Operator opt)
        {
            var parameter = Expression.Parameter(typeof(T), "t");
            return parameter.Property(propertyName).Operation(opt, value).ToLambda<Func<T, bool>>(parameter);
        }

    }
}
