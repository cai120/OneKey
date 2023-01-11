using System.Collections.Generic;
using System.Linq.Expressions;

namespace OneKey.Shared.Helpers
{
    public class DynamicExpressionSearcher : ExpressionVisitor
    {
        public HashSet<Expression> DynamicExpressions { get; } = new HashSet<Expression>();
        private bool containsParameter = false;

        public override Expression Visit(Expression node)
        {
            bool originalContainsParameter = containsParameter;
            containsParameter = false;
            base.Visit(node);
            if (!containsParameter)
            {
                if (node?.NodeType == ExpressionType.Parameter)
                    containsParameter = true;
                else
                    DynamicExpressions.Add(node);
            }
            containsParameter |= originalContainsParameter;

            return node;
        }
    }
}