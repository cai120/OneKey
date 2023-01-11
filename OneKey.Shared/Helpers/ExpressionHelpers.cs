using System.Linq.Expressions;

namespace OneKey.Shared.Helpers
{
    public static class ExpressionHelpers
    {
        public static Expression Simplify(this Expression expression)
        {
            var searcher = new DynamicExpressionSearcher();
            searcher.Visit(expression);
            return new DynamicExpressionEvaluator(searcher.DynamicExpressions).Visit(expression);
        }

        public static Expression<T> Simplify<T>(this Expression<T> expression)
        {
            return (Expression<T>)Simplify((Expression)expression);
        }
    }
}