using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// User role
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// The washer
        /// </summary>
        [EnumDescription("清洗工")]
        Washer = 1,

        /// <summary>
        /// The nurse
        /// </summary>
        [EnumDescription("护士")]
        Nurse = 2,

        /// <summary>
        /// The dcotor
        /// </summary>
        [EnumDescription("医生")]
        Dcotor = 4
    }
}
