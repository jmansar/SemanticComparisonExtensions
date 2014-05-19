# SemanticComparisonExtensions


## Overview

SemanticComparisonExtensions is a .NET library that make it easier to compare object hierarchies using [SemanticComparison]. The library provides set of extensions methods for the Likeness type that can configure the likeness to operate on inner properties and collections.


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

With the semantic comparison extension methods, you can build the likeness object configured to compare all the objects in hierarchy memberwise using the syntax below.

```csharp
invoice.AsSource().OfLikeness<InvoiceDto>()
        .WithInnerLikeness(t => t.Issuer, s => s.Issuer)
        .WithCollectionInnerLikeness(t => t.Items, s => s.InvoiceItems)
        .ShouldEqual(invoiceDto);
```

With the WithInnerLikeness and WithCollectionInnerLikeness methods you can configure inner likeness for the property or collection items that should be used in the comparison process. If you do need to override default members evaluators you can omit the third parameter (as in the example above).


Example with the custom overrides:
```csharp
invoice.AsSource().OfLikeness<InvoiceDto>()
        .WithInnerLikeness(t => t.Issuer, s => s.Issuer, 
            likeness => likeness.Without(x => x.Address))
        .WithCollectionInnerLikeness(t => t.Items, s => s.InvoiceItems, 
            likeness => likeness.Without(x => x.Quantity))
        .ShouldEqual(invoiceDto);

```




[SemanticComparison]:http://www.nuget.org/packages/SemanticComparison
