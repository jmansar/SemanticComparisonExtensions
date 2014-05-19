using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class AnotherTypeWithAnotherInnerCollectionBuilder
    {
        private readonly List<AnotherObjectWithSingleStringProperty> _innerCollection = new List<AnotherObjectWithSingleStringProperty>();
        private bool _innerCollectionNull = false;

        public AnotherTypeWithAnotherInnerCollectionBuilder WithInnerCollectionItem(AnotherObjectWithSingleStringProperty item)
        {
            _innerCollection.Add(item);
            return this;
        }

        public AnotherTypeWithAnotherInnerCollectionBuilder WithInnerCollectionItemValue(string stringValue)
        {
            return WithInnerCollectionItem(new AnotherObjectWithSingleStringProperty()
            {
                StringTypeProperty = stringValue
            });
        }

        public AnotherTypeWithAnotherInnerCollectionBuilder WithInnerCollectionItemTypeOfObjectWithInt(string stringValue, int intValue)
        {
            return WithInnerCollectionItem(new AnotherObjectWithAnotherIntPropertyDerived
            {
                StringTypeProperty = stringValue,
                IntTypePropertyFromDerivedClass = intValue
            });
        }

        public AnotherTypeWithAnotherInnerCollectionBuilder WithInnerCollectionItemsCount(int count)
        {
            for (var i = 0; i < count; i++)
            {
                WithInnerCollectionItem(new AnotherObjectWithSingleStringProperty());
            }

            return this;
        }

        public AnotherTypeWithAnotherInnerCollectionBuilder WithNullInnerCollection()
        {
            _innerCollectionNull = true;
            return this;
        }

        public AnotherTypeWithAnotherInnerCollection Build()
        {
            return new AnotherTypeWithAnotherInnerCollection()
            {
                OtherCollection = _innerCollectionNull ? null : _innerCollection
            };
        }
    }
}
