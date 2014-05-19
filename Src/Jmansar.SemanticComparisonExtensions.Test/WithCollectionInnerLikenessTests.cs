using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.Builder;
using Jmansar.SemanticComparisonExtensions.Test.TestData;
using NUnit.Framework;
using Ploeh.SemanticComparison;
using Ploeh.SemanticComparison.Fluent;

namespace Jmansar.SemanticComparisonExtensions.Test
{
    [TestFixture]
    public class WithCollectionInnerLikenessTests
    {
        [Test]
        public void Should_Return_Equal_When_Corresponding_Inner_Collections_Are_Empty()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Equal_When_Corresponding_Inner_Collections_Are_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, d => d.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }


        [Test]
        public void Should_Return_Equal_When_Corresponding_Inner_Collections_Items_Are_Equal_Using_Default_Likeness()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, d => d.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Corresponding_Inner_Collections_Items_Are_Not_Equal_Using_Default_Likeness()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("QWERTY")
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, d => d.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Use_Inner_Comparison_Overrides_To_Force_Not_Equal()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, d => d.OtherCollection, 
                    likeness => likeness.With(x => x.StringTypeProperty).EqualsWhen((s,d) => false))
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Use_Inner_Comparison_Overrides_To_Force_Equal()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item1")
                .WithInnerCollectionItemValue("QWERTY")
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, d => d.OtherCollection,
                    likeness => likeness.With(x => x.StringTypeProperty).EqualsWhen((s, d) => true))
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Corresponding_Inner_Collections_Items_Are_Not_Equal_Using_Likeness_For_Overriden_Types()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .WithInnerCollectionItemTypeOfObjectWithInt("item2", 2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .WithInnerCollectionItemTypeOfObjectWithInt("item2", 99)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerSpecificLikeness(t => t.Collection, d => d.OtherCollection, 
                    (Likeness<AnotherObjectWithAnotherIntPropertyDerived, ObjectWithAnotherIntPropertyDerived>  likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Equal_When_Corresponding_Inner_Collections_Items_Are_Equal_Using_Likeness_For_Overriden_Types()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .WithInnerCollectionItemTypeOfObjectWithInt("item2", 2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .WithInnerCollectionItemTypeOfObjectWithInt("item2", 2)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerSpecificLikeness(t => t.Collection, d => d.OtherCollection,
                    (Likeness<AnotherObjectWithAnotherIntPropertyDerived, ObjectWithAnotherIntPropertyDerived> likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Destination_Collection_Inner_Collection_Contains_BaseType_Items_And_Derived_Likeness_Is_Set()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .WithInnerCollectionItemTypeOfObjectWithInt("item2", 2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .WithInnerCollectionItemValue("item2")
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerSpecificLikeness(t => t.Collection, d => d.OtherCollection,
                    (Likeness<AnotherObjectWithAnotherIntPropertyDerived, ObjectWithAnotherIntPropertyDerived> likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Destination_Collection_Inner_Collection_Contains_Item_That_Cannot_Be_Cast_To_Likeness_Property_Type()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerSpecificLikeness(t => t.Collection, d => d.OtherCollection,
                    (Likeness<AnotherObjectWithAnotherIntPropertyDerived, ObjectWithAnotherStringPropertiesDerived> likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Collection_Inner_Collection_Contains_Item_That_Cannot_Be_Cast_To_Likeness_Property_Type()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemTypeOfObjectWithInt("item1", 1)
                .Build()
                ;

            // exercise
            var action = new TestDelegate(() => value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerSpecificLikeness(t => t.Collection, d => d.OtherCollection,
                    (Likeness<AnotherObjectWithAnotherStringPropertiesDerived, ObjectWithAnotherIntPropertyDerived> likeness) => likeness)
                .Equals(other));

            // verify
            Assert.That(action, Throws.ArgumentException.And.Message.Contains("Source value is type of 'Jmansar.SemanticComparisonExtensions.Test.TestData.AnotherObjectWithAnotherIntPropertyDerived', cannot cast to 'Jmansar.SemanticComparisonExtensions.Test.TestData.AnotherObjectWithAnotherStringPropertiesDerived"));
        }

        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        public void Should_Return_Not_Equal_When_Inner_Collections_Counts_Are_Different(int valueCollectionCount,
            int otherCollectionCount)
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(valueCollectionCount)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(otherCollectionCount)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Is_Null_And_Destination_Inner_Collection_Is_Not_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(2)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Is_Not_Null_And_Destination_Inner_Collection_Is_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Item_Is_Null_And_Destination_Inner_Collection_Item_Is_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItem(null)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItem(null)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Item_Is_Not_Null_And_Destination_Inner_Collection_Item_Is_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item")
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItem(null)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Item_Is_Null_And_Destination_Inner_Collection_Item_Is_Not_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithAnotherInnerCollectionBuilder()
                .WithInnerCollectionItem(null)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemValue("item")
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionInnerLikeness(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }
    }
}
