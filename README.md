### View Helper (Vh) - Tag Helper

Tag Helper for displaying and editing of model properties.  The goal is to simplify the rendering of properties and 
keep continuity in the rendering of such properties so that a common UI look and feel is established.


**With the tag helper**

```html
@model MyDomain.Project

<div class="row form-horizontal">
    <vh-form-display asp-for="@Model.Invoice.Name" bs-Col="@VhCol.md6"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.StoreBranch" bs-Col="@VhCol.md6"></vh-form-display>

    <vh-form-display asp-for="@Model.Invoice.Amount" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.Tax" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.InvoiceStatus" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.PaymentOptions" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.IsPaid" bs-Col="@VhCol.md2"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.IsOverDue" bs-Col="@VhCol.md4"></vh-form-display>
</div>
```

**Without the tag helper**

```html
@model MyDomain.Project

<div class="row form-horizontal">

    <div class="form-group col-6">
        <label asp-for="@Model.Description" class="control-label"></label>
        <div class="border bg-light rounded">@Model.Description</div>
    </div>

    <div class="form-group col-6">
        <label asp-for="@Model.Location" class="control-label"></label>
        <div class="border bg-light rounded">@Model.Location</div>
    </div>

    <div class="form-group col-12">
        <label asp-for="@Model.ProjectManager" class="control-label"></label>
        <div class="border bg-light rounded">@Model.Name</div>
    </div>

    <div class="form-group col-12">
        <label asp-for="@Model.AdressLine1" class="control-label"></label>
        <div class="border bg-light rounded">@Model.AdressLine1</div>
    </div>

    <div class="form-group col-12">
        <label asp-for="@Model.AdressLine2" class="control-label"></label>
        <div class="border bg-light rounded">@Model.AdressLine2</div>
    </div>

    <div class="form-group col-12">
        <label asp-for="@Model.CityStateZip" class="control-label"></label>
        <div class="border bg-light rounded">@Model.CityStateZip</div>
    </div>

    <div class="form-group col-12">
        <label asp-for="@Model.Country" class="control-label"></label>
        <div class="border bg-light rounded">@Model.Country</div>
    </div>

</div>
```


#### Available Tag Helpers

**Display**

```html
@model MyDomain.Project

<div class="row form-horizontal">
    <vh-form-display asp-for="@Model.Invoice.Name" bs-Col="@VhCol.md6"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.StoreBranch" bs-Col="@VhCol.md6"></vh-form-display>

    <vh-form-display asp-for="@Model.Invoice.Amount" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.Tax" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.InvoiceStatus" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.PaymentOptions" bs-Col="@VhCol.md3"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.IsPaid" bs-Col="@VhCol.md2"></vh-form-display>
    <vh-form-display asp-for="@Model.Invoice.IsOverDue" bs-Col="@VhCol.md4"></vh-form-display>
</div>
```

**Edit**

```html
@model MyDomain.Project

<div class="row form-horizontal">
    <vh-form-edit asp-for="@Model.Invoice.Name" bs-Col="@VhCol.md6"></vh-form-edit>
    <vh-form-edit asp-for="@Model.Invoice.StoreBranch" bs-Col="@VhCol.md6"></vh-form-edit>

    <vh-form-edit asp-for="@Model.Invoice.Amount" bs-Col="@VhCol.md3"></vh-form-edit>
    <vh-form-edit asp-for="@Model.Invoice.Tax" bs-Col="@VhCol.md3"></vh-form-edit>
    <vh-form-edit asp-for="@Model.Invoice.InvoiceStatus" bs-Col="@VhCol.md3"></vh-form-edit>
    <vh-form-edit asp-for="@Model.Invoice.PaymentOptions" bs-Col="@VhCol.md3"></vh-form-edit>
    <vh-form-edit asp-for="@Model.Invoice.IsPaid" bs-Col="@VhCol.md2"></vh-form-edit>
    <vh-form-edit asp-for="@Model.Invoice.IsOverDue" bs-Col="@VhCol.md4"></vh-form-edit>
</div>
```

**Link - Display Property which is also a link**


```html
<div class="row form-horizontal">
    <vh-form-display-link asp-for="@Model.ProjectName" label-Override="Project">
        <a asp-page="/Index" asp-area="" asp-route-id="@Model.ProjectId" />
    </vh-form-display-link>
</div>
```

**Available Attributes - From `VhFormPropTagHelper.cs`**

```csharp
// HTML Attributes

/// <summary>
/// Gets or sets for expression.
/// </summary>
/// <value>
/// For.
/// </value>
[HtmlAttributeName(_forAttributeName)]
public ModelExpression For { get; set; }

/// <summary>
/// Gets or sets the Bootstrap column.
/// </summary>
/// <value>
/// The Bootstrap Column
/// </value>
[HtmlAttributeName(_bsCol)]
public string BsCol { get; set; }

/// <summary>
/// The boolean true Display Value if not Check Box.
/// </summary>
[HtmlAttributeName(_boolTrue)]
public string BoolTrue { get; set; } // =  "Yes";

/// <summary>
/// The boolean false Display Value if not Check Box.
/// </summary>
[HtmlAttributeName(_boolFalse)]
public string BoolFalse { get; set; } // = "No";

/// <summary>
/// The boolean null Display Value if not Check Box.
/// </summary>
[HtmlAttributeName(_boolNull)]
public string BoolNull { get; set; } // = "n/a";

/// <summary>
/// Gets or sets the enumeration selection list.
/// </summary>
/// <value>
/// The enumeration selection list.
/// </value>
[HtmlAttributeName(_enumSelList)]
public IEnumerable<SelectListItem> EnumSelList { get; set; }

/// <summary>
/// Gets the Tag Helper HTML generator.
/// </summary>
/// <value>
/// The Tag Helper HTML generator.
/// </value>
public IHtmlGenerator Generator { get; }
```