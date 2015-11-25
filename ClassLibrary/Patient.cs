using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassLibrary
{
    public sealed class Patient
    {
        /// <summary>
        /// 病人自增序号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 病人编号
        /// </summary>
        public string PatientSN { get; set; }

        /// <summary>
        /// 病人名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string PatientSex { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string PatientPhoneNo { get; set; }

        /// <summary>
        /// 住址
        /// </summary>
        public string PatientAddr { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 胃镜或者肠镜等
        /// </summary>
        public string Endoscope { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 是否为阳性
        /// </summary>
        public bool IsPositive { get; set; }
    }
}
