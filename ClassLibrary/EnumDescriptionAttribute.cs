using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// EnumDescriptionAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class EnumDescriptionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="description">The description.</param>
        public EnumDescriptionAttribute(string description)
        {
            this.Description = description;
        }

        /// <summary>
        /// The description
        /// </summary>
        public string Description { get; private set; }
    }
}