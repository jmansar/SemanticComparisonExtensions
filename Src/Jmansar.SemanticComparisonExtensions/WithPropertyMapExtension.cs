using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Ploeh.SemanticComparison;

namespace Jmansar.SemanticComparisonExtensions
{
    public static class WithPropertyMapExtension
    {
        public static Likeness<TSource, TDestination> WithPropertyMap
            <TSource, TDestination, TSourceProperty, TDestinationProperty>
            (
                this Likeness<TSource, TDestination> likeness,
                Expression<Func<TDestination, TDestinationProperty>> propertyPicker,
                Expression<Func<TSource, TSourceProperty>> sourcePropertyPicker
            )
        {
            return likeness
                .With(propertyPicker)
                .EqualsWhen((s, d) =>
                {
                    var sourceVal = ExpressionUtils.GetValue(sourcePropertyPicker, s);
                    var destVal = ExpressionUtils.GetValue(propertyPicker, d);

                    return object.Equals(sourceVal, destVal);
                });
        }
    }
}
