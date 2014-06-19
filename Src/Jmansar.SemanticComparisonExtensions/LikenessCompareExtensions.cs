using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ploeh.SemanticComparison;

namespace Jmansar.SemanticComparisonExtensions
{
    public static class LikenessCompareExtensions
    {
        public static bool CompareCollectionsUsingLikeness
            <TSourceCollectionItem, TDestinationCollectionItem>
            (
                this IEnumerable<TSourceCollectionItem> sourceCollection,
                IEnumerable<TDestinationCollectionItem> destCollection,
                Func<Likeness<TSourceCollectionItem, TDestinationCollectionItem>, Likeness<TSourceCollectionItem, TDestinationCollectionItem>> likenessDefFunc = null

            )
            where TSourceCollectionItem : class
            where TDestinationCollectionItem : class
        {
            return sourceCollection.CompareCollectionsUsingSpecificLikeness(destCollection, likenessDefFunc);
        }

        public static bool CompareCollectionsUsingSpecificLikeness
            <TSourceCollectionItem, TDestinationCollectionItem, TSourceCollectionItemSubType, TDestinationCollectionItemSubType>
            (
                this IEnumerable<TSourceCollectionItem> sourceCollection,
                IEnumerable<TDestinationCollectionItem> destCollection,
                Func<Likeness<TSourceCollectionItemSubType, TDestinationCollectionItemSubType>, Likeness<TSourceCollectionItemSubType, TDestinationCollectionItemSubType>> likenessDefFunc = null

            )
            where TSourceCollectionItemSubType : class, TSourceCollectionItem
            where TDestinationCollectionItemSubType : class, TDestinationCollectionItem
            where TSourceCollectionItem : class
            where TDestinationCollectionItem : class
        {
            return CompareUtils.CompareCollectionsUsingLikeness(likenessDefFunc, sourceCollection, destCollection);
        }
    }
}
