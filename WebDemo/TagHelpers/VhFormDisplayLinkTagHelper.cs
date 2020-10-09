using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
 

    /// <summary>
    /// Property Display Tag Helper.  Allows embedding an anchor tag to render a link 
    /// </summary>
    public class VhFormDisplayLinkTagHelper : VhFormPropTagHelper, ITagHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VhFormDisplayTagHelper"/> class.
        /// </summary>
        /// <param name="generator">The generator.</param>
        public VhFormDisplayLinkTagHelper(IHtmlGenerator generator) : base(generator)
        {
        }

        /// <summary>
        /// Gets the tag mode <see cref="TagModeEnum" />.
        /// </summary>
        /// <value>
        /// The tag mode.
        /// </value>
        public override TagModeEnum TagMode { get; } = TagModeEnum.DisplayLink;
    }