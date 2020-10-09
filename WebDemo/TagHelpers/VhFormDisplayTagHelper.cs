using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

/// <summary>
/// Property Display Tag Helper.  Render property for Display.
/// </summary>
public class VhFormDisplayTagHelper : VhFormPropTagHelper, ITagHelper
{
    /// <summary>
    /// Initializes a new instance of the <see cref="VhFormDisplayTagHelper"/> class.
    /// </summary>
    /// <param name="generator">The generator.</param>
    public VhFormDisplayTagHelper(IHtmlGenerator generator) : base(generator)
    {
    }

    /// <summary>
    /// Gets the tag mode <see cref="TagModeEnum" />.
    /// </summary>
    /// <value>
    /// The tag mode.
    /// </value>
    public override TagModeEnum TagMode { get; } = TagModeEnum.Display;
}