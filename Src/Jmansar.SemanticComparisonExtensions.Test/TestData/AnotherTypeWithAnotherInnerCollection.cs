using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jmansar.SemanticComparisonExtensions.Test.TestData
{
    public class AnotherTypeWithAnotherInnerCollection 
    {
        public IEnumerable<AnotherObjectWithSingleStringProperty> OtherCollection { get; set; }
    }
}
