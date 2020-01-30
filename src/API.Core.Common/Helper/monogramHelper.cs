using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Common.Helper
{
    // 字段解析
    public static class MonogramHelper
    {

        /// <summary>
        /// 解析查询字段
        /// </summary>
        /// <param name="QueryField"></param>
        /// <returns></returns>
        public static string GetQueryField(string[] QueryField)
        {
            if (QueryField != null && QueryField.Length > 0)
            {
                StringBuilder queryField = new StringBuilder();
                foreach (var item in QueryField)
                {
                    queryField.Append(item + ",");
                }

                return queryField.ToString()[0..^1];

            }
            else
                return "*";
        }

    }
}
