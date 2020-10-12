/// <summary>
/// View Helper Bootstrap Classes Used by <see cref="VhFormPropTagHelper"/> for determining how property is rendered.
/// </summary>
public static partial class VhClass
{
    // Additional Values Bootstrap Classes - Placed in partial View of VhClass so not additional changes are needed for accessibility

    // File name VhClass_WebDemoSpecific.cs The file name could be anything, but recommended to align with some naming conventions.

    /// <summary>
    /// The Bootstrap column constant - used to format computed attributed tagged properties in VhFormPropTagHelper.cd
    /// </summary>
    public const string Computed = "border border-success";

    /// <summary>
    /// The Bootstrap column constant - used to format optionally computed attributed tagged properties in VhFormPropTagHelper.cd
    /// </summary>
    public const string ComputedOptionally = "border border-info";
}