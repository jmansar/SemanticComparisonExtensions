# SemanticComparisonExtensions


## Overview

SemanticComparisonExtensions is a .NET library that provides set of convenience methods that make it easier to compare collections and object hierarchies using [SemanticComparison]. 

#### Methods listing

Method | Target | Description 
--- | --- | ---
WithPropertyMap | Likeness  | Method configures likeness to compare specified source and destination properties to determine objects equality.
[WithCollectionSequenceEquals](#innerCollectionEqualsUsage) | Likeness | Method configures likeness to compare specified source and destination collections items using default equality.
[WithInnerLikeness](#innerLikenessUsage) | Likeness | Methods configures likeness to compare specified source and destination properties using specified inner likeness.
[WithInnerSpecificLikeness](#innerSpecificLikenessUsage) | Likeness | The extended version of WithInnerLikeness. Allows to specify inner likeness for derived types of properties types.
[WithCollectionInnerLikeness](#innerLikenessUsage) | Likeness | Method configures likeness to compare specified source and destination collections using inner likeness for items.
[WithCollectionInnerSpecificLikeness](#innerSpecificLikenessUsage) | Likeness | The extended version of WithCollectionInnerLikeness. Allows to specify inner likeness for derived types of item types.
[CompareCollectionsUsingLikeness](#compareCollectionsUsingLikenessUsage) | IEnumerable\<T\> | Method compares collection with other collection using specified likeness for items.
[CompareCollectionsUsingSpecificLikeness](#compareCollectionsUsingLikenessUsage) | IEnumerable\<T\> | The extended version of CompareCollectionsUsingLikeness. Allows to specify likeness for derived types of item types.

## Download
The nuget package is available in the nuget.org feed.

https://www.nuget.org/packages/SemanticComparisonExtensions
## Usage

Let's say the Invoice and Invoice dto object hierarchies need to be compared in the verification stage of the unit test. 

```csharp
public class Invoice
{
    public string Number { get; set; }
    public IList<InvoiceItem> InvoiceItems { get; set; }
    public Issuer Issuer { get; set; }
}

public class InvoiceItem
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}

public class Issuer
{
    public string Name { get; set; }
    public string Address { get; set; }
}

public class InvoiceDto
{
    public string Number { get; set; }
    public IList<InvoiceItemDto> Items { get; set; }
    public IssuerDto Issuer { get; set; }

}

public class InvoiceItemDto
{
    public string ProductName { get; set; }
    public int Quantity { get; set; }
}

public class IssuerDto
{
    public string Name { get; set; }
    public string Address { get; set; }
}

```
<a name="innerLikenessUsage"></a>
With the semantic comparison extension methods, you can build the likeness object configured to compare all the objects in hierarchy memberwise using the syntax below.

```csharp
invoice.AsSource().OfLikeness<InvoiceDto>()
        .WithInnerLikeness(t => t.Issuer, s => s.Issuer)
        .WithCollectionInnerLikeness(t => t.Items, s => s.InvoiceItems)
        .ShouldEqual(invoiceDto);
```

With the **WithInnerLikeness** and **WithCollectionInnerLikeness** methods you can configure inner likeness for the property or collection items that should be used in the comparison process. If you do need to override default members evaluators you can omit the third parameter (as in the example above).


Example with the custom overrides:
```csharp
invoice.AsSource().OfLikeness<InvoiceDto>()
        .WithInnerLikeness(t => t.Issuer, s => s.Issuer, 
            likeness => likeness.Without(x => x.Address))
        .WithCollectionInnerLikeness(t => t.Items, s => s.InvoiceItems, 
            likeness => likeness.Without(x => x.Quantity))
        .ShouldEqual(invoiceDto);

```
Naturally you can invoke those extension methods on the inner likeness if you need to compare multi level object graph.

#### <a name="compareCollectionsUsingLikenessUsage"></a>Comparing collections using likeness for items.

```csharp
var value = new List<InvoiceItem>();
var other = new List<InvoiceItemDto>();

var result = value.CompareCollectionsUsingLikeness(other, likeness => likeness);

```



#### <a name="innerCollectionEqualsUsage"></a>Comparing inner collection items using default equality.

```csharp
public class Parent
{
    public IEnumerable<int> Numbers { get; set; } 
}
```

The code below will cause Numbers collection to be compared item by item using default equality (Equals method).

```csharp
value.AsSource().OfLikeness<Root>()
        .WithCollectionSequenceEquals(r => r.Numbers)
        .ShouldEqual(other);
```

#### <a name="innerSpecificLikenessUsage"></a>Comparing inner items using inner likeness for derived types.

Sometimes the class definition contains inner properties that are base types. By default WithInnerLikeness and WithCollectionInnerLikeness methods infer likeness generic type parameters from the property picker lambda expression. So, if you assign objects that inherit from the base class defined in the parent class definition the constructed likeness will be base class likeness, that doesn't include fields from the derived class.  

To compare objects using likeness that operates on derived classes you need to specify those derived classes explicitly. There are separate versions of the extension methods for that purpose: **WithInnerSpecificLikeness**, **WithCollectionInnerSpecificLikeness**.

```csharp
public abstract class InnerBase
{
    
}

public class Inner : InnerBase
{
    public string Text { get; set; }
}

public class Parent
{
    public InnerBase Inner { get; set; }
}
```

```csharp
parent.AsSource().OfLikeness<Parent>()
        .WithInnerSpecificLikeness(t => t.Inner, s => s.Inner, (Likeness<Inner, Inner> likeness) => likeness)
        .ShouldEqual(other);
```

## Additional features
### Logging
The extensions methods provide diagnostic messages that are meant to help identify the exact item that is equal/not equal in the object graph. By default the logging is performed to the trace output. However, you can provide your own implementation of the message writer by implementing **IDiagnosticsWriter** interface and assigning it to the **DiagnosticsWriterLocator.DiagnosticsWriter**.

[SemanticComparison]:http://www.nuget.org/packages/SemanticComparison
