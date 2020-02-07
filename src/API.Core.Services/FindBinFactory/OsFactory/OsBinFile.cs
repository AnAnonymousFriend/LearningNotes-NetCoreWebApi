using System;

namespace API.Core.Services.ModulePlant
{
    // os 创建bin文件的具体实现类
    public class OsBinFile : BInFile
    {
        // os具体显示寻找Bin文件的函数
        public override void CreateBinFile()
        {
            Console.WriteLine("OS 创建bin文件");
        }
    }
}
