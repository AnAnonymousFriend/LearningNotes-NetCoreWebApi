using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Model.Models
{
    //对应数据库的BinInfo表
    [SugarTable("BinInfo")]
    public class BinInfo
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// PN 型号名
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 255, IsNullable = true)]
        public string Pn { get; set; }


        /// <summary>
        /// SN Bin文件序列号
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 256, IsNullable = true)]
        public string Sn { get; set; }

        /// <summary>
        /// Waveband 波段
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 256, IsNullable = true)]
        public string Waveband { get; set; }


        /// <summary>
        /// Distance 距离
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 256, IsNullable = true)]
        public string Distance { get; set; }


        /// <summary>
        /// Isddm
        /// </summary>
       [SugarColumn(ColumnName = "is_ddm")]
        public bool Isddm { get; set; }


        /// <summary>
        /// BinType bin文件的封装类型
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 256, IsNullable = true, ColumnName = "bin_type")]
        public string BinType { get; set; }


        /// <summary>
        /// BinMeter bin文件的米数（仅线缆）
        /// </summary>
        [SugarColumn(ColumnDataType = "varchar", Length = 256, IsNullable = true, ColumnName = "bin_mi")]
        public string BinMeter { get; set; }

        /// <summary>
        /// compatibility 兼容类型
        /// </summary>
        public string Compatibility { get; set; }


        /// <summary>
        /// Manufacturer 厂商名
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// version 版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// version 版本
        /// </summary>
        public DateTime CreateTime { get; set; }

    }
}
