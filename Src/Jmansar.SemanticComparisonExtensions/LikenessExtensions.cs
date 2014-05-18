using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ploeh.SemanticComparison;
using Ploeh.SemanticComparison.Fluent;

namespace Jmansar.SemanticComparisonExtensions
{
    public static class LikenessExtensions
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
            <TSource, TDestination, TSourceProperty, TDestinationProperty,TSourcePropertySubType, TDestinationPropertySubType>
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
                    var sourceVal = sourcePropertyPicker.Compile().Invoke(s);
                    var destVal = propertyPicker.Compile().Invoke(d);
                    if (sourceVal == null && destVal == null)
                    {
                        return true;
                    }

                    if (sourceVal == null || destVal == null)
                    {
                        return false;
                    }


                    var sourceValCast = sourceVal as TSourcePropertySubType;
                    if (sourceValCast == null)
                    {
                        throw new ArgumentException(
                            String.Format("Source property value is type of '{1}', cannot cast to '{0}'",
                                typeof(TSourcePropertySubType).FullName, sourceVal.GetType().FullName));
                    }

                    var destValCast = destVal as TDestinationPropertySubType;
                    if (destValCast == null)
                    {
                        // destination value has different type than destination type of inner likeness passed,
                        // so it is not equal
                        return false;
                    }

                    var innerLikeness = sourceValCast.AsSource().OfLikeness<TDestinationPropertySubType>();
                    if (likenessDefFunc != null)
                    {
                        innerLikeness = likenessDefFunc.Invoke(innerLikeness);
                    }

                    return innerLikeness.Equals(destValCast);
                });
        }

        public static Likeness<TSource, TDestination> WithCollectionSequenceEquals<TSource, TDestination>
            (
                this Likeness<TSource, TDestination> likeness,
                Expression<Func<TDestination, IEnumerable>> propertyPicker,
                Expression<Func<TSource, IEnumerable>> sourcePropertyPicker
            )
        {
            return likeness.With(propertyPicker)
                .EqualsWhen((s, d) =>
                {
                    var sourceVal = sourcePropertyPicker.Compile().Invoke(s);
                    var destVal = propertyPicker.Compile().Invoke(d);
                    if (sourceVal == null && destVal == null)
                    {
                        return true;
                    }

                    if (sourceVal == null || destVal == null)
                    {
                        return false;
                    }

                    return sourceVal.Cast<object>().SequenceEqual(destVal.Cast<object>());
                });
        }

        public static Likeness<TType, TType> WithCollectionSequenceEquals<TType>(this Likeness<TType, TType> likeness,
         Expression<Func<TType, IEnumerable>> propertyPicker)
        {
            return likeness.WithCollectionSequenceEquals(propertyPicker, propertyPicker);
        }

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
            where TSourcePropertySubType : class
            where TDestinationPropertySubType : class
        {
            return likeness.With(propertyPicker)
                .EqualsWhen((s, d) =>
                {
                    var sourceCollection = sourcePropertyPicker.Compile().Invoke(s);
                    var destCollection = propertyPicker.Compile().Invoke(d);
                    if (sourceCollection == null && destCollection == null)
                    {
                        return true;
                    }

                    if (sourceCollection == null || destCollection == null)
                    {
                        return false;
                    }

                    var sourceList = sourceCollection.ToList();
                    var destList = destCollection.ToList();

                    if (sourceList.Count() != destList.Count())
                    {
                        return false;
                    }


                    for (var i = 0; i < sourceList.Count(); i++)
                    {
                        var sourceVal = sourceList[i];
                        var destVal = destList[i];

                        if (sourceVal == null && destVal == null)
                        {
                            continue;
                        }

                        if (sourceVal == null || destVal == null)
                        {
                            return false;
                        }

                        var sourceValCast = sourceVal as TSourcePropertySubType;
                        if (sourceValCast == null)
                        {
                            throw new ArgumentException(
                                String.Format("Source collection contains item of type '{1}', cannot cast to '{0}'",
                                    typeof(TSourcePropertySubType).FullName, sourceVal.GetType().FullName));
                        }

                        var destValCast = destVal as TDestinationPropertySubType;
                        if (destValCast == null)
                        {
                            // destination value has different type than destination type of inner likeness passed,
                            // so it is not equal
                            return false;
                        }

                        var innerLikeness =
                                sourceValCast.AsSource().OfLikeness<TDestinationPropertySubType>();

                        if (likenessDefFunc != null)
                        {
                            innerLikeness = likenessDefFunc.Invoke(innerLikeness);
                        }

                        if (!innerLikeness.Equals(destValCast))
                            return false;

                    }

                    return true;
                });
        }
    }
}
