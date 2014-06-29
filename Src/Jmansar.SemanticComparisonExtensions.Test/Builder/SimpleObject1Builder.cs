using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class SimpleObject1Builder
    {
        private int _intValue;
        private string _stringValue;

        public SimpleObject1Builder WithIntValue(int value)
        {
            _intValue = value;
            return this;
        }

        public SimpleObject1Builder WithStringValue(string value)
        {
            _stringValue = value;
            return this;
        }

        public SimpleObject1 Build()
        {
            return new SimpleObject1()
            {
                IntValue1 = _intValue,
                StringValue1 = _stringValue
            };
        }
    }
}
