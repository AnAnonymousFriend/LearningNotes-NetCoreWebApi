using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Model.Models
{
    [SugarTable("bin_madalena_type")]
    public class MadalenaType
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }


        /// <summary>
        /// 型号Id
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 255, IsNullable = true, ColumnName = "madalena_id")]
        public string MadalenaId { get; set; }


        /// <summary>
        /// 兼容品牌
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 255, IsNullable = true,ColumnName = "compatible_type")]
        public string CompatibleType { get; set; }


        /// <summary>
        /// 类型
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 255, IsNullable = true, ColumnName = "type")]
        public string Type { get; set; }


        /// <summary>
        /// 处理方式
        /// </summary>
        [SugarColumn(ColumnDataType = "int", Length = 11, IsNullable = true, ColumnName = "processing_method")]
        public int ProcessingMethod { get; set; }

        /// <summary>
        /// 阈值
        /// </summary>
        [SugarColumn(ColumnDataType = "int", Length = 11, IsNullable = true, ColumnName = "threshold_value")]
        public int ThresholdValue { get; set; }


        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnDataType = "tinyint", Length = 11, IsNullable = true, ColumnName = "state")]
        public int State { get; set; }

    }


 
}
