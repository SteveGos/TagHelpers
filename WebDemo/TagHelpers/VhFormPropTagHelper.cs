using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VhTagHelperAttributes;
using WebDemo.Attributes;

// See Application specific logic.
// -- Has custom formatting for properties with Attributes [ComputedAttribute] and [ComputedOptionallyAttribute]

/// <summary>
/// Property render Tag Helper - abstract.
/// </summary>
public abstract class VhFormPropTagHelper : TagHelper
{
    // HTML Attribute Names
    private const string _forAttributeName = "asp-for";

    private const string _bsCol = "bs-Col";
    private const string _boolTrue = "bool-True";
    private const string _boolFalse = "bool-False";
    private const string _boolNull = "bool-Null";

    private const string _enumSelList = "enumSelList";

    // Class Append Attributes

    private const string _classBsColumnWrapper = "classBsColumnWrapper"; // Column Wrapper
    private const string _classInput = "class-Input-Display"; // Property Input/Display
    private const string _classLabel = "class-Label"; // Label
    private const string _classValidate = "class-Validate"; // Validate

    // Label Override
    private const string _labelOverride = "label-Override";

    // No Label - Do not write a label
    private const string _noLabel = "no-Label";

    // No Wrapper - Do not wrap in bootstrap column
    private const string _inLine = "in-Line";

    // Boolean Defaults...

    private const string boolTrueDefault = "Yes";
    private const string boolFalseDefault = "No";
    private const string boolNullDefault = "n/a";

    // Tag Mode...

    private TagModeEnum procesTageMode;

    /// <summary>
    /// Gets or sets the Tag Helper view context.
    /// </summary>
    /// <value>
    /// The view context.
    /// </value>
    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    // HTML Attributes

    /// <summary>
    /// Gets or sets the value for appending to the bootstrap column wrapper div tag.
    /// </summary>
    /// <value>
    /// Value to append to the Boot Strap Column
    /// </value>
    [HtmlAttributeName(_classBsColumnWrapper)]
    public string ClassBsColl { get; set; }

    /// <summary>
    /// Gets or sets data to append to the Class for the Input (input)/Display (div) tag.
    /// </summary>
    /// <value>
    /// Data to append to the Class for the Input/Display field.
    /// </value>
    [HtmlAttributeName(_classInput)]
    public string ClassInput { get; set; }

    /// <summary>
    /// Gets or sets data to append to the Class for the Label div tag.
    /// </summary>
    /// <value>
    /// Data to append to the Class for the Label field.
    /// </value>
    [HtmlAttributeName(_classLabel)]
    public string ClassLabel { get; set; }

    /// <summary>
    /// Gets or sets data to append to the Class for the Validate div tag.
    /// </summary>
    /// <value>
    /// The class label.
    /// </value>
    [HtmlAttributeName(_classValidate)]
    public string ClassValidate { get; set; }

