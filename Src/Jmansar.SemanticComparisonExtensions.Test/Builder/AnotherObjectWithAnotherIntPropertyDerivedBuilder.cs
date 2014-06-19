using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class AnotherObjectWithAnotherIntPropertyDerivedBuilder
    {
        private string _stringTypeProperty;
        private int _intProperty;

        public AnotherObjectWithAnotherIntPropertyDerivedBuilder WithStringValue(string value)
        {
            _stringTypeProperty = value;
            return this;
        }

        public AnotherObjectWithAnotherIntPropertyDerivedBuilder WithIntValue(int value)
        {
            _intProperty = value;
            return this;
        }

        public AnotherObjectWithAnotherIntPropertyDerived Build()
        {
            return new AnotherObjectWithAnotherIntPropertyDerived()
            {
                StringTypeProperty = _stringTypeProperty,
                IntTypePropertyFromDerivedClass = _intProperty
            };
        }
    }
}
