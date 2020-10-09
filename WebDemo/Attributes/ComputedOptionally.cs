using System;

namespace WebDemo.Attributes
{
    /// <summary>
    /// Computed - Indicates value is Optionally Computed
    /// </summary>
    public class ComputedOptionallyAttribute : Attribute
    {
        /// <summary>
        /// Quality Characteristic Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ComputedOptionallyAttribute()
        {
        }
    }
}