    /// <summary>
    /// Gets or sets a value for the label.  Overrides the property display name.
    /// </summary>
    /// <value>
    /// The label to override the property display name with.
    /// </value>
    [HtmlAttributeName(_labelOverride)]
    public string LabelOverride { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to not display a label.  If <c>true</c> no label is rendered..
    /// </summary>
    /// <value>
    ///   <c>true</c> if [no label]; otherwise, <c>false</c>.
    /// </value>
    [HtmlAttributeName(_noLabel)]
    public bool NoLabel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to render the wrapper tag with d-inline and without a default bootstrap column.
    /// </summary>
    /// <value>
    ///   <c>true</c> renders wrapper d-inline without column;.
    /// </value>
    [HtmlAttributeName(_inLine)]
    public bool InLine { get; set; }

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
    /// Gets or sets the selection list.
    /// </summary>
    /// <value>
    /// The selection list.
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

    // Override Properties

    /// <summary>
    /// Gets the tag mode <see cref="TagModeEnum"/>.
    /// </summary>
    /// <value>
    /// The tag mode.
    /// </value>
    public abstract TagModeEnum TagMode { get; }

    // Constructor

    /// <summary>
    /// Initializes a new instance of the <see cref="VhFormPropTagHelper"/> class.
    /// </summary>
    /// <param name="generator">The generator.</param>
    public VhFormPropTagHelper(IHtmlGenerator generator)
    {
        Generator = generator;
    }

    /// <summary>
    /// Asynchronously executes the <see cref="T:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper" /> with the given <paramref name="context" /> and
    /// <paramref name="outputTagHelper" />.
    /// </summary>
    /// <param name="context">Contains information associated with the current HTML tag.</param>
    /// <param name="outputTagHelper">A HTML element used to generate an HTML tag.</param>
    /// <remarks>
    /// By default this calls into <see cref="M:Microsoft.AspNetCore.Razor.TagHelpers.TagHelper.Process(Microsoft.AspNetCore.Razor.TagHelpers.TagHelperContext,Microsoft.AspNetCore.Razor.TagHelpers.TagHelperOutput)" />.
    /// </remarks>
    public override async Task ProcessAsync(
        TagHelperContext context,
        TagHelperOutput outputTagHelper)
    {
        // **************************************
        // Variable
        // **************************************

        var isNullable = false;
        var isboolean = false;
        var isGenericType = For.Metadata.ModelType.IsGenericType;
        VhModelType = For.Metadata.ModelType;

        if (isGenericType)
        {
            isNullable = For.Metadata.ModelType.GetGenericTypeDefinition() == typeof(Nullable<>);

            if (isNullable)
            {
                VhModelType = Nullable.GetUnderlyingType(For.Metadata.ModelType);
                isboolean = VhModelType == typeof(bool);
            }
        }
        else
        {
            isboolean = VhModelType == typeof(bool);
        }

        var isEnum = VhModelType.IsEnum;

        VhTemplateTypeEnum vhTemplateType;
        var booleanOption = VhBooleanRenderEnum.None;

        if (isboolean)
        {
            booleanOption = isboolean ? (isNullable ? VhBooleanRenderEnum.MultiStateRadio : VhBooleanRenderEnum.CheckBox) : VhBooleanRenderEnum.None;

            vhTemplateType = VhTemplateTypeEnum.VhBool;
        }
        else if (isEnum)
        {
            vhTemplateType = VhTemplateTypeEnum.VhEnum;
        }
        else
        {
            vhTemplateType = VhTemplateTypeEnum.VhProp;
        }

        // **************************************
        //  Class Append Attributes - set to empty string if null
        // **************************************

        ClassBsColl ??= string.Empty;
        ClassInput ??= string.Empty;
        ClassLabel ??= string.Empty;
        ClassValidate ??= string.Empty;
        LabelOverride ??= string.Empty;

        // **************************************
        // Set Classes
        // **************************************

        // Boolean Displays - Use Order:
        //   1) From Tag Helper on Page/View
        //   2) VhBooleanAttribute
        //   3) VfDefaults
        // --------------------------------------------------------------

        var oType = For.ModelExplorer.Metadata.ContainerMetadata.ModelType;
        var attrs = oType?.GetProperty(For.ModelExplorer.Metadata.Name)?.GetCustomAttributes(typeof(VhBooleanAttribute), false);
        var vhBooleanAttribute = (VhBooleanAttribute)attrs?.FirstOrDefault();

        // Use Page/View Inputs
        var boolTrue = (BoolTrue ?? string.Empty).ToString();
        var boolFalse = (BoolFalse ?? string.Empty).ToString();
        var boolNull = (BoolNull ?? string.Empty).ToString();

        // If property has  vhBooleanAttribute -- then use attribute if not set from Page/View Inputs
        if (vhBooleanAttribute != null)
        {
            if (string.IsNullOrWhiteSpace(boolTrue) && !string.IsNullOrWhiteSpace(vhBooleanAttribute.BooleanTrue))
            {
                boolTrue = vhBooleanAttribute.BooleanTrue;
            }

            if (string.IsNullOrWhiteSpace(boolFalse) && !string.IsNullOrWhiteSpace(vhBooleanAttribute.BooleanFalse))
            {
                boolTrue = vhBooleanAttribute.BooleanTrue;
            }

            if (string.IsNullOrWhiteSpace(boolNull) && !string.IsNullOrWhiteSpace(vhBooleanAttribute.BooleanNull))
            {
                boolTrue = vhBooleanAttribute.BooleanTrue;
            }
        }

        // Use Defaults if not set from viewBag or [VhBooleanAttribute]
        BoolTrue = string.IsNullOrWhiteSpace(boolTrue) ? boolTrueDefault : boolTrue;
        BoolFalse = string.IsNullOrWhiteSpace(boolFalse) ? boolFalseDefault : boolFalse;
        BoolNull = string.IsNullOrWhiteSpace(boolNull) ? boolNullDefault : boolNull;

        // Display String
        var displayString = For.Model?.ToString();

        procesTageMode = TagMode;

        // -------------------------------------------------
        // -------------------------------------------------
        // Start Application specific logic.
        // -------------------------------------------------
        // -------------------------------------------------

        attrs = oType?.GetProperty(For.ModelExplorer.Metadata.Name)?.GetCustomAttributes(typeof(ComputedAttribute), false);
        var computedAttribute = (ComputedAttribute)attrs?.FirstOrDefault();

        attrs = oType?.GetProperty(For.ModelExplorer.Metadata.Name)?.GetCustomAttributes(typeof(ComputedOptionallyAttribute), false);
        var computedOptionallyAttribute = (ComputedOptionallyAttribute)attrs?.FirstOrDefault();

        var isComputed = computedAttribute != null;
        var isComputedOptionally = computedOptionallyAttribute != null;
        //isQualChar = qualCharAttribute != null;

        if (isComputed || isComputedOptionally)
        {
            switch (vhTemplateType)
            {
                case VhTemplateTypeEnum.VhBool:
                    break;

                case VhTemplateTypeEnum.VhEnum:
                case VhTemplateTypeEnum.VhProp:

                    if (isComputed)
                    {
                        ClassInput = $"{VhClass.Computed} {ClassInput}";
                    }
                    else
                    {
                        ClassInput = $"{VhClass.ComputedOptionally} {ClassInput}";
                    }

                    break;

                default:
                    break;
            }
        }

        // -------------------------------------------------
        // -------------------------------------------------
        // END - Application specific logic.
        // -------------------------------------------------
        // -------------------------------------------------

        switch (vhTemplateType)
        {
            case VhTemplateTypeEnum.VhEnum:

                if (For.Model != null)
                {
                    static string GetDisplayName(Enum enumValue)
                    {
                        string retValue = null;

                        var output = enumValue.ToString();

                        var memberArray = enumValue.GetType().GetMember(output);

                        // if in Enumeration set VhDisplayString
                        if (memberArray.Any())
                        {
                            var member = enumValue.GetType().GetMember(output).First();
                            var attributes = member.GetCustomAttributes(typeof(DisplayAttribute), false);

                            // If Display Attribute Found - Set to Display Name
                            if (attributes.Any())
                            {
                                var attr = (DisplayAttribute)attributes[0];
                                retValue = attr.Name;
                            }

                            // If DisplayAttribute Name isn't set - use ENUM Value
                            return string.IsNullOrWhiteSpace(retValue) ? enumValue.ToString() : retValue;
                        }

                        // ENUM Value wasn't a valid enumeration so returning null.
                        return retValue;
                    }

                    displayString = GetDisplayName(For.Model as Enum);
                }

                break;

            case VhTemplateTypeEnum.VhProp:

                if (For.Model != null && !string.IsNullOrWhiteSpace(For.Metadata.DisplayFormatString))
                {
                    try
                    {
                        var formattedString = string.Format(For.Metadata.DisplayFormatString, For.Model);
                        displayString = formattedString;
                    }
                    catch (Exception)
                    {
                        //throw new Exception($"Invalid Format String. {formatStr} for property {propertyName}.", ex);
                        displayString = "Bad Format String";
                    }
                }
                else
                {
                    displayString = For.Model?.ToString();
                }

                break;

            default:

                break;
        }

        // Link

        // Generate HTML

        // Replace this parent tag helper with div tags wrapping the entire form block
        outputTagHelper.TagName = "div";

        // Append / Adjust Outer Bootstrap Column Div as needed
        string classAttr;

        string classAttrNoWrapper = string.Empty;

        //if (NoWrapper)
        //{
        //    classAttrNoWrapper = "d-inline";
        //}

        string wrapperClass;

        if (InLine)
        {
            wrapperClass = $"{VhClass.DefaultWrapperInlineClass} {BsCol} {ClassBsColl}";
        }
        else
        {
            wrapperClass = $"{VhClass.DefaultWrapperClass} {BsCol} {ClassBsColl}";
        }

        if (vhTemplateType == VhTemplateTypeEnum.VhBool && booleanOption == VhBooleanRenderEnum.CheckBox)
        {
            // align-self-center
            classAttr = RemoveDupAttributes($"{wrapperClass} {VhClass.AlignSelfCenter}");
        }
        else
        {
            classAttr = RemoveDupAttributes($"{wrapperClass}");
        }

        outputTagHelper.Attributes.SetAttribute("class", classAttr);

        // Generate Inner HTML

        await GenerateHtml(context, outputTagHelper, isNullable, vhTemplateType, booleanOption, displayString);
    }

    private async Task GenerateHtml(
        TagHelperContext context,
        TagHelperOutput outputTagHelper,
        bool isNullable,
        VhTemplateTypeEnum vhTemplateType,
        VhBooleanRenderEnum booleanOption,
        string displayString)
    {
        switch (procesTageMode)
        {
            case TagModeEnum.DisplayLink:

                TagHelperContent innerTagHelperContent = outputTagHelper.GetChildContentAsync().Result;
                var htmlString = innerTagHelperContent.GetContent();
                ProperyLinkTagBuilder = GetProperyLinkTagBuilder(htmlString);

                break;

            case TagModeEnum.Display:
            case TagModeEnum.Edit:
            default:
                break;
        }

        switch (vhTemplateType)
        {
            case VhTemplateTypeEnum.VhBool:

                await GenerateBoolean(context, outputTagHelper, booleanOption, isRequired: For.Metadata.IsRequired);
                break;

            case VhTemplateTypeEnum.VhEnum:

                await GenerateEnum(context, outputTagHelper, displayString, isNullable);
                break;

            case VhTemplateTypeEnum.VhProp:

                await GenerateProp(context, outputTagHelper, displayString);
                break;

            default:

                await GenerateOther(context, outputTagHelper, displayString);
                break;
        }
    }

    //  Private Methods, Properties, ENUMS, Classes

    #region Private Methods, Properties, ENUMS, Classes

    // Properties

    private TagBuilder ProperyLinkTagBuilder { get; set; }

    private Type VhModelType { get; set; }

    // Generate HTML

    private async Task GenerateBoolean(
        TagHelperContext context,
        TagHelperOutput outputTagHelper,
        VhBooleanRenderEnum booleanOption,
        bool isRequired)
    {
        TagHelperOutput labelElement;
        TagHelperOutput inputElement;
        TagHelperOutput validationMessageElement;

        outputTagHelper.Content.Clear();

        if (booleanOption == VhBooleanRenderEnum.CheckBox)
        {
            labelElement = await CreateCheckboxLabelElement(context, appendClass: ClassLabel);
            inputElement = await CreateCheckBoxElement(context);

            IHtmlContent innerDiv;

            switch (procesTageMode)
            {
                case TagModeEnum.DisplayLink:
                case TagModeEnum.Display:

                    inputElement.Attributes.RemoveAll("disabled");
                    inputElement.Attributes.Add(new TagHelperAttribute("disabled"));

                    if (ProperyLinkTagBuilder != null)
                    {
                        ProperyLinkTagBuilder.InnerHtml.Clear();
                        ProperyLinkTagBuilder.InnerHtml.AppendHtml(labelElement.Content.GetContent());

                        labelElement.Content.Clear();

                        using var writer = new System.IO.StringWriter();
                        ProperyLinkTagBuilder.WriteTo(writer, HtmlEncoder.Default);
                        labelElement.Content.AppendHtml(writer.ToString());
                    }

                    innerDiv = WrapElementsWithDiv(
                        new List<IHtmlContent>()
                        {
                                    inputElement,
                                    labelElement
                        },
                        VhClass.DefaultCheckboxCustomClass
                    );

                    outputTagHelper.Content.AppendHtml(innerDiv);

                    break;

                case TagModeEnum.Edit:

                    validationMessageElement = await CreateValidationMessageElement(context, appendClass: ClassValidate);

                    innerDiv = WrapElementsWithDiv(
                        new List<IHtmlContent>()
                        {
                                inputElement,
                                labelElement,
                                validationMessageElement
                        },
                        VhClass.DefaultCheckboxCustomClass
                    );

                    outputTagHelper.Content.AppendHtml(innerDiv);

                    break;

                default:
                    break;
            }

            return;
        }

        if (booleanOption == VhBooleanRenderEnum.MultiStateRadio)
        {
            if (!NoLabel)
            {
                // Manually new-up each child asp form tag helper element
                labelElement = await CreateLabelElement(context, appendClass: ClassLabel);

                if (ProperyLinkTagBuilder != null)
                {
                    ProperyLinkTagBuilder.InnerHtml.Clear();
                    ProperyLinkTagBuilder.InnerHtml.AppendHtml(labelElement.Content.GetContent());

                    labelElement.Content.Clear();

                    using var writer = new System.IO.StringWriter();
                    ProperyLinkTagBuilder.WriteTo(writer, HtmlEncoder.Default);
                    labelElement.Content.AppendHtml(writer.ToString());
                }

                outputTagHelper.Content.AppendHtml(labelElement);

                outputTagHelper.Content.AppendHtml("<br />");
            }

            string guid;

            // TRUE BUTTON

            guid = Guid.NewGuid().ToString();

            inputElement = await CreateRadioButtonElement(context, boolValue: true, idAttr: guid);
            inputElement.Attributes.RemoveAll("checked");

            if (For.Model != null)
            {
                if ((bool)For.Model == true)
                {
                    inputElement.Attributes.Add(new TagHelperAttribute("checked"));
                }
            }

            labelElement = await CreateRadioButtonlLabelElement(context, idAttr: guid, displayNameString: BoolTrue);

            switch (procesTageMode)
            {
                case TagModeEnum.DisplayLink:
                case TagModeEnum.Display:

                    inputElement.Attributes.RemoveAll("disabled");
                    inputElement.Attributes.Add(new TagHelperAttribute("disabled"));

                    break;

                case TagModeEnum.Edit:
                default:
                    break;
            }

            if (ProperyLinkTagBuilder != null)
            {
                ProperyLinkTagBuilder.InnerHtml.Clear();
                ProperyLinkTagBuilder.InnerHtml.AppendHtml(labelElement.Content.GetContent());

                labelElement.Content.Clear();

                using var writer = new System.IO.StringWriter();
                ProperyLinkTagBuilder.WriteTo(writer, HtmlEncoder.Default);
                labelElement.Content.AppendHtml(writer.ToString());
            }

            var innerDivTrue = WrapElementsWithDiv(
                new List<IHtmlContent>()
                {
                    inputElement,
                    labelElement
                },
                VhClass.DefaultRadioButtonCustomClass
            );

            outputTagHelper.Content.AppendHtml(innerDivTrue);

            // FALSE BUTTON

            guid = Guid.NewGuid().ToString();

            inputElement = await CreateRadioButtonElement(context, boolValue: false, idAttr: guid);

            inputElement.Attributes.RemoveAll("checked");

            if (For.Model != null)
            {
                if ((bool)For.Model == false)
                {
                    inputElement.Attributes.Add(new TagHelperAttribute("checked"));
                }
            }

            labelElement = await CreateRadioButtonlLabelElement(context, idAttr: guid, displayNameString: BoolFalse);

            switch (procesTageMode)
            {
                case TagModeEnum.DisplayLink:
                case TagModeEnum.Display:

                    inputElement.Attributes.RemoveAll("disabled");
                    inputElement.Attributes.Add(new TagHelperAttribute("disabled"));

                    break;

                case TagModeEnum.Edit:
                default:
                    break;
            }

            var innerDivFalse = WrapElementsWithDiv(
                new List<IHtmlContent>()
                {
                    inputElement,
                    labelElement
                },
                VhClass.DefaultRadioButtonCustomClass
            );

            outputTagHelper.Content.AppendHtml("&nbsp;&nbsp;");

            outputTagHelper.Content.AppendHtml(innerDivFalse);

            // NULL BUTTON
            if (!isRequired)
            {
                guid = Guid.NewGuid().ToString();

                inputElement = await CreateRadioButtonElement(context, boolValue: true, idAttr: guid);

                inputElement.Attributes.RemoveAll("checked");

                if (For.Model == null)
                {
                    inputElement.Attributes.Add(new TagHelperAttribute("checked"));
                }

                labelElement = await CreateRadioButtonlLabelElement(context, idAttr: guid, displayNameString: BoolNull);

                switch (procesTageMode)
                {
                    case TagModeEnum.DisplayLink:
                    case TagModeEnum.Display:

                        inputElement.Attributes.RemoveAll("disabled");
                        inputElement.Attributes.Add(new TagHelperAttribute("disabled"));

                        break;

                    case TagModeEnum.Edit:
                    default:
                        break;
                }

                var innerDivNull = WrapElementsWithDiv(
                    new List<IHtmlContent>()
                    {
                        inputElement,
                        labelElement
                    },
                    VhClass.DefaultRadioButtonCustomClass
                );

                outputTagHelper.Content.AppendHtml("&nbsp;&nbsp;");

                outputTagHelper.Content.AppendHtml(innerDivNull);
            }
        }
    }

    private async Task GenerateOther(
        TagHelperContext context,
        TagHelperOutput outputTagHelper,
        string displayString)
    {
        TagHelperOutput labelElement;
        TagHelperOutput inputElement;
        TagHelperOutput validationMessageElement;

        outputTagHelper.Content.Clear();

        if (!NoLabel)
        {
            // Manually new-up each child asp form tag helper element
            labelElement = await CreateLabelElement(context, appendClass: ClassLabel);
            outputTagHelper.Content.AppendHtml(labelElement);
        }

        switch (procesTageMode)
        {
            case TagModeEnum.DisplayLink:
            case TagModeEnum.Display:

                var htmlContent = CreateDisplayElement(Guid.NewGuid().ToString(), displayString, appendClass: ClassInput);
                outputTagHelper.Content.AppendHtml(htmlContent);

                break;

            case TagModeEnum.Edit:

                inputElement = await CreateInputElement(context, appendClass: ClassInput);
                outputTagHelper.Content.AppendHtml(inputElement);

                validationMessageElement = await CreateValidationMessageElement(context, appendClass: ClassValidate);
                outputTagHelper.Content.AppendHtml(validationMessageElement);

                break;

            default:
                break;
        }
    }

    private async Task GenerateProp(
        TagHelperContext context,
        TagHelperOutput outputTagHelper,
        string displayString)
    {
        TagHelperOutput labelElement;
        TagHelperOutput inputElement;
        TagHelperOutput validationMessageElement;

        outputTagHelper.Content.Clear();

        if (!NoLabel)
        {
            // Manually new-up each child asp form tag helper element
            labelElement = await CreateLabelElement(context, appendClass: ClassLabel);
            outputTagHelper.Content.AppendHtml(labelElement);
        }

        TagBuilder htmlContent;

        switch (procesTageMode)
        {
            case TagModeEnum.DisplayLink:
            case TagModeEnum.Display:

                if (ProperyLinkTagBuilder != null)
                {
                    ProperyLinkTagBuilder.InnerHtml.Append(displayString);

                    htmlContent = CreateDisplayElement(Guid.NewGuid().ToString(), string.Empty, appendClass: ClassInput);

                    htmlContent.InnerHtml.AppendHtml(ProperyLinkTagBuilder);
                    outputTagHelper.Content.AppendHtml(htmlContent);
                }
                else
                {
                    htmlContent = CreateDisplayElement(Guid.NewGuid().ToString(), displayString, appendClass: ClassInput);
                    outputTagHelper.Content.AppendHtml(htmlContent);
                }

                break;

            case TagModeEnum.Edit:

                inputElement = await CreateInputElement(context, appendClass: ClassInput);
                outputTagHelper.Content.AppendHtml(inputElement);

                validationMessageElement = await CreateValidationMessageElement(context, appendClass: ClassValidate);
                outputTagHelper.Content.AppendHtml(validationMessageElement);

                break;

            default:
                break;
        }
    }

    private async Task GenerateEnum(
        TagHelperContext context,
        TagHelperOutput outputTagHelper,
        string displayString,
        bool isNullable)
    {
        TagHelperOutput labelElement;
        TagHelperOutput inputElement;
        TagHelperOutput validationMessageElement;

        outputTagHelper.Content.Clear();

        if (!NoLabel)
        {// Manually new-up each child asp form tag helper element
            labelElement = await CreateLabelElement(context, appendClass: ClassLabel);
            outputTagHelper.Content.AppendHtml(labelElement);
        }

        switch (procesTageMode)
        {
            case TagModeEnum.DisplayLink:
            case TagModeEnum.Display:

                var htmlContent = CreateDisplayElement(Guid.NewGuid().ToString(), displayString, appendClass: ClassInput);
                outputTagHelper.Content.AppendHtml(htmlContent);

                break;

            case TagModeEnum.Edit:

                inputElement = await CreateEnumSelectElement(context, isNullable, appendClass: ClassInput, selectListOverride: EnumSelList);
                outputTagHelper.Content.AppendHtml(inputElement);

                //inputElement = await CreateInputElement(context);
                //output.Content.AppendHtml(inputElement);

                validationMessageElement = await CreateValidationMessageElement(context, appendClass: ClassValidate);
                outputTagHelper.Content.AppendHtml(validationMessageElement);

                break;

            default:

                break;
        }
    }

    // Private Tag Element Methods

    private async Task<TagHelperOutput> CreateLabelElement(
        TagHelperContext context,
        string appendClass)
    {
        LabelTagHelper labelTagHelper =
            new LabelTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("label");

        await labelTagHelper.ProcessAsync(context, tagHelperOutput);

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultLabelClass} {appendClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        if (!string.IsNullOrWhiteSpace(LabelOverride))
        {
            tagHelperOutput.Content.Clear();
            tagHelperOutput.Content.Append(LabelOverride);
        }

        return tagHelperOutput;
    }

    private async Task<TagHelperOutput> CreateInputElement(
        TagHelperContext context,
        string appendClass)
    {
        InputTagHelper inputTagHelper =
            new InputTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("input");

        await inputTagHelper.ProcessAsync(context, tagHelperOutput);

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultInputClass} {appendClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        return tagHelperOutput;
    }

    private async Task<TagHelperOutput> CreateEnumSelectElement(
        TagHelperContext context,
        bool isNullable,
        string appendClass,
        IEnumerable<SelectListItem> selectListOverride)
    {
        var seleList = selectListOverride ?? GetEnumSelect(VhModelType, isNullable);

        SelectTagHelper selectTagHelper =
            new SelectTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext,
                Items = seleList
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("select");

        await selectTagHelper.ProcessAsync(context, tagHelperOutput);

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultInputClass} {appendClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        return tagHelperOutput;
    }

    private TagBuilder CreateDisplayElement(
        string name,
        string displayString,
        string appendClass)
    {
        // Create tag builder
        var builder = new TagBuilder("div");

        // Render tag
        builder.RenderStartTag();

        // Create valid id
        builder.GenerateId(name, "_");

        var classAttr = RemoveDupAttributes($"{VhClass.DefaultDisplayClass} {appendClass}");

        builder.MergeAttribute("class", classAttr);

        builder.InnerHtml.Append(displayString);

        // Render tag
        builder.RenderEndTag();

        return builder;
    }

    private async Task<TagHelperOutput> CreateValidationMessageElement(
        TagHelperContext context,
        string appendClass)
    {
        ValidationMessageTagHelper validationMessageTagHelper =
            new ValidationMessageTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("span");

        await validationMessageTagHelper.ProcessAsync(context, tagHelperOutput);

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultValidationMessageClass} {appendClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        return tagHelperOutput;
    }

    // Check Box

    private async Task<TagHelperOutput> CreateCheckBoxElement(TagHelperContext context)
    {
        InputTagHelper inputTagHelper =
            new InputTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext,
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("input");

        await inputTagHelper.ProcessAsync(context, tagHelperOutput);

        tagHelperOutput.Attributes.RemoveAll("type");
        tagHelperOutput.Attributes.Add(new TagHelperAttribute("type", "checkbox"));

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultCheckboxInputClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        return tagHelperOutput;
    }

    private async Task<TagHelperOutput> CreateCheckboxLabelElement(
        TagHelperContext context,
        string appendClass)
    {
        LabelTagHelper labelTagHelper =
            new LabelTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("label");

        await labelTagHelper.ProcessAsync(context, tagHelperOutput);

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultCheckboxLabelClass} {appendClass}");

        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        return tagHelperOutput;
    }

