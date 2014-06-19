using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class AnotherObjectWithSingleStringPropertyBuilder
    {
        private string _stringTypeProperty;

        public AnotherObjectWithSingleStringPropertyBuilder WithStringValue(string value)
        {
            _stringTypeProperty = value;
            return this;
        }

        public AnotherObjectWithSingleStringProperty Build()
        {
            return new AnotherObjectWithSingleStringProperty()
            {
                StringTypeProperty = _stringTypeProperty
            };
        }
    }
}
