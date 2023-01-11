using System.Collections.Generic;
using System.Linq.Expressions;

namespace OneKey.Shared.Helpers
{
    public class DynamicExpressionEvaluator : ExpressionVisitor
    {
        private HashSet<Expression> dynamicExpressions;

        public DynamicExpressionEvaluator(HashSet<Expression> parameterlessExpressions)
        {
            this.dynamicExpressions = parameterlessExpressions;
        }
        public override Expression Visit(Expression node)
        {
            if (dynamicExpressions.Contains(node))
                return Evaluate(node);
            else
                return base.Visit(node);
        }

        private Expression Evaluate(Expression node)
        {
            if (node.NodeType == ExpressionType.Constant)
            {
                return node;
            }
            object value = Expression.Lambda(node).Compile().DynamicInvoke();
            return Expression.Constant(value, node.Type);
        }
    }
}