    // Radio Button

    private async Task<TagHelperOutput> CreateRadioButtonlLabelElement(
        TagHelperContext context,
        string idAttr,
        string displayNameString)
    {
        LabelTagHelper labelTagHelper =
           new LabelTagHelper(Generator)
           {
               For = this.For,
               ViewContext = this.ViewContext
           };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("label");

        await labelTagHelper.ProcessAsync(context, tagHelperOutput);

        tagHelperOutput.Attributes.RemoveAll("for");
        tagHelperOutput.Attributes.Add(new TagHelperAttribute("for", idAttr));

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultRadioButtonLabelClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        tagHelperOutput.Content.SetContent(displayNameString);

        return tagHelperOutput;
    }

    private async Task<TagHelperOutput> CreateRadioButtonElement(
        TagHelperContext context,
        bool boolValue,
        string idAttr)
    {
        InputTagHelper inputTagHelper =
            new InputTagHelper(Generator)
            {
                For = this.For,
                ViewContext = this.ViewContext,
            };

        TagHelperOutput tagHelperOutput = CreateTagHelperOutput("input");

        await inputTagHelper.ProcessAsync(context, tagHelperOutput);

        tagHelperOutput.Attributes.RemoveAll("type");
        tagHelperOutput.Attributes.Add(new TagHelperAttribute("type", "radio"));

        tagHelperOutput.Attributes.RemoveAll("value");
        tagHelperOutput.Attributes.Add(new TagHelperAttribute("value", boolValue));

        tagHelperOutput.Attributes.RemoveAll("id");
        tagHelperOutput.Attributes.Add(new TagHelperAttribute("id", idAttr));

        var curClassAttributes = tagHelperOutput.Attributes.FirstOrDefault(a => a.Name == "class")?.Value;
        var allClassAttributes = RemoveDupAttributes($"{curClassAttributes} {VhClass.DefaultRadioButtonInputClass}");
        tagHelperOutput.Attributes.SetAttribute("class", $"{allClassAttributes}");

        return tagHelperOutput;
    }

