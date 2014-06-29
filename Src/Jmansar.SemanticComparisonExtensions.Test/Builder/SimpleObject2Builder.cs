using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class SimpleObject2Builder
    {
        private int _intValue;
        private string _stringValue;

        public SimpleObject2Builder WithIntValue(int value)
        {
            _intValue = value;
            return this;
        }

        public SimpleObject2Builder WithStringValue(string value)
        {
            _stringValue = value;
            return this;
        }

        public SimpleObject2 Build()
        {
            return new SimpleObject2()
            {
                IntValue2 = _intValue,
                StringValue2 = _stringValue
            };
        }
    }
}
