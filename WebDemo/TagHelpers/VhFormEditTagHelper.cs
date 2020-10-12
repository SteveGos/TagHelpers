// MIT License - See __LICENSE_AND_INFO file for Licensing details and requirements

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

/// <summary>
/// Property Edit Tag Helper. Render property for Edit.
/// </summary>
public class VhFormEditTagHelper : VhFormPropTagHelper, ITagHelper
{
    public VhFormEditTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    public override TagModeEnum TagMode { get; } = TagModeEnum.Edit;
}