    #endregion Private Methods, Properties, ENUMS, Classes

    // Private Support Methods, ENUMS, and Classes

    #region Private Support Methods, ENUMS, and Classes

    private IHtmlContent WrapElementsWithDiv(List<IHtmlContent> elements, string classValue)
    {
        TagBuilder div = new TagBuilder("div");
        div.AddCssClass(classValue);
        foreach (IHtmlContent element in elements)
        {
            div.InnerHtml.AppendHtml(element);
        }

        return div;
    }

    private TagHelperOutput CreateTagHelperOutput(string tagName)
    {
        return new TagHelperOutput(
            tagName: tagName,
            attributes: new TagHelperAttributeList(),
            getChildContentAsync: (s, t) =>
            {
                return Task.Factory.StartNew<TagHelperContent>(
                        () => new DefaultTagHelperContent());
            }
        );
    }

    private static string RemoveDupAttributes(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        var coll = value.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList().Distinct();
        return string.Join(" ", coll);
    }

    private IEnumerable<SelectListItem> GetEnumSelect(Type type, bool isNullable)
    {
        List<SelectListItem> retVal = new List<SelectListItem>();

        var coll = new List<EnumerationData>();

        var fields = type.GetFields().ToList();

        var enumArr = Enum.GetValues(type);

        if (enumArr.Length <= 0)
        {
            return retVal;
        }

        int? maxOrder = null;

        foreach (var item_ in enumArr)
        {
            var itm1_ = item_;
            var fi_ = fields.FirstOrDefault(o => o.Name == itm1_.ToString());

            var enumData = new EnumerationData
            {
                Id = (int)fi_.GetRawConstantValue(),
                Code = item_.ToString(),
                Name = GetEnumAttributeValueString<DisplayAttribute>(item_ as Enum, y => y.Name),
            };

            if (string.IsNullOrWhiteSpace(enumData.Name))
            {
                enumData.Name = item_.ToString();
            }

            enumData.Order = GetEnumAttributeValueIntegerNullable<DisplayAttribute>(item_ as Enum, y => y.GetOrder());

            if (enumData.Order.HasValue)
            {
                if (maxOrder.HasValue)
                {
                    maxOrder = maxOrder.Value < enumData.Order.Value ? enumData.Order.Value : maxOrder.Value;
                }
                else
                {
                    maxOrder = enumData.Order.Value;
                }
            }

            coll.Add(enumData);
        }

        foreach (var item_ in coll.Where(o => !o.Order.HasValue).OrderBy(o => o.Name))
        {
            item_.Order = int.MaxValue;
        }

        coll = coll.OrderBy(o => o.Order).ThenBy(o => o.Name).ToList();

        var xxx = For.Model;

        if (isNullable)
        {
            retVal.Add(new SelectListItem { Text = "Pick one", Value = string.Empty });
        }
        else if (For.Model == null)
        {
            retVal.Add(new SelectListItem { Text = "Pick one", Value = string.Empty });
        }
        else if (!coll.Any(o => o.Id == (int)For.Model))
        {
            retVal.Add(new SelectListItem { Text = "Pick one", Value = string.Empty });
        }

        foreach (var item in coll.OrderBy(o => o.Order).ThenBy(o => o.Name).ToList())
        {
            if (For.Model != null && (int)For.Model == item.Id)
            {
                retVal.Add(new SelectListItem { Text = item.Name, Value = item.Code, Selected = true });
            }
            else
            {
                retVal.Add(new SelectListItem { Text = item.Name, Value = item.Code });
            }
        }

        return retVal;
    }

