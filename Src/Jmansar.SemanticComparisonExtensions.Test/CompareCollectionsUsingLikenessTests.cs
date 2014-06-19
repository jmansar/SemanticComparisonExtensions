using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.Builder;
using Jmansar.SemanticComparisonExtensions.Test.TestData;
using NUnit.Framework;
using Ploeh.SemanticComparison;

namespace Jmansar.SemanticComparisonExtensions.Test
{
    [TestFixture]
    public class CompareCollectionsUsingLikenessTests
    {
        [Test]
        public void Should_Return_Equal_When_Collections_Are_Empty()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new List<ObjectWithSingleStringProperty>();

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new List<AnotherObjectWithSingleStringProperty>();

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Equal_When_Collections_Are_Null()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = null;

            IEnumerable<AnotherObjectWithSingleStringProperty> other = null;

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        public void Should_Return_Not_Equal_When_Collections_Counts_Are_Different(int valueCollectionCount, int otherCollectionCount)
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[valueCollectionCount];

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[otherCollectionCount];

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Is_Null_And_Destination_Is_Not_Null()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = null;

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new List<AnotherObjectWithSingleStringProperty>();

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other); 

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Collection_Is_Not_Null_And_Destination_Collection_Is_Null()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new List<ObjectWithSingleStringProperty>();

            IEnumerable<AnotherObjectWithSingleStringProperty> other = null;

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other); 

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Equal_When_Source_Collection_Item_Is_Null_And_Destination_Collection_Item_Is_Null()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                null
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                null
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Collection_Item_Is_Not_Null_And_Destination_Collection_Item_Is_Null()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build()
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                null
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Item_Is_Null_And_Destination_Inner_Collection_Item_Is_Not_Null()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                null
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build()
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Equal_When_Collections_Items_Are_Equal_Using_Default_Likeness()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Collections_Items_Are_Not_Equal_Using_Default_Likeness()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("ASDF").Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Be_Able_To_Use_Comparison_Overrides_To_Force_Not_Equal()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other, likeness => likeness.With(x => x.StringTypeProperty).EqualsWhen((s, d) => false));

            // verify
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Should_Be_Able_To_Use_Comparison_Overrides_To_Force_Equal()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new ObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test").Build(),
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("ASDF").Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingLikeness(other, likeness => likeness.With(x => x.StringTypeProperty).EqualsWhen((s, d) => true));

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Collections_Items_Are_Not_Equal_Using_Likeness_For_Overriden_Types()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(3).Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingSpecificLikeness(other, 
                (Likeness<ObjectWithAnotherIntPropertyDerived, AnotherObjectWithAnotherIntPropertyDerived> likeness) =>
                    likeness);

            // verify
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Should_Return_Equal_When__Collections_Items_Are_Equal_Using_Likeness_For_Overriden_Types()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingSpecificLikeness(other, 
                (Likeness<ObjectWithAnotherIntPropertyDerived, AnotherObjectWithAnotherIntPropertyDerived> likeness) =>
                    likeness);

            // verify
            Assert.That(result, Is.True);
        }
        
        [Test]
        public void Should_Return_Not_Equal_When_Destination_Collection_Contains_BaseType_Items_And_Derived_Likeness_Is_Set()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new AnotherObjectWithSingleStringPropertyBuilder().WithStringValue("test2").Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingSpecificLikeness(other, 
                (Likeness<ObjectWithAnotherIntPropertyDerived, AnotherObjectWithAnotherIntPropertyDerived> likeness) =>
                    likeness);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Destination_Collection_Collection_Contains_Item_That_Cannot_Be_Cast_To_Likeness_Destination_Type()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            // exercise
            var result = value.CompareCollectionsUsingSpecificLikeness(other,
                (Likeness<ObjectWithAnotherIntPropertyDerived, AnotherObjectWithAnotherStringPropertiesDerived> likeness) =>
                    likeness);

            // verify
            Assert.That(result, Is.False);
        }
        
        [Test]
        public void Should_Return_Not_Equal_When_Source_Collection_Contains_Item_That_Cannot_Be_Cast_To_Likeness_Source_Type()
        {
            // fixture setup
            IEnumerable<ObjectWithSingleStringProperty> value = new ObjectWithSingleStringProperty[]
            {
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new ObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            IEnumerable<AnotherObjectWithSingleStringProperty> other = new AnotherObjectWithSingleStringProperty[]
            {
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test").WithIntValue(1).Build(),
                new AnotherObjectWithAnotherIntPropertyDerivedBuilder().WithStringValue("test2").WithIntValue(2).Build(),
            };

            // exercise
            var action = new TestDelegate(() => value.CompareCollectionsUsingSpecificLikeness(other,
                (Likeness<ObjectWithAnotherStringPropertiesDerived, AnotherObjectWithAnotherIntPropertyDerived> likeness) =>
                    likeness));

            // verify
            Assert.That(action, Throws.ArgumentException.And.Message.Contains("Source value is type of 'Jmansar.SemanticComparisonExtensions.Test.TestData.ObjectWithAnotherIntPropertyDerived', cannot cast to 'Jmansar.SemanticComparisonExtensions.Test.TestData.ObjectWithAnotherStringPropertiesDerived"));
        }
    }
}
