using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// User type
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// The normal
        /// </summary>
        [EnumDescription("普通用户")]
        Normal = 1,

        /// <summary>
        /// The admin
        /// </summary>
        [EnumDescription("管理员")]
        Admin = 9,
    }
}
