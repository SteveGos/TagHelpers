# View Helper (Vh) - Tag Helper

Tag Helper for displaying and editing of model properties.


**With the tag helper**

```html
@model MyDomain.Project

<div class="row form-horizontal">
    <vh-form-display asp-for="@Model.Description" bsCol="@VhCol.md6"></vh-form-display>
    <vh-form-display asp-for="@Model.Location" bsCol="@VhCol.md6"></vh-form-display>
    <vh-form-display asp-for="@Model.ProjectManager"></vh-form-display>
    <vh-form-display asp-for="@Model.AdressLine1"></vh-form-display>
    <vh-form-display asp-for="@Model.AdressLine2"></vh-form-display>
    <vh-form-display asp-for="@Model.CityStateZip"></vh-form-display>
    <vh-form-display asp-for="@Model.Country"></vh-form-display>
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