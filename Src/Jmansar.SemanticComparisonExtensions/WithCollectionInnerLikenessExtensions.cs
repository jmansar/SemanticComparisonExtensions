using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Jmansar.SemanticComparisonExtensions.Diagnostics;
using Ploeh.SemanticComparison;
using Ploeh.SemanticComparison.Fluent;

namespace Jmansar.SemanticComparisonExtensions
{
    public static class WithCollectionInnerLikenessExtensions
    {
        public static Likeness<TSource, TDestination> WithCollectionInnerLikeness
            <TSource, TDestination, TSourceProperty, TDestinationProperty>
            (
                this Likeness<TSource, TDestination> likeness,
                Expression<Func<TDestination, IEnumerable<TDestinationProperty>>> propertyPicker,
                Expression<Func<TSource, IEnumerable<TSourceProperty>>> sourcePropertyPicker,
                Func<Likeness<TSourceProperty, TDestinationProperty>, Likeness<TSourceProperty, TDestinationProperty>>
                    likenessDefFunc = null
            )
            where TSourceProperty : class
            where TDestinationProperty : class
        {
            return likeness.WithCollectionInnerSpecificLikeness(propertyPicker, sourcePropertyPicker, likenessDefFunc);
        }

        public static Likeness<TSource, TDestination> WithCollectionInnerSpecificLikeness
            <TSource, TDestination, TSourceProperty, TDestinationProperty, TSourcePropertySubType, TDestinationPropertySubType>
            (
                this Likeness<TSource, TDestination> likeness,
                Expression<Func<TDestination, IEnumerable<TDestinationProperty>>> propertyPicker,
                Expression<Func<TSource, IEnumerable<TSourceProperty>>> sourcePropertyPicker,
                Func<Likeness<TSourcePropertySubType, TDestinationPropertySubType>, Likeness<TSourcePropertySubType, TDestinationPropertySubType>> likenessDefFunc = null
            )
            where TSourcePropertySubType : class, TSourceProperty
            where TDestinationPropertySubType : class, TDestinationProperty
            where TSourceProperty : class
            where TDestinationProperty : class
        {
            return likeness.With(propertyPicker)
                .EqualsWhen((s, d) =>
                {
                    DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage(String.Format("Comparing inner collections using likeness. Source: {0} Destination: {1}.", sourcePropertyPicker, propertyPicker));
                    
                    var sourceCollection = ExpressionUtils.GetValue(sourcePropertyPicker, s);
                    var destCollection = ExpressionUtils.GetValue(propertyPicker, d);

                    return CompareUtils.CompareCollectionsUsingLikeness(likenessDefFunc, sourceCollection, destCollection);
                });
        }

      

        
    }
}
