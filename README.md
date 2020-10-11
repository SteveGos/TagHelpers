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

**Link**



