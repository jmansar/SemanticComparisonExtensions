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
    public class WithPropertyMapTests
    {
        [Test]
        public void Should_Return_Not_Equal_When_Source_And_Destination_Properties_Are_Not_Equal()
        {
            // fixture setup
            var value = new SimpleObject1Builder()
                .WithIntValue(1)
                .WithStringValue("test1")
                .Build();

            var other = new SimpleObject2Builder()
                .WithIntValue(2)
                .WithStringValue("test2")
                .Build();

            // exercise
            var result = value.AsSource().OfLikeness<SimpleObject2>()
                .WithPropertyMap(t => t.StringValue2, s => s.StringValue1)
                .WithPropertyMap(t => t.IntValue2, s => s.IntValue1)
                .Equals(other)
                ;


            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_One_Of_Source_And_Destination_Properties_Is_Not_Equal()
        {
            // fixture setup
            var value = new SimpleObject1Builder()
                .WithIntValue(1)
                .WithStringValue("test2")
                .Build();

            var other = new SimpleObject2Builder()
                .WithIntValue(2)
                .WithStringValue("test2")
                .Build();

            // exercise
            var result = value.AsSource().OfLikeness<SimpleObject2>()
                .WithPropertyMap(t => t.StringValue2, s => s.StringValue1)
                .WithPropertyMap(t => t.IntValue2, s => s.IntValue1)
                .Equals(other)
                ;


            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Equal_When_Source_And_Destination_Properties_Are_Equal()
        {
            // fixture setup
            var value = new SimpleObject1Builder()
                .WithIntValue(2)
                .WithStringValue("test2")
                .Build();

            var other = new SimpleObject2Builder()
                .WithIntValue(2)
                .WithStringValue("test2")
                .Build();

            // exercise
            var result = value.AsSource().OfLikeness<SimpleObject2>()
                .WithPropertyMap(t => t.StringValue2, s => s.StringValue1)
                .WithPropertyMap(t => t.IntValue2, s => s.IntValue1)
                .Equals(other)
                ;


            // verify
            Assert.That(result, Is.True);
        }
    }
}
