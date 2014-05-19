using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmansar.SemanticComparisonExtensions.Test.TestData
{
    public class AnotherObjectWithAnotherIntPropertyDerived : AnotherObjectWithSingleStringProperty
    {
        public int IntTypePropertyFromDerivedClass { get; set; }

    }
}
