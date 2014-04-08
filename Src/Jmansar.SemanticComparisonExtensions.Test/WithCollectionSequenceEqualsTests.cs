using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jmansar.SemanticComparisonExtensions.Test.Builder;
using Jmansar.SemanticComparisonExtensions.Test.TestData;
using NUnit.Framework;
using Ploeh.SemanticComparison.Fluent;

namespace Jmansar.SemanticComparisonExtensions.Test
{
    [TestFixture]
    public class WithCollectionSequenceEqualsTests
    {
        [Test]
        public void Should_Return_Equal_When_Corresponding_Inner_Collections_Items_Are_Equal_Using_Default_Equality()
        {
            // fixture setup
            var item1 = new ObjectWithSingleStringProperty();
            var item2 = new ObjectWithSingleStringProperty();

            var value = new AnotherTypeWithInnerCollectionBuilder()
                .WithInnerCollectionItem(item1)
                .WithInnerCollectionItem(item2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItem(item1)
                .WithInnerCollectionItem(item2)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionSequenceEquals(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_At_Least_One_Of_Corresponding_Inner_Collections_Items_Is_Not_Equal_Using_Default_Equality()
        {
            // fixture setup
            var item1 = new ObjectWithSingleStringProperty();
            var item2 = new ObjectWithSingleStringProperty();
            var item3 = new ObjectWithSingleStringProperty();

            var value = new AnotherTypeWithInnerCollectionBuilder()
                .WithInnerCollectionItem(item1)
                .WithInnerCollectionItem(item2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItem(item1)
                .WithInnerCollectionItem(item3)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionSequenceEquals(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }


        [Test]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        [TestCase(3, 0)]
        [TestCase(0, 3)]
        public void Should_Return_Not_Equal_When_Inner_Collections_Counts_Are_Different(int valueCollectionCount, int otherCollectionCount)
        {
            // fixture setup
            var value = new AnotherTypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(valueCollectionCount)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(otherCollectionCount)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionSequenceEquals(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Is_Null_And_Destination_Inner_Collection_Is_Not_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(2)
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionSequenceEquals(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Source_Inner_Collection_Is_Not_Null_And_Destination_Inner_Collection_Is_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithInnerCollectionBuilder()
                .WithInnerCollectionItemsCount(2)
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionSequenceEquals(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Equal_When_Source_Inner_Collection_Is_Null_And_Destination_Inner_Collection_Is_Null()
        {
            // fixture setup
            var value = new AnotherTypeWithInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            var other = new TypeWithInnerCollectionBuilder()
                .WithNullInnerCollection()
                .Build()
                ;

            // exercise
            var result = value.AsSource().OfLikeness<TypeWithInnerCollection>()
                .WithCollectionSequenceEquals(t => t.Collection, s => s.OtherCollection)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }
    }

}
