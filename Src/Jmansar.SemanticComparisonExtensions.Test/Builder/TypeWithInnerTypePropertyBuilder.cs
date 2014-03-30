using Jmansar.SemanticComparisonExtensions.Test.TestData;

namespace Jmansar.SemanticComparisonExtensions.Test.Builder
{
    public class TypeWithInnerTypePropertyBuilder
    {
        private string _innerPropertyValue = "value";
        private string _innerAnotherPropertyValue = "another";
        private bool _innerPropertyNull = false;
        private bool _innerPropertyTypeOfObjectWithString;
        private bool _innerPropertyTypeOfObjectWithInt;

        private ObjectWithSingleStringProperty _inner = new ObjectWithSingleStringProperty()
        {
            StringTypeProperty = "value"
        };


        public TypeWithInnerTypePropertyBuilder WithNullProperty()
        {
            _innerPropertyNull = true;
            return this;
        }

        public TypeWithInnerTypePropertyBuilder WithInnerPropertyTypeOfObjectWithString()
        {
            _innerPropertyTypeOfObjectWithString = true;
            return this;
        }

        public TypeWithInnerTypePropertyBuilder WithInnerPropertyTypeOfObjectWithInt()
        {
            _innerPropertyTypeOfObjectWithInt = true;
            return this;
        }

        public TypeWithInnerTypePropertyBuilder WithInnerPropertyValue(string value)
        {
            _innerPropertyValue = value;
            return this;
        }

        public TypeWithInnerTypePropertyBuilder WithInnerAnotherPropertyValue(string value)
        {
            _innerAnotherPropertyValue = value;
            return this;
        }


        public TypeWithInnerTypeProperty Build()
        {
            var result = new TypeWithInnerTypeProperty();
            if (_innerPropertyNull)
            {
                return result;
            }

            if (_innerPropertyTypeOfObjectWithString)
            {
                result.ObjectTypeProperty = new ObjectWithAnotherStringPropertiesDerived
                {
                    StringTypePropertyFromDerivedClass = _innerAnotherPropertyValue
                };
            }
            else if (_innerPropertyTypeOfObjectWithInt)
            {
                result.ObjectTypeProperty = new ObjectWithAnotherIntPropertyDerived();
            }
            else
            {
                result.ObjectTypeProperty = new ObjectWithSingleStringProperty();
            }

            result.ObjectTypeProperty.StringTypeProperty = _innerPropertyValue;
            return result;

        }
    }
}
