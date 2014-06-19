using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class ObjectWithAnotherIntPropertyDerivedBuilder
    {
        private string _stringTypeProperty;
        private int _intProperty;

        public ObjectWithAnotherIntPropertyDerivedBuilder WithStringValue(string value)
        {
            _stringTypeProperty = value;
            return this;
        } 
        
        public ObjectWithAnotherIntPropertyDerivedBuilder WithIntValue(int value)
        {
            _intProperty = value;
            return this;
        }

        public ObjectWithSingleStringProperty Build()
        {
            return new ObjectWithAnotherIntPropertyDerived()
            {
                StringTypeProperty = _stringTypeProperty,
                IntTypePropertyFromDerivedClass = _intProperty
            };
        }
    }
}
