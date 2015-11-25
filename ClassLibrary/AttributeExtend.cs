using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// Attribute ����
    /// </summary>
    public static class AttributeExtend
    {
        /// <summary>
        /// Gets the description attribute value.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="name">The name.</param>
        /// <returns>Description coutent</returns>
        public static string GetEnumDescription<T>(string name)
        {
            Type tp = typeof(T);
            MemberInfo[] mi = tp.GetMember(name);

            if (mi != null && mi.Length > 0)
            {
                var attr = Attribute.GetCustomAttribute(mi[0], typeof(EnumDescriptionAttribute)) as EnumDescriptionAttribute;

                if (attr != null)
                {
                    return attr.Description;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the enum value.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="description">The description.</param>
        /// <returns>Enum value</returns>
        public static T GetEnumValue<T>(string description)
        {
            Type tp = typeof(T);
            var ms = tp.GetMembers().Where(m => m.ReflectedType.IsEnum);

            foreach (var m in ms)
            {
                var attr = Attribute.GetCustomAttribute(m, typeof(EnumDescriptionAttribute)) as EnumDescriptionAttribute;

                if (attr == null)
                {
                    continue;
                }

                if (!string.Equals(attr.Description, description))
                {
                    continue;
                }

                var values = m.ReflectedType.GetEnumValues();
                var names = m.ReflectedType.GetEnumNames();

                for (int i = 0; i < names.Length; i++)
                {
                    if (names[i].Equals(m.Name))
                    {
                        return (T)values.GetValue(i);
                    }
                }

                return default(T);
            }

            return default(T);
        }
    }
}