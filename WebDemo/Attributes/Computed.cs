using System;

namespace WebDemo.Attributes
{
    /// <summary>
    /// Computed - Indicates value is Computed and should not be data entered
    /// </summary>
    public class ComputedAttribute : Attribute
    {
        /// <summary>
        /// Quality Characteristic Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ComputedAttribute()
        {
        }
    }
}