using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.SemanticComparison;
using Ploeh.SemanticComparison.Fluent;

namespace Jmansar.SemanticComparisonExtensions
{
    internal static class CompareUtils
    {
        internal static bool CompareUsingLikeness
            <TSourceProperty, TDestinationProperty, TSourcePropertySubType, TDestinationPropertySubType>
            (
                Func<Likeness<TSourcePropertySubType, TDestinationPropertySubType>,
                Likeness<TSourcePropertySubType, TDestinationPropertySubType>> likenessDefFunc,
                TSourceProperty sourceVal,
                TDestinationProperty destVal
            )
            where TSourceProperty : class
            where TDestinationProperty : class
            where TSourcePropertySubType : class, TSourceProperty
            where TDestinationPropertySubType : class, TDestinationProperty
        {
            if (CheckIfBothNulls(sourceVal, destVal))
            {
                return true;
            }

            if (CheckIfAtleastOneIsNull(sourceVal, destVal))
            {
                return false;
            }


            var sourceValCast = sourceVal as TSourcePropertySubType;
            if (sourceValCast == null)
            {
                throw new ArgumentException(
                    String.Format("Source value is type of '{1}', cannot cast to '{0}'",
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
        }

        internal static bool CompareCollectionsUsingLikeness
            <TSourceProperty, TDestinationProperty, TSourcePropertySubType, TDestinationPropertySubType>
            (
                Func<Likeness<TSourcePropertySubType, TDestinationPropertySubType>, Likeness<TSourcePropertySubType, TDestinationPropertySubType>> likenessDefFunc,
                IEnumerable<TSourceProperty> sourceCollection, 
                IEnumerable<TDestinationProperty> destCollection
            )
            where TSourcePropertySubType : class, TSourceProperty
            where TDestinationPropertySubType : class, TDestinationProperty
            where TSourceProperty : class
            where TDestinationProperty : class
        {
            if (CheckIfBothNulls(sourceCollection, destCollection))
            {
                return true;
            }

            if (CheckIfAtleastOneIsNull(sourceCollection, destCollection))
            {
                return false;
            }

            var sourceList = sourceCollection.ToList();
            var destList = destCollection.ToList();

            if (!CheckIfHaveSameNumberOfItems(sourceList, destList))
            {
                return false;
            }

            for (var i = 0; i < sourceList.Count(); i++)
            {
                var sourceVal = sourceList[i];
                var destVal = destList[i];

                if (!CompareUsingLikeness(likenessDefFunc, sourceVal, destVal))
                    return false;
            }

            return true;
        }

        internal static bool CheckIfHaveSameNumberOfItems(ICollection sourceList, ICollection destList)
        {
            return sourceList.Count == destList.Count;
        }


        internal static bool CheckIfBothNulls(object sourceVal, object destVal)
        {
            return sourceVal == null && destVal == null;
        }

        internal static bool CheckIfAtleastOneIsNull(object sourceVal, object destVal)
        {
            return sourceVal == null || destVal == null;
        }

        internal static bool CheckIfCollectionEqual(IEnumerable sourceVal, IEnumerable destVal)
        {
            if (CheckIfBothNulls(sourceVal, destVal))
            {
                return true;
            }

            if (CheckIfAtleastOneIsNull(sourceVal, destVal))
            {
                return false;
            }

            return sourceVal.Cast<object>().SequenceEqual(destVal.Cast<object>());
        }
    }
}
