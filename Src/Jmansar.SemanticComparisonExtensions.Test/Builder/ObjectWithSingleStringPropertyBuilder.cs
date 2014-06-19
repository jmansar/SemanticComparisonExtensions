using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class ObjectWithSingleStringPropertyBuilder
    {
        private string _stringTypeProperty;

        public ObjectWithSingleStringPropertyBuilder WithStringValue(string value)
        {
            _stringTypeProperty = value;
            return this;
        }

        public ObjectWithSingleStringProperty Build()
        {
            return new ObjectWithSingleStringProperty()
            {
                StringTypeProperty = _stringTypeProperty
            };
        }
    }
}
