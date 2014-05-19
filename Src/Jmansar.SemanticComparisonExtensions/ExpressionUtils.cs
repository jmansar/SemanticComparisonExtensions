using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Jmansar.SemanticComparisonExtensions
{
    internal class ExpressionUtils
    {
        internal static TProperty GetValue<TType, TProperty>(Expression<Func<TType, TProperty>> propertyPicker, TType obj)
        {
            return propertyPicker.Compile().Invoke(obj);
        }
    }
}
