using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Diagnostics;
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
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Source and destination values are both null. Return value: equal.");
                return true;
            }

            if (CheckIfAtleastOneIsNull(sourceVal, destVal))
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("One of the values is null other is not null. Return value: not equal.");
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
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Destination value has different type than destination type of inner likeness passed. Return value: not equal.");
                return false;
            }

            var innerLikeness = sourceValCast.AsSource().OfLikeness<TDestinationPropertySubType>();
            if (likenessDefFunc != null)
            {
                innerLikeness = likenessDefFunc.Invoke(innerLikeness);
            }

            return EqualsWithDiagnostics(innerLikeness, destValCast);
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
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Source and destination values are both null. Return value: equal.");
                return true;
            }

            if (CheckIfAtleastOneIsNull(sourceCollection, destCollection))
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("One of the values is null other is not null. Return value: not equal.");
                return false;
            }

            var sourceList = sourceCollection.ToList();
            var destList = destCollection.ToList();

            if (!CheckIfHaveSameNumberOfItems(sourceList, destList))
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Source and destination collections have different number of items. Return value: not equal.");
                return false;
            }

            var result = true;
            for (var i = 0; i < sourceList.Count(); i++)
            {
                var sourceVal = sourceList[i];
                var destVal = destList[i];

                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage(String.Format("Comparing collections. Item index: {0}", i));
                result &= CompareUsingLikeness(likenessDefFunc, sourceVal, destVal);
            }

            return result;
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
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Source and destination values are both null. Return value: equal.");
                return true;
            }

            if (CheckIfAtleastOneIsNull(sourceVal, destVal))
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("One of the values is null other is not null. Return value: not equal.");
                return false;
            }

            var result = sourceVal.Cast<object>().SequenceEqual(destVal.Cast<object>());
            if (result)
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Collections are equal. Return value: equal.");
            }
            else
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("Collections are not equal. Return value: not equal.");
            }

            return result;
        }

        private static bool EqualsWithDiagnostics<TSource, TDestination>(Likeness<TSource, TDestination> likeness, TDestination value)
        {

            try
            {
                likeness.ShouldEqual(value);
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage("The source and destination values are equal.");

                return true;
            }
            catch (LikenessException ex)
            {
                DiagnosticsWriterLocator.DiagnosticsWriter.WriteMessage(String.Format("The source and destination values are not equal. Details: {0}", ex.Message));
                return false;
            }
        }
    }
}
