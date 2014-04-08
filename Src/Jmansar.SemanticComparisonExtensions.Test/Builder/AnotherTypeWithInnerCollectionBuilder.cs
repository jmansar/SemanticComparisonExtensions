using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;
using Moq;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class AnotherTypeWithInnerCollectionBuilder
    {
        private readonly List<ObjectWithSingleStringProperty> _innerCollection = new List<ObjectWithSingleStringProperty>();
        private bool _innerCollectionNull = false;

        public AnotherTypeWithInnerCollectionBuilder WithInnerCollectionItem(ObjectWithSingleStringProperty item)
        {
            _innerCollection.Add(item);
            return this;
        }

        public AnotherTypeWithInnerCollectionBuilder WithInnerCollectionItemsCount(int count)
        {
            for (var i = 0; i < count; i++)
            {
                WithInnerCollectionItem(new ObjectWithSingleStringProperty());
            }

            return this;
        }

        public AnotherTypeWithInnerCollectionBuilder WithNullInnerCollection()
        {
            _innerCollectionNull = true;
            return this;
        }

        public AnotherTypeWithInnerCollection Build()
        {
            return new AnotherTypeWithInnerCollection()
            {
                OtherCollection = _innerCollectionNull ? null : _innerCollection
            };
        }
    }
}
