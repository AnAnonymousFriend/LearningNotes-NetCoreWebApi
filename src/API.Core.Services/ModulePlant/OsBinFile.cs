using System;
using System.Collections.Generic;
using System.Text;

namespace API.Core.Services.ModulePlant
{
    // os 创建bin文件的具体实现类
    public class OsBinFile : BInFile
    {
        public override void CreateBinFile()
        {
            Console.WriteLine("OS 创建bin文件");
        }
    }
}
