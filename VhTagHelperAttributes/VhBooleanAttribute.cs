// MIT License - See __LICENSE_AND_INFO file for Licensing details and requirements

using System;

namespace VhTagHelperAttributes
{
    /// <summary>
    /// Define values for boolean display when boolean being rendered as more than a single check box. Use to override values set in tag helper.
    /// these can be overridden in the view with HTML attributes.
    /// </summary>
    /// <seealso cref="System.Attribute" />
    public class VhBooleanAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VhBooleanAttribute"/> class.
        /// </summary>
        public VhBooleanAttribute()
        {
        }

        /// <summary>
        /// Label to render for a true value
        /// </summary>
        public string BooleanTrue { get; set; }

        /// <summary>
        /// Label to render for a false value
        /// </summary>
        public string BooleanFalse { get; set; }

        /// <summary>
        /// Label to render for a null value
        /// </summary>
        public string BooleanNull { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VhBooleanAttribute"/> class.
        /// </summary>
        /// <param name="booleanTrue">The boolean true text to display.</param>
        /// <param name="booleanFalse">The boolean true text to display.</param>
        /// <param name="booleanNull">The boolean true text to display.</param>
        public VhBooleanAttribute(
            string booleanTrue = "",
            string booleanFalse = "",
            string booleanNull = "")
        {
            BooleanTrue = booleanTrue;
            BooleanFalse = booleanFalse;
            BooleanNull = booleanNull;
        }
    }
}