    private static string GetEnumAttributeValueString<T>(Enum e, Func<T, object> selector) where T : Attribute
    {
        var value_ = GetEnumAttributeValue(e, selector);
        return (string)value_;
    }

    private static int? GetEnumAttributeValueIntegerNullable<T>(Enum e, Func<T, object> selector) where T : Attribute
    {
        var value_ = GetEnumAttributeValue(e, selector);

        return (int?)value_;
    }

    private static object GetEnumAttributeValue<T>(Enum e, Func<T, object> selector) where T : Attribute
    {
        var output_ = e.ToString();
        var member_ = e.GetType().GetMember(output_).First();
        var attributes_ = member_.GetCustomAttributes(typeof(T), false);

        if (attributes_.Length > 0)
        {
            return selector((T)attributes_[0]);
        }

        return null;
    }

    private TagBuilder GetProperyLinkTagBuilder(string html)
    {
        var node = HtmlAgilityPack.HtmlNode.CreateNode(html);

        if (node != null && node.Name.Equals("a"))
        {
            var hrefAttr = node.Attributes.FirstOrDefault(o => o.Name.Equals("href"));

            if (!string.IsNullOrWhiteSpace(hrefAttr?.Value))
            {
                var tagBuilder = new TagBuilder(node.Name);
                tagBuilder.MergeAttributes(node.Attributes.ToDictionary(x => x.Name, x => x.Value));
                return tagBuilder;
            }
        }
        return null;
    }

