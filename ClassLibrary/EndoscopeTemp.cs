using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace ClassLibrary
{
    /// <summary>
    /// 内镜清洗临时对象
    /// </summary>
    [DataContract]
    public class EndoscopeTemp
    {
        /// <summary>
        /// ID
        /// </summary>
        [DataMember]
        public int Id { get; set; }

        /// <summary>
        /// 清洗状态 0手动清洗；1 自动清洗；
        /// </summary>
        [DataMember]
        public string AutoCleanNo { get; set; }

        /// <summary>
        /// 清洗日期
        /// </summary>
        [DataMember]
        public DateTime? WashDate { get; set; }

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
        /// 清洗工编号
        /// </summary>
        [DataMember]
        public string WasherNo { get; set; }

        /// <summary>
        /// 清洗工名
        /// </summary>
        [DataMember]
        public string WasherName { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        [DataMember]
        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 清洗耗时
        /// </summary>
        [DataMember]
        public string TotalTime { get; set; }

        /// <summary>
        /// 刷洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? BrushWashBegin { get; set; }

        /// <summary>
        /// 刷洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? BrushWashEnd { get; set; }

        /// <summary>
        /// 初洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? FirstWashBegin { get; set; }

        /// <summary>
        /// 初洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? FirstWashEnd { get; set; }

        /// <summary>
        /// 酶洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? EnzymeWashBegin { get; set; }

        /// <summary>
        /// 酶洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? EnzymeWashEnd { get; set; }

        /// <summary>
        /// 清洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? CleanOutBegin { get; set; }

        /// <summary>
        /// 清洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? CleanOutEnd { get; set; }

        /// <summary>
        /// 浸泡消毒开始
        /// </summary>
        [DataMember]
        public TimeSpan? DipInBegin { get; set; }

        /// <summary>
        /// 浸泡消毒结束
        /// </summary>
        [DataMember]
        public TimeSpan? DipInEnd { get; set; }

        /// <summary>
        /// 末洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? LastWashBegin { get; set; }

        /// <summary>
        /// 末洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? LastWashEnd { get; set; }

        /// <summary>
        /// 二次清洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? DipInSecBegin { get; set; }

        /// <summary>
        /// 二次清洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? DipInSecEnd { get; set; }

        /// <summary>
        /// 质量
        /// </summary>
        [DataMember]
        public bool Quality { get; set; }

        /// <summary>
        /// 浓度合格
        /// </summary>
        [DataMember]
        public bool Disinfection { get; set; }

        /// <summary>
        /// 二次末洗开始
        /// </summary>
        [DataMember]
        public TimeSpan? LastWashSecBegin { get; set; }

        /// <summary>
        /// 二次末洗结束
        /// </summary>
        [DataMember]
        public TimeSpan? LastWashSecEnd { get; set; }

        /// <summary>
        /// 清洗类型
        /// </summary>
        [DataMember]
        public string CleanType { get; set; }
    }
}
