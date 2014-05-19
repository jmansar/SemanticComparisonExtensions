using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Ploeh.SemanticComparison;

namespace Jmansar.SemanticComparisonExtensions
{
    public static class WithInnerLikenessExtensions
    {
        public static Likeness<TSource, TDestination> WithInnerLikeness
            <TSource, TDestination, TSourceProperty, TDestinationProperty>
            (
                this Likeness<TSource, TDestination> likeness,
                Expression<Func<TDestination, TDestinationProperty>> propertyPicker,
                Expression<Func<TSource, TSourceProperty>> sourcePropertyPicker,
                Func<Likeness<TSourceProperty, TDestinationProperty>, Likeness<TSourceProperty, TDestinationProperty>> likenessDefFunc = null
            )
            where TSourceProperty : class
            where TDestinationProperty : class
        {
            return WithInnerSpecificLikeness(likeness, propertyPicker, sourcePropertyPicker, likenessDefFunc);
        }

        public static Likeness<TSource, TDestination> WithInnerSpecificLikeness
            <TSource, TDestination, TSourceProperty, TDestinationProperty, TSourcePropertySubType, TDestinationPropertySubType>
            (
                this Likeness<TSource, TDestination> likeness,
                Expression<Func<TDestination, TDestinationProperty>> propertyPicker,
                Expression<Func<TSource, TSourceProperty>> sourcePropertyPicker,
                Func<Likeness<TSourcePropertySubType, TDestinationPropertySubType>, Likeness<TSourcePropertySubType, TDestinationPropertySubType>> likenessDefFunc
            )
            where TSourceProperty : class
            where TDestinationProperty : class
            where TSourcePropertySubType : class, TSourceProperty
            where TDestinationPropertySubType : class, TDestinationProperty
        {
            return likeness.With(propertyPicker)
                .EqualsWhen((s, d) =>
                {
                    var sourceVal = ExpressionUtils.GetValue(sourcePropertyPicker, s);
                    var destVal = ExpressionUtils.GetValue(propertyPicker, d);

                    return CompareUtils.CompareUsingLikeness(likenessDefFunc, sourceVal, destVal);
                });
        }


    }
}