    // ENUMS

    /// <summary>
    /// Tag Modes
    /// </summary>
    public enum TagModeEnum
    {
        DisplayLink,
        Display,
        Edit
    }

    private enum VhTemplateTypeEnum
    {
        VhBool,
        VhEnum,
        VhProp
    }

    /// <summary>
    /// Boolean Render Options
    /// </summary>
    private enum VhBooleanRenderEnum
    {
        None,
        CheckBox,
        MultiStateRadio
    }

    // Private Classes

    private class EnumerationData
    {
        /// <summary>
        /// int value of ENUM.
        /// <para/>
        /// Example. Corresponds to 1 or 2 in public ENUM Gender { Male = 0, Female = 1 }.
        /// </summary>
        /// <value>Enumeration Integer Value</value>
        [Display(Name = "Id")]
        public int Id { get; set; }

        /// <summary>
        /// Code value of enumeration.
        /// <para/>
        /// Example. Corresponds to 'Male' or 'Female' in public ENUM Gender { Male = 0, Female = 1 }.
        /// </summary>
        /// <value>Code value of enumeration.</value>
        [Display(Name = "Code")]
        [Required(ErrorMessage = "Code required.", AllowEmptyStrings = false)]
        [StringLength(64, ErrorMessage = "Code must not exceed {1} characters.")]
        public string Code { get; set; }

        /// <summary>
        /// Name - Defaults to Code if Name isn't present in data annotation.
        /// </summary>
        /// <value>Name - Defaults to Code if Name isn't present in data annotation.</value>
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Name required.", AllowEmptyStrings = false)]
        public string Name { get; set; }

        ///// <summary>
        ///// Group Name. From Data Annotation.
        ///// </summary>
        ///// <value>Group Name. From Data Annotation.</value>
        //[Display(Name = "Group Name")]
        //public string GroupName { get; set; }

        /// <summary>
        /// Order. From Data Annotation.
        /// </summary>
        /// <value>Order. From Data Annotation.</value>
        [Display(Name = "Order")]
        public int? Order { get; set; }
    }

    #endregion Private Support Methods, ENUMS, and Classes
}