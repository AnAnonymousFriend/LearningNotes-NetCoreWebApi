﻿using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Model.Models
{
    //对应数据库的BinInfo表
    [SugarTable("Bin_File")]
    public class BinFiles
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// FileByte 文件字节
        /// </summary>
        [SugarColumn(ColumnName = "file_byte")]
        public byte[] FileByte { get; set; }

    }
}
