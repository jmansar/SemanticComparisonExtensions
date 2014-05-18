using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;
using Moq;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class TypeWithInnerCollectionBuilder
    {
        private readonly List<ObjectWithSingleStringProperty> _innerCollection = new List<ObjectWithSingleStringProperty>();
        private bool _innerCollectionNull = false;

        public TypeWithInnerCollectionBuilder WithInnerCollectionItem(ObjectWithSingleStringProperty item)
        {
            _innerCollection.Add(item);
            return this;
        }

        public TypeWithInnerCollectionBuilder WithInnerCollectionItemValue(string stringValue)
        {
            return WithInnerCollectionItem(new ObjectWithSingleStringProperty()
            {
                StringTypeProperty = stringValue
            });
        }

        public TypeWithInnerCollectionBuilder WithInnerCollectionItemTypeOfObjectWithInt(string stringValue, int intValue)
        {
            return WithInnerCollectionItem(new ObjectWithAnotherIntPropertyDerived
            {
                StringTypeProperty = stringValue,
                IntTypePropertyFromDerivedClass = intValue
            });
        }

        public TypeWithInnerCollectionBuilder WithInnerCollectionItemsCount(int count)
        {
            for (var i = 0; i < count; i++)
            {
                WithInnerCollectionItem(new ObjectWithSingleStringProperty());
            }

            return this;
        }

        public TypeWithInnerCollectionBuilder WithNullInnerCollection()
        {
            _innerCollectionNull = true;
            return this;
        }

        public TypeWithInnerCollection Build()
        {
            return new TypeWithInnerCollection()
            {
                Collection = _innerCollectionNull ? null : _innerCollection
            };
        }
    }
}
