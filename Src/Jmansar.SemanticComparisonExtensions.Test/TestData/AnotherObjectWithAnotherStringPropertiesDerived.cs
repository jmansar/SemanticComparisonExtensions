using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmansar.SemanticComparisonExtensions.Test.TestData
{
    public class AnotherObjectWithAnotherStringPropertiesDerived : AnotherObjectWithSingleStringProperty
    {
        public string StringTypePropertyFromDerivedClass { get; set; }
    }
}
