using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jmansar.SemanticComparisonExtensions.Test.Builder;
using Jmansar.SemanticComparisonExtensions.Test.TestData;
using NUnit.Framework;
using Ploeh.SemanticComparison;
using Ploeh.SemanticComparison.Fluent;

namespace Jmansar.SemanticComparisonExtensions.Test
{
    [TestFixture]
    public class WithInnerLikenessTests
    {
        [Test] 
        public void Should_Return_Not_Equal_Without_Setting_InnerLikeness()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder().Build();
            var other = new TypeWithInnerTypePropertyBuilder().Build();
            
            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>().Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Equal_When_Setting_InnerLikeness_And_InnerProperty_Equal()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder().Build();
            var other = new TypeWithInnerTypePropertyBuilder().Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Setting_InnerLikeness_But_InnerProperty_Not_Equal()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyValue("value")
                .Build();
            var other = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyValue("value2")
                .Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Use_Inner_Comparison_Overrides()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyValue("value")
                .Build();
            var other = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyValue("value2")
                .Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty, likeness => likeness
                    .With(t => t.StringTypeProperty).EqualsWhen((s, d) => true))
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Equal_When_InnerLikeness_Set_And_Both_Properties_Null()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder().WithNullProperty().Build();
            var other = new TypeWithInnerTypePropertyBuilder().WithNullProperty().Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty)
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }

        [Test]
        public void Should_Return_Not_Equal_When_InnerLikeness_Set_And_Source_Property_Is_Null_And_Destination_Not_Null()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder().WithNullProperty().Build();
            var other = new TypeWithInnerTypePropertyBuilder().WithInnerPropertyValue("test").Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_InnerLikeness_Set_And_Source_Property_Is_Not_Null_And_Destination_Is_Null()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder().Build();
            var other = new TypeWithInnerTypePropertyBuilder().WithNullProperty().Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Be_Able_To_Pass_InnerLikeness_For_Derived_Type()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithString()
                .WithInnerAnotherPropertyValue("another1")
                .WithInnerPropertyValue("value")
                .Build();
            var other = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithString()
                .WithInnerAnotherPropertyValue("another2")
                .WithInnerPropertyValue("value")
                .Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerSpecificLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty, 
                    (Likeness<ObjectWithAnotherStringPropertiesDerived, ObjectWithAnotherStringPropertiesDerived> likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }


        [Test]
        public void Should_Use_InnerComparison_Overrides_When_Derived_Type_InnerLikeness_Set()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithString()
                .WithInnerAnotherPropertyValue("another1")
                .WithInnerPropertyValue("value")
                .Build();
            var other = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithString()
                .WithInnerAnotherPropertyValue("another2")
                .WithInnerPropertyValue("value")
                .Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerSpecificLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty,
                    (Likeness<ObjectWithAnotherStringPropertiesDerived, ObjectWithAnotherStringPropertiesDerived> likeness) => likeness
                        .With(x => x.StringTypePropertyFromDerivedClass)
                            .EqualsWhen((s,d) => s.StringTypePropertyFromDerivedClass == "another1" && d.StringTypePropertyFromDerivedClass == "another2"))
                .Equals(other);

            // verify
            Assert.That(result, Is.True);
        }


        [Test]
        public void Should_Return_Not_Equal_When_Derived_Type_Likeness_Set_And_DestinationProperty_Is_TypeOf_BaseClass()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithString()
                .Build();

            var other = new TypeWithInnerTypePropertyBuilder()
                .Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerSpecificLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty,
                    (Likeness<ObjectWithAnotherStringPropertiesDerived, ObjectWithAnotherStringPropertiesDerived> likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }

        [Test]
        public void Should_Return_Not_Equal_When_Derived_Type_Likeness_Set_And_DestinationProperty_Cannot_Be_Cast_To_Destination_Type_Of_InnerLikeness()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithString()
                .Build();

            var other = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyTypeOfObjectWithInt()
                .Build();

            // exercise sut
            var result = value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerSpecificLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty,
                    (Likeness<ObjectWithAnotherStringPropertiesDerived, ObjectWithAnotherStringPropertiesDerived> likeness) => likeness)
                .Equals(other);

            // verify
            Assert.That(result, Is.False);
        }



        [Test]
        public void Should_Throw_Argument_Exception_When_Derived_Type_Likeness_Set_But_Source_Property_Value_Cannot_Be_Cast_To_Source_Type_Of_InnerLikeness()
        {
            // fixture setup
            var value = new TypeWithInnerTypePropertyBuilder()
                .Build();

            var other = new TypeWithInnerTypePropertyBuilder()
                .WithInnerPropertyValue("something")
                .Build();

            // exercise sut
            var action = new TestDelegate(() => value.AsSource().OfLikeness<TypeWithInnerTypeProperty>()
                .WithInnerSpecificLikeness(t => t.ObjectTypeProperty, s => s.ObjectTypeProperty,
                    (Likeness<ObjectWithAnotherStringPropertiesDerived, ObjectWithAnotherStringPropertiesDerived> likeness) => likeness)
                .Equals(other));

            // verify
            Assert.That(action, Throws.ArgumentException.And.Message.Contains("Source property value is type of 'Jmansar.SemanticComparisonExtensions.Test.TestData.ObjectWithSingleStringProperty', cannot cast to 'Jmansar.SemanticComparisonExtensions.Test.TestData.ObjectWithAnotherStringPropertiesDerived'"));
        }

    }
}
