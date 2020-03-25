using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Model
{
    public  class LeagueTables
    {
        /*此实体类为三张表联合查询所用
         逻辑如下：
            左表与右表进行联合查询
            右表与第三张表进行联合查询 */

        /// <summary>
        /// 左表
        /// </summary>
        public string LeftTable { get; set; }

        /// <summary>
        /// 右表
        /// </summary>
        public string RightTable { get; set; }

        /// <summary>
        /// 第三张表
        /// </summary>
        public string ThreeTable { get; set; }

        /// <summary>
        /// 右表主键
        /// </summary>
        public string RightKey { get; set; }

        /// <summary>
        /// 左表外键
        /// </summary>
        public string ForeignKey { get; set; }


        public string ThreeKey { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int IntPageIndex { get; set; } = 0;

        /// <summary>
        /// 页大小
        /// </summary>
        public int IntPageSize { get; set; } = 10;

        /// <summary>
        /// 排序条件
        /// </summary>
        public string OrderByFileds { get; set; }


        /// <summary>
        /// 查询字段
        /// </summary>
        public string[] QueryField { get; set; }

    }
}
