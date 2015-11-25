using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ClassLibrary
{
    /// <summary>
    /// 内镜表
    /// </summary>
    [DataContract]
    public sealed class EndoscopeInfo
    {
        /// <summary>
        /// 记录编号
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 内镜编号
        /// </summary>
        [DataMember]
        public string EndoscopeSn { get; set; }

        /// <summary>
        /// 内镜磁卡号
        /// </summary>
        [DataMember]
        public string EndoscopeSim { get; set; }

        /// <summary>
        /// 内镜类型
        /// </summary>
        [DataMember]
        public string EndoscopeType { get; set; }

        /// <summary>
        /// 内镜类型名
        /// </summary>
        [DataMember]
        public string EndoscopeName { get; set; }

        /// <summary>
        /// 内镜钢印号
        /// </summary>
        [DataMember]
        public string EndoscopeSeal { get; set; }

        /// <summary>
        /// 内镜登记时间
        /// </summary>
        [DataMember]
        public DateTime EndoscopeUseTime { get; set; }

        /// <summary>
        /// 内镜使用年限
        /// </summary>
        [DataMember]
        public int EndoscopeServYear { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string Remark { get; set; }
    }
}
