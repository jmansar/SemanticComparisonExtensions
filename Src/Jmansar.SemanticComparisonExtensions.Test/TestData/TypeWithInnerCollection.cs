using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jmansar.SemanticComparisonExtensions.Test.TestData
{
    public class TypeWithInnerCollection
    {
        public IEnumerable<ObjectWithSingleStringProperty> Collection { get; set; }
    